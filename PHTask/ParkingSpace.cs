namespace PHTask;

public class ParkingSpace
{
    public string Id { get; private set; }

    public Vehicle? Vehicle { get; set; }

    public bool IsOccupied => Vehicle != null;

    public CarParkFloor Floor { get; }
    
    public ParkingSpace(CarParkFloor floor, string id)
    {
        Floor = floor;
        Id = id;
    }
}