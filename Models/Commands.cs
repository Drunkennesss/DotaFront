using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DotaInfoSystem.Models
{
   
    public abstract class MyCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public abstract void Execute(object param);
        
        void ICommand.Execute(object parameter)
        {
            Execute(parameter);
        }
        public bool CanExecute(object param) => true;
    }
    
    class LoginCommand : MyCommand
    {
        private IConnection connection;
        private IPageHolder pageHolder;
        private IPasswordContainer password;
                

        public LoginCommand(IConnection con, IPageHolder pages, IPasswordContainer pass)
        {
            connection = con;
            pageHolder = pages;
            password = pass;
        }

        public override void Execute(object obj)
        {
            string name = (obj as string) ?? string.Empty;
            string pass = password.PassWord;
            bool canLog = connection.Login(name, pass);
            if (canLog)
            {
                /*
                 * I have no idea why, when i call change current page
                 * it doesn't work until second click, so i call it twice
                 * (this is a fucking mess)
                 */
                pageHolder.ChangeCurrentPage(PageNumber.Heroes);
                pageHolder.ChangeCurrentPage(PageNumber.Heroes);
            }
        }

        
        
    }

    public class ToLogCommand : MyCommand
    {
        private IPageHolder pages;

        public ToLogCommand(IPageHolder pageHolder)
        {
            pages = pageHolder;
        }


        public override void Execute(object param)
        {
            /*
            * I have no idea why, when i call change current page
            * it doesn't work until second click, so i call it twice
            * (this is a fucking mess)
            */
            pages.ChangeCurrentPage(PageNumber.Login);
            pages.ChangeCurrentPage(PageNumber.Login);
        }
    }

    public class ToSpellsCommand : MyCommand
    {

        private IPageHolder pages;
        public ToSpellsCommand(IPageHolder pageHolder)
        {
            pages = pageHolder;
        }

        public override void Execute(object param)
        {
            /*
            * I have no idea why, when i call change current page
            * it doesn't work until second click, so i call it twice
            * (this is a ******* mess)
            */
            pages.ChangeCurrentPage(PageNumber.Spells);
            pages.ChangeCurrentPage(PageNumber.Spells);
        }
    }


    public class ToHeroesCommand : MyCommand
    {
        private IPageHolder pages;

        public ToHeroesCommand(IPageHolder pageHolder)
        {
            pages = pageHolder;
        }


        public override void Execute(object param)
        {
            /*
            * I have no idea why, when i call change current page
            * it doesn't work until second click, so i call it twice
            * (this is a fucking mess)
            */
            pages.ChangeCurrentPage(PageNumber.Heroes);
            pages.ChangeCurrentPage(PageNumber.Heroes);
        }
    }   
    
}
