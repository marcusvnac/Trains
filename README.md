# Trains

##Problem Description

The local commuter railroad services a number of towns in Kiwiland.  Because of monetary concerns, all of the tracks are 'one-way.'  That is, a route from Kaitaia to Invercargill does not imply the existence of a route from Invercargill to Kaitaia.  In fact, even if both of these routes do happen to exist, they are distinct and are not necessarily the same distance!

The purpose of this problem is to help the railroad provide its customers with information about the routes.  In particular, you will compute the distance along a certain route, the number of different routes between two towns, and the shortest route between two towns.

Input:  A directed graph where a node represents a town and an edge represents a route between two towns.  The weighting of the edge represents the distance between the two towns.  A given route will never appear more than once, and for a given route, the starting and ending town will not be the same town.

Output: For test input 1 through 5, if no such route exists, output 'NO SUCH ROUTE'.  Otherwise, follow the route as given; do not make any extra stops!  For example, the first problem means to start at city A, then travel directly to city B (a distance of 5), then directly to city C (a distance of 4).
The distance of the route A-B-C.
The distance of the route A-D.
The distance of the route A-D-C.
The distance of the route A-E-B-C-D.
The distance of the route A-E-D.
The number of trips starting at C and ending at C with a maximum of 3 stops.  In the sample data below, there are two such trips: C-D-C (2 stops). and C-E-B-C (3 stops).
The number of trips starting at A and ending at C with exactly 4 stops.  In the sample data below, there are three such trips: A to C (via B,C,D); A to C (via D,C,D); and A to C (via D,E,B).
The length of the shortest route (in terms of distance to travel) from A to C.
The length of the shortest route (in terms of distance to travel) from B to B.
The number of different routes from C to C with a distance of less than 30.  In the sample data, the trips are: CDC, CEBC, CEBCDC, CDCEBC, CDEBC, CEBCEBC, CEBCEBCEBC.

Test Input:
For the test input, the towns are named using the first few letters of the alphabet from A to D.  A route between two towns (A to B) with a distance of 5 is represented as AB5.
Graph: AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7
Expected Output:
Output #1: 9
Output #2: 5
Output #3: 13
Output #4: 22
Output #5: NO SUCH ROUTE
Output #6: 2
Output #7: 3
Output #8: 9
Output #9: 9
Output #10: 7



##Solution
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
