using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_UI.DataAccess;
using WPF_UI.DTO;

namespace WPF_UI.Service
{
    class AppUserService : IAppUserService
    {
        private DataAccessLayer databaseConnection;
        private string dbHost;
        private string dbUser;
        private string dbPassw;
        private string dbName;

        public AppUserService()
        {
            this.dbHost = "localhost";
            this.dbUser = "root";
            this.dbPassw = "Breneli23";
            this.dbName = "moviedb";
        }

        public bool ValidUser(string username, string password)
        {
            Console.WriteLine("<ValidUser> username: {0}", username);
            Console.WriteLine("<ValidUser> password: {0} ", password);
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                /* TEMPORARY code to simulate DB query */

                databaseConnection = new DataAccessLayer(dbHost, dbUser, dbPassw, dbName);
                AppUser user = new AppUser();
                user.Username = username;
                user.Password = password;

                user = DataAccessLayer.existUser(databaseConnection.Connstring, databaseConnection.Conn, user );

                if(user.UserId>0 && user.Fullname!=null)
                {
                    Console.WriteLine("<ValidUser> User in DB: {0}", user.Fullname);
                    return true;
                }
                else
                {
                    Console.WriteLine("<ValidUser>  User not found in DB: {0}", user.Username);
                    return false;
                }
            }
            else
            {
                /* Additional validation for null operation */
                return false;
            }
        }

        public AppUser ExistUser(string username, string password)
        {
            AppUser user = new AppUser();
            Console.WriteLine("<ValidUser> username: {0}", username);
            Console.WriteLine("<ValidUser> password: {0} ", password);
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                /* TEMPORARY code to simulate DB query */

                databaseConnection = new DataAccessLayer(dbHost, dbUser, dbPassw, dbName);
                user.Username = username;
                user.Password = password;

                user = DataAccessLayer.existUser(databaseConnection.Connstring, databaseConnection.Conn, user);

                if (user.UserId > 0 && user.Fullname != null)
                {
                    Console.WriteLine("<ValidUser> User in DB: {0}", user.Fullname);
                    return user;
                }
                else
                {
                    Console.WriteLine("<ValidUser>  User not found in DB: {0}", user.Username);
                    return user;
                }
            }
            else
            {
                /* Additional validation for null operation */
                return null;
            }
        }
    }
}
