using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using Avalonia.Media;
using System.Collections.Generic;

namespace AnyaProject
{
    public partial class Redactirovanie : Window
    {
        private List<Product> spisok1 = new List<Product>();
        private Product _tovar;
        private ProductsWindow1 tovar1;

        public Redactirovanie()
        {
            InitializeComponent();
        }

        public Redactirovanie(Product tovar, ProductsWindow1 pomogite, List<Product> spisok)
        {
            spisok1 = spisok;
            tovar1 = pomogite;
            _tovar = tovar;
            InitializeComponent();
            DataContext = tovar;
        }

        private void ApplyRedactirovanie(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TovarName.Text)) //если наша строка непустая
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

            TadaText.Text = "ТА - ДА!";

            //ДОБАВИЛИ ОЧЩЕНИЕ СПИСКА
            tovar1.Tovarslistbox.Items.Clear();

            foreach (var tovar in spisok1)
            {
                if (tovar.Stock == 0)
                {
                    tovar.change_color = new SolidColorBrush(Colors.Gray);
                }
                else
                {
                    tovar.change_color = new SolidColorBrush(Colors.White);
                }
                tovar1.Tovarslistbox.Items.Add(tovar);
            }
        }
    }
}
