using CoreLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace GeolocationUtils.Platforms.iOS
{
    public class LocationManageriOS
    {
        protected CLLocationManager locationManager;

        public LocationManageriOS()
        {
            locationManager = new ();
            locationManager.PausesLocationUpdatesAutomatically = false;

            if (UIDevice.CurrentDevice.CheckSystemVersion(9,0))
            {
                locationManager.AllowsBackgroundLocationUpdates = true;
            }
        }

        public CLLocationManager GetLocationManager() => this.locationManager;

        public void StartLocationUpdates()
        {
            if (CLLocationManager.LocationServicesEnabled)
            {
                //set the desired accuracy, in meters
                locationManager.DesiredAccuracy = 1;
                locationManager.LocationsUpdated += (object sender, CLLocationsUpdatedEventArgs e) =>
                {
                    // fire our custom Location Updated event
                    LocationUpdated(this, new LocationUpdatedEventArgs(e.Locations[e.Locations.Length - 1]));
                };
                locationManager.StartUpdatingLocation();
            }
        }

        public void StopLocationUpdates()
        {
            if (CLLocationManager.LocationServicesEnabled)
            {
                locationManager.StopUpdatingLocation();
            }
        }

        public event EventHandler<LocationUpdatedEventArgs> LocationUpdated = delegate { };

    }
}
