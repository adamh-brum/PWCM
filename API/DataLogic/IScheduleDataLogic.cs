namespace API.DataLogic
{
    using API.DataLogic.ViewModels;
    using API.DataLogic.Models;
    using System.Collections.Generic;
    using System;

    public interface IScheduleDataLogic
    {
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