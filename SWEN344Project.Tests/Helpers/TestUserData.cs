using SWEN344Project.BusinessInterfaces;
using SWEN344Project.Models.PersistentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEN344Project.Tests.Helpers
{
    public class TestUserData
    {
        public TestUserData(TestPersistenceObject pbo)
        {
            pbo.Users.AddEntity(u1);
            pbo.Users.AddEntity(u2);
            pbo.Users.AddEntity(u3);
            pbo.Users.AddEntity(u4);
            pbo.SaveChanges();
        }

        public User u1 = new Models.PersistentModels.User { UserID = 1, FacebookID = "1" };
        public User u2 = new Models.PersistentModels.User { UserID = 2, FacebookID = "2" };
        public User u3 = new Models.PersistentModels.User { UserID = 3, FacebookID = "3" };
        public User u4 = new Models.PersistentModels.User { UserID = 4, FacebookID = "4" };
    }
}
