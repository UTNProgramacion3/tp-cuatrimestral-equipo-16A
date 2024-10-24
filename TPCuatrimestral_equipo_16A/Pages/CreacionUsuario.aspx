<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="CreacionUsuario.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.CreacionUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h2>Crear Usuario</h2>
        <asp:Panel ID="PanelCrearUsuario" runat="server">
            <div class="mb-3">
                <label for="ddlRol" class="form-label">Rol</label>
                <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-select">
                    <asp:ListItem Value="Admin">Administrador</asp:ListItem>
                    <asp:ListItem Value="Medico">Médico</asp:ListItem>
                    <asp:ListItem Value="Paciente">Paciente</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtApellido" class="form-label">Apellido</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtDocumento" class="form-label">Documento</label>
                <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtDireccion" class="form-label">Dirección</label>
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtMatricula" class="form-label">Matrícula</label>
                <asp:TextBox ID="txtMatricula" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="ddlEspecialidad" class="form-label">Especialidad</label>
                <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select">
                    <asp:ListItem Value="1">Especialidad 1</asp:ListItem>
                    <asp:ListItem Value="2">Especialidad 2</asp:ListItem>
                    <asp:ListItem Value="3">Especialidad 3</asp:ListItem>
                </asp:DropDownList>
            </div>

            <asp:Button ID="btnCrear" runat="server" Text="Crear Usuario" CssClass="btn btn-primary" OnClick="btnCrear_Click" />
        </asp:Panel>
    </div>
</asp:Content>
