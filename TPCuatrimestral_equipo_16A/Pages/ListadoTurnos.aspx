<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="ListadoTurnos.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Views.WebForm2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="m-5">
        <div class="row g-4 d-flex justify-content-center">
            <div class="col-md-6">
                <div class="mb-3">
                    <asp:TextBox CssClass="form-control" TextMode="Date" runat="server" ID="txtBoxFecha" />
                </div>
                <div>
                    <asp:GridView ID="dgvTurnos" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-hover table-striped table-bordered">
                        <Columns>
                            <asp:BoundField DataField="NombrePaciente" HeaderText="Nombre Paciente" />
                            <asp:BoundField DataField="ApellidoPaciente" HeaderText="Apellido Paciente" />
                            <asp:BoundField DataField="DniPaciente" HeaderText="Dni" />
                            <asp:BoundField DataField="NombreMedico" HeaderText="Nombre Doctor" />
                            <asp:BoundField DataField="ApellidoMedico" HeaderText="Apellido Doctor" />
                            <asp:BoundField DataField="Especialidad" HeaderText="Especialidad" />
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha"/>
                            <asp:BoundField DataField="Horario" HeaderText="Horario"/>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>





</asp:Content>
