<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="VerUsuario.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.VerUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1">
    <head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Ver Usuario</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1">
        <div class="container mt-4">
            <h1 class="text-center mb-4">Información del Usuario</h1>
            <div class="card shadow">
                <div class="card-body">
                    <h5 class="card-title">Datos Generales</h5>
                    <div class="row">
                        <div class="col-md-6">
                            <label class="form-label">Nombre:</label>
                            <asp:Label ID="lblNombre" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">Apellido:</label>
                            <asp:Label ID="lblApellido" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="col-md-6 mt-3">
                            <label class="form-label">Fecha de Nacimiento:</label>
                            <asp:Label ID="lblFechaNacimiento" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="col-md-6 mt-3">
                            <label class="form-label">Documento:</label>
                            <asp:Label ID="lblDocumento" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="col-md-6 mt-3">
                            <label class="form-label">Email Personal:</label>
                            <asp:Label ID="lblEmailPersonal" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="col-md-6 mt-3">
                            <label class="form-label">Teléfono:</label>
                            <asp:Label ID="lblTelefono" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="card-body">
                    <h5 class="card-title">Rol y Otros Detalles</h5>
                    <div class="row">
                        <div class="col-md-6">
                            <label class="form-label">Rol:</label>
                            <asp:Label ID="lblRol" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">Legajo:</label>
                            <asp:Label ID="lblLegajo" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="col-md-6 mt-3">
                            <label class="form-label">Matrícula:</label>
                            <asp:Label ID="lblMatricula" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="col-md-6 mt-3">
                            <label class="form-label">Especialidad:</label>
                            <asp:Label ID="lblEspecialidad" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="card-body">
                    <h5 class="card-title">Domicilio</h5>
                    <div class="row">
                        <div class="col-md-6">
                            <label class="form-label">Calle:</label>
                            <asp:Label ID="lblCalle" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <label class="form-label">Número:</label>
                            <asp:Label ID="lblNumero" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <label class="form-label">Piso:</label>
                            <asp:Label ID="lblPiso" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="col-md-3 mt-3">
                            <label class="form-label">Depto:</label>
                            <asp:Label ID="lblDepto" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="col-md-6 mt-3">
                            <label class="form-label">Localidad:</label>
                            <asp:Label ID="lblLocalidad" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="col-md-6 mt-3">
                            <label class="form-label">Provincia:</label>
                            <asp:Label ID="lblProvincia" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="col-md-6 mt-3">
                            <label class="form-label">Código Postal:</label>
                            <asp:Label ID="lblCodigoPostal" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</asp:Content>
