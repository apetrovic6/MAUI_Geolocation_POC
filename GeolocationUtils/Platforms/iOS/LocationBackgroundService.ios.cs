using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreBluetooth;
using GeolocationUtils.Platforms.iOS;
using GeolocationUtils.Services.Interfaces;
using UIKit;

namespace GeolocationUtils.Platforms.iOS
{
    public class LocationBackgroundServiceiOS : IBackgroundService
    {
        private readonly LocationManageriOS locationManager;
        nint taskID;
        bool IsRunning;

        public LocationBackgroundServiceiOS(LocationManageriOS locationManager)
        {
            this.locationManager = locationManager;
        }
        public bool IsServiceIsRunning()
        {
            return IsRunning;
        }

        public void Start()
        {
            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
            Geolocation.Default.GetLocationAsync(request);
            Console.WriteLine("service started");
            locationManager.StartLocationUpdates();
            locationManager.LocationUpdated += HandleLocationChanged;
            IsRunning = true;
        }

        public void Stop()
        {
            locationManager.StopLocationUpdates();
            locationManager.LocationUpdated -= HandleLocationChanged;
            IsRunning = false;
        }

        public void HandleLocationChanged(object sender, LocationUpdatedEventArgs e)
        {
            var location = e.Location;
            Console.WriteLine($"Latitude: {location.Coordinate.Latitude}, Longitude: {location.Coordinate.Longitude}, Altitude: {location.Altitude}");
           
        }
    }
}
