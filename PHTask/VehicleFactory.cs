namespace PHTask;

public static class VehicleFactory
{
    private static int _vehicleCounter = 1;
    private static List<Vehicle> _vehicles = new();
    
    public static Vehicle CreateVehicle(VehicleType type)
    {
        var vehicleUId = $"{type.ToString()}_{_vehicleCounter++}";
        var newVehicle = new Vehicle{ VehicleType = type, Id = vehicleUId};
        _vehicles.Add(newVehicle);

        return newVehicle;
    }
}