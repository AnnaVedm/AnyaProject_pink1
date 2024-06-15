using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnyaProject
{
    public class User : INotifyPropertyChanged
    {

        private string _userName;
        private string _userPassword;
        private string _userStatus;

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged("UserName");
                }
            }
        }

        public string UserPassword
        {
            get { return _userPassword; }
            set
            {
                if (_userStatus != value)
                {
                    _userPassword = value;
                    OnPropertyChanged("UserPassword");
                }
            }
        }

        public string UserStatus
        {
            get { return _userStatus; }
            set
            {
                if (_userStatus != value)
                {
                    _userStatus = value;
                    OnPropertyChanged("UserStatus");
                }
            }
        }

        public static ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>()
        {
            new User()
            {
                UserName = "Отсутствует",
                UserPassword = "",
                UserStatus = "Гость"
            },

            new User()
            {
                UserName = "admin",
                UserPassword = "admin",
                UserStatus = "Queen"
            },

            new User()
            {
                UserName = "user",
                UserPassword = "user",
                UserStatus = "Thief"
            }
        };

        public List<Product> Products { get; set; } = new List<Product>();
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
