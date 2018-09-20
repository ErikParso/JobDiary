using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MyJobDiary.Services
{
    public class CurrentLocationProvider
    {
        public static async Task<Placemark> GetLocation()
        {
            Placemark ret = null;
            App.LoadingService.StartLoading("Načítavam pozíciu");

            try
            {
                var location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Low));
                if (location != null)
                {
                    var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                    if (placemarks != null)
                    {
                        ret = placemarks.FirstOrDefault();
                    }
                }
            }
            catch (Exception e)
            {
                App.DialogService.ShowDialog("Lokalizácia neúspešná", e.Message);
            }

            App.LoadingService.StopLoading();
            return ret;
        }
    }
}
