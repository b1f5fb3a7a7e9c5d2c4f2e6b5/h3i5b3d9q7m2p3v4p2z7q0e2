<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Account.aspx.cs" Inherits="_Account" Trace="true"%>
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
    
        <asp:Button ID="ButtonDeleteAccount" runat="server" OnClick="DeleteAccount_Click" Text="Удалить аккаунт" />
&nbsp;<asp:Button ID="ButtonMetaData" runat="server" OnClick="MetaData_Click" Text="Показать метаданные" />
&nbsp;<asp:Button ID="ButtonExit" runat="server" OnClick="Exit_Click" Text="Выйти" />
&nbsp;</div>
    </form>
     <br />
</body>
</html>
