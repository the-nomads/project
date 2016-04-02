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
        private readonly IPersistenceBusinessObject _pbo;
        public EventBusinessObject(IPersistenceBusinessObject pbo)
        {
            this._pbo = pbo;
        }

        public List<Event> GetEventsForUser(User user)
        {
            var events = this._pbo.ValidEvents.Where(x => x.UserID == user.UserID).ToList();
            return events;
        }

        public void CreateNewEvent(User user, Event toCreate)
        {
            toCreate.UserID = user.UserID;
            this._pbo.Events.AddEntity(toCreate);
            this._pbo.SaveChanges();
        }

        public void EditEvent(int eventID, Event toUpdate)
        {
            var e = this._pbo.Events.All.FirstOrDefault(x => x.EventID == eventID);
            e.UpdateFields(toUpdate);
            this._pbo.SaveChanges();
        }

        public Event GetEvent(int eventID)
        {
            var e = this._pbo.ValidEvents.FirstOrDefault(x => x.EventID == eventID);
            return e;
        }

        public void DeleteEvent(int eventID)
        {
            var e = this._pbo.ValidEvents.FirstOrDefault(x => x.EventID == eventID);
            if (e != null)
            {
                e.IsDeleted = true;
            }
        }
    }
}
