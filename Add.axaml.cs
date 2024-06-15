using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections;
using System.Collections.Generic;
using Bitmap = Avalonia.Media.Imaging.Bitmap;
using Avalonia.Media;

namespace AnyaProject
{
    public partial class Add : Window
    {
        //private List<Product> korzinka = new List<Product>();
        //public List<Product> tovars2 = new List<Product>();
        //private User _user;
        private ProductsWindow1 _listupdate;
        public Bitmap _selectedImage;  // Переменная для хранения выбранного изображения

        public Add()
        {
            InitializeComponent();
        }

        public Add(ProductsWindow1 forupdate)
        {
            InitializeComponent();
            _listupdate = forupdate;
            //_user = user;
            //tovars2 = Tovars;
        }

        private async void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filters.Add(new FileDialogFilter() { Name = "Images", Extensions = { "png", "jpg", "jpeg", "bmp" } });

            var selectedFiles = await dialog.ShowAsync(this);

            if (selectedFiles != null && selectedFiles.Length > 0)
            {
                string imagePath = selectedFiles[0];
                _selectedImage = new Bitmap(imagePath);

                //var image = _listupdate.Tovarslistbox.FindControl<Image>("tovarImage");
                ////    image.Source = new Bitmap(fileName);
                //image.Source = _selectedImage;
            }
        }

        private void DobavitTovar_Button(object sender, RoutedEventArgs e)
        {
            Product product = new Product()
            {
                TovarName = TovarName.Text,
                Manufacturer = TovarProizvoditel.Text,
                Description = TovarOpisanie.Text,
                Price = Convert.ToDouble(TovarPrice.Text),
                Stock = Convert.ToInt32(TovarOstatok.Text),
                TovarImage = _selectedImage
            };

            //tovars2.Add(product);

            ////при добавлении товара запсукаем метод updateItemsList и передаем туда добавленного производителя
            //_listupdate.UpdateItemsList(product.Manufacturer);


            _listupdate.UpdateProductsList(product);
            product.tovarsVisibility = true;
            //ProductsWindow1 productsWindow1 = new ProductsWindow1(_user, tovars2);
            //productsWindow1.Show();
            //foreach (var product in ProductsList)
            //{
            //    product.AddVKorzinuVisibility = true;
            //}
            this.Close();
        }
    }
}
