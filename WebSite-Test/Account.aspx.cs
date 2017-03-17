using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Account : System.Web.UI.Page
{
    private string Guid { set; get; }

    private _Default DefPage { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (PreviousPage == null) Response.Redirect("Default.aspx");

        DefPage = PreviousPage as _Default;

        this.Title = Guid = System.Guid.NewGuid().ToString();

        Response.Write($"Id: {DefPage.Account.User.Id} <br />" +
                       $"Login: {DefPage.Account.User.Login} <br />" +
                       $"Password: {DefPage.Account.User.Password} <br />" +
                       $"Salt: {DefPage.Account.User.Salt} <br />");

        DefPage.Account.Database.DeleteSession(DefPage.Account.User.Id);
        DeleteCookie();

        CreateSession();
    }

    private void CreateSession()
    {
        DefPage.Account.Database.AddSession(new Session
        {
            Id = DefPage.Account.User.Id,
            KeySession = Guid
        });

        CreateCookie();
    }

    private void CreateCookie()
    {
        Response.Cookies.Add(new HttpCookie("localhost")
        {
            Expires = DateTime.Now.AddMinutes(1),
            ["KeySession"] = Guid
        });
    }

    /// <summary>
    /// ВЫйти
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        DefPage.Account.Database.DeleteSession(DefPage.Account.User.Id);
        DeleteCookie();

        Response.Redirect($"Default.aspx?err=true");
    }

    /// <summary>
    /// Удалить акк
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {
        DefPage.Account.DeleteAccount();
        DeleteCookie();

        Response.Redirect($"Default.aspx?err=true");
    }

    private void DeleteCookie()
    {
        if (Request.Cookies["localhost"] != null)
        {
            Response.Cookies.Add(new HttpCookie("localhost") { Expires = DateTime.Now.AddDays(-1d) });
        }
    }
}