<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="SeleccionarTurno.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.SeleccionarMedico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row g-4 d-flex justify-content-center">
        <div class="col-md-6">
            <div class="mb-3">
                <label class="form-label" runat="server">Seleccionar Especialidad:</label>
                
                <asp:DropDownList CssClass="form-select" runat="server" ID="ddlEspecialidades">
                    <asp:ListItem Text="..." Value="" Selected="True"/>
                    <asp:ListItem Text="Medico Clínico" />
                    <asp:ListItem Text="Gastronterologo" />
                    <asp:ListItem Text="Cirujano" />
                    <asp:ListItem Text="Odontologo" />
                    <asp:ListItem Text="Ortodoncia" />
                </asp:DropDownList>

            </div>
            <div class="mb-3">
             <label class="from-label" runat="server">Seleccionar un Medico:</label>
                <asp:GridView id="dvgMedicos" runat="server" AutoGenerateColumns="false"
                 CssClass="table table-hover table-striped table-bordered">
                    <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre"/>
                     <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                        <asp:BoundField DataField="Horario" HeaderText="Horario" />
                    <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>


</asp:Content>
