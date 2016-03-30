using SWEN344Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEN344Project.BusinessInterfaces
{
    public class EventBusinessObject : IEventBusinessObject
    {
        public List<Event> GetEventsForUser(User user)
        {
            using (var ctx = new CaveWallContext())
            {
                var events = ctx.Events.Where(x => x.UserID == user.UserID).ToList();
                return events;
            }
        }
    }
}
