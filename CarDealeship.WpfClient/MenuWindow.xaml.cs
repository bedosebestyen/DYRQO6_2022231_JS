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
using System.Windows.Shapes;

namespace CarDealeship.WpfClient
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void bt_cars_Click(object sender, RoutedEventArgs e)
        {
            MainWindow carsWindow = new MainWindow();
            this.Visibility = Visibility.Visible;
            carsWindow.Show();
        }

        

        private void bt_customers_Click(object sender, RoutedEventArgs e)
        {
            CustomersWindow customersWindow = new CustomersWindow();
            this.Visibility = Visibility.Visible;
            customersWindow.Show();
        }

        private void bt_shops_Click(object sender, RoutedEventArgs e)
        {
            ShopWindow shopsWindow = new ShopWindow();
            this.Visibility = Visibility.Visible;
            shopsWindow.Show();
        }

        private void bt_managers_Click(object sender, RoutedEventArgs e)
        {
            ManagerWindow managersWindow = new ManagerWindow();
            this.Visibility = Visibility.Visible;
            managersWindow.Show();
        }

        private void bt_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        
    }
}
