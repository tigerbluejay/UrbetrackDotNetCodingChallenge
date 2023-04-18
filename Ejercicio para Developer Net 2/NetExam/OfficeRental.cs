namespace NetExam
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using NetExam.Abstractions;
    using NetExam.Dto;

    public class OfficeRental : IOfficeRental
    {
        public void AddLocation(LocationSpecs locationSpecs)
        {
            // check if locationSpecs is null, if it is return a console error message
            // if its not proceed to
                // check if it LocationName or Neighbohood are null or white space,
                // if either is, return a console error message
                    // extract LocationName and Neighborhood from location specs
                    // into an object that can be saved in memory or to the database
                            // (here add the whole object with LocationName and Neighborhood to the list)
                            // (since there would eventually be many objects in this list)
                        // for instance a property defined in this same class, so I can access it
                        // when retrieving a location. - for this define a public property of
                        // type IEnumerable<ILocation> and
                        // in a separate class implement a concrete class for ILocation with the Name property
                    // add a constructor for office rental that uses dependency injection to set the properties
                    // such as the IEnumerable<ILocation> object.
            // return nothing since return type is void

            // next: throw an exception of any type when location name already exists, for this:
            // compare the locationSpecs.Name incoming parameter property with
            // the existing IEnumerable<ILocation> object's name property for this:
                // implement a comparison function where we pass as parameter the incoming parameter property
                // inside the function loop though the IEnumerable<ILocation> object.Name property and check
                // for a match with the incoming parameter, return true if match, return false if no match
                    // the function would look something like this
                        // private bool LocationNameExists(locationSpecs.Name)
                        // {
                        //  foreach (var item in IEnumerable<ILocation>object)
                        //       {
                        //          if (item.Name == locationSpecs.Name)
                        //          { return true }
                        //       }
                        //      return false
                        // }
            // if result is true throw the exception
            
        }

        public void AddOffice(OfficeSpecs officeSpecs)
        {
            // remember to check if officeSpecs is a null object and if
            // either of the three properties it contains are null or white space
            // return console error message if so

            // define a property IEnumerable<IOffice> and build in the constructor
            // retrieve officeSpecs paramater values (LocationName, Name, MaxCapacity)
            // and save them into a new IOffice element in the IEnumberable (which has Location Name and Name)
            // what do we do with MaxCapacity?
            
            // check whether location exists before adding office
            // bring the IEnumerable<ILocation>object and check if parameter officeSpecs.LocationName matches
            // if it matches return true
            // if it doesnt match return false - and throw exception
            // private method could be 
            // the function would look something like this -
                // private bool LocationNameExists2(officeSpecs.LocationName)
                // {
                //  foreach (var item in IEnumerable<ILocation>object)
                //       {
                //          if (item.Name == officeSpecs.LocationName)
                //          { return true }
                //       }
                //      return false
                // }


            // return nothing since return type is void
        }

        public void BookOffice(BookingRequest bookingRequest)
        {
            // incoming parameter bookingRequest (LocationName, OfficeName, DateTime)
            // search if Location Name exists in IEnumerable<ILocation>
                // search if Office Name exists in IEnumerable<IOffice>
                    // search if in the IEnumerable<IBooking>object dateTime stamp exists
                            // if it doesnt persist it that is save it

            // if location name does not exist return console error
            // if office name does not exist return console error
            // if datetime does not exist and loc and off exist, persist to memory 


            // AND BookOffice will throw an exception when booking an already taken office
            // bookingRequest (LocationName, OfficeName, DateTime, Hours, UserName)
            // so here I would have to check if location name exists
            // check if office name exists in that location
            // check that an office is not taken (how do i know this)
            //      check if the datetime and the datetime plus one hour is taken, if it is throw an exception
        }

        public IEnumerable<IBooking> GetBookings(string locationName, string officeName)
        {
            // check in the IEnumerable<IOffice>object
            // bool = IEnumerable<IOffice>object.Any(u=>u.LocationName == locationName && u.Name == officeName)
            // if true location and office exist and we can retrieve bookings
            //
            // return the IEnumerable<IBooking>object (DateTime)

            // if false location and/or office do not exist and console error message is output.
            //
        }

        public IEnumerable<ILocation> GetLocations()
        {
            // return the IEnumerable<ILocation> property
        }

        public IEnumerable<IOffice> GetOffices(string locationName)
        {
            // return an IEnumerable<IOffice> (LocationName Name) based on the locationName string parameter.
            // use LINQ IEnumerable<IOffice>object.Where(u => u.LocationName == locationName).ToList();
            // return this new filtered object.
        }

        public IEnumerable<IOffice> GetOfficeSuggestion(SuggestionRequest suggestionRequest)
        {
            throw new NotImplementedException();
        }
    }
}