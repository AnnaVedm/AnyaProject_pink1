using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;

namespace AnyaProject;

public partial class Korzina : Window
{
    private User _user;
    public Korzina()
    {
        InitializeComponent();
    }
    public Korzina(User user)
    {
        InitializeComponent();

        
        _user = user;

        DataContext = this;

        UpdateKorzina();
    }

    private void dropiskorzina(object sender, RoutedEventArgs e)
    {
        Button drop = (Button)sender;
        Product droptovar = (Product)drop.DataContext;

        _user.Products.Remove(droptovar);

        UpdateKorzina();
    }

    private void UpdateKorzina()
    {
        Tovarslistbox_Korzina.Items.Clear();
        foreach (var p in _user.Products)
        {
            Tovarslistbox_Korzina.Items.Add(p);
        }
    }
}
