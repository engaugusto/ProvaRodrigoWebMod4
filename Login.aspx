<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" MasterPageFile="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
    </div>
</asp:Content>
