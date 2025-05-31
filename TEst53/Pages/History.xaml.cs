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
using TEst53.Data;

namespace TEst53.Pages
{
    /// <summary>
    /// Логика взаимодействия для History.xaml
    /// </summary>
    public partial class History : Page
    {
        private Data.PartnersImport _partner;
        //public Data.PartnersImport _currentpartner = new Data.PartnersImport();
        public History(Data.PartnersImport partner)
        {
            InitializeComponent();
            _partner = partner;
            HistoryGrid.ItemsSource = Data.MasterFloorEntities.GetContext().PartnerProductsImport.Where(p=> p.IdPartnerName == _partner.IdPartnerName).ToList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new Pages.List());
        }
    }
}
