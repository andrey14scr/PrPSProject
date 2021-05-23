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

namespace PrPS
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
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

        }
    }
}
