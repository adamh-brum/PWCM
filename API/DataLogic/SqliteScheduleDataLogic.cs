namespace API.DataLogic
{
    using API.DataLogic.Models;
    using System.Linq;
    using System.Collections.Generic;
    using System;

    public class SqliteScheduleDataLogic : IScheduleDataLogic
    {
        /// <summary>
        /// Schedules content to be displayed by beacons
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="beaconIds"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public void ScheduleContent(int contentId, Guid[] beaconIds, DateTime startDate, DateTime endDate)
        {
            using (var db = new ApplicationDbContext())
            {
                foreach (var beaconId in beaconIds)
                {
                    db.ScheduledItems.Add(new ScheduledItem()
                    {
                        BeaconId = beaconId,
                        ContentId = contentId,
                        StartDateTime = startDate,
                        EndDateTime = endDate
                    });
                }

                db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all the content for a given beacon at a givent time 
        /// </summary>
        /// <param name="beaconId">ID of the beacon</param>
        /// <param name="currentTime">Current time</param>
        /// <returns></returns>
        public ViewModels.ScheduledContent GetScheduledContent(Guid beaconId, DateTime currentTime)
        {
            using (var db = new ApplicationDbContext())
            {
                var beacon = db.Beacons.FirstOrDefault(b => b.Id == beaconId);
                var contentIds = db.ScheduledItems.Where(s => s.BeaconId == beaconId && s.StartDateTime <= currentTime && s.EndDateTime >= currentTime)?.Select(s => s.ContentId);

                if (contentIds != null && beacon != null)
                {
                    var content = db.Content.Where(c => contentIds.Contains(c.Id))?.ToList();
                    if (content != null)
                    {
                        return new ViewModels.ScheduledContent()
                        {
                            Location = beacon.Location,
                            Content = content
                        };
                    }
                }
            }

            return null;
        }
    }
}