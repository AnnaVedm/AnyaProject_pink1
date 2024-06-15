using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using NAudio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace AnyaProject
{
    public partial class ProductsWindow1 : Window
    {
        private User _currentUser;
        //private SortOrder currentSortOrder = SortOrder.Ascending;

        //private static string _searchString = "";
        //public static List<string> Manufacturers = new List<string>() { "Все производители" };
        public string fileName;
        private string _searchText = string.Empty;
        private string _sortCriteria = "Default";
        private string _selectedManufacturer = "Все производители";
        private ComboBox _comboBox;

        public static ObservableCollection<Product> ShownProducts { get; set; } = new ObservableCollection<Product>();

        public ProductsWindow1()
        {
            InitializeComponent();
            DataContext = this;
        }

        public ProductsWindow1(User currentUser)
        {
            _currentUser = currentUser;

            InitializeComponent();
            DataContext = this;

            var comboBox = this.FindControl<ComboBox>("ProizvoditeliList");
            _comboBox = comboBox;

            comboboxFill(_comboBox);
            //InitializeComboBox();

            ApplyFiltersAndSort();

            queenStatus.Text = "Статус: " + _currentUser.UserStatus;
            queenName.Text = "Имя: " + _currentUser.UserName;

            foreach (var product in Product.ProductsList)
            {
                product.Otobrazhenie = _currentUser.UserStatus == "Queen";
            }

            foreach (var product in Product.ProductsList)
            {
                ShownProducts.Clear();
                if (product.Stock == 0)
                {
                    Color customColor = Color.FromRgb(102, 98, 103);
                    SolidColorBrush brush = new SolidColorBrush(customColor);
                    product.change_color = brush;
                }

                ShownProducts.Add(product);
            }

            UpdateListBox(Product.ProductsList);

            Button addProductButton = this.FindControl<Button>("DobavitTovar");
            if (_currentUser.UserStatus != "Queen")
            {
                addProductButton.IsVisible = false;
            }
            else if (_currentUser.UserStatus == "гость")
            {
                queenName.Text = "";
            }
            else
            {
                addProductButton.IsVisible = true;
            }

            foreach (var product in Product.ProductsList)
            {
                product.AddVKorzinuVisibility = true;
            }
        }

        //private void InitializeComboBox()
        //{
        //    // Очищаем комбо бокс перед добавлением элементов
        //    _comboBox.Items.Clear();

        //    // Добавляем "Все производители" в начало списка
        //    _comboBox.Items.Add("Все производители");

        //    // Добавляем уникальных производителей из списка продуктов
        //    foreach (var product in Product.ProductsList)
        //    {
        //        string manufacturer = product.Manufacturer;
        //        if (!_comboBox.Items.Cast<ComboBoxItem>().Any(item => string.Equals(item.Content?.ToString(), manufacturer, StringComparison.OrdinalIgnoreCase)))
        //        {
        //            _comboBox.Items.Add(new ComboBoxItem { Content = manufacturer });
        //        }
        //    }

        //    // Выбираем первый элемент (в данном случае "Все производители")
        //    _comboBox.SelectedIndex = 0;
        //}

        private void comboboxFill(ComboBox _combobox)
        {
            foreach (var item in Product.ProductsList)
            {
                string manufacturer = item.Manufacturer;

                if (!_comboBox.Items.Cast<ComboBoxItem>().Any(item => string.Equals(item.Content?.ToString(), manufacturer, StringComparison.OrdinalIgnoreCase)))
                {
                    _comboBox.Items.Add(new ComboBoxItem { Content = manufacturer });
                }
            }
        }

        public void UpdateProductsList(Product newProduct)
        {
            //Product.ProductsList.Add(newProduct);

            string manufacturer = newProduct.Manufacturer;

            if (!_comboBox.Items.Cast<ComboBoxItem>().Any(item => string.Equals(item.Content?.ToString(), manufacturer, StringComparison.OrdinalIgnoreCase)))
            {
                _comboBox.Items.Add(new ComboBoxItem { Content = manufacturer });
            }

            newProduct.Otobrazhenie = _currentUser.UserStatus == "Queen";
            newProduct.AddVKorzinuVisibility = _currentUser.UserStatus == "Queen";

            if (newProduct.Stock == 0)
            {
                Color customColor = Color.FromRgb(102, 98, 103);
                SolidColorBrush brush = new SolidColorBrush(customColor);
                newProduct.change_color = brush;
            }
            else
            {
                SolidColorBrush brush = new SolidColorBrush(Colors.Transparent);
                newProduct.change_color = brush;
            }

            Product.ProductsList.Add(newProduct);

            UpdateListBox(Product.ProductsList);
        }

        //public async void DobavitPicture(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    var topLevel = await openFileDialog.ShowAsync(this);
        //    fileName = String.Join("", topLevel);

        //    var image = Tovarslistbox.FindControl<Image>("MyImage");
        //    image.Source = new Bitmap(fileName);
        //}

        private void AddTovar_Button(object sender, RoutedEventArgs e)
        {
            Add ap = new Add(this);
            ap.Show();
        }

        private void DeleteTovar_Button(object sender, RoutedEventArgs e)
        {
            Button deleteButton = (Button)sender;
            Product product = (Product)deleteButton.DataContext;

            bool delete = true;

            foreach (var person in User.Users)
            {
                if (person.Products.Contains(product))
                {
                    Message message = new Message();
                    message.oshibka1.Text = "Ошибка!!! Вы не можете удалить товар из ассортимента, т.к он присутствует в корзине у пользователя!";
                    message.Show();

                    delete = false;
                    deletetovar(delete, product);
                    break;
                }
            }

            deletetovar(delete, product);
        }

        private void deletetovar(bool delete, Product product)
        {
            if (delete == true)
            {
                Product.ProductsList.Remove(product);
                ShownProducts.Remove(product);
            }
        }

        private void RedactButton(object sender, RoutedEventArgs e)
        {
            Button edittovar = (Button)sender;
            Product editTovar = (Product)edittovar.DataContext;
            Redactirovanie tovar = new Redactirovanie(editTovar, this, Product.ProductsList);
            tovar.Show();
        }

        private void DobavitKorzinu(object sender, RoutedEventArgs e)
        {
            Button addtobasket = (Button)sender;
            Product product = (Product)addtobasket.DataContext;

            if (product.Stock == 0)
            {
                Message message = new Message();
                message.oshibka1.Text = "ОШИБКА!!! Вы не можете добавить в корзину товар, которого нет на складе!!";
                message.Show();
            }
            else
            {
                _currentUser.Products.Add(product);
            }
        }

        private void PoitiKorzinu(object sender, RoutedEventArgs e)
        {
            Korzina basket = new Korzina(_currentUser);
            basket.Show();
        }

        private void PriceUbivanie(object sender, RoutedEventArgs e)
        {
            _sortCriteria = "CostDesc";
            ApplyFiltersAndSort();
            //UpdateListWithSearchAndSort(Search.Text.ToLower(), null, SortedList);
        }

        private void PriceVozrastanie(object sender, RoutedEventArgs e)
        {
            _sortCriteria = "CostAsc";
            ApplyFiltersAndSort();
            //UpdateListWithSearchAndSort(Search.Text.ToLower(), null, SortedList);
        }

        private void ProizvoditelAlfavit(object sender, RoutedEventArgs e)
        {
            _sortCriteria = "Manufacturer";
            ApplyFiltersAndSort();
            //UpdateListWithSearchAndSort(Search.Text.ToLower(), null, SortedList);
        }

        /*private void ProizvoditelObratno(object sender, RoutedEventArgs e)
        {
            List<Product> SortedList = Product.ProductsList.OrderByDescending(item => item.Manufacturer).ToList();
            UpdateListBox(SortedList);
            //UpdateListWithSearchAndSort(Search.Text.ToLower(), null, SortedList);
        }*/

        private void Exit_ButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow auth = new MainWindow();
            auth.Show();
            this.Close();
        }
        //--------------------------------------------------------
        private void UpdateListBox(List<Product> ListToAdd)
        {
            ShownProducts.Clear();

            foreach (var item in ListToAdd)
            {
                ShownProducts.Add(item);
            }
        }


        private void TextBox_TextChanged(object sender, KeyEventArgs e)
        {
            _searchText = Find.Text.ToLower();
            ApplyFiltersAndSort();
        }

        private void ApplyFiltersAndSort()
        {
            var filteredProducts = Product.ProductsList.Where(product =>
                (_selectedManufacturer == "Все производители" || product.Manufacturer == _selectedManufacturer) &&
                (string.IsNullOrWhiteSpace(_searchText) ||
                    product.TovarName.ToLower().Contains(_searchText) ||
                    product.Manufacturer.ToLower().Contains(_searchText) ||
                    product.Description.ToLower().Contains(_searchText) ||
                    product.Price.ToString().Contains(_searchText))
            );

            List<Product> sortedProducts;
            switch (_sortCriteria)
            {
                case "CostDesc":
                    sortedProducts = filteredProducts.OrderByDescending(item => item.Price).ToList();
                    break;
                case "CostAsc":
                    sortedProducts = filteredProducts.OrderBy(item => item.Price).ToList();
                    break;
                case "Manufacturer":
                    sortedProducts = filteredProducts.OrderBy(item => item.Manufacturer).ToList();
                    break;
                default:
                    sortedProducts = filteredProducts.ToList();
                    break;
            }

            UpdateListBox(sortedProducts);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            if (comboBox.SelectedItem != null && comboBox.SelectedItem is ComboBoxItem comboBoxItem)
            {
                _selectedManufacturer = comboBoxItem.Content as string ?? "Все производители";
                ApplyFiltersAndSort();
            }
        }
    }
}
