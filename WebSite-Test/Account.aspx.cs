using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Account : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (DataPage.Сondition != Сondition.Account) Redirect();

        Title = DataPage.Guid.ToString();

        Label1.Text = $"Id: {DataPage.Account.User.Id} <br />" +
                      $"Login: {DataPage.Account.User.Login} <br />" +
                      $"Password: {DataPage.Account.User.Password} <br />" +
                      $"Salt: {DataPage.Account.User.Salt} <br />";

        DataPage.Account.Database.DeleteSession(DataPage.Account.User.Id);
        DeleteCookie();

        CreateSession();
    }

    private void CreateSession()
    {
        DataPage.Account.Database.AddSession(new Session
        {
            Id = DataPage.Account.User.Id,
            KeySession = DataPage.Guid.ToString()
        });

        CreateCookie();
    }

    private void CreateCookie()
    {
        Response.Cookies.Add(new HttpCookie("localhost")
        {
            Expires = DateTime.Now.AddMinutes(1),
            ["KeySession"] = DataPage.Guid.ToString()
        });
    }

    protected void Exit_Click(object sender, EventArgs e)
    {
        DataPage.Account.Database.DeleteSession(DataPage.Account.User.Id);
        DeleteCookie();

        Redirect();
    }

    protected void DeleteAccount_Click(object sender, EventArgs e)
    {
        DataPage.Account.DeleteAccount();
        DeleteCookie();

        Redirect();
    }

    private void DeleteCookie()
    {
        if (Request.Cookies["localhost"] != null)
        {
            Response.Cookies.Add(new HttpCookie("localhost") { Expires = DateTime.Now.AddDays(-1d) });
        }
    }

    protected void MetaData_Click(object sender, EventArgs e)
    {
        Label1.Text = $"Id: {DataPage.Account.User.Id} <br />" +
                      $"Login: {DataPage.Account.User.Login} <br />" +
                      $"Password: {DataPage.Account.User.Password} <br />" +
                      $"Salt: {DataPage.Account.User.Salt} <br />" +
                      $"Metadata: <br /> {DPAPI.Decrypt(DataPage.Account.User.Metadata)} <br />";

    }

    private void Redirect()
    {
        DataPage.Сondition = Сondition.Default;
        Response.Redirect("http://localhost:57676/Default.aspx", true);
    }
} 