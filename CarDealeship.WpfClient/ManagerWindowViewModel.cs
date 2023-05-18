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
    public class ManagerWindowViewModel : ObservableRecipient
    {
		private Manager selectedManager;

		public RestCollection<Manager> Managers { get; set; }
		public ICommand CreateManagerCommand { get; set; }

        public ICommand UpdateManagerCommand { get; set; }

        public ICommand DeleteManagerCommand { get; set; }

        public Manager SelectedManager
		{
			get { return selectedManager; }
			set 
			{
				if (value != null)
				{
					selectedManager = new Manager()
					{
						Name= value.Name,
						Age= value.Age,
						Salary= value.Salary
					};

					OnPropertyChanged();
					(DeleteManagerCommand as RelayCommand).NotifyCanExecuteChanged();
					(UpdateManagerCommand as RelayCommand).NotifyCanExecuteChanged();
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

		public ManagerWindowViewModel()
		{
			if (!IsInDesignMode)
			{
				Managers = new RestCollection<Manager>("http://localhost:18906/", "manager");

				CreateManagerCommand = new RelayCommand(() =>
				{
					Managers.Add(new Manager()
					{
						Name = SelectedManager.Name,
						Age = SelectedManager.Age,
						Salary= SelectedManager.Salary
					});
				});

				DeleteManagerCommand = new RelayCommand(() =>
				{
					Managers.Delete(SelectedManager.ManagerId);
				},
				() =>
				{
					return SelectedManager != null;
				});

				UpdateManagerCommand = new RelayCommand(() => 
				{
					Managers.Update(SelectedManager);
				});

				SelectedManager = new Manager();
			}
		}

    }
}
