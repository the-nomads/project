using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWEN344Project.BusinessInterfaces;
using SWEN344Project.Tests.Helpers;

namespace SWEN344Project.Tests.UnitTests
{
    [TestClass]
    public class EventTests
    {
        [TestMethod]
        public void Event_Test_GetEventsForUser()
        {
            this.SetupTest();
            var evt = new Models.PersistentModels.Event
            {
                EventName = "Test Event",
                EventIsAllDay = true,
                EventStartDate = DateTime.Now,
                IsDeleted = false,
                UserID = tud.u1.UserID
            };
            this.ebo.CreateNewEvent(tud.u1, evt);
            var evts = this.ebo.GetEventsForUser(tud.u1);
            Assert.AreEqual(evts.Count, 1);
            var evt2 = evts[0];
            Assert.AreEqual(evt, evt2);
        }

        [TestMethod]
        public void Event_Test_CreateNewEvent()
        {
            this.SetupTest();
            var evt = new Models.PersistentModels.Event
            {
                EventName = "Test Event",
                EventIsAllDay = true,
                EventStartDate = DateTime.Now,
                IsDeleted = false,
                UserID = tud.u1.UserID
            };
            this.ebo.CreateNewEvent(tud.u1, evt);
            var evts = this.ebo.GetEventsForUser(tud.u1);
            Assert.AreEqual(evts.Count, 1);
            var evt2 = evts[0];
            Assert.AreEqual(evt, evt2);
        }

        [TestMethod]
        public void Event_Test_EditEvent()
        {
            this.SetupTest();
            var evt = new Models.PersistentModels.Event
            {
                EventName = "Test Event",
                EventIsAllDay = true,
                EventStartDate = DateTime.Now,
                IsDeleted = false,
                UserID = tud.u1.UserID,
                EventID = 1,
            };
            this.ebo.CreateNewEvent(tud.u1, evt);
            evt.EventName = "EDITED";
            evt.EventStartDate = new DateTime(2033, 5, 5);
            this.ebo.EditEvent(1, evt);

            var evts = this.ebo.GetEventsForUser(tud.u1);
            var evt2 = evts[0];
            Assert.AreEqual(evt2.EventName, "EDITED");
            Assert.AreEqual(evt2.EventStartDate, new DateTime(2033, 5, 5));
        }

        [TestMethod]
        public void Event_Test_GetEvent()
        {
            this.SetupTest();
            var evt = new Models.PersistentModels.Event
            {
                EventName = "Test Event",
                EventIsAllDay = true,
                EventStartDate = DateTime.Now,
                IsDeleted = false,
                UserID = tud.u1.UserID,
                EventID = 1,
            };
            this.ebo.CreateNewEvent(tud.u1, evt);

            var evt2 = this.ebo.GetEvent(evt.EventID);
            Assert.AreEqual(evt, evt2);
        }

        [TestMethod]
        public void Event_Test_DeleteEvent()
        {
            this.SetupTest();
            var evt = new Models.PersistentModels.Event
            {
                EventName = "Test Event",
                EventIsAllDay = true,
                EventStartDate = DateTime.Now,
                IsDeleted = false,
                UserID = tud.u1.UserID,
                EventID = 1,
            };
            this.ebo.CreateNewEvent(tud.u1, evt);
            this.ebo.DeleteEvent(evt.EventID);

            var evts = this.ebo.GetEventsForUser(tud.u1);
            Assert.AreEqual(evts.Count, 0);
        }

        private EventBusinessObject ebo;
        private TestUserData tud;
        private TestPersistenceObject pbo;
        private void SetupTest()
        {
            this.pbo = new TestPersistenceObject();
            this.ebo = new EventBusinessObject(pbo);
            this.tud = new TestUserData(pbo);
        }
    }
}
