using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

public class Database
{
    private readonly DataContext _db;

    public Database()
    {
        _db = new DataContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
    }

    public bool AddUser(User user)
    {
        if (ExistsUser(user.Login)) return false;

        _db.GetTable<User>().InsertOnSubmit(user);
        _db.SubmitChanges();
        return true;
    }

    public bool AddSession(Session session)
    {
        if (!Checking(session)) return false;

        DeleteSession(session.Id);

        _db.GetTable<Session>().InsertOnSubmit(session);
        _db.SubmitChanges();
        return true;
    }

    public User GetUser(string login, string password)
    {
        return (from user in _db.GetTable<User>().ToArray()
            where user.Login == login && user.Password == password
            select user).Single();
    }

    public User GetUser(string keySession)
    {
        return (from user in _db.GetTable<User>().ToArray()
                join session in _db.GetTable<Session>().ToArray()
                on user.Id equals session.Id
                where session.KeySession == keySession
                select user).Single();
    }

    public string GetSalt(string login)
    {
        return (from user in _db.GetTable<User>().ToArray()
                where user.Login == login
                select user).Single().Salt;
    }

    private Session GetSession(int id)
    {
        return (from session in _db.GetTable<Session>().ToArray()
                where session.Id == id
                select session).Single();
    }

    public bool DeleteUser(User user)
    {
        if (!ExistsUser(user.Login)) return false;

        DeleteSession(user.Id);

        _db.GetTable<User>().DeleteOnSubmit(user);
        _db.SubmitChanges();
        return true;
    }

    public bool DeleteSession(int id)
    {
        if (!ExistsSession(id)) return false;

        _db.GetTable<Session>().DeleteOnSubmit(GetSession(id));
        _db.SubmitChanges();
        return true;
    }

    public bool ExistsUser(string login, string password = null)
    {
        return (password == null) 
            ? _db.GetTable<User>().ToArray().Any(
                any => any.Login == login) 
            : _db.GetTable<User>().ToArray().Any(
                any => any.Login == login && any.Password == password);
    }

    public bool ExistsSession(string keyString)
    {
        return _db.GetTable<Session>().ToArray().Any(
            any => any.KeySession == keyString);
    }

    public bool ExistsSession(int id)
    {
        return _db.GetTable<Session>().ToArray().Any(
            any => any.Id == id);
    }

    private bool Checking(Session session)
    {
        return (_db.GetTable<Session>().Count() < 1000);
    }
}