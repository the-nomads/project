using SWEN344Project.Helpers;
using SWEN344Project.Models;
using SWEN344Project.Models.PersistentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEN344Project.BusinessInterfaces
{
    public class UserBusinessObject : IUserBusinessObject
    {
        private readonly IPersistenceBusinessObject _pbo;
        public UserBusinessObject(IPersistenceBusinessObject pbo)
        {
            this._pbo = pbo;
        }

        public User GetOrCreateUser(string FacebookID)
        {
            var u = this._pbo.Users.All.FirstOrDefault(x => x.FacebookID == FacebookID);
            if (u == null)
            {
                u = new User();
                u.FacebookID = FacebookID;
                this._pbo.Users.AddEntity(u);
                this._pbo.SaveChanges(); // Gives the user a UserID, so we can use it below

                var uf = new UserFinance();
                uf.UserID = u.UserID;
                uf.Amount = Constants.MoneyUsersStartWith;
                uf.Currency = Constants.Currency.USD;
                this._pbo.UserFinances.AddEntity(uf);
                this._pbo.SaveChanges();
            }
            return u;
        }
    }
}
