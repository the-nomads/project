using SWEN344Project.Models;
using SWEN344Project.Models.PersistentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEN344Project.BusinessInterfaces
{
    public interface IEventBusinessObject
    {
        /// <summary>
        /// Gets all events for a given user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        List<Event> GetEventsForUser(User user);

        /// <summary>
        /// Creates a new event for a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="toCreate"></param>
        void CreateNewEvent(User user, Event toCreate);

        /// <summary>
        /// Edits a given event
        /// <para>Does not check if the event belongs to the current user</para>
        /// </summary>
        /// <param name="eventID">EventID of the event to update</param>
        /// <param name="toUpdate">new updated event. EventID is not required</param>
        void EditEvent(int eventID, Event toUpdate);

        /// <summary>
        /// Gets a particular event
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        Event GetEvent(int eventID);

        /// <summary>
        /// Deletes an event
        /// <para>Does not check if the event belongs to the current user</para>
        /// </summary>
        /// <param name="eventID"></param>
        void DeleteEvent(int eventID);
    }
}
