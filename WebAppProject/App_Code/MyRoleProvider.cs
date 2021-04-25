using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebAppProject.Models;

namespace WebAppProject.App_Code
{
    public class MyRoleProvider : RoleProvider
    {
        WebAppProjectContext db ;
        public MyRoleProvider()
        {
            db = new WebAppProjectContext();
        }
        public override string ApplicationName { get; set; }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            var lookingRoleForUser = db.AllCustomers.FirstOrDefault(a => a.Email == username);
            var getRoleForEmploee = db.AllEmploee.Where(a => a.Email == username);
            if (lookingRoleForUser != null)
            {
                var getRoleForUser = db.AllCustomers.Where(a => a.Email == username);
                return getRoleForUser.Select(a => a.Role.RoleName).ToArray();
            }
            return getRoleForEmploee.Select(a => a.Role.RoleName).ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}