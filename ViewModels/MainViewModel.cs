using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using DotaInfoSystem.Views;
using DotaInfoSystem.Models;

namespace DotaInfoSystem.ViewModels
{
    class MainViewModel : ViewModelBase, IPageHolder
    {
        private Page currentPage;

        private Page[] pages;
        public Page CurrentPage
        {
            get => currentPage;
            private set 
            { 
                base.OnPropertyChanged("CurrentPage");
                currentPage = value;
            }
        }

        Page IPageHolder.CurrentPage
        {
            get => this.CurrentPage;            
        }
        public MainViewModel(IConnection con) : base(con)
        {
            
            var loginPage = new Login();
            loginPage.DataContext = new LoginViewModel(con, this, loginPage);

            var heroesPage = new Heroes();
            heroesPage.DataContext = new HeroesViewModel(con, this);

            var spellsPage = new Spells();
            spellsPage.DataContext = new SpellsViewModel(con, this);

            pages = new Page[]{ 
                loginPage,
                heroesPage,
                spellsPage
            };
            CurrentPage = pages[(int)PageNumber.Login];
        }

        void IPageHolder.ChangeCurrentPage(PageNumber number)
        {
            //TODO: Make this independent of array's element position
            CurrentPage = pages[(int)number];
        }


    }
}
