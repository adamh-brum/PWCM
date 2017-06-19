namespace APITest
{
    using System;
    using System.Collections.Generic;
    using API.DataLogic.Models;
    using API.Helpers;
    using Xunit;

    public class UnitTest1
    {
        [Fact]
        public void NoBeacons_NoTimeslots()
        {
            // Arrange
            List<BeaconAvailability> beaconAvailability = new List<BeaconAvailability>();

            // Act
            var result = TimeslotGenerator.Generate(beaconAvailability);

            // Assert
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void NullBeacons_NoTimeslots()
        {
            // Act
            var result = TimeslotGenerator.Generate(null);

            // Assert
            Assert.Equal(0, result.Count);   
        }

        [Fact]
        public void OneBeacon_NoBookings_3MonthsAvailableTimeslots()
        {
            // Arrange
            List<BeaconAvailability> beaconAvailability = new List<BeaconAvailability>();
            beaconAvailability.Add(new BeaconAvailability()
            {
                BeaconId = Guid.NewGuid(),
                FriendlyName = "Test Beacon",
                Location = "Cyberspace",
            });

            // Act
            var result = TimeslotGenerator.Generate(beaconAvailability);

            // Assert
            Assert.
        }
    }
}
