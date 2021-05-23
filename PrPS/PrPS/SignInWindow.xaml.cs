using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using PrPS.DAL.Core;
using PrPS.DAL.Core.Entities;
using PrPS.Dtos;

namespace PrPS
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        private bool isConfirmed = false;

        public SignInWindow()
        {
            InitializeComponent();
        }

        private void tblRegister_MouseEnter(object sender, MouseEventArgs e)
        {
            tblRegister.TextDecorations = TextDecorations.Underline;
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void tblRegister_MouseLeave(object sender, MouseEventArgs e)
        {
            tblRegister.TextDecorations = null;
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void tblRegister_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("123");
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text;
            string password = pbPassword.Password;
            User user = null;
            Role role = null;

            using (PrPsContext db = new PrPsContext())
            {
                user = db.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
            }

            if (user == null)
            {
                MessageBox.Show(Application.Current.FindResource("IncorrectData").ToString());
                return;
            }

            using (PrPsContext db = new PrPsContext())
            {
                role = db.Roles.First(r => r.Id == user.RoleId);
            }

            UserDto userDto = new UserDto()
            {
                Id = user.Id,
                Name = user.Name,
                Login = user.Login,
                Role = role
            };

            isConfirmed = true;
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = isConfirmed;
        }
    }
}
