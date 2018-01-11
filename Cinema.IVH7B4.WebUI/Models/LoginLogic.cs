using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cinema.IVH7B4.Domain.Entities;

namespace Cinema.IVH7B4.WebUI.Models
{
    public class LoginLogic
    {
        // Check the login credentials that have been inserted with data in the database for a match
        public static bool CheckCashRegisterLogin(string username, string password, IEnumerable<CashRegisterLogin> logins)
        {
            IEnumerable<CashRegisterLogin> findAccount = logins.Where(l => l.Username == username);
            if (findAccount.ToList().Count < 1)
            {
                return false;
            }
            else
            {
                List<CashRegisterLogin> foundAccount = findAccount.ToList();

                if (foundAccount[0].Password == password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool CheckManagerLogin(string username, string password, IEnumerable<ManagerLogin> logins)
        {
            IEnumerable<ManagerLogin> findAccount = logins.Where(l => l.Username == username);
            if (findAccount.ToList().Count < 1)
            {
                return false;
            }
            else
            {
                List<ManagerLogin> foundAccount = findAccount.ToList();

                if (foundAccount[0].Password == password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool CheckBackOfficeLogin(string username, string password, IEnumerable<BackOfficeLogin> logins)
        {
            IEnumerable<BackOfficeLogin> findAccount = logins.Where(l => l.Username == username);
            if (findAccount.ToList().Count < 1)
            {
                return false;
            }
            else
            {
                List<BackOfficeLogin> foundAccount = findAccount.ToList();

                if (foundAccount[0].Password == password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


    }
}