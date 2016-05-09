using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWEN344Project.BusinessInterfaces;
using SWEN344Project.Tests.Helpers;
using System.Linq;

namespace SWEN344Project.Tests.UnitTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void Test_User_GetOrCreateUser()
        {
            this.SetupTest();

            var existingUser = ubo.GetOrCreateUser("1");
            Assert.AreEqual(existingUser.UserID, 1);

            var nonExistingUser = ubo.GetOrCreateUser("ADFJERIUFNF");
            var lastUser = this.pbo.Users.All.Last();
            Assert.AreEqual(nonExistingUser.UserID, lastUser.UserID);
        }

        private UserBusinessObject ubo;
        private TestUserData tud;
        private TestPersistenceObject pbo;
        private void SetupTest()
        {
            this.pbo = new TestPersistenceObject();
            this.ubo = new UserBusinessObject(pbo);
            this.tud = new TestUserData(pbo);
        }
    }
}
