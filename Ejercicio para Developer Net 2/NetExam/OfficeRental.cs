namespace NetExam
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using NetExam.Abstractions;
    using NetExam.Dto;
    using NetExam.Models;

    public class OfficeRental : IOfficeRental
    {
        public List<Location> LocationList { get; set; } = new List<Location>();
        public List<Office> OfficeList { get; set; } = new List<Office>();
        public List<Request> RequestList { get; set; } = new List<Request>();

        public void AddLocation(LocationSpecs locationSpecs)
        {
            if (locationSpecs is null)
            {
                Console.WriteLine("Location may not be empty");
                return;
            }
            
            if (string.IsNullOrWhiteSpace(locationSpecs.Name) || 
                string.IsNullOrWhiteSpace(locationSpecs.Neighborhood))
            {
                Console.WriteLine("Name and Neighborhood cannot be empty");
                return;
            }

            bool match = LocationNameExists(locationSpecs.Name);
            if (match) 
            { 
                throw new Exception("Location Already Exists");
            }

            var record = new Location();
            record.Name = locationSpecs.Name;
            record.Neighborhood = locationSpecs.Neighborhood;
            LocationList.Add(record);

            Console.WriteLine("Location and Neighborhood Successfully Added");

            Console.WriteLine("Complete Location List");

            foreach (var item in LocationList)
            {
                Console.WriteLine($"{item.Name} in {item.Neighborhood}");
            }

        }
        private bool LocationNameExists(string specsName)
        {
            foreach (var item in LocationList)
            {
                if (item.Name == specsName)
                { return true; }
            }
            return false;
        }

        public void AddOffice(OfficeSpecs officeSpecs)
        {
            if (officeSpecs is null)
            {
                Console.WriteLine("Location may not be empty");
                return;
            }

            if ((string.IsNullOrWhiteSpace(officeSpecs.LocationName)) ||
                (string.IsNullOrWhiteSpace(officeSpecs.Name)) ||
                (officeSpecs.MaxCapacity <= 0))
            {
                Console.WriteLine("Location Name, Office Name and Maximum Capacity cannot be empty");
                return;
            }

            bool match = LocationNameExists(officeSpecs.LocationName);
            if (!match)
            {
                throw new Exception("Location Does Not Exist");
            }

            var record = new Office();
            record.LocationName = officeSpecs.LocationName;
            record.Name = officeSpecs.Name;
            record.MaxCapacity = officeSpecs.MaxCapacity;
            OfficeList.Add(record);

            Console.WriteLine("Office Successfully Added");

            Console.WriteLine("Complete Office List:");

            foreach (var item in OfficeList)
            {
                Console.WriteLine($"{item.Name} in {item.LocationName} with" +
                    $"a max capacity of {item.MaxCapacity}");
            }

        }

        public void BookOffice(BookingRequest bookingRequest)
        {
            if (bookingRequest is null)
            {
                Console.WriteLine("Booking Request may not be empty");
                return;
            }

            if ((string.IsNullOrWhiteSpace(bookingRequest.LocationName)) ||
                (string.IsNullOrWhiteSpace(bookingRequest.OfficeName)) ||
                (string.IsNullOrWhiteSpace(bookingRequest.UserName)) ||
                (bookingRequest.Hours <= 0))
            {
                Console.WriteLine("Booking Request Information must be complete");
            }

            bool match = LocationNameExists(bookingRequest.LocationName);
            if (!match)
            {
                throw new Exception("Location Does Not Exist");
            }

            bool reservationMatch = OfficeIsTaken(bookingRequest);
            if (reservationMatch)
            {
                throw new Exception("Office is Taken, Reservation for that Office Already Exists");
            }


            var request = new Request();
            request.LocationName = bookingRequest.LocationName;
            request.OfficeName = bookingRequest.OfficeName;
            request.UserName = bookingRequest.UserName;
            request.Hours = bookingRequest.Hours;
            request.DateTime = bookingRequest.DateTime;
            RequestList.Add(request);

            Console.WriteLine("Request Successfully Added");

            Console.WriteLine("Complete Booking Request List:");

            foreach (var item in RequestList)
            {
                Console.WriteLine($"{item.OfficeName} for {item.UserName} in {item.LocationName} " +
                    $" booked at {item.DateTime} for a period of {item.Hours} hours");
            }

        }

        private bool OfficeIsTaken(BookingRequest newRequest)
        {
            var result = RequestList.Where(x => x.LocationName == newRequest.LocationName &&
            x.OfficeName == newRequest.OfficeName && (x.DateTime <= newRequest.DateTime &&
            x.DateTime.AddHours((double)x.Hours) >= newRequest.DateTime)).Any();
            return result;

        }

        public IEnumerable<IBooking> GetBookings(string locationName, string officeName)
        {
            IEnumerable<IBooking> result = (IEnumerable<IBooking>)RequestList.
                Where(x => x.LocationName == locationName && x.OfficeName == officeName);
  
            return result;
        }

        public IEnumerable<ILocation> GetLocations()
        {
            IEnumerable<Location> result = LocationList.ToList();
            return result;

        }

        public IEnumerable<IOffice> GetOffices(string locationName)
        {
            IEnumerable<Office> result = OfficeList.ToList();
            return result;

        }

        public IEnumerable<IOffice> GetOfficeSuggestion(SuggestionRequest suggestionRequest)
        {
            throw new NotImplementedException();
        }

        
    }
}