using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWEN344Project.Models
{
    public class Event
    {

        [Key]
        public int EventID { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public string EventName { get; set; }
        public bool EventIsAllDay { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }

        public bool IsDeleted { get; set; }

        /// <summary>
        /// Updates this event to what the other event is
        /// <para>Ignores EventID and UserID fields</para>
        /// </summary>
        /// <param name="otherEvent"></param>
        public void UpdateFields(Event otherEvent)
        {
            this.EventName = otherEvent.EventName;
            this.EventIsAllDay = otherEvent.EventIsAllDay;
            this.EventStartDate = otherEvent.EventStartDate;
            this.EventEndDate = otherEvent.EventEndDate;
        }
    }
}