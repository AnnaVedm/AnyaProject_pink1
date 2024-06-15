using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using NAudio;
using NAudio.Wave;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AnyaProject
{
    public partial class MainWindow : Window
    {
        //private List<Product> Tovars1 = new List<Product>();

        

        //User user = new User();
        //private User _user;

        //public MainWindow()
        //{
        //    InitializeComponent();
        //}

        public MainWindow(/*List<Product> tovars*/)
        {
            //Tovars1 = tovars;
            //_user = user;
            InitializeComponent();
            DataContext = this;
        }

        private void VoitiVAkkaynt(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Name.Text)) //���� ������ ��������, �� ��������� ��� ������ �� ������� ���� �����
            {
                string login = Name.Text;
                string password = Password.Text;

                User proverka = User.Users.FirstOrDefault(u => u.UserName == login && u.UserPassword == password);

                if (proverka != null)
                {
                    // ���� �������� �������
                    Oshibka.Text = null;

                    ProductsWindow1 wp = new ProductsWindow1(proverka /*Tovars1*/);
                    wp.Show();

                    this.Close();            
                }
                else
                {
                    // ������ �����
                    Oshibka.Text = "������� ������� ������";
                    Password.Clear();
                }
            }
        }

        private void Guest_Button(object sender, RoutedEventArgs e)
        {
            //��� ����� � ����� ����� �������� ������������ ��� ������� �������� - �� �������� ����� � ��� ������
            ProductsWindow1 open_okno = new ProductsWindow1(User.Users[0] /*Tovars1*/);
            open_okno.Show();
            this.Close();
        }
    }
}