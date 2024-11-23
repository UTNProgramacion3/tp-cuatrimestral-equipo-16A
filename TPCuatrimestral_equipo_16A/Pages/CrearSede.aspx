<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="CrearSede.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.AgregarSede" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <button type="button" class="btn btn-dark" data-bs-toggle="modal" data-bs-target="#modalSede">
        +
    </button>
    <div class="modal fade" id="modalSede" tabindex="-1" aria-labelledby="modalSedeLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalSedeLabel">Agregar Nueva Sede</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:TextBox ID="txtNombreSede" runat="server" CssClass="form-control" required="True" placeholder="Nombre de la Sede" ></asp:TextBox>
                    <br />
                    <asp:TextBox ID="txtCalleSede" runat="server" CssClass="form-control" required="True" placeholder="Calle" ></asp:TextBox>
                    <br />
                    <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" required="True" placeholder="Numero"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="txtPiso" runat="server" CssClass="form-control" placeholder="Piso"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="txtDepto" runat="server" CssClass="form-control" placeholder="Depto"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" required="True" placeholder="Localidad"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control" required="True" placeholder="Provincia"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" placeholder="Codigo Postal"></asp:TextBox>
                    <br />
                </div>
                <div class="modal-footer">
                    <asp:button type="button" class="btn btn-dark" runat="server" Text="Cerrar" data-bs-dismiss="modal"/>
                    <asp:Button ID="btnGuardarSede" runat="server" CssClass="btn btn-dark" Text="Guardar" OnClick="btnGuardarSede_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
