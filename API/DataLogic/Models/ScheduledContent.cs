namespace API.DataLogic.Models
{
    using System;
    using System.Collections.Generic;

    public class ScheduledContent
    {
        public string Location { get; set; }

        public List<Content> Content { get; set; }
    }
}