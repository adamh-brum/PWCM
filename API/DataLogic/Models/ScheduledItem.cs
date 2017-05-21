namespace API.DataLogic.Models
{
    using System;

    public class ScheduledItem
    {
        public Guid ContentId { get; set; }

        public Guid BeaconId { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }
    }
}