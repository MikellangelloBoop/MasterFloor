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
    /// Логика взаимодействия для List.xaml
    /// </summary>
    public partial class List : Page
    {
        public List()
        {
            InitializeComponent();
            PartnerList.ItemsSource = Data.MasterFloorEntities.GetContext().PartnersImport.ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new Pages.Login());
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //Classes.Manager.MainFrame.Navigate(new Pages.AddEdit(null));

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            //Classes.Manager.MainFrame.Navigate(new Pages.AddEdit((sender as Button).DataContext as Data.PartnersImport));

        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new Pages.History());

        }
    }
}
