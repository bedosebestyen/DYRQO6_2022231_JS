using CarDealeship.WpfClient.CarDealership.WpfClient;
using DYRQO6_HFT_2022231.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CarDealeship.WpfClient
{
    public class ShopWindowViewModel : ObservableRecipient
    {
        private CarShop selectedShop;

        public RestCollection<CarShop> Shops { get; set; }

        public ICommand CreateShopCommand { get; set; }

        public ICommand UpdateShopCommand { get; set; }

        public ICommand DeleteShopCommand { get; set; }

        

        public CarShop SelectedShop
        {
            get { return selectedShop; }
            set 
            {
                if (value != null) 
                {
                    selectedShop = new CarShop()
                    {
                        Name = value.Name,
                        AvailableCarsCount= value.AvailableCarsCount,
                        Address= value.Address
                    };

                    OnPropertyChanged();
                    (DeleteShopCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateShopCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public ShopWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Shops = new RestCollection<CarShop>("http://localhost:18906/", "carshop");

                CreateShopCommand = new RelayCommand(() =>
                {
                    Shops.Add(new CarShop()
                    {
                        Name = SelectedShop.Name,
                        AvailableCarsCount = SelectedShop.AvailableCarsCount,
                        Address = SelectedShop.Address
                    });
                });

                DeleteShopCommand = new RelayCommand(() =>
                {
                    Shops.Delete(SelectedShop.ShopId);
                },
                () => {
                    return SelectedShop != null;
                });

                UpdateShopCommand = new RelayCommand(() =>
                {
                    Shops.Update(SelectedShop);
                });

                SelectedShop = new CarShop();
            }
        }
    }
}
