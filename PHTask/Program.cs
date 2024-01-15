using PHTask;

var carPark = new CarPark("Alster-CarPark");
carPark.AddFloor(5);
carPark.AddFloor(5);
carPark.AddFloor(5);

var carA = VehicleFactory.CreateVehicle(VehicleType.Car);
var carB = VehicleFactory.CreateVehicle(VehicleType.Car);
var carC = VehicleFactory.CreateVehicle(VehicleType.Car);
var motA = VehicleFactory.CreateVehicle(VehicleType.Car);
var motB = VehicleFactory.CreateVehicle(VehicleType.Car);
var motC = VehicleFactory.CreateVehicle(VehicleType.Car);

carPark.Enter(carA);
carPark.Enter(carB);
carPark.Enter(carC);
carPark.Enter(motA);
carPark.Enter(motB);
carPark.Enter(motC);

carPark.Exit(motB);

var searchResult = carPark.Search(carB);

Console.WriteLine($"{carB.Id} -> {searchResult.FloorId} | {searchResult.ParkingSpaceId}");

