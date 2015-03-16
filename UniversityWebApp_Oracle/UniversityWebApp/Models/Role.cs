using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using UniversityWebApp.Repository;
using UniversityWebApp.Repository.Gateway;

namespace UniversityWebApp.Models
{
    public class Role : RoleProvider
    {
        private UserGateway aUserGateway=new UserGateway();
        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string userId)
        {
            int id = Convert.ToInt16(userId);
            var user = aUserGateway.GetById(id);
            string[] roles = { user.UserType };
            return roles;

        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}