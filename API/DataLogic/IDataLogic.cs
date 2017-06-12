namespace API.DataLogic
{
    using API.DataLogic.ViewModels;
    using API.DataLogic.Models;
    using System.Collections.Generic;
    using System;

    public interface IDataLogic
    {
        /// <summary>
        /// Saves content to the DB
        /// </summary>
        /// <param name="title">The title for the content</param>
        /// <param name="content">Content</param>
        /// <returns>Content ID</returns>
        int AddContent(string title, string content);

        /// <summary>
        /// Returns the content that matches the given ID
        /// </summary>
        /// <param name="contentId">Content ID</param>
        /// <returns>Content data model</returns>
        Content GetContent(int contentId);

        /// <summary>
        /// Adds a beacon 
        /// </summary>
        /// <param name="beaconId">Beacon ID from the new beacon</param>
        /// <param name="friendlyName">The friendly name users will view when they manage beacons</param>
        /// <param name="location">Defines the location</param>
        void AddBeacon(Guid beaconId, string beaconName, string friendlyName, string location);

        /// <summary>
        /// Returns a beacon based on the beacon ID
        /// </summary>
        /// <param name="beaconId"></param>
        /// <returns></returns>
        Beacon GetBeacon(Guid beaconId);

        /// <summary>
        /// Returns all beacons registered in the system
        /// </summary>
        /// <returns>All beacons</returns>
        List<Beacon> GetBeacons();

        /// <summary>
        /// Schedules content to be displayed by beacons
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="beaconIds"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        void ScheduleContent(int contentId, Guid[] beaconIds, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Returns content currently scheduled for a given beacon
        /// </summary>
        /// <param name="beaconId"></param>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        ViewModels.ScheduledContent GetScheduledContent(Guid beaconId, DateTime currentTime);
    }
}