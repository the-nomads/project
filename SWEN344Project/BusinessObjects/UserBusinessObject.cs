using SWEN344Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEN344Project.BusinessInterfaces
{
    public class UserBusinessObject : IUserBusinessObject
    {
        public User GetOrCreateUser(string FacebookID)
        {
            using (var ctx = new CaveWallContext())
            {
                User u = ctx.Users.FirstOrDefault(x => x.FacebookID == FacebookID);
                if (u == null)
                {
                    u = new User();
                    u.FacebookID = FacebookID;
                    ctx.Users.Add(u);
                    ctx.SaveChanges();
                }
                return u;
            }
        }
    }
}
