<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="CambioPassword.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.CambioPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5 d-flex justify-content-center">
        <div class="card shadow-lg" style="width: 100%; max-width: 400px; border-radius: 15px; background-color: #6096B4; color: white;">
            <div class="card-body">
                <h2 class="text-center mb-4" style="font-size: 24px;">Gestión de Cambio de Contraseña</h2>
                <hr />
               <asp:Panel id="pnlForm" runat="server">
                <div class="form-group">
                    <label for="txtContraseñaActual" class="form-label" style="color: white;">Ingresa tu contraseña:</label>
                    <asp:TextBox runat="server" ID="txtContraseñaActual" TextMode="Password" CssClass="form-control mb-4" />
                </div>

                <div class="form-group">
                    <label for="txtNuevaContraseña" class="form-label" style="color: white;">Ingresa nueva contraseña:</label>
                    <asp:TextBox runat="server" ID="txtNuevaContraseña" TextMode="Password" CssClass="form-control mb-4" />
                </div>
                <div class="form-group text-center">
                    <asp:Button runat="server" ID="btnCambiarContraseña" Text="Cambiar Contraseña" CssClass="btn btn-light btn-block" style="font-weight: 700; background-color: #EEF7FF; color: black; border-radius: 50px; padding: 10px 25px; transition: all 0.3s ease;" OnClick="btnCambiarContraseña_Click" />
                </div>

               </asp:Panel>
                <div class="form-group text-center">
                    <asp:Label ID="lblMensaje" CssClass="form-label" Text="" ForeColor="White" Font-Size="Medium" runat="server" />
                </div>

            </div>
        </div>
    </div>
</asp:Content>
