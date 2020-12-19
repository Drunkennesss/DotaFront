using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security;
using DotaInfoSystem.Models;

namespace DotaInfoSystem.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page, IPasswordContainer
    {
        public Login()
        {
            InitializeComponent();
        }

        string IPasswordContainer.PassWord { get => this.passWord.Password; }
    }
}
