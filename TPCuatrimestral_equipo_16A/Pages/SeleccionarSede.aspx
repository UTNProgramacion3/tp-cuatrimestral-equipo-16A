<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="SeleccionarSede.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.SeleccionarSede" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="/Stylesheets/DataGridView.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="conteiner m-3">
        <div class="row g-4 d-flex justify-content-center">
            <div class="col-md-6">
                <div class="mb-3">
                    <label class="form-label fw-bold" runat="server">Seleccionar Sede</label>
                    <div class="mb-3">
                        <asp:TextBox ID="txtBuscarSede" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:Button class="btn btn-dark" type="button" ID="btnLimpiarFiltros" Text="Limpiar Filtro" runat="server" OnClick="btnLimpiarFiltros_Click" />
                        <asp:Button class="btn btn-dark" type="button" ID="btnBuscar" Text="Buscar..." runat="server" OnClick="btnBuscar_Click"/>
                    </div>
                </div>
                <div>
                    <asp:GridView ID="dgvSedes" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-hover table-striped table-bordered" OnSelectedIndexChanged="dgvSedes_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="Sede.Id" HeaderText="Id" />
                            <asp:BoundField DataField="Sede.Nombre" HeaderText="Sede" />
                            <asp:BoundField DataField="Direccion.Calle" HeaderText="Calle" />
                            <asp:BoundField DataField="Direccion.Numero" HeaderText="Altura" />
                            <asp:BoundField DataField="Direccion.Localidad" HeaderText="Localidad" />
                            <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" />
                        </Columns>
                    </asp:GridView>
                    <div>
                        <label class="form-label fw-bold" runat="server">Sede Seleccionada</label>
                        <asp:TextBox ID="txtbSedeSeleccionada" CssClass="form-control mb-3" Disabled="true" runat="server"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:Button class="btn btn-dark" Text="Atrás" runat="server" ID="btnAtras" OnClick="btnAtras_Click" />
                        <asp:Button class="btn btn-dark" Text="Siguiente" ID="btnSiguiente" runat="server" OnClick="btnSiguiente_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>




</asp:Content>
