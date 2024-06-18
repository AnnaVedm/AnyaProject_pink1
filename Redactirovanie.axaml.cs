using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;

namespace AnyaProject
{
    public partial class Redactirovanie : Window
    {
        private Product _tovar;
        private ComboBox _combobox;
        private Bitmap _selectedImage1;
        private Bitmap _originalImage;

        public Redactirovanie()
        {
            InitializeComponent();
        }

        public Redactirovanie(Product tovar, ProductsWindow1 pomogite, ComboBox combobox) : this()
        {
            _tovar = tovar;
            DataContext = tovar;
            _combobox = combobox;

            // ���������� ������� ����������� ������ ��� �������� ����
            if (_tovar.TovarImage != null)
            {
                var imageControl = this.FindControl<Image>("SelectedImage");
                if (imageControl != null)
                {
                    imageControl.Source = _tovar.TovarImage;
                }
            }
        }

        private async void SelectImageButton_Click1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filters.Add(new FileDialogFilter() { Name = "Images", Extensions = { "png", "jpg", "jpeg", "bmp" } });

            var selectedFiles = await dialog.ShowAsync(this);

            if (selectedFiles != null && selectedFiles.Length > 0)
            {
                string imagePath = selectedFiles[0];
                _selectedImage1 = new Bitmap(imagePath);

                // ��������� ������� ���������� ������������ ��������� ������������
                var imageControl = this.FindControl<Image>("SelectedImage");
                if (imageControl != null)
                {
                    imageControl.Source = _selectedImage1;
                }
            }
        }

        private void ApplyRedactirovanie(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TovarName.Text))
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

            // ���� ������� ����� �����������, ������������� ���
            // � ��������� ������ ��������� ������� ����������� ������
            if (_selectedImage1 != null)
            {
                _tovar.TovarImage = _selectedImage1;
            }

            TadaText.Text = "�� - ��!";

            // ��������� ������ ������� � �� ����������� � ����������
            ProductsWindow1.comboboxFill(Product.ProductsList, _combobox);

            // ������� ������ ������������ �������
            ProductsWindow1.ShownProducts.Clear();

            // �������� �� ���� ������� � ������������� ���� � ����������� �� ������� ������
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
                // ��������� ����� � ������ ������������ �������
                ProductsWindow1.ShownProducts.Add(tovar);
            }

            // ��������� ���� ����� ���������� ���������
            Close();
        }
    }
}
