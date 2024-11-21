<%@ Page Title="Validar Email" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="ValidarEmail.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.ValidarEmail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function mostrarCampoContraseña() {
            document.getElementById('divContraseña').style.display = 'block';
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Validación de Correo Electrónico</h3>

    <asp:Label ID="lblMensaje" runat="server" Text="Por favor, valide su cuenta"></asp:Label>

    <div id="divContraseña" style="display:none;">
        <label for="txtNuevaContraseña">Nueva Contraseña:</label>
        <asp:TextBox ID="txtNuevaContraseña" runat="server" TextMode="Password" />
        <asp:Button ID="btnCambiarContraseña" runat="server" Text="Cambiar Contraseña" OnClick="btnCambiarContraseña_Click" />
    </div>
</asp:Content>
