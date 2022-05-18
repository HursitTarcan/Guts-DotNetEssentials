using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PixelPass
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IAccountInfoCollection _accountInfoCollection;
        private AccountInfo _currentAccountInfo;
        private DispatcherTimer _timer;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void openItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string startDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.InitialDirectory = startDirectory;

            if (openFileDialog.ShowDialog() == true)
            {
                _accountInfoCollection = AccountInfoCollectionReader.Read(openFileDialog.FileName);
                try
                {
                    Title = _accountInfoCollection.Name;

                    accountInfoListBox.ItemsSource = _accountInfoCollection.AccountInfos;
                    newAccountInfoButton.IsEnabled = true;
                }
                catch (Exception ex)
                {
           
                }
            }
        }

        private void accountInfoListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currentAccountInfo = accountInfoListBox.SelectedItem as AccountInfo;

            titleTextBlock.Text = _currentAccountInfo.Title;
            usernameTextBlock.Text = _currentAccountInfo.Username; 
            notesTextBlock.Text = _currentAccountInfo.Notes;
            expirationTextBlock.Text = Convert.ToString(_currentAccountInfo.Expiration);
            detailsCanvas.Background = new SolidColorBrush(Colors.White);
            copyButton.IsEnabled = true;
            
            if (_currentAccountInfo.IsExpired == true)
            {
                copyButton.IsEnabled = false;
                detailsCanvas.Background = new SolidColorBrush(Colors.LightCoral);
            }
        }

        private void copyButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(_currentAccountInfo.Password);
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(1000); 
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            expirationProgressBar.Value += 1; 
            if (expirationProgressBar.Value == 5)
            {
                expirationProgressBar.IsEnabled = false;
                expirationProgressBar.Value = 0; 
                copyButton.IsEnabled = false; 
                MessageBox.Show("Time over!");
                Clipboard.Clear();
                _timer.Stop();
            }
        }

        private void newAccountInfoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CreateAccountInfoWindow createAccountInfoWindow = new CreateAccountInfoWindow(_accountInfoCollection);
            createAccountInfoWindow.ShowDialog();
           
            this.Show();
            accountInfoListBox.Items.Refresh();
        }
    }
}
