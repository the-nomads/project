using SWEN344Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEN344Project.BusinessInterfaces
{
    public interface IEventBusinessObject
    {
        List<Event> GetEventsForUser(User user);
        void CreateNewEvent(User user, Event toCreate);
    }
}
