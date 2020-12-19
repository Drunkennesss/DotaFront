using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DotaInfoSystem.ViewModels;
using DotaInfoSystem.Models;

namespace DotaInfoSystem.ViewModels
{
    class LoginViewModel : ViewModelBase
    {
        private IPageHolder pageHolder;
        private IPasswordContainer passwordContainer;
        private string userName = string.Empty;        

        public ICommand Login { get; private set; }

        

        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                base.OnPropertyChanged(nameof(UserName));                
            }
        }
        
        public LoginViewModel(IConnection con, IPageHolder pages, IPasswordContainer password) : base(con)
        {
            pageHolder = pages;
            passwordContainer = password;
            Login = new LoginCommand(connection, pageHolder, passwordContainer);
        }       

    }
}
