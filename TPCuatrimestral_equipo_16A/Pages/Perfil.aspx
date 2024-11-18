<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Views.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row g-4 m-5 d-flex justify-content-center">
        <div class="col-md-4">
            <div class="mb-3">
                <label class="form-label fw-bold">Usuario</label>
                <input type="text" class="form-control" id="user" value="" required runat="server" disabled="disabled">
            </div>
            <div class="mb-3">
                <label class="form-label fw-bold">Contraseña</label>
                <input type="text" class="form-control" id="pass" required runat="server" disabled="disabled">
                <div class="form-check">
                    <asp:CheckBox class="form-check-input d-flex align-items-center" type="checkbox" ID="passCheck" runat="server" AutoPostBack="True" OnCheckedChanged="ChkBoxChecked" />
                    <label class="form-check-label">Mostrar Contraseña</label>
                </div>
            </div>
            <div class="mb-3">
                <label class="form-label fw-bold">Nombre</label>
                <input type="text" class="form-control" id="nombre" required runat="server" disabled="disabled">
            </div>
            <div class="mb-3">
                <label class="form-label fw-bold">Apellido</label>
                <input type="text" class="form-control" id="apellido" required runat="server" disabled="disabled">
            </div>
            <div class="mb-3">
                <label class="form-label fw-bold">Dni</label>
                <input type="text" class="form-control" id="dni" required runat="server" disabled="disabled">
            </div>
            <div class="mb-3">
                <label class="form-label fw-bold">Correo Electronico</label>
                <input type="text" class="form-control" id="mail" required runat="server" disabled="disabled">
            </div>
            <div class="mb-3">
                <asp:Button class="btn btn-light" type="button" ID="btnGuardar" runat="server" disabled="disabled" Text="Guardar Cambios" runat="server" OnClick="BtnGuardar_OnClick" />
                <asp:Button class="btn btn-light" Text="Modificar" type="button" ID="btnModificar" runat="server" OnClick="BtnModificar_OnClick" />
            </div>
        </div>
        <div class="col-md-2">
            <div class="mt-4">
                <img src="https://media.istockphoto.com/id/1222357475/es/vector/icono-de-vista-previa-de-imagen-marcador-de-posici%C3%B3n-de-imagen-para-el-sitio-web-o-el-dise%C3%B1o.jpg?s=612x612&w=0&k=20&c=vQOno8TpmbwnHeM6ylVkfSiwbVa6viw5_AvH6PSp-DM=" class="rounded" alt="empty" width="200" height="200" />
                <div class="mt-3">
                    <button class="btn btn-light" type="button" id="btnCargarImagen" runat="server" disabled>Cargar...</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
