using PHTask.Exceptions;

namespace PHTask;

public class CarPark
{
    public string Id { get; }

    private int floorCount = 0;
    private readonly List<CarParkFloor> _floors = new();

    public int FloorCount => _floors.Count;
    
    public int TotalParkingSpaces => _floors.Sum(floor => floor.ParkingSpaces.Length);
    
    public int FreeSpaces => TotalParkingSpaces - _floors.Sum(floor => floor.ParkingSpaces.Count(x => x.IsOccupied));

    public bool IsOccupied => _floors.All(x => x.IsOccupied) || _floors.Count == 0;
    
    public CarPark(string id)
    {
        Id = id;
    }

    public void AddFloor(int parkingSpaces)
    {
        _floors.Add(new CarParkFloor(this, floorCount++, parkingSpaces));
    }
    
    public void Enter(Vehicle vehicle)
    {
        if (IsOccupied)
            throw new OccupiedException("CarPark is occupied. Come back later");
        
        _floors.First(x => !x.IsOccupied).Enter(vehicle);
    }

    public void Exit(Vehicle vehicle)
    {
        var vehicleParkingSpace = _floors.SelectMany(y => y.ParkingSpaces).FirstOrDefault(y => y.Vehicle == vehicle);

        if (vehicleParkingSpace == null)
            throw new VehicleNotFoundException("Vehicle is not inside the CarPark");

        vehicleParkingSpace.Floor.Exit(vehicle);
    }

    public VehicleSearchResult Search(Vehicle vehicleToSearch)
    {
        var parkingSpace = _floors.SelectMany(x => x.ParkingSpaces).FirstOrDefault(x => x.Vehicle == vehicleToSearch);

        if (parkingSpace == null)
            throw new VehicleNotFoundException("Vehicle is not inside the CarPark");

        return new VehicleSearchResult
        {
            FloorId = parkingSpace.Floor.FLoorId,
            ParkingSpaceId = parkingSpace.Id
        };
    }
}