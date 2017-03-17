using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

public enum Role { Guest, Participant }
public class Account
{
    public User User { private set; get; }
    public Database Database { private set; get; }

    public Account()
    {
        Database = new Database();
    }

    public bool Authorization(User user)
    {
        if (!Database.ExistsUser(user.Login, user.Password)) return false;

        //вытащить данные из бд
        User = Database.GetUser(user.Login, user.Password);
        return true;
    }

    public void SetUser(string keySession)
    {
        User = Database.GetUser(keySession);
    }

    public bool Registration(User user)
    {
        if (!Database.AddUser(user)) return false;
        return Authorization(user);
    }

    public void DeleteAccount()
    {
        Database.DeleteUser(User);
        User = null;
    }
}