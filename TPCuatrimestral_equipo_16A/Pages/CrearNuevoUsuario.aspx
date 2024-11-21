<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="CrearNuevoUsuario.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.CrearNuevoUsuario" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h2>Crear Usuario</h2>
        <asp:Panel ID="PanelCrearUsuario" runat="server">
            <div class="mb-3">
                <label for="ddlRol" class="form-label">Rol</label>
                <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-select" OnSelectedIndexChanged="ddlRol_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="1" Text="Administrador"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Empleado"></asp:ListItem>
                    <asp:ListItem Value="3" Text="Paciente"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="mb-3" id="posicion">
                <label for="posicionEmpleado" class="form-label">Posición empleado</label>
                <asp:DropDownList ID="posicionEmpleado" runat="server" CssClass="form-select">
                    <asp:ListItem Value="1">Médico</asp:ListItem>
                    <asp:ListItem Value="2">Recepcionista</asp:ListItem>
                    <asp:ListItem Value="2">Supervisor</asp:ListItem>
                </asp:DropDownList>

                <!-- Campos de Médico y Empleado -->
                <div class="mb-3" id="legajoDiv" style="display: none;">
                    <label for="txtLegajo" class="form-label">Legajo</label>
                    <asp:TextBox ID="txtLegajo" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3" id="matriculaDiv" style="display: none;">
                    <label for="txtMatricula" class="form-label">Matrícula</label>
                    <asp:TextBox ID="txtMatricula" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3" id="especialidadDiv" style="display: none;">
                    <label for="ddlEspecialidad" class="form-label">Especialidad</label>
                    <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select">
                        <asp:ListItem Value="1">Especialidad 1</asp:ListItem>
                        <asp:ListItem Value="2">Especialidad 2</asp:ListItem>
                        <asp:ListItem Value="3">Especialidad 3</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtApellido" class="form-label">Apellido</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtFechaNacimiento" class="form-label">Fecha Nacimiento</label>
                <asp:Calendar ID="txtFechaNacimiento" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtDocumento" class="form-label">Documento</label>
                <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtEmailPersonal" class="form-label">Email Personal</label>
                <asp:TextBox ID="txtEmailPersonal" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtTelefono" class="form-label">Telefono</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
            </div>
            <div class="container mt-5">
                <h2>Datos Personales</h2>
                <asp:Panel ID="Panel1" runat="server">
                    <div class="mb-3">
                        <label for="txtCalle" class="form-label">Calle</label>
                        <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="txtNumero" class="form-label">Número</label>
                        <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="txtPiso" class="form-label">Piso</label>
                        <asp:TextBox ID="txtPiso" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="txtDepto" class="form-label">Depto</label>
                        <asp:TextBox ID="txtDepto" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="txtLocalidad" class="form-label">Localidad</label>
                        <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="txtProvincia" class="form-label">Provincia</label>
                        <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="txtCodigoPostal" class="form-label">Código Postal</label>
                        <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" />
                    </div>
                </asp:Panel>
            </div>




            <asp:Button ID="btnCrear" runat="server" Text="Crear Usuario" CssClass="btn btn-primary" OnClick="btnCrear_Click" />
        </asp:Panel>
    </div>

    <script type="text/javascript">
        function toggleFields() {
            var role = document.getElementById('<%= ddlRol.ClientID %>').value;
        var posicion = document.getElementById('<%= posicionEmpleado.ClientID %>').value;

            document.getElementById('matriculaDiv').style.display = 'none';
            document.getElementById('especialidadDiv').style.display = 'none';
            document.getElementById('legajoDiv').style.display = 'none';
            document.getElementById('posicion').style.display = 'none';

            if (role == "4") { // 2 es Empleado
                document.getElementById('posicion').style.display = 'block';

                if (posicion == "1") { // 1 es Médico
                    document.getElementById('matriculaDiv').style.display = 'block';
                    document.getElementById('especialidadDiv').style.display = 'block';
                }
                document.getElementById('legajoDiv').style.display = 'block';
            }
        }

        window.onload = function () {
            document.getElementById('<%= ddlRol.ClientID %>').addEventListener('change', toggleFields);
        document.getElementById('<%= posicionEmpleado.ClientID %>').addEventListener('change', toggleFields);

            toggleFields();
        };
    </script>

</asp:Content>
