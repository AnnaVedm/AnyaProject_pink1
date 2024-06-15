using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyaProject
{
    public class Product : INotifyPropertyChanged
    {
        private string _name;
        private string _manufacturer;
        private string _description;
        private double _price;
        private bool _AddVKorzinuVisibility;
        private int _stock;
        private Bitmap _tovarImage;

        private bool otobrazhenie;

        public static List<Product> ProductsList = new List<Product>();
       
        public bool AddVKorzinuVisibility
        {
            get { return _AddVKorzinuVisibility; }
            set
            {
                if (_AddVKorzinuVisibility != value)
                {
                    _AddVKorzinuVisibility = value;
                    OnPropertyChanged("AddVKorzinuVisibility");
                }
            }
        }

        public bool Otobrazhenie
        {
            get { return otobrazhenie; }
            set
            {
                if (otobrazhenie != value)
                {
                    otobrazhenie = value;
                    OnPropertyChanged("Otobrazhenie");
                }
            }
        }
        public string TovarName
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("TovarName");
                }
            }
        }

        public string Manufacturer
        {
            get { return _manufacturer; }
            set
            {
                if (_manufacturer != value)
                {
                    _manufacturer = value;
                    OnPropertyChanged("Manufacturer");
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public double Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged("Price");
                }
            }
        }
        public int Stock
        {
            get { return _stock; }
            set
            {
                if (_stock != value)
                {
                    _stock = value;
                    OnPropertyChanged("Stock");
                }
            }
        }

        //public string fileName { get; set; }
        public Bitmap TovarImage
        {
            get { return _tovarImage; }
            set
            {
                if (_tovarImage != value)
                {
                    _tovarImage = value;
                    OnPropertyChanged("TovarImage");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public SolidColorBrush change_color
        {
            get; set;
        }

        //определяем видимость товара в соответствии с выбранным item в combobox
        public bool tovarsVisibility { get; set; } = true;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
