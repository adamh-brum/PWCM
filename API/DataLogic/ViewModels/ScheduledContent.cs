namespace API.DataLogic.ViewModels
{
    using System;
    using System.Collections.Generic;
    using API.DataLogic.Models;

    public class ScheduledContent
    {
        public string Location { get; set; }

        public List<Content> Content { get; set; }
    }
}