using CarDealeship.WpfClient.CarDealership.WpfClient;
using DYRQO6_HFT_2022231.Models;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Newtonsoft.Json.Linq;

namespace CarDealeship.WpfClient
{
    public class CustomersViewModel : ObservableRecipient
    {
        private Customer selectedCustomer;

        public RestCollection<Customer> Customer { get; set; }

        public ICommand CreateCustomerCommand { get; set; }

        public ICommand DeleteCustomerCommand { get; set; }

        public ICommand UpdateCustomerCommand { get; set; }

        public Customer SelectedCustomer
        {
            get { return selectedCustomer; }
            set
            {
                if (value != null)
                {
                    selectedCustomer = new Customer()
                    {
                        Name =value.Name,
                        Address = value.Address,
                        Age = value.Age
                    };

                    OnPropertyChanged();
                    (DeleteCustomerCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateCustomerCommand as RelayCommand).NotifyCanExecuteChanged();
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

        public CustomersViewModel()
        {

            if (!IsInDesignMode)
            {
                Customer = new RestCollection<Customer>("http://localhost:18906/", "customer");

                CreateCustomerCommand = new RelayCommand(() =>
                {
                    Customer.Add(new Customer()
                    {
                        Name = SelectedCustomer.Name,
                        Age = SelectedCustomer.Age,
                        Address = SelectedCustomer.Address
                    });
                });

                DeleteCustomerCommand = new RelayCommand(() =>
                {
                    Customer.Delete(SelectedCustomer.CustomerId);
                },
                () => {
                    return SelectedCustomer  != null;
                });
                UpdateCustomerCommand = new RelayCommand(() =>
                {
                    Customer.Update(SelectedCustomer);
                });

                SelectedCustomer = new Customer();
            }

        }
    }
}
