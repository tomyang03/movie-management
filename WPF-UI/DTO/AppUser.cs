using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_UI.DTO
{
    public class AppUser
    {
        int userId;
        int roleId;
        string username;
        string fullname;
        string password;

        public AppUser() { }

        public AppUser(int userId, int roleId, string username, string fullname)
        {
            this.userId = userId;
            this.roleId = roleId;
            this.username = username;
            this.fullname = fullname;
        }

        public AppUser(int userId, int roleId, string username, string fullname, string password)
        {
            this.userId = userId;
            this.roleId = roleId;
            this.username = username;
            this.fullname = fullname;
            this.password = password;
        }

        public AppUser(int roleId, string username, string fullname, string password)
        {
            //this.userId = userId;
            this.roleId = roleId;
            this.username = username;
            this.fullname = fullname;
            this.password = password;
        }

        public int UserId
        {
            get
            {
                return this.userId;
            }
            set
            {
                this.userId = value;
            }
        }
        public int RoleId
        {
            get
            {
                return this.roleId;
            }
            set
            {
                this.roleId = value;
            }
        }
        public String Username
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
            }
        }
        public String Fullname
        {
            get
            {
                return this.fullname;
            }
            set
            {
                this.fullname = value;
            }
        }
        public String Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }


        public override string ToString()
        {
            //return base.ToString();
            return $"[userId: {this.userId}, roleId: {this.roleId}, username: {this.username}, fullname: {this.fullname}, password: {this.password}] ";

        }
    }
}
