<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Trace="true"%>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title>...</title>
    </head>
    <body>
        <form id="form" runat="server">
            <div>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <br />
                <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
                <br />
                <asp:Button ID="ButtonAuthorization" runat="server" Text="Авторизация" OnClick="ButtonAuthorization_Click"/>
                <br />
                <br />
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                <br />
                <asp:TextBox ID="TextBox4" runat="server" TextMode="Password"></asp:TextBox>
                <br />
                <asp:TextBox ID="TextBox5" runat="server" TextMode="Password"></asp:TextBox>
                <br />
                <asp:Button ID="ButtonRegistration" runat="server" Text="Регистрация" OnClick="ButtonRegistration_Click" />
                <br />
                <br />
                <recaptcha:RecaptchaControl
                    ID="recaptcha"
                    runat="server"
                    PublicKey="6LdsehkUAAAAAGXOrhtYuPKl5gFqYOf6HLoEonvw"
                    PrivateKey="6LdsehkUAAAAAND-pdU5fMYPuTEcNxeoyMlcVHOB"
                    Theme="clean"
                    Lang="ru"
                />
                <br />
            </div>
        </form>
    </body>
</html>
