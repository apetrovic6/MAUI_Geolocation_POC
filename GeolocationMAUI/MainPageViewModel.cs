using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GeolocationUtils.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeolocationMAUI
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly IBackgroundService backgroundLocationService;

        public MainPageViewModel(IBackgroundService backgroundLocationService)
        {
            this.backgroundLocationService = backgroundLocationService;
        }

        [RelayCommand]
        private void StartService()
        {
            if (backgroundLocationService.IsServiceIsRunning())
            {
                App.Current.MainPage.DisplayAlert("Error", "Location service is already running", "Ok");
            }
            else
            {
                backgroundLocationService.Start();
            }
        }

        [RelayCommand]
        private void StopService()
        {
            backgroundLocationService.Stop();
        }
    }
}
