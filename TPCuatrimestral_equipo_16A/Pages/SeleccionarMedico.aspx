<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="SeleccionarMedico.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.SeleccionarMedico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="conteiner m-3">
        <div class="row g-4 d-flex justify-content-center">
            <div class="col-md-6">
                <div class="mb-3">
                    <label class="form-label fw-bold" runat="server">Seleccionar Medico</label>
                </div>
                <div>
                    <label class="form-label fw-bold" runat="server">Filtrar Especialidad:</label>
                </div>
                <div class="mb-3">
                    <div class="mb-3">
                        <asp:TextBox ID="txtBuscarEspecialidad" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtBuscarEspecialidad_TextChanged"></asp:TextBox>
                    </div>
                    <div>
                        <asp:DropDownList CssClass="form-select" runat="server" ID="ddlEspecialidades"></asp:DropDownList>
                    </div>
                </div>
                <div class="mb-3">
                    <asp:Button class="btn btn-dark" type="button" ID="btnBuscar" Text="Buscar..." runat="server" />
                </div>
                <div>
                    <div class="mb-2">
                        <label class="from-label fw-bold" runat="server">Seleccionar un Medico:</label>
                    </div>
                    <asp:GridView ID="dgvMedicos" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-hover table-striped table-bordered">
                        <Columns>
                            <asp:BoundField DataField="Persona.Nombre" HeaderText="Nombre Medico" />
                            <asp:BoundField DataField="Persona.Apellido" HeaderText="Apellido Medico" />
                            <asp:BoundField DataField="Especialidad.Nombre" HeaderText="Especialidad" />
                            <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="mb-3">
                    <asp:Button class="btn btn-dark" Text="Atrás" runat="server" ID="btnAtras" OnClick="btnAtras_Click" />
                    <asp:Button class="btn btn-dark" Text="Siguiente" ID="btnSiguiente" runat="server" OnClick="btnSiguiente_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
