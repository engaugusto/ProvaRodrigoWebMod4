<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox runat="server" ID="txtLogin"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtPass"></asp:TextBox>
        <asp:Button runat="server" ID="btnLogin" OnClick="btnLogin_Click" Text="Logar"/>
    </div>
    </form>
</body>
</html>
