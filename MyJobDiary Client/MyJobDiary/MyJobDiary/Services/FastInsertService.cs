using MyJobDiary.Managers;
using MyJobDiary.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MyJobDiary.Services
{
    public class FastInsertService : IFastInsertService
    {
        private readonly CachedTableManager<Shift> _shiftManager;
        private readonly ILocationService _locationService;

        public FastInsertService(
            CachedTableManager<Shift> shiftManager,
            ILocationService locationService)
        {
            _shiftManager = shiftManager;
            _locationService = locationService;
        }

        public async Task<Shift> GetCurrentshift()
         => await GetCurrentshift(GetCurrentTime());

        public async Task<(Insertion, Shift)> InsertFast()
        {
            var insertion = Insertion.Departure;
            var currentTime = GetCurrentTime();
            var currentLocation = await _locationService.GetLocation();
            var currentShift = await GetCurrentshift(currentTime);
            if (currentShift == null)
            {
                currentShift = CreateInitShift(currentLocation, currentTime);
                insertion = Insertion.Departure;
            }
            else if (DateTime.Equals(currentShift.DepartureTime, currentShift.TimeFrom) &&
                     DateTime.Equals(currentShift.DepartureTime, currentShift.TimeTo) &&
                     DateTime.Equals(currentShift.DepartureTime, currentShift.ArrivalTime))
            {
                currentShift.TimeFrom = currentTime;
                currentShift.TimeTo = currentTime;
                currentShift.ArrivalTime = currentTime;
                currentShift.Location = currentLocation.Locality;
                currentShift.Country = currentLocation.CountryCode;
                insertion = Insertion.WorkStart;
            }
            else if (DateTime.Equals(currentShift.TimeFrom, currentShift.TimeTo) &&
                     DateTime.Equals(currentShift.TimeFrom, currentShift.ArrivalTime))
            {
                currentShift.TimeTo = currentTime;
                currentShift.ArrivalTime = currentTime;
                insertion = Insertion.WorkEnd;
            }
            else if (DateTime.Equals(currentShift.TimeTo, currentShift.ArrivalTime))
            {
                currentShift.ArrivalTime = currentTime;
                currentShift.ArrivalLocation = currentLocation.Locality;
                insertion = Insertion.Arrival;
            }
            else
            {
                currentShift = CreateInitShift(currentLocation, currentTime);
                insertion = Insertion.Departure;
            }
            await _shiftManager.SaveAsync(currentShift);
            return (insertion, currentShift);
        }

        private async Task<Shift> GetCurrentshift(DateTime currentTime)
        {
            var shifts = await _shiftManager.GetAsync();
            return shifts.Where(s => s.DepartureTime <= currentTime)
                .OrderByDescending(s => s.DepartureTime)
                .FirstOrDefault();
        }

        private Shift CreateInitShift(Placemark currentLocation, DateTime currentTime)
        {
            return new Shift()
            {
                DepartureLocation = currentLocation.Locality,
                DepartureTime = currentTime,
                TimeFrom = currentTime,
                TimeTo = currentTime,
                ArrivalTime = currentTime,
                IsClosed = true,
                WithDiets = true,
                IsNightShift = currentTime.Hour > 18
            };
        }

        private DateTime GetCurrentTime()
            => DateTime.Now.Date.Add(TruncTime(DateTime.Now.TimeOfDay));

        private TimeSpan TruncTime(TimeSpan timeSpan)
            => TimeSpan.FromMinutes(Math.Round(timeSpan.TotalMinutes / 5) * 5);
    }
}
