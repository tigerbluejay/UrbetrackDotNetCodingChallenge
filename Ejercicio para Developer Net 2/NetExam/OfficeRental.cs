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
        public List<Suggestion> SuggestionList { get; set; } = new List<Suggestion>();

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
            // TWO OUTSTANDING ISSUES, FIRST AddOffice() does not currently add ResourcesNeeded
            // I need to implement that and add it to previous code make sure nothing breaks.
            // SECOND, There's an issue with office Name and LocationName, need to be on the lookout for that.


            // if suggestion request is null, Console.WriteLine

            // AddLocation(Name, Neighborhood) (Centro 1, Centro)
            // AddOffice(LocationName, Name, MaxCapacity, AvailableResources)
            // ("Centro 1", "1", 12, new[] { "wi-fi", "proyector", "cafe" })

            // GetOfficeSuggestion(CapacityNeeded, PreferredNeighborhood, ResourcesNeeded)
            // (CapacityNeeded 18, Preferred Neighborhood Palermo, ResourcesNeeded null
            // ExpectedLocation Centro 2, ExpectedOffice 1)

            // excluyentes: capacidad necesaria, y recursos solicidados
            // prioridad: dar prioridad al barrio solicitado pero ofrecer otros si ese no se puede
            // prioridad: siempre seleccionar oficinas lo mas chicas posibles
            // prioridad: siempre seleccionar oficinas con menor cantidad de recursos posibles

            /*---- OFFICE LIST ----- */

            // el IEnumerable<OfficeList> tiene los siguientes datos
            // AddOffice(LocationName, Name, MaxCapacity, AvailableResources)
            //("Centro 1", "1", 12, new[] { "wi-fi", "proyector", "cafe" })
            //("Centro 1", "2", 8, new[] { "wi-fi", "tv", "cafe" })
            //("Centro 1", "3", 8, new[] { "wi-fi" })
            //("Centro 1", "4", 4, new[] { "tv" })
            //("Centro 2", "1", 20, new[] { "wi-fi", "proyector", "cafe", "catering" })
            //("Centro 2", "2", 6, new[] { "wi-fi", "tv", "cafe" })
            //("Centro 2", "3", 6, new[] { "wi-fi", "tv" })
            //("Palermo", "1", 10, new[] { "wi-fi", "tv" })
            //("Palermo", "2", 8, new[] { "wi-fi", "tv" })

            // to be Matched with
            // (CapacityNeeded, Preferred Neighborhood, ResourcesNeeded
            // ExpectedLocation, ExpectedOffice)
            //(18, "Palermo", null,"Centro 2", "1")
            //(6, "Centro", new[] { "wi-fi", "tv" }, "Centro 2", "3")
            //(2, null, null, "Centro 1", "4")
            //(2, null, new[] { "proyector", "catering" }, "Centro 2", "1")
            //(30, null, null, null, null)

            // HasRequiredCapacity()
            // var query = officeList.Where( n => n.MaxCapacity >= CapacityNeeded)

            // HasRequiredResources()
            // var filteredList = query.Where(n =>
            // n.AvailableResources is IEnumerable<string> stringList &&
            // ResourcesNeeded.All(s => stringList.Contains(s)));

            /*---- PRIORIZAR BARRIO PREFERIDO ----- */

            // added locations
            //OfficeRental.AddLocation(new LocationSpecs("Centro 1", "Centro"));
            //OfficeRental.AddLocation(new LocationSpecs("Centro 2", "Centro"));
            //OfficeRental.AddLocation(new LocationSpecs("Palermo", "Palermo"));


            // PrioritizePreferredNeighborhood()
            // obetner listado de Locations por barrio solicitado
                // var filteredLocationList = LocationList.Where(n => n.Neighbohood == PreferredNeighborhood).ToList();
            // filteredLocationList es un IEnumerable<Location> con Name y Neighborhood, pero los 
            // resultados solo corresponden a los Location que corresponden al barrio preferido.


            ////// ordenar sugerencias 2 ejemplos el primero es el primero que genere, no es el mejor
            ////var orderedList = filteredList.OrderBy(n => { // estamos ordenando el filterd list by name
            ////    string officeName = n.LocationName;
            ////    string name = filteredLocationList.FirstOrDefault(n => n.Name.StartsWith(officeName));
            ////    return name;
            ////});
            //var orderedList = filteredList.OrderBy(office => {
            //    string officeName = office.LocationName;
            //    string name = filteredLocationList.FirstOrDefault(location => location.Name == officeName)?.Name ?? officeName;
            //    return name;
            //});


            //You have filteredLocationList which is a list of complex type Location which has two string properties
            //Name and Neighborhood. You have filteredList which is a list of complex type Office which has four properties, three
            //of which are strings: LocationName, Name, MaxCapacity, and a fourth property which is an IEnumerable<string>
            //called AvailableResources. You want to use LINQ to order filteredList to produce a list called orderedList, which orders results
            //in the filteredList by LocationName, where ascending priority is given to the value contained in the filteredLocationList's Name property.

            /*---- SIEMPRE SELECCIONAR LAS OFICINAS MAS CHICAS POSIBLES ----- */

            // PrioritizeSmallerOffices()  // PrioritizeMinimumAvailableResourceOffices()


            // dar prioridad a las oficinas mas chicas
            // result = orderedList.OrderBy(x => int.Parse(x.MaxCapacity))
            //             .ThenBy(x => x.AvailableResources.Count())
            //             .ToList();

            /*---- SIEMPRE SELECCIONAR LAS OFICINAS CON MENOS RECURSOS PRIMERO ----- */


            //// dar prioridad a las oficinas con menos recursos

            //I have an IEnumerable<Office> called orderedList. orderedList has four properties.
            //The first three are strings and they are called LocationName, Name, and MaxCapacity.
            //The fourth property is an IEnumerable<string> called AvailableResources.
            //I have to reorder orderList according to two criteria.
            //First I have to order in ascending order by MaxCapacity. Although MaxCapacity is a
            //string, it contains ints coded as strings. The ordering must be done treating these strings
            //as ints.
            //Then I have to order orderedList in ascending order according to the count of available resources.
            //That is, items with AvailableResources containing fewer strings should be ordered first.
            //I want to do this using the OrderBy and ThenBy LINQ methods.


            /*---- OVERARCHING WORKFLOW ----- */

            // if suggestion request is null, Console.WriteLine

            // if CapacityNeeded is not null
            // HasRequiredCapacity()
            // if CapacityNeeded is null
            // query = officeList

            // if ResourcesNeeded is not null
            // HasRequiredResources()
            // if ResourcesNeeded is null
            // filteredList = query

            // if PreferredNeighbohood is not null
            // PrioritizePreferredNeighborhood()
            // if PreferredNeighborhood is null
            // orderedList = filteredList

            // PrioritizeSmallerOffices()  // PrioritizeMinimumAvailableResourceOffices()

            // persist to suggestion

            //foreach (var item in result)
            //{
            //    var suggestion = new Suggestion
            //    {
            //        LocationName = item.LocationName,
            //        Name = item.Name,
            //        MaxCapacity = item.MaxCapacity,
            //        AvailableResources = item.AvailableResources
            //    };
            //    SuggestionList.Add(suggestion);
            //}

            // return SuggestionList;

            //I have an IEnumerable<Office> result which has four properties.The first three:
            // Location Name, Name, and Max Capacity are strings. The fourth, AvailableResources
            // is an IEnumerable < string >.result is loaded with three items.
            // I also have a property called SuggestionList which is of type IEnumerable<Suggestion>
            // Suggestion has the same four properties that result has and of the same type.
            // I need to loop through result and add to SuggestionList all three items that appear
            // on result.

        }


    }
}