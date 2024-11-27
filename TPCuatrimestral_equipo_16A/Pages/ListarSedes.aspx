<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="ListarSedes.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.ListarSedes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<!-- Scripts de Bootstrap -->
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
<link href="/Stylesheets/DataGridView.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container m-6">
        <div class="row g-4 d-flex justify-content-center">
            <div class="col-md-6">
                <div class="mt-3">
                    <label class="form-label fw-bold" runat="server">Seleccionar Sede</label>
                    <div class="mb-3">
                        <asp:TextBox ID="txtBuscarSede" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:Button class="btn btn-dark" type="button" ID="btnLimpiarFiltros" Text="Limpiar Filtro" runat="server" OnClick="btnLimpiarFiltros_Click" />
                        <asp:Button class="btn btn-dark" type="button" ID="btnBuscar" Text="Buscar..." runat="server" OnClick="btnBuscar_Click" />
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
                </div>

                <div>
                    <label class="form-label fw-bold" runat="server">Sede Seleccionada</label>
                    <asp:TextBox ID="txtbSedeSeleccionada" CssClass="form-control mb-3" Disabled="true" runat="server"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Button type="button" class="btn btn-dark" Text="Atras" ID="btnAtras" runat="server" OnClick="btnAtras_Click1" />
                    <asp:Button type="button" class="btn btn-dark" Text="Crear" runat="server" ID="btnCrear" data-bs-toggle="modal" data-bs-target="#modalCrearSede" OnClientClick="return false;" />
                    <asp:Button type="button" class="btn btn-dark" Text="Modificar" runat="server" ID="btnModificar" data-bs-toggle="modal" data-bs-target="#modalModificarSede" Enabled="false" OnClientClick="return false;"/>
                </div>

                <div>
                    <div id="modificarSedeSucces" class="alert alert-success mt-3" role="alert" runat="server" visible="false">
                        ¡Sede modificada correctamente!
               
                    </div>
                    <div id="modificarDedeFailure" class="alert alert-danger mt-3" role="alert" runat="server" visible="false">
                        ¡Error al modificar sede!

                    </div>
                    <div id="crearSedeSuccess" class="alert alert-success mt-3" role="alert" runat="server" visible="false">
                        ¡Sede Creada correctamente!
               
                    </div>
                    <div id="crearSedeFailure" class="alert alert-danger mt-3" role="alert" runat="server" visible="false">
                        ¡Error al crear sede! Complete todos los campos requieridos (Nombre de Sede, Calle, Numero, Localidad, Provincia)
                    </div>
                    <div id="campoInvalidoNumerSede" class="alert alert-danger mt-3" role="alert" runat="server" visible="false">
                        ¡Error al crear sede! El campo Numero de calle solo permite numeros
                    </div>
                </div>
            </div>
        </div>
    </div>

    <section>
        <div>
            <div class="modal fade" id="modalModificarSede" tabindex="-1" aria-labelledby="modalModificarSedeLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="modalModificarSedeLabel">Modificar Sede</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <label class="form-label fw-bold" runat="server">Nombre Sede</label>
                            <asp:TextBox ID="txtNombreSede" runat="server" CssClass="form-control"></asp:TextBox>
                            <br />
                            <label class="form-label fw-bold" runat="server">Calle</label>
                            <asp:TextBox ID="txtCalleSede" runat="server" CssClass="form-control"></asp:TextBox>
                            <br />
                            <label class="form-label fw-bold" runat="server">Numero</label>
                            <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control"></asp:TextBox>
                            <br />
                            <label class="form-label fw-bold" runat="server">Piso</label>
                            <asp:TextBox ID="txtPiso" runat="server" CssClass="form-control"></asp:TextBox>
                            <br />
                            <label class="form-label fw-bold" runat="server">Depto</label>
                            <asp:TextBox ID="txtDepto" runat="server" CssClass="form-control"></asp:TextBox>
                            <br />
                            <label class="form-label fw-bold" runat="server">Localidad</label>
                            <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control"></asp:TextBox>
                            <br />
                            <label class="form-label fw-bold" runat="server">Provincia</label>
                            <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control"></asp:TextBox>
                            <br />
                            <label class="form-label fw-bold" runat="server">Codigo Postal</label>
                            <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control"></asp:TextBox>
                            <br />
                        </div>
                        <div class="modal-footer">
                            <asp:Button type="button" ID="btnModificarSedeCerrar" class="btn btn-dark" runat="server" Text="Cerrar" data-bs-dismiss="modal" />
                            <asp:Button type="button" ID="btnModificarSede" runat="server" CssClass="btn btn-dark" Text="Confirmar" OnClick="btnModificarSede_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <section>
    <div>
        <div class="modal fade" id="modalCrearSede" tabindex="-1" aria-labelledby="modalCrearSedeLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Agregar Nueva Sede</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <label class="form-label fw-bold" runat="server">Nombre Sede</label>
                        <asp:TextBox ID="txtCrearNombreSede" runat="server" CssClass="form-control"></asp:TextBox>
                        <br />
                        <label class="form-label fw-bold" runat="server">Calle</label>
                        <asp:TextBox ID="txtCrearCalleSede" runat="server" CssClass="form-control"></asp:TextBox>
                        <br />
                        <label class="form-label fw-bold" runat="server">Numero</label>
                        <asp:TextBox ID="txtCrearNumeroSede" runat="server" CssClass="form-control"></asp:TextBox>
                        <br />
                        <label class="form-label fw-bold" runat="server">Piso</label>
                        <asp:TextBox ID="txtCrearPisoSede" runat="server" CssClass="form-control"></asp:TextBox>
                        <br />
                        <label class="form-label fw-bold" runat="server">Depto</label>
                        <asp:TextBox ID="txtCrearDeptoSede" runat="server" CssClass="form-control"></asp:TextBox>
                        <br />
                        <label class="form-label fw-bold" runat="server">Localidad</label>
                        <asp:TextBox ID="txtCrearLocalidadSede" runat="server" CssClass="form-control"></asp:TextBox>
                        <br />
                        <label class="form-label fw-bold" runat="server">Provincia</label>
                        <asp:TextBox ID="txtCrearProvinciaSede" runat="server" CssClass="form-control"></asp:TextBox>
                        <br />
                        <label class="form-label fw-bold" runat="server">Codigo Postal</label>
                        <asp:TextBox ID="txtCrearCodigoPostalSede" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="modal-footer">
                        <asp:Button type="button" ID="btnCrearSedeCerrar" class="btn btn-dark" runat="server" Text="Cerrar" data-bs-dismiss="modal" />
                        <asp:Button type="button" ID="btnGuardarSede" runat="server" CssClass="btn btn-dark" Text="Guardar" OnClick="btnGuardarSede_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    </section>


</asp:Content>
