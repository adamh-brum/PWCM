namespace API.DataLogic
{
    using API.DataLogic.Models;
    using System.Collections.Generic;
    using System;

    public static class DataGenerator
    {
        public static InMemoryDataLogic GenerateData()
        {
            var dataLogic = new InMemoryDataLogic();
            
            // Add stub data
            Guid beaconId = Guid.Parse("74278BDA-B644-4520-8F0C-720EAF059935");
            dataLogic.AddBeacon(beaconId, "33497", "White Beacon", "Reception");
            Guid contentId = dataLogic.AddContent("Welcome to @Pheonix", "<div style='color: darkcyan'><p>While you should feel free to enjoy the newly refurbished building, please note <b>contactors are still on site and not all areas are open</b>. For more details, visit the portal page <a href='www.google.com'>Accommodation Help</a></p></div>");
            dataLogic.ScheduleContent(contentId, new Guid[1]{beaconId}, DateTime.Now.AddHours(-1), DateTime.Now.AddHours(1));
        
            return dataLogic;
        }
    }
}