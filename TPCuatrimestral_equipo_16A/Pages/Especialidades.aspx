<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="Especialidades.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.Especialidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <link href="/Stylesheets/DataGridView.css" rel="stylesheet" type="text/css" />

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">




    <div class="container m-6">
        <div class="row g-4 d-flex justify-content-center">
            <div class="col-md-6">
                <div class="mt-3">
                    <label class="form-label fw-bold" runat="server">Filtrar Especialidad</label>
                    <div class="mb-3">
                        <asp:TextBox ID="txtBuscarEspecialidad" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:Button class="btn btn-dark" type="button" ID="btnLimpiarFiltros" Text="Limpiar Filtro" runat="server" OnClick="btnLimpiarFiltros_Click" />
                        <asp:Button class="btn btn-dark" type="button" ID="btnBuscar" Text="Buscar..." runat="server" OnClick="btnBuscar_Click" />
                    </div>
                </div>

                <div>
                    <asp:GridView ID="dgvEspecialidades" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-hover table-striped table-bordered" OnSelectedIndexChanged="dgvEspecialidades_SelectedIndexChanged">
                        <columns>
                            <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="Id" HeaderText="Id" />
                            <asp:BoundField DataField="Nombre" HeaderText="Especialidad" />
                            <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" />
                        </columns>
                    </asp:GridView>
                </div>

                <div>
                    <label class="form-label fw-bold" runat="server">Especialidad Seleccionada</label>
                    <asp:TextBox ID="txtEspecialidadSeleccionada" CssClass="form-control mb-3" Disabled="true" runat="server"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Button type="button" class="btn btn-dark" Text="Atras" ID="btnAtras" runat="server" OnClick="btnAtras_Click" />
                    <asp:Button type="button" class="btn btn-dark" Text="Crear" runat="server" ID="btnCrear" data-bs-toggle="modal" data-bs-target="#modalCrearEspecialidad" OnClientClick="return false;" />
                    <asp:Button type="button" class="btn btn-dark" Text="Modificar" runat="server" ID="btnModificar" data-bs-toggle="modal" data-bs-target="#modalModificarEspecialidad" Enabled="false" OnClientClick="return false;" />
                </div>

                <div>
                    <div id="modificarEspecialidadSuccess" class="alert alert-success mt-3" role="alert" runat="server" visible="false">
                        ¡Especialidad modificada correctamente!
               
                    </div>
                    <div id="modificarEspecialidadFailure" class="alert alert-danger mt-3" role="alert" runat="server" visible="false">
                        ¡Error al modificar Especialidad!

                    </div>
                    <div id="crearEspecialidadSuccess" class="alert alert-success mt-3" role="alert" runat="server" visible="false">
                        ¡Especialidad Creada correctamente!
               
                    </div>
                    <div id="crearEspecialidadFailure" class="alert alert-danger mt-3" role="alert" runat="server" visible="false">
                        ¡Error al crear Especialidad! Complete todos los campos requieridos (Nombre Especialidad)
                    </div>
                </div>
            </div>
        </div>
    </div>

    <section>
        <div>
            <div class="modal fade" id="modalModificarEspecialidad" tabindex="-1" aria-labelledby="modalModificarEspecialidadLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="modalModificarEspecialidadLabel">Modificar Especialidad</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <label class="form-label fw-bold" runat="server">Nombre Especialidad</label>
                            <asp:TextBox ID="txtNombreEspecialidad" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="modal-footer">
                            <asp:Button type="button" ID="btnModificarEspecialidadCerrar" class="btn btn-dark" runat="server" Text="Cerrar" data-bs-dismiss="modal" />
                            <asp:Button type="button" ID="btnModificarEspecialidad" runat="server" CssClass="btn btn-dark" Text="Confirmar" OnClick="btnModificarEspecialidad_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <section>
        <div>
            <div class="modal fade" id="modalCrearEspecialidad" tabindex="-1" aria-labelledby="modalCrearEspecialidadLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Agregar Nueva Especialidad</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <label class="form-label fw-bold" runat="server">Nombre Especialidad</label>
                            <asp:TextBox ID="txtCrearNombreEspecialidad" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="modal-footer">
                            <asp:Button type="button" ID="btnCrearEspecialidadCerrar" class="btn btn-dark" runat="server" Text="Cerrar" data-bs-dismiss="modal" />
                            <asp:Button type="button" ID="btnGuardarEspecialidad" runat="server" CssClass="btn btn-dark" Text="Guardar" OnClick="btnGuardarEspecialidad_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>



</asp:Content>