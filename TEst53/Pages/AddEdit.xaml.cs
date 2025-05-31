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
    /// Логика взаимодействия для AddEdit.xaml
    /// </summary>
    public partial class AddEdit : Page
    {
        public string FlagAddOrEdit = "default";
        public Data.PartnersImport _currentpartner = new Data.PartnersImport();

        public AddEdit(PartnersImport partner)
        {

            
            InitializeComponent();

            if(partner != null)
            {
                _currentpartner = partner;
                FlagAddOrEdit = "edit";
            }
            else
            {
                FlagAddOrEdit = "add";
            }
            DataContext = _currentpartner;

            Init();
        }

        public void Init()
        {
            try
            {
                TypeComboBox.ItemsSource = Data.MasterFloorEntities.GetContext().TypeOfPartner.ToList();
                IdLabel.Visibility = Visibility.Hidden;
                IdTExtBox.Visibility = Visibility.Hidden;
                if (FlagAddOrEdit == "edit")
                {


                    NameTextBox.Text = _currentpartner.PartnerName.Name;
                    RaitingBox.Text = _currentpartner.Reiting.ToString();
                    RegionBox.Text = _currentpartner.Adress.Regions.RegionOf;
                    CityBOx.Text = _currentpartner.Adress.Cities.CityOf.ToString();
                    StreetBox.Text = _currentpartner.Adress.Streets.StreetOf.ToString();
                    HOUSEBOx.Text = _currentpartner.Adress.HouseNum.ToString();
                    INDEXBOx.Text = _currentpartner.Adress.Indexes.IndexOf.ToString();
                    FIOBOX.Text = _currentpartner.Directors.FIO;
                    NumberBox.Text = _currentpartner.PhoneOfPartner;
                    EMAILBOX.Text = _currentpartner.EmailOfPartner;

                    TypeComboBox.SelectedItem = Data.MasterFloorEntities.GetContext().TypeOfPartner.Where(d => d.Id == _currentpartner.IdTypeOfParther).FirstOrDefault();

                }


            }
            catch (Exception) { }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new Pages.List());
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder errors = new StringBuilder();
                if(string.IsNullOrEmpty(NameTextBox.Text))
                {
                    errors.AppendLine("Заполните наименование!");
                }
                if (TypeComboBox.SelectedItem == null)
                {
                    errors.AppendLine("Выберите тип партнера!");
                }
                if(string.IsNullOrEmpty(RaitingBox.Text))
                {
                    errors.AppendLine("Заполните Рейтинг!");
                }
                if(!int.TryParse(RaitingBox.Text, out int raiting) || raiting <0)
                {
                    errors.AppendLine("Рейтинг должен быть целым неотрицательным числом!");
                }

                if (string.IsNullOrEmpty(RegionBox.Text))
                {
                    errors.AppendLine("Заполните Регион!");
                }
                if (string.IsNullOrEmpty(CityBOx.Text))
                {
                    errors.AppendLine("Заполните город!");
                }
                if (string.IsNullOrEmpty(StreetBox.Text))
                {
                    errors.AppendLine("Заполните Улицу!");
                }
                if (string.IsNullOrEmpty(HOUSEBOx.Text))
                {
                    errors.AppendLine("Заполните Номер дома!");
                }

                if (string.IsNullOrEmpty(INDEXBOx.Text))
                {
                    errors.AppendLine("Заполните Индекс!");
                }
                if (!int.TryParse(INDEXBOx.Text, out int ind) || ind < 0)
                {
                    errors.AppendLine("Заполните индекс согласно стандарту, т.е. цифрами!");
                }
                if (string.IsNullOrEmpty(FIOBOX.Text))
                {
                    errors.AppendLine("Заполните Фио!");
                }
                if (string.IsNullOrEmpty(NumberBox.Text))
                {
                    errors.AppendLine("Заполните номер телефона!");
                }
                if (string.IsNullOrEmpty(EMAILBOX.Text))
                {
                    errors.AppendLine("Заполните Почту!");
                }

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString(), "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                var selectedCategory = TypeComboBox.SelectedItem as Data.TypeOfPartner;
                if (selectedCategory == null)
                {
                    MessageBox.Show("Выберите тип партнера!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return ;
                }

                _currentpartner.IdTypeOfParther = selectedCategory.Id;
                _currentpartner.Reiting = Convert.ToInt32(RaitingBox.Text);
                _currentpartner.PhoneOfPartner = NumberBox.Text;
                _currentpartner.EmailOfPartner = EMAILBOX.Text;

                var searchDirector = (from item in Data.MasterFloorEntities.GetContext().Directors where item.FIO == FIOBOX.Text select item).FirstOrDefault();
                if (searchDirector != null) 
                {
                    _currentpartner.IdDirector = searchDirector.Id; 
                }
                else
                {
                    Data.Directors directors = new Data.Directors();
                    {
                        directors.FIO = FIOBOX.Text;
                    };
                    Data.MasterFloorEntities.GetContext().Directors.Add(directors);
                    Data.MasterFloorEntities.GetContext().SaveChanges();
                    _currentpartner.IdDirector = directors.Id;
                }





                var searchPartnerName = Data.MasterFloorEntities.GetContext().PartnerName.FirstOrDefault(pn => pn.Name == NameTextBox.Text);
                if (searchPartnerName != null) 
                {
                    _currentpartner.IdPartnerName = searchPartnerName.Id; 
                }
                else
                {
                    var partnerName = new Data.PartnerName { Name = NameTextBox.Text };
                    Data.MasterFloorEntities.GetContext().PartnerName.Add(partnerName);
                    Data.MasterFloorEntities.GetContext().SaveChanges();
                    _currentpartner.IdPartnerName = partnerName.Id;
                }

                int houseNum = int.Parse(HOUSEBOx.Text);
                int indexOf = int.Parse(INDEXBOx.Text);

                var address = MasterFloorEntities.GetContext().Adress
                    .FirstOrDefault(a => a.Regions.RegionOf == RegionBox.Text &&
                                         a.Cities.CityOf == CityBOx.Text &&
                                         a.Streets.StreetOf == StreetBox.Text &&
                                         a.HouseNum == houseNum &&
                                         a.Indexes.IndexOf == indexOf);
                if (address == null)
                {
                    address = new Adress
                    {
                        Regions = new Regions { RegionOf = RegionBox.Text },
                        Cities = new Cities { CityOf = CityBOx.Text },
                        Streets = new Streets { StreetOf = StreetBox.Text },
                        HouseNum = houseNum,
                        Indexes = new Indexes { IndexOf = indexOf }
                    };
                    MasterFloorEntities.GetContext().Adress .Add(address);
                    MasterFloorEntities.GetContext().SaveChanges ();
                
                }

                _currentpartner.IdAdress = address.Id;

                if(address.Id != 0)
                {
                    _currentpartner.IdAdress = address.Id;
                }
                else
                {
                    MessageBox.Show("Ошибка сохранения адресса", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (FlagAddOrEdit == "add")
                {
                    Data.MasterFloorEntities.GetContext().PartnersImport.Add(_currentpartner);
                    Data.MasterFloorEntities.GetContext().SaveChanges();
                    MessageBox.Show("Успешно добавленно", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else if (FlagAddOrEdit == "edit")
                {
                    Data.MasterFloorEntities.GetContext ().SaveChanges ();
                    MessageBox.Show("Успешно сохранено", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                Classes.Manager.MainFrame.Navigate(new Pages.List());

            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Exception innerExceprion = ex.InnerException;
                while (innerExceprion != null)
                {
                    Console.WriteLine(innerExceprion.Message);
                    innerExceprion= innerExceprion.InnerException;
                }
            }
        }
    }
}
