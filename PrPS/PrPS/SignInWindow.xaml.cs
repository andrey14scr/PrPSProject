using System;
using System.Collections.Generic;
using System.Globalization;
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
        private bool _isConfirmed = false;
        private bool _isLogin = true;

        private PasswordBox pbConfirmPassword = null;
        private Label lblConfirmPassword = null;

        public SignInWindow()
        {
            InitializeComponent();

            tblLogReg.Width = MeasureString(tblLogReg.Text).Width;
        }

        private void tblRegister_MouseEnter(object sender, MouseEventArgs e)
        {
            tblLogReg.Width = MeasureString(tblLogReg.Text).Width;

            tblLogReg.TextDecorations = TextDecorations.Underline;
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void tblRegister_MouseLeave(object sender, MouseEventArgs e)
        {
            tblLogReg.TextDecorations = null;
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void tblRegister_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_isLogin)
            {
                pbConfirmPassword = new PasswordBox() 
                { 
                    Name = "pbConfirmPassword", 
                    Width = pbPassword.Width, Height = pbPassword.Height, 
                    FontSize = pbPassword.FontSize, 
                    HorizontalAlignment = pbPassword.HorizontalAlignment, VerticalAlignment = pbPassword.VerticalAlignment, 
                    Margin = new Thickness(0,  pbPassword.Margin.Top + pbPassword.Margin.Top - tbLogin.Margin.Top, 0, 0)

                };
                lblConfirmPassword = new Label()
                {
                    Name = "lblConfirmPassword", 
                    Content = Application.Current.FindResource("ConfirmPassword").ToString(), 
                    Width = lblPassword.Width, Height = lblPassword.Height, FontSize = lblPassword.FontSize, 
                    HorizontalAlignment = lblPassword.HorizontalAlignment, VerticalAlignment = lblPassword.VerticalAlignment, 
                    Margin = new Thickness(0, lblPassword.Margin.Top + lblPassword.Margin.Top - lblLogin.Margin.Top, 0, 0),
                    Padding = lblPassword.Padding
                };

                grMain.Children.Add(pbConfirmPassword);
                grMain.Children.Add(lblConfirmPassword);

                double diff = pbConfirmPassword.Margin.Top - pbPassword.Margin.Top;

                MoveUp(ref btnConfirm, diff);
                MoveUp(ref lblQuestion, diff);
                MoveUp(ref lblLogReg, diff);

                lblQuestion.Content = Application.Current.FindResource("AlreadyHaveAccount").ToString();
                tblLogReg.Text = Application.Current.FindResource("ToSignIn").ToString();
                btnConfirm.Content = Application.Current.FindResource("Register").ToString();

                this.Height += diff;
            }
            else
            {
                double diff = pbConfirmPassword.Margin.Top - pbPassword.Margin.Top;

                grMain.Children.Remove(pbConfirmPassword);
                grMain.Children.Remove(lblConfirmPassword);

                pbConfirmPassword = null;
                lblConfirmPassword = null;

                MoveUp(ref btnConfirm, -diff);
                MoveUp(ref lblQuestion, -diff);
                MoveUp(ref lblLogReg, -diff);

                lblQuestion.Content = Application.Current.FindResource("DontHaveAccount").ToString();
                tblLogReg.Text = Application.Current.FindResource("Register").ToString();
                btnConfirm.Content = Application.Current.FindResource("ToSignIn").ToString();

                this.Height -= diff;
            }

            tblLogReg.Width = MeasureString(tblLogReg.Text).Width;

            _isLogin = !_isLogin;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text;
            string password = pbPassword.Password;
            User user = null;
            Role role = null;

            if (_isLogin)
            {
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

                CloseForm(user);
            }
            else
            {
                if (pbPassword.Password != pbConfirmPassword.Password)
                {
                    MessageBox.Show(Application.Current.FindResource("IncorrectData").ToString());
                    return;
                }

                using (PrPsContext db = new PrPsContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Login == login);
                }

                if (user != null)
                {
                    MessageBox.Show(Application.Current.FindResource("IncorrectData").ToString());
                    return;
                }

                user = new User() {Login = tbLogin.Text, Password = pbPassword.Password};

                using (PrPsContext db = new PrPsContext())
                {
                    user.RoleId = Guid.Parse("165992ba-2f10-4c89-8371-ce66434043da");
                    user.Name = "New User";

                    db.Users.Add(user);
                    db.SaveChanges();
                }

                CloseForm(user);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = _isConfirmed;
        }

        private static void MoveUp(ref Label control, double diff)
        {
            control.Margin = new Thickness(control.Margin.Left, control.Margin.Top + diff, control.Margin.Right, control.Margin.Bottom);
        }

        private static void MoveUp(ref Button control, double diff)
        {
            control.Margin = new Thickness(control.Margin.Left, control.Margin.Top + diff, control.Margin.Right, control.Margin.Bottom);
        }

        private Size MeasureString(string candidate)
        {
            var formattedText = new FormattedText(
                candidate,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(this.tblLogReg.FontFamily, this.tblLogReg.FontStyle, this.tblLogReg.FontWeight, this.tblLogReg.FontStretch),
                this.tblLogReg.FontSize,
                Brushes.Blue,
                new NumberSubstitution(),
                1);

            return new Size(formattedText.Width, formattedText.Height);
        }

        private void CloseForm(User user)
        {
            Properties.Settings.Default.Account = user.Login;
            Properties.Settings.Default.Save();
            _isConfirmed = true;
            Close();
        }
    }
}
