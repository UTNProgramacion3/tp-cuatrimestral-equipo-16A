<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="ListadoTurnos.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Views.ListadoTurnos" Async="true" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Stylesheets/DataGridView.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="m-5">
        <div class="row g-4 d-flex justify-content-center">
            <div class="col-md-6">
                <div class="mb-3">
                    <asp:TextBox CssClass="form-control" TextMode="Date" runat="server" ID="txtBoxFecha" />
                </div>
                <div>
                    <asp:GridView ID="dgvTurnos" runat="server" CssClass="table table-hover table-striped table-bordered"
                         OnSelectedIndexChanged="dgvTurnos_SelectedIndexChanged" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" HeaderText="Id" DataField="Turno.Id" />
                            <asp:BoundField HeaderText="Fecha" DataField="Turno.Fecha" />
                            <asp:BoundField HeaderText="Hora" DataField="Turno.Hora" />
                            <asp:BoundField HeaderText="Nombre Medico" DataField="Medico.Nombre" />
                            <asp:BoundField HeaderText="Apellido Medico" DataField="Medico.Apellido" />
                            <asp:BoundField HeaderText="Nombre Paciente" DataField="Paciente.Nombre" />
                            <asp:BoundField HeaderText="Apellido Paciente" DataField="Paciente.Apellido" />
                            <asp:Boundfield HeaderText="Sede" DataField="Sede.Nombre" />
                            <asp:BoundField HeaderText="Estado de Turno" DataField="Turno.EstadoTurno" />
                            <asp:CommandField ShowSelectButton="True" SelectText="Reprogramar" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>





</asp:Content>
