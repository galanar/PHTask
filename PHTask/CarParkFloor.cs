using PHTask.Exceptions;

namespace PHTask;

public class CarParkFloor
{
    public CarPark CarPark { get; }
    public int FLoorId { get; }
    
    public ParkingSpace[] ParkingSpaces { get; }
    
    public int FreeSpaces => ParkingSpaces.Count(x => !x.IsOccupied);
    public bool IsOccupied => ParkingSpaces.All(x => x.IsOccupied);
    
    public CarParkFloor(CarPark owner, int id, int maxSpaces)
    {
        if (maxSpaces <= 0)
            throw new IndexOutOfRangeException(nameof(maxSpaces));
        CarPark = owner;

        ParkingSpaces = new ParkingSpace[maxSpaces];
        
        for (var i = 0; i < maxSpaces; i++)
        {
            ParkingSpaces[i] = new ParkingSpace(this, $"{id}-{i}");
        }
    }
    
    public void Enter(Vehicle vehicle)
    {
        if (IsOccupied)
            throw new OccupiedException("Floor is occupied.");

        ParkingSpaces.First(x => !x.IsOccupied).Vehicle = vehicle;
        vehicle.IsInsideCarPark = true;
    }
    
    public void Exit(Vehicle vehicle)
    {
        var parkingSpace = ParkingSpaces.FirstOrDefault(x => x.Vehicle == vehicle);

        if (parkingSpace == null)
            throw new Exception("Vehicle is not on this floor");


        parkingSpace.Vehicle = null;
        vehicle.IsInsideCarPark = false;
    }
}