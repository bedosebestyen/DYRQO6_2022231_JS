using CarDealeship.WpfClient.CarDealership.WpfClient;
using DYRQO6_HFT_2022231.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
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
    public class CarsWindowViewModel : ObservableRecipient
    {
        private Cars selectedCar;

        public RestCollection<Cars> Cars { get; set; }

        public ICommand CreateCarCommand { get; set; }

        public ICommand DeleteCarCommand { get; set; }

        public ICommand UpdateCarCommand { get; set; }

        public Cars SelectedCar
        {
            get { return selectedCar; }
            set {
                if (value != null)
                {
                    selectedCar = new Cars()
                    {
                        CarType = value.CarType,
                        CarId = value.CarId,
                        CarColor = value.CarColor,
                        Price = value.Price
                    };

                    OnPropertyChanged();
                    (DeleteCarCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateCarCommand as RelayCommand).NotifyCanExecuteChanged();
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

        public CarsWindowViewModel() 
        {
            
            if (!IsInDesignMode)
            {
                Cars = new RestCollection<Cars>("http://localhost:18906/", "cars", "hub");

                CreateCarCommand = new RelayCommand(() =>
                {
                    Cars.Add(new Cars()
                    {
                        CarType = SelectedCar.CarType,
                        CarColor = SelectedCar.CarColor,
                        Price = SelectedCar.Price
                    });
                });

                DeleteCarCommand = new RelayCommand(() =>
                {
                    Cars.Delete(SelectedCar.CarId);
                },
                () => {
                    return SelectedCar != null;
                });


                UpdateCarCommand = new RelayCommand(() =>
                {
                    Cars.Update(SelectedCar);
                });

                SelectedCar = new Cars();
            }
            
        }
    }
}
