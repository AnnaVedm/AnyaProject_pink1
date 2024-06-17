using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using Avalonia.Media;
using System.Collections.Generic;
using Avalonia.Media.Imaging;
using Bitmap = Avalonia.Media.Imaging.Bitmap;

namespace AnyaProject
{
    public partial class Redactirovanie : Window
    {
        private List<Product> spisok1 = new List<Product>();
        private Product _tovar;
        private ProductsWindow1 tovar1;
        private Bitmap _selectedImage1;
        private ComboBox _combobox;
        public Bitmap _selectedImage;  // ���������� ��� �������� ���������� �����������
        private Bitmap _originalImage; // ���������� ��� �������� ������������� �����������

        private async void SelectImageButton_Click1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filters.Add(new FileDialogFilter() { Name = "Images", Extensions = { "png", "jpg", "jpeg", "bmp" } });

            var selectedFiles = await dialog.ShowAsync(this);

            if (selectedFiles != null && selectedFiles.Length > 0)
            {
                string imagePath = selectedFiles[0];
                _selectedImage1 = new Bitmap(imagePath);

                // ����� ������� Image � �������� ��� �������� (Source)
                var imageControl = this.FindControl<Image>("SelectedImage");
                if (imageControl != null)
                {
                    imageControl.Source = _selectedImage1;
                }
            }
        }
        public Redactirovanie()
        {
            InitializeComponent();
        }

        public Redactirovanie(Product tovar, ProductsWindow1 pomogite, ComboBox combobox)
        {
            tovar1 = pomogite;
            _tovar = tovar;
            InitializeComponent();
            DataContext = tovar;
            _combobox = combobox;
        }

        private void ApplyRedactirovanie(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TovarName.Text)) //���� ���� ������ ��������
            {
                _tovar.TovarName = TovarName.Text;
            }
            if (!string.IsNullOrWhiteSpace(TovarProizvoditel.Text))
            {
                _tovar.Manufacturer = TovarProizvoditel.Text;
            }

            if (!string.IsNullOrWhiteSpace(TovarOpisanie.Text))
            {
                _tovar.Description = TovarOpisanie.Text;
            }
            if (!string.IsNullOrWhiteSpace(TovarOstatok.Text))
            {
                _tovar.Stock = Convert.ToInt32(TovarOstatok.Text);
            }
            if (!string.IsNullOrWhiteSpace(TovarPrice.Text))
            {
                _tovar.Price = Convert.ToDouble(TovarPrice.Text);
            }

            if (_selectedImage1 != null)
            {
                _tovar.TovarImage = _selectedImage1;
            }

            TadaText.Text = "�� - ��!";

            //�������� �������� ������
            ProductsWindow1.comboboxFill(Product.ProductsList, _combobox);

            ProductsWindow1.ShownProducts.Clear();
            foreach (var tovar in Product.ProductsList)
            {
                if (tovar.Stock == 0)
                {
                    tovar.change_color = new SolidColorBrush(Colors.Gray);
                }
                else
                {
                    tovar.change_color = new SolidColorBrush(Colors.White);
                }
                ProductsWindow1.ShownProducts.Add(tovar);
            }

            Close(); // ��������� ���� ����� ���������� ���������
        }
    }
}
