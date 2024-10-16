<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Views.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row g-3">
        <div class="col-md-4">
            <label class="form-label">Usuario</label>
            <input type="text" class="form-control" id="user" value="" required runat="server">
        </div>
        <div class="col-md-4">
            <label class="form-label">Contraseña</label>
            <input type="password" class="form-control" id="pass" value="" required runat="server">
        </div>
        <div class="col-md-4">
            <div class="form-check">
                <input class="form-check-input" type="checkbox" id="passCheck" runat="server">
                <label class="form-check-label">
                    Mostrar Contraseña
                </label>
            </div>
        </div>
        <div class="col-md-4">
            <label class="form-label">Nombre</label>
                <input type="text" class="form-control" id="nombre" required runat="server">
        </div>
        <div class="col-md-4">
            <label class="form-label">Apellido</label>
            <input type="text" class="form-control" id="validationDefault03" required runat="server">
        </div>
        <div class="col-md-4">
            <label class="form-label">Dni</label>
            <input type="text" class="form-control" id="dni" required runat="server">
        </div>
        <div class="col-md-4">
            <label class="form-label">Correo Electronico</label>
            <input type="text" class="form-control" id="mail" required runat="server">
        </div>
        <div class="col-12">
            <button class="btn btn-primary" type="button" id="btnModificar" runat="server">Modificar</button>
            <button class="btn btn-primary" type="button" id="bntGuardar" runat="server">Guardar Cambios</button>
        </div>
    </div>

</asp:Content>
