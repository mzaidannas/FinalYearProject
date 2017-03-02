using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalYearProject.Models
{
    public interface IUser
    {
        bool Exists(Login user);
        bool Athenticate(Login user);
        bool isDoctor(Login user);
        void AddUser(Login user);
        void DeleteUser(Login user);
        List<Login> GetLogins();
        int GetPersonID(string userName, string password);
        string GetPassword(string email);
        void RegisterDoctor(MainPerson person);
    }
}
