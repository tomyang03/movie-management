using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_UI.DTO;

namespace WPF_UI.Service
{
    interface IAppUserService
    {
        bool ValidUser(string username, string password);

        AppUser ExistUser(string username, string password);
    }
}
