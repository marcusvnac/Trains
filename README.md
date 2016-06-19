# Trains

The solution developed was a RESTful API created with ASP.NET Web API 2.

The application was created in following enviroment:

- Microsoft Windows 7 / 8.1 x64;
- Visual Studio 2013 Ultimate;
- .NET Framework 4.5;

To compile and run, open the solution file 'Trains.sln' under the Trains directory and execute the WebProject TrainsAPI.

In localhost, the application will run in IIS Express on URI http://localhost:13672/api/TrainsAPI/

The created API will provide the following methods:

	- RouteDistance: Computes the distance bewteen two towns. (Tests 1 to 5)
			* Parameter: Route
			* HTTP Verb: GET
			* Example Call: http://localhost:13672/api/TrainsAPI/RouteDistance/A-D-C
			* Return: Integer that represents a distance found. 
					If returned -1, no route is found. This way, the cliente can process the return and show 'NO SUCH ROUTE' message.

	- AvailableRoutesMaxStops: Computes all routes between two towns with a number maximum of stops. (Test 6)
			* Parameters: Start city, End cuty, Maximum Stops
			* HTTP Verb: GET
			* Example Call: http://localhost:13672/api/TrainsAPI/AvailableRoutesMaxStops/C/C/3
			* Return: List of route and number of stops of each route. Empty if no route is found.

	- AvailableRoutesNumStops: Computes all routes between two nodes with a defined number of stops. (Test 7)
			* Parameters: Start city, End cuty, Number of Stops
			* HTTP Verb: GET
			* Example Call: http://localhost:13672/api/TrainsAPI/AvailableRoutesNumStops/A/C/4
			* Return: List of route and number od stops of each route. Empty if no route is found.

	- AvailableRoutesMaxDistance: Computes the number of routes between two nodes with a maximum distance. (Test 10)
			* Parameters: Start city, End cuty, Max distance
			* HTTP Verb: GET
			* Example Call: http://localhost:13672/api/TrainsAPI/AvailableRoutesMaxDistance/C/C/30
			* Return: Integer that represents the number of routes found.

	- ShortestRoute: Computes the shortest route between two towns. (Tests 8 and 9)
			* Parameter: Start city, End city
			* HTTP Verb: GET
			* Example Call: http://localhost:13672/api/TrainsAPI/ShortestRoute/A/C
			* Return: Route and cost of each route. Empty if no route is found.


The informations about cities and their connections are in CityConnectios.txt file, in TrainsAPI project folder.

The unit tests were created with Visual Studio buil-in unit testing framework. To run it, only open Test Explorer window and click Run All after compile the solution.
