using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PrPS.DAL.Core;
using PrPS.DAL.Core.Entities;
using PrPS.Dtos;

namespace PrPS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserDto _currentAccount;

        public MainWindow()
        {
            InitializeComponent();

            App.Language = new CultureInfo("ru-RU");

            using (PrPsContext db = new PrPsContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Login == Properties.Settings.Default.Account);
                if (user is not null)
                {
                    _currentAccount = new UserDto()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Login = user.Login,
                        Role = user.Role
                    };
                }
            }

            if (_currentAccount is null)
            {
                LoginForm();
            }
        }

        private void miAccExit_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Account = string.Empty;
            Properties.Settings.Default.Save();

            LoginForm();
        }

        private void LoginForm()
        {
            SignInWindow signin = new SignInWindow();
            var result = signin.ShowDialog();
            if (result == null || !result.Value)
            {
                Close();
            }
        }

        private void miExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
