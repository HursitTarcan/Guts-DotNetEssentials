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

namespace PixelPass
{
    /// <summary>
    /// Interaction logic for CreateOrUpdateAccountInfoWindow.xaml
    /// </summary>
    public partial class CreateAccountInfoWindow : Window
    {
        private IAccountInfoCollection _accountInfoCollection = null; 

        public CreateAccountInfoWindow(IAccountInfoCollection accountInfoCollection)
        {
            _accountInfoCollection = accountInfoCollection;
            InitializeComponent();
        }

        private void expirationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (expirationDateTextBlock != null)
            {
                expirationDateTextBlock.Text = $"{expirationSlider.Value}/{DateTime.Now.Month}/{DateTime.Now.Year}";
            }    
        }

        private void passwordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            passwordStrengthTextBlock.Text = Convert.ToString(PasswordValidator.CalculateStrength(passwordTextBox.Text));
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            AccountInfo accountInfo = new AccountInfo();
            accountInfo.Title = titleTextBox.Text;
            accountInfo.Username = usernameTextBox.Text;    
            accountInfo.Password = passwordTextBox.Text;
            accountInfo.Notes = notesTextBox.Text;
            int[] dateInt = Array.ConvertAll(expirationDateTextBlock.Text.Split('/'), int.Parse);
            DateTime expirationDate = new DateTime(dateInt[2], dateInt[1], dateInt[0]);
            accountInfo.Expiration = expirationDate; 

            _accountInfoCollection.AccountInfos.Add(accountInfo);
            this.Close(); 
        }
    }
}
