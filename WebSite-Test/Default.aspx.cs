using System;
using System.Text;
using System.Security.Cryptography;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ExistsCookie()) RequestCookies();
    }

    protected void ButtonRegistration_Click(object sender, EventArgs e)
    {
        //if (!Page.IsValid) return; //block recaptcha

        if (string.IsNullOrWhiteSpace(TextBox3.Text) ||
            string.IsNullOrWhiteSpace(TextBox4.Text) ||
            TextBox4.Text != TextBox5.Text) return;

        var guid = Guid.NewGuid().ToString();

        if (!DataPage.Account.Registration(new User
        {
            Login = TextBox3.Text,
            Password = Encoding.UTF8.GetString(SHA256.Create().ComputeHash(
                Encoding.UTF8.GetBytes(TextBox4.Text + guid))),
            Salt = guid,
            Metadata = DPAPI.Encrypt(PsApiWrapper.GetPerformanceInfo().GetAll())
        })) return;

        Redirect();
    }

    protected void ButtonAuthorization_Click(object sender, EventArgs e)
    {
        //if (!Page.IsValid) return; //block recaptcha

        if (!DataPage.Account.Authorization(new User{
            Login = TextBox1.Text,
            Password = Encoding.UTF8.GetString(SHA256.Create().ComputeHash(
                Encoding.UTF8.GetBytes(TextBox2.Text + DataPage.Account.Database.GetSalt(TextBox1.Text))))
        })) return;

        Redirect();
    }

    private bool ExistsCookie()
    {
        var cookie = Request.Cookies["localhost"];

        var keySession = cookie?["KeySession"];

        return keySession != null && DataPage.Account.Database.ExistsSession(keySession);
    }

    private void RequestCookies()
    {
        var cookie = Request.Cookies["localhost"];
        if (cookie == null) return;

        DataPage.Account.SetUser(cookie["KeySession"]);

        Redirect();
    }

    private void Redirect()
    {
        DataPage.Сondition = Сondition.Account;
        Response.Redirect("http://localhost:57676/Account.aspx", true);
    }
}