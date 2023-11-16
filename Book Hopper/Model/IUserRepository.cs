using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Book_Hopper.Model
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        void Add(UserModel userModel);
        void edit(UserModel userModel);
        void remove(int id);
        UserModel GetById(int id);
        UserModel GetBUsername(string username);
        IEnumerable<UserModel> GetByAll();
    }
}
