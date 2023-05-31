using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AndroidX.Core.App;
using GeolocationUtils.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeolocationUtils.Services
{
    [Service]
    public class BackgroundLocationServiceAndroid : Service, IBackgroundService
    {
        // A notification requires an id that is unique to the application.
        const string NOTIFICATION_ID = "9000";
       
        public static bool IsRunning;

        public BackgroundLocationServiceAndroid()
        {
          
        }

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
            Geolocation.Default.GetLocationAsync(request);

            Task.Run(async () =>
            {
                while (IsRunning)
                {
                    Console.WriteLine("Service running");
                    GeolocationUtils.Services.LocationService.GetLocation();
                    Thread.Sleep(2000);
                }
            });

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                var notificationChannel = new NotificationChannel(NOTIFICATION_ID, NOTIFICATION_ID, NotificationImportance.Low);
                notificationManager.CreateNotificationChannel(notificationChannel);
            }

            var notificationBuilder = new NotificationCompat.Builder(this, channelId: NOTIFICATION_ID)
                                            .SetContentTitle("Location service started")
                                            .SetContentText("Service is running in Foreground")
                                            //.SetSmallIcon(Resource.Mipmap.appicon_round)
                                            .SetPriority(1)
                                            .SetOngoing(true)
                                            .SetChannelId(NOTIFICATION_ID)
                                            .SetAutoCancel(true);

            StartForeground(9000, notificationBuilder.Build());
            return base.OnStartCommand(intent, flags, startId);
        }

        public override void OnCreate()
        {
            IsRunning = true;
            base.OnCreate();
        }

        public override void OnDestroy()
        {
            IsRunning = false;
            base.OnDestroy();
        }

        public void Start()
        {
            var intent = new Intent(Android.App.Application.Context, typeof(BackgroundLocationServiceAndroid));
            Android.App.Application.Context.StartForegroundService(intent);
        }

        public void Stop()
        {
            var intent = new Intent(Android.App.Application.Context, typeof(BackgroundLocationServiceAndroid));
            Android.App.Application.Context.StopService(intent);
        }

        public bool IsServiceIsRunning() => IsRunning;
    }

}
