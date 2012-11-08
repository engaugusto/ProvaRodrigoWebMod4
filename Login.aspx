<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" MasterPageFile="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<<<<<<< HEAD
    <div class="divCentral">
        <div>
            Login do Sistema:
        </div>
        <div class="divCentralLogin">
            <div style="margin-left: 100px; padding-top: 50px">
                <table width="100%">
                    <tr>
                        <td>Login:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtLogin" runat="server" Width="300px" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Senha:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtSenha" runat="server" Width="300px" MaxLength="50" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>

                </table>
            </div>
            <div style="padding-top: 60px">

                <div align="right">
                    <asp:Button ID="btnEsqueciSenha" runat="server" Text="Esqueci Minha Senha" Width="150px" CssClass="divBotao"/>
                    &nbsp;
                    <asp:Button ID="btnLogin" runat="server" Text="Login" Width="120px" CssClass="divBotao"/>
                    </div>
            </div>
        </div>
=======
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    
    <form id="form1" runat="server">
    <div>
<<<<<<< HEAD
        <asp:TextBox runat="server" ID="txtLogin"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtPass"></asp:TextBox>
        <asp:Button runat="server" ID="btnLogin" OnClick="btnLogin_Click" Text="Logar"/>
=======
        Testing asfjahsofhnal
>>>>>>> 960b90c52da007860fd274d9469ecdb16133b49c
>>>>>>> c326718a6dbdca0d72352f06ea3ad45af9f5532c
    </div>
</asp:Content>
