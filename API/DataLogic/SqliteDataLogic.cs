namespace API.DataLogic
{
    using API.DataLogic.Models;
    using System.Linq;
    using System.Collections.Generic;
    using System;

    public class SqliteDataLogic : IDataLogic
    {
        public SqliteDataLogic()
        {

        }

        /// <summary>
        /// Saves content to the DB
        /// </summary>
        /// <param name="title">The title for the content</param>
        /// <param name="content">Content</param>
        /// <returns>Content ID</returns>
        public int AddContent(string title, string content)
        {
            Content newObject = new Content()
            {
                Title = title,
                Value = content
            };

            using (var db = new ApplicationDbContext())
            {
                db.Content.Add(newObject);
                db.SaveChanges();
            }

            return newObject.Id;
        }

        /// <summary>
        /// Returns the content that matches the given ID
        /// </summary>
        /// <param name="contentId">Content ID</param>
        /// <returns>Content data model</returns>
        public Content GetContent(int contentId)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Content.FirstOrDefault(c => c.Id == contentId);
            }
        }

        /// <summary>
        /// Adds a beacon 
        /// </summary>
        /// <param name="beaconId">Beacon ID from the new beacon</param>
        /// <param name="friendlyName">The friendly name users will view when they manage beacons</param>
        /// <param name="location">Defines the location</param>
        public void AddBeacon(Guid beaconId, string beaconName, string friendlyName, string location)
        {
            var newObject = new Beacon()
            {
                Id = beaconId,
                Name = beaconName,
                FriendlyName = friendlyName,
                Location = location
            };

            using (var db = new ApplicationDbContext())
            {
                db.Beacons.Add(newObject);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a beacon by the given UUID
        /// </summary>
        /// <param name="beaconUUID">UUID</param>
        public void DeleteBeacon(Guid beaconUUID)
        {
            using (var db = new ApplicationDbContext())
            {
                var objectToDelete = db.Beacons.FirstOrDefault(beacon => beacon.Id == beaconUUID);

                if (objectToDelete != null)
                {
                    db.Beacons.Remove(objectToDelete);
                }

                db.SaveChanges();
            }
        }

        /// <summary>
        /// Returns a beacon based on the beacon ID
        /// </summary>
        /// <param name="beaconId"></param>
        /// <returns></returns>
        public Beacon GetBeacon(Guid beaconId)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Beacons.FirstOrDefault(b => b.Id == beaconId);
            }
        }

        /// <summary>
        /// Returns all beacons registered in the system
        /// </summary>
        /// <returns>All beacons</returns>
        public List<Beacon> GetBeacons()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Beacons.ToList();
            }
        }

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

        public IEnumerable<Rating> GetRatings()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Ratings;
            }
        }

        public void UpdateRating(int contentId, int rating)
        {
            using (var db = new ApplicationDbContext())
            {
                var contentRating = db.Ratings.FirstOrDefault(r => r.ContentId == contentId);
                if (contentRating == null)
                {
                    contentRating = new Rating()
                    {
                        ContentId = contentId
                    };

                    db.Ratings.Add(contentRating);
                }

                // Now update the content rating
                contentRating.RatingCount += rating;

                // Save the changes
                db.SaveChanges();
            }
        }

        public IEnumerable<string> GetMetadata(string key)
        {
            List<string> metadataValues = new List<string>();

            using (var db = new ApplicationDbContext())
            {
                metadataValues = db.Metadata.Where(m => m.Key == key)?.Select(m => m.Value).ToList();
            }

            return metadataValues;
        }

        public void AddMetadata(string key, string value)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Metadata.Add(new Metadata()
                {
                    Key = key,
                    Value = value
                });

                db.SaveChanges();
            }
        }
    }
}