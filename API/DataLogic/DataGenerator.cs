namespace API.DataLogic
{
    using API.DataLogic.Models;
    using System.Collections.Generic;
    using System;

    public static class DataGenerator
    {
        public static IDataLogic GenerateSqliteData()
        {
            var dataLogic = new SqliteDataLogic();
            SeedData(dataLogic);
            return dataLogic;
        }

        private static void SeedData(IDataLogic dataLogic)
        {
            // Add stub data
            Guid beaconId = Guid.Parse("74278BDA-B644-4520-8F0C-720EAF059935");
            if (dataLogic.GetBeacon(beaconId) == null)
            {
                dataLogic.AddBeacon(beaconId, "33497", "White Beacon", "Reception");

                int contentId = dataLogic.AddContent("Dark outside? Blinds down", "<h3 style='color: red'>You are now in a secure zone. Leaving the blinds open while it's dark outside lets outsiders look in. </h3>");
                dataLogic.ScheduleContent(contentId, new Guid[1] { beaconId }, DateTime.Now.AddHours(-1), DateTime.Now.AddHours(1));
            }
        }
    }
}