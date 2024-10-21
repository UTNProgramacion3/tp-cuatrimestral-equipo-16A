<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="SeleccionarPaciente.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Views.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="row g-4 d-flex justify-content-center">

        <div class="col-md-6">
            <label class="from-label" runat="server">Seleccionar un Paciente:</label>
            <asp:GridView ID="dgvPacientes" runat="server" AutoGenerateColumns="False"
                CssClass="table table-hover table-striped table-bordered" OnSelectedIndexChanged="dgvPacientes_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Apellido" HeaderText="Edad" />
                    <asp:BoundField DataField="Dni" HeaderText="Dni" />
                    <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" />
                </Columns>
            </asp:GridView>
            <div class="mb-3">
                <label class="form-label" runat="server">Nombre de Paciente:</label>
                <input type="text" class="form-control" id="nombrePaciente" value="" disabled readonly />
            </div>
            <div class="mb-3">
                <label class="form-label" runat="server">Apellido de Paciente:</label>
                <input type="text" class="form-control" id="apellidoPaciente" value="" disabled readonly />
            </div>
            <div class="mb-3">
                <input class="btn btn-primary" type="button" value="Atrás" id="btnAtras"/>
                <input class="btn btn-primary" type="button" value="Siguiente" id="btnSiguiente"/>
            </div>
        </div>
    </div>

</asp:Content>
