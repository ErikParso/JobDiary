using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MyJobDiary.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILoadingService _loadingService;
        private readonly IDialogService _dialogService;

        public LocationService(
            ILoadingService loadingService,
            IDialogService dialogService)
        {
            _loadingService = loadingService;
            _dialogService = dialogService;
        }


        public async Task<Placemark> GetLocation()
        {
            Placemark ret = null;
            _loadingService.StartLoading("Načítavam pozíciu");

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
                _dialogService.ShowDialog("Lokalizácia neúspešná", e.Message);
            }

            _loadingService.StopLoading();
            return ret;
        }
    }
}
