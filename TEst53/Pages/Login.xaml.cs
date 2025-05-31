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

namespace TEst53.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder errors = new StringBuilder();
                if (string.IsNullOrEmpty(LoginBox.Text))
                {
                    errors.AppendLine("Введите Логин");
                }
                if (string.IsNullOrEmpty(PasswordBoxx.Password))
                {
                    errors.AppendLine("Введите Пароль");
                }
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (Data.MasterFloorEntities.GetContext().PartnersImport.Any(d => d.EmailOfPartner == LoginBox.Text && PasswordBoxx.Password == d.Password)) 
                {
                    var user = Data.MasterFloorEntities.GetContext().PartnersImport.FirstOrDefault(d => d.EmailOfPartner == LoginBox.Text && PasswordBoxx.Password == d.Password);

                    Classes.Manager.MainFrame.Navigate(new Pages.List());
                    MessageBox.Show("Успех!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                        
                }
                else
                {
                    MessageBox.Show("Неправилный логин/пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }




            }
            catch ( Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new Pages.List());
        }
    }
}
