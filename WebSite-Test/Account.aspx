<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Account.aspx.cs" Inherits="_Account" %>
<%@ Reference Page="~/Default.aspx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>...</title>
</head>
<body>

    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
    
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Удалить аккаунт" />
&nbsp;<asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Показать метаданные" />
&nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Выйти" />
&nbsp;</div>
    </form>
</body>
</html>
