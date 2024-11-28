<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="EdicionPaciente.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.EdicionPaciente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="card mx-auto" style="width: 50%;">
            <div class="card-header text-center">
                <h3>Editar Paciente</h3>
            </div>
            <div class="card-body">
                    <div class="form-group">
                        <label for="txtNombre">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="txtApellido">Apellido</label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="txtDocumento">Documento</label>
                        <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="txtTelefono">Teléfono</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="txtFechaNacimiento">Fecha de Nacimiento</label>
                        <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>
                    <div class="form-group">
                        <label for="txtEmail">Email Personal</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="txtObraSocial">Obra Social</label>
                        <asp:TextBox ID="txtObraSocial" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="txtNroAfiliado">Número de Afiliado</label>
                        <asp:TextBox ID="txtNroAfiliado" runat="server" CssClass="form-control" />
                    </div>

                    <asp:Label ID="lblMensajeError" runat="server" CssClass="text-danger" Visible="false" />

                    <div class="form-group text-center mt-4">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
                    </div>
            </div>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.3/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</asp:Content>
