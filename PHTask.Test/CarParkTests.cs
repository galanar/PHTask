using PHTask.Exceptions;

namespace PHTask.Test;
using FluentAssertions;

public class Tests
{
    private CarPark _carPark;
    [SetUp]
    public void Setup()
    {
        _carPark = new CarPark("Alster-Parkhaus");
    }
    
    [Test]
    public void CarPark_Should_Have_200_TotalSpaces_On_Two_Floors()
    {
        // Arrange
        
        // Act
        _carPark.AddFloor(100);
        _carPark.AddFloor(100);

        
        // Assert
        _carPark.TotalParkingSpaces.Should().Be(200);
        _carPark.FloorCount.Should().Be(2);

    }
    
    [Test]
    public void CarPark_Should_Have_200_FreeSpaces_On_Two_Floors()
    {
        // Arrange
        
        // Act
        _carPark.AddFloor(100);
        _carPark.AddFloor(100);

        
        // Assert
        _carPark.FreeSpaces.Should().Be(200);
        _carPark.FloorCount.Should().Be(2);
    }
    
    [Test]
    public void CarPark_Should_Have_200_TotalSpaces_And_Has_2_Occupied_Spaces()
    {
        // Arrange
        var vehicleA = VehicleFactory.CreateVehicle(VehicleType.Car);
        var vehicleB = VehicleFactory.CreateVehicle(VehicleType.Car);
        
        // Act
        _carPark.AddFloor(100);
        _carPark.AddFloor(100);

        _carPark.Enter(vehicleA);
        _carPark.Enter(vehicleB);
        
        // Assert
        _carPark.TotalParkingSpaces.Should().Be(200);
        _carPark.FreeSpaces.Should().Be(198);
    }
    
    [Test]
    public void CarPark_Should_Have_2_TotalSpaces_And_Has_0_Free_Spaces()
    {
        // Arrange
        var vehicleA = VehicleFactory.CreateVehicle(VehicleType.Car);
        var vehicleB = VehicleFactory.CreateVehicle(VehicleType.Car);
        
        // Act
        _carPark.AddFloor(1);
        _carPark.AddFloor(1);

        _carPark.Enter(vehicleA);
        _carPark.Enter(vehicleB);
        
        // Assert
        _carPark.TotalParkingSpaces.Should().Be(2);
        _carPark.FreeSpaces.Should().Be(0);
    }
    
    [Test]
    public void CarPark_Exit_Should_Have_CarPark_200_TotalSpaces_And_1_Occupied_Spaces()
    {
        // Arrange
        var vehicleA = VehicleFactory.CreateVehicle(VehicleType.Car);
        var vehicleB = VehicleFactory.CreateVehicle(VehicleType.Car);
        
        // Act
        _carPark.AddFloor(100);
        _carPark.AddFloor(100);

        _carPark.Enter(vehicleA);
        _carPark.Enter(vehicleB);
        _carPark.Exit(vehicleB);
        
        // Assert
        _carPark.TotalParkingSpaces.Should().Be(200);
        _carPark.FreeSpaces.Should().Be(199);
    }
    
    [Test]
    public void CarPark_Search_Should_Return_Floor_0_Space_0()
    {
        // Arrange
        var vehicleA = VehicleFactory.CreateVehicle(VehicleType.Car);
        // Act
        _carPark.AddFloor(1);
        _carPark.AddFloor(1);

        _carPark.Enter(vehicleA);

        var searchResult = _carPark.Search(vehicleA);

        // Assert
        searchResult.FloorId.Should().Be(0);
        searchResult.ParkingSpaceId.Should().Be("0-0");
    }
    
    [Test]
    public void CarPark_Should_Throw_OccupiedException()
    {
        // Arrange
        var vehicleA = VehicleFactory.CreateVehicle(VehicleType.Car);
        var vehicleB = VehicleFactory.CreateVehicle(VehicleType.Car);
        var vehicleC = VehicleFactory.CreateVehicle(VehicleType.Car);
        
        // Act
        _carPark.AddFloor(1);
        _carPark.AddFloor(1);

        _carPark.Enter(vehicleA);
        _carPark.Enter(vehicleB);
        
        var enterAction = () => _carPark.Enter(vehicleC);

        // Assert
        enterAction.Should().Throw<OccupiedException>();
    }
    
    [Test]
    public void CarPark_Search_Should_Throw_VehicleNotFoundException()
    {
        // Arrange
        var vehicleA = VehicleFactory.CreateVehicle(VehicleType.Car);
        var vehicleB = VehicleFactory.CreateVehicle(VehicleType.Car);
        // Act
        _carPark.AddFloor(1);
        _carPark.AddFloor(1);

        _carPark.Enter(vehicleA);

        var searchAction = () => _carPark.Search(vehicleB);

        // Assert
        searchAction.Should().Throw<VehicleNotFoundException>();
    }
}