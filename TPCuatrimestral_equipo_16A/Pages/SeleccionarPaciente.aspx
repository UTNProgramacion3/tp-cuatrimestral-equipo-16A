<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="SeleccionarPaciente.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Views.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="row g-4 m-5 d-flex justify-content-center">
        <div class="col-md-6">
            <div class="col">
                <label class="mb-2 fw-bold" runat="server">Filtrar por DNI</label>
                <div class="input-group mb-2">
                    <input type="text" class="form-control" id="ipDni" value="" />
                    <input class="btn btn-dark" type="button" value="Filtrar" id="btnFiltrar" />
                </div>
            </div>
            <label class="from-label mb-3 fw-bold" runat="server">Seleccionar un Paciente:</label>
            <asp:GridView ID="dgvPacientes" runat="server" AutoGenerateColumns="False"
                CssClass="table table-hover table-striped table-bordered" OnSelectedIndexChanged="dgvPacientes_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                    <asp:BoundField DataField="Dni" HeaderText="Dni" />
                    <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" />
                </Columns>
            </asp:GridView>
            <div class="mb-3">
                <label class="form-label fw-bold" runat="server">Nombre de Paciente:</label>
                <input type="text" class="form-control" id="inputNombrePaciente" value="" disabled readonly runat="server" />
            </div>
            <div class="mb-3">
                <label class="form-label fw-bold" runat="server">Apellido de Paciente:</label>
                <input type="text" class="form-control" id="inputApellidoPaciente" value="" disabled readonly runat="server" />
            </div>
            <div class="mb-3">
                <input class="btn btn-light" type="button" value="Atrás" id="btnAtras" />
                <input class="btn btn-light" type="button" value="Siguiente" id="btnSiguiente" />
            </div>
        </div>
    </div>

</asp:Content>
