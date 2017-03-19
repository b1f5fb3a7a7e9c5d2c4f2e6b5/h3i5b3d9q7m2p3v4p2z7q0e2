using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    public Account Account { private set; get; }
    protected void Page_Load(object sender, EventArgs e)
    {
        Account = new Account();

        if (Request.QueryString["err"] != null) return;

        if (ExistsCookie()) Redirects();
    }

    protected void ButtonRegistration_Click(object sender, EventArgs e)
    {
        //if (!Page.IsValid) return; //block recaptcha

        if (string.IsNullOrWhiteSpace(TextBox3.Text) ||
            string.IsNullOrWhiteSpace(TextBox4.Text) ||
            TextBox4.Text != TextBox5.Text) return;

        var guid = Guid.NewGuid().ToString();

        if (!Account.Registration(new User
        {
            Login = TextBox3.Text,
            Password = Encoding.UTF8.GetString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(TextBox4.Text + guid))),
            Salt = guid,
            Metadata = PsApiWrapper.GetPerformanceInfo().GetAll()
        })) return;

        Server.Transfer("Account.aspx", true);
    }

    protected void ButtonAuthorization_Click(object sender, EventArgs e)
    {
        //if (!Page.IsValid) return; //block recaptcha

        if (!Account.Authorization(new User{
            Login = TextBox1.Text,
            Password = Encoding.UTF8.GetString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(TextBox2.Text + Account.Database.GetSalt(TextBox1.Text))))
        })) return;

        Server.Transfer("Account.aspx", true);
    }

    public bool ExistsCookie()
    {
        var cookie = Request.Cookies["localhost"];

        var keySession = cookie?["KeySession"];

        return keySession != null && Account.Database.ExistsSession(keySession);
    }

    private void Redirects()
    {
        var cookie = Request.Cookies["localhost"];
        if (cookie == null) return;

        Account.SetUser(cookie["KeySession"]);

        Server.Transfer("Account.aspx", true);
    }
}