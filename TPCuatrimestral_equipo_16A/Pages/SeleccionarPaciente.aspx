<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="SeleccionarPaciente.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Views.SeleccionarPaciente" Async="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="row g-4 m-5 d-flex justify-content-center">
        <div class="col-md-6">
            <div class="col">
                <label class="mb-2 fw-bold" runat="server">Filtrar por DNI</label>
                <div class="input-group mb-2">
                    <asp:TextBox class="form-control" ID="txtbFiltrar" runat="server"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Button class="btn btn-dark" type="button" ID="btnLimpiarFiltros" Text="Limpiar Filtro" runat="server" OnClick="btnLimpiarFiltros_Click" />
                    <asp:Button class="btn btn-dark" type="button" ID="btnBuscar" Text="Buscar..." runat="server" OnClick="btnBuscar_Click" />
                </div>
            </div>
            <label class="from-label mb-3 fw-bold" runat="server">Seleccionar un Paciente:</label>
            <asp:GridView ID="dgvPacientes" runat="server" AutoGenerateColumns="False"
                CssClass="table table-hover table-striped table-bordered" DataKeyNames="PacienteId" OnSelectedIndexChanged="dgvPacientes_SelectedIndexChanged" SelectionMode="Single">
                <Columns>
                    <asp:BoundField Visible="False" DataField="PacienteId" HeaderText="Id" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                    <asp:BoundField DataField="Documento" HeaderText="Documento" />
                    <asp:BoundField DataField="NroAfiliado" HeaderText="Numero de Afiliado" />
                    <asp:BoundField DataField="ObraSocial" HeaderText="Obra Social" />
                    <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar"/>
                </Columns>
            </asp:GridView>
            <div class="mb-3">
                <label class="form-label fw-bold" runat="server">Nombre de Paciente:</label>
                <asp:TextBox Text="" runat="server" class="form-control" Disabled="True" ReadOnly="True" ID="txtBoxNombrePaciente"/>
            </div>
            <div class="mb-3">
                <label class="form-label fw-bold" runat="server">Apellido de Paciente:</label>
                <asp:TextBox Text="" runat="server" CssClass="form-control" Disabled="True" ReadOnly="True" ID="txtBoxApellidoPaciente" />
            </div>
            <div class="mb-3">
                <label class="form-label fw-bold" runat="server">Numero de Documento: </label>
                <asp:TextBox Text="" runat="server" CssClass="form-control" Disabled="True" ReadOnly="True" ID="txtBNumeroDeDocumento" />
            </div>
            <div class="mb-3">
                <asp:Button class="btn btn-dark" Text="Atrás" runat="server" ID="btnAtras" OnClick="btnAtras_Click" />
                <asp:Button class="btn btn-dark" Text="Siguiente" ID="btnSiguiente" runat="server" OnClick="BtnSiguiente_OnClick" />
            </div>
        </div>
    </div>

</asp:Content>
