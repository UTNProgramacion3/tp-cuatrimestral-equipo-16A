<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Stylesheets/Login.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/login.js" type="text/javascript"></script>
    <script type="text/javascript">
        var txtUsernameID = '<%= txtEmail.ClientID %>';
        var txtPasswordID = '<%= txtPassword.ClientID %>';
        var btnLoginID = '<%= btnLogin.ClientID %>';
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="login-container">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="card-login">
                    <h2 class="login-title">Iniciar Sesión</h2>

                    <asp:Label ID="lblEmail" runat="server" Text="Ingresa tu nombre de usuario." CssClass="label"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" oninput="toggleFields()"></asp:TextBox>

                    <asp:Label ID="lblPassword" runat="server" Text="Ingresa tu password" CssClass="label"></asp:Label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="textbox" TextMode="Password" Enabled="false" oninput="toggleFields()"></asp:TextBox>

                    <asp:Button ID="btnLogin" CssClass="login-btn" Text="Ingresar!" runat="server" Enabled="false" OnClick="btnLogin_Click"/>
                    <asp:Label Text="" runat="server" ID="lblMessage" />
                    <asp:Label ID="lblRegistrarse" runat="server" Text="No tienes una cuenta?" CssClass="label"></asp:Label>
                    <a href="#" class="reg-link">Registrate</a>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
