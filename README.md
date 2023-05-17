# Urbetrack Dot Net Coding Challenge
# A Complete Solution by Jose Maria Iriarte

This project is my answer to an interview challenge at Urbetrack.

To solve the challenge, I created several methods that handle the main functionality proposed by the tests.
These public methods include AddLocation(), AddOffice(), BookOffice(), GetBookings(), GetLocations(), GetOffices(), and GetOfficeSuggestion()

To these methods I needed to add some helper methods to keep code organized. These include
LocationNameExists(), OfficeIsTaken(), OfficesWithRequiredCapacity(), OfficesWithRequiredResources(), OfficesByPreferredNeighborhood(),
and OfficesBySizeandResourceQuantity();

The functions do what their name indicates. In the case of helper methods, except for the first two, the functions return a progressively
filtered list of offices which is required by the challenge's unit tests.

All of these functions reside in the OfficeRental class.

DTOs are predefined with their interfaces to allow for information to travel from the Unit Tests to OfficeRental.

I added some Models although the model Suggestion is superflous, and was not needed in the end.
I'm not sure I absolutely needed to define Models in addition to the DTOs but that's how I solved it.

The main difficulty of the challenge lay in the GetOfficeSuggestion() method, which used LINQ queries extensively, some of which I had to generate with the assistance of Artificial Intelligence.

All tests are green and code is mostly clean code.




