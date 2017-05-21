namespace API.DataLogic
{
    using API.DataLogic.Models;
    using System.Linq;
    using System.Collections.Generic;
    using System;

    public class InMemoryDataLogic : IDataLogic
    {
        private List<Content> contentList = new List<Content>();
        private List<Beacon> beaconList = new List<Beacon>();
        private List<ScheduledItem> schedule = new List<ScheduledItem>();

        /// <summary>
        /// Saves content to the DB
        /// </summary>
        /// <param name="title">The title for the content</param>
        /// <param name="content">Content</param>
        /// <returns>Content ID</returns>
        public Guid AddContent(string title, string content)
        {
            Guid id = Guid.NewGuid();

            this.contentList.Add(new Content()
            {
                Id = id,
                Title = title,
                Value = content
            });

            return id;
        }

        /// <summary>
        /// Returns the content that matches the given ID
        /// </summary>
        /// <param name="contentId">Content ID</param>
        /// <returns>Content data model</returns>
        public Content GetContent(Guid contentId)
        {
            return this.contentList.FirstOrDefault(c => c.Id == contentId);
        }

        /// <summary>
        /// Adds a beacon 
        /// </summary>
        /// <param name="beaconId">Beacon ID from the new beacon</param>
        /// <param name="friendlyName">The friendly name users will view when they manage beacons</param>
        /// <param name="location">Defines the location</param>
        public void AddBeacon(Guid beaconId, string beaconName, string friendlyName, string location)
        {
            this.beaconList.Add(new Beacon()
            {
                Id = beaconId,
                Name = beaconName,
                FriendlyName = friendlyName,
                Location = location
            });
        }

        /// <summary>
        /// Returns a beacon based on the beacon ID
        /// </summary>
        /// <param name="beaconId"></param>
        /// <returns></returns>
        public Beacon GetBeacon(Guid beaconId) 
        {
            return this.beaconList.FirstOrDefault(b => b.Id == beaconId);
        }

        /// <summary>
        /// Returns all beacons registered in the system
        /// </summary>
        /// <returns>All beacons</returns>
        public List<Beacon> GetBeacons()
        {
            return this.beaconList;
        }

        /// <summary>
        /// Schedules content to be displayed by beacons
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="beaconIds"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public void ScheduleContent(Guid contentId, Guid[] beaconIds, DateTime startDate, DateTime endDate)
        {
            foreach(var beaconId in beaconIds)
            {
                this.schedule.Add(new ScheduledItem()
                {
                    BeaconId = beaconId,
                    ContentId = contentId,
                    StartDateTime = startDate,
                    EndDateTime = endDate
                });
            }
        }

        /// <summary>
        /// Gets all the content for a given beacon at a givent time 
        /// </summary>
        /// <param name="beaconId">ID of the beacon</param>
        /// <param name="currentTime">Current time</param>
        /// <returns></returns>
        public ScheduledContent GetScheduledContent(Guid beaconId, DateTime currentTime)
        {
            var beacon = this.beaconList.FirstOrDefault(b => b.Id == beaconId);
            var contentIds = this.schedule.Where(s => s.BeaconId == beaconId && s.StartDateTime <= currentTime && s.EndDateTime >= currentTime)?.Select(s => s.ContentId);

            if(contentIds != null && beacon != null)
            {
                var content = this.contentList.Where(c => contentIds.Contains(c.Id))?.ToList();
                if(content != null)
                {
                    return new ScheduledContent()
                    {
                        Location = beacon.Location,
                        Content = content
                    };
                }
            }

            return null;
        }
    }
}