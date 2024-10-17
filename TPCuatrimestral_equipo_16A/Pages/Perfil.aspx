<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Views.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row g-4 d-flex justify-content-center">
        <div class="col-md-2">
            <div class="mb-3">
                <label class="form-label">Usuario</label>
                <input type="text" class="form-control" id="user" value="" required runat="server">
            </div>
            <div class="mb-3">
                <label class="form-label">Contraseña</label>
                <input type="text" class="form-control" id="pass" required runat="server">
                <div class="form-check">
                    <asp:CheckBox class="form-check-input" type="checkbox" ID="passCheck" runat="server" AutoPostBack="True" OnCheckedChanged="ChkBoxChecked" />
                    <label class="form-check-label">Mostrar Contraseña</label>
                </div>
            </div>
            <div class="mb-3">
                <label class="form-label">Nombre</label>
                <input type="text" class="form-control" id="nombre" required runat="server">
            </div>
            <div class="mb-3">
                <label class="form-label">Apellido</label>
                <input type="text" class="form-control" id="apellido" required runat="server">
            </div>
            <div class="mb-3">
                <label class="form-label">Dni</label>
                <input type="text" class="form-control" id="dni" required runat="server">
            </div>
            <div class="mb-3">
                <label class="form-label">Correo Electronico</label>
                <input type="text" class="form-control" id="mail" required runat="server">
            </div>
            <div class="mb-3">
                <button class="btn btn-primary" type="button" id="bntGuardar" runat="server">Guardar Cambios</button>
                <button class="btn btn-primary" type="button" id="btnModificar" runat="server">Modificar</button>
            </div>
        </div>
    </div>

</asp:Content>
