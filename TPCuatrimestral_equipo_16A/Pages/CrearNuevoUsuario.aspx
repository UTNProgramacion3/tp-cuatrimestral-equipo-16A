<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="CrearNuevoUsuario.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.CrearNuevoUsuario" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-popup {
    position: fixed;
    top: 20%;
    left: 50%;
    transform: translate(-50%, -20%);
    width: 300px;
    background-color: #fff;
    border: 1px solid #ccc;
    box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);
    border-radius: 8px;
    z-index: 1000;
    animation: fadeIn 0.3s;
}

.card-popup-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 10px;
    background-color: #1d1e2c;
    color: #f7ebec;
    border-bottom: 1px solid #ccc;
    border-radius: 8px 8px 0 0;
}

.card-popup-body {
    padding: 15px;
    color: #59656f;
}

.close-button {
    background: none;
    border: none;
    color: #f7ebec;
    font-size: 18px;
    cursor: pointer;
}

.close-button:hover {
    color: #e63946;
}

@keyframes fadeIn {
    from {
        opacity: 0;
        transform: translate(-50%, -25%);
    }
    to {
        opacity: 1;
        transform: translate(-50%, -20%);
    }
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="mensaje" class="card-popup" style="display: none;">
    <div class="card-popup-header">
        <span id="mensajeTitulo"></span>
        <button onclick="cerrarMensaje()" class="close-button">&times;</button>
    </div>
    <div class="card-popup-body">
        <p id="mensajeTexto"></p>
    </div>
</div>
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
                <span class="text-danger" id="errorDdlRol"></span>
            </div>
            <div class="mb-3" id="posicion">
                <div>
                <label for="posicionEmpleado" class="form-label">Posición empleado</label>
                <asp:DropDownList ID="posicionEmpleado" runat="server" CssClass="form-select">
                    <asp:ListItem Value="1">Médico</asp:ListItem>
                    <asp:ListItem Value="2">Recepcionista</asp:ListItem>
                    <asp:ListItem Value="2">Supervisor</asp:ListItem>
                </asp:DropDownList>
                <span class="text-danger" id="errorPosicion"></span>

                </div>

                <!-- Campos de Médico y Empleado -->
                <div class="mb-3" id="legajoDiv" style="display: none;">
                    <label for="txtLegajo" class="form-label">Legajo</label>
                    <asp:TextBox ID="txtLegajo" runat="server" CssClass="form-control" />
                    <span class="text-danger" id="errorLegajo"></span>

                </div>
                <div class="mb-3" id="matriculaDiv" style="display: none;">
                    <label for="txtMatricula" class="form-label">Matrícula</label>
                    <asp:TextBox ID="txtMatricula" runat="server" CssClass="form-control" />
                    <span class="text-danger" id="errorMatricula"></span>

                </div>
                <div class="mb-3" id="especialidadDiv" style="display: none;">
                    <label for="ddlEspecialidad" class="form-label">Especialidad</label>
                    <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select">
                        <asp:ListItem Value="1">Especialidad 1</asp:ListItem>
                        <asp:ListItem Value="2">Especialidad 2</asp:ListItem>
                        <asp:ListItem Value="3">Especialidad 3</asp:ListItem>
                    </asp:DropDownList>
                    <span class="text-danger" id="errorEspecialidad"></span>
                </div>
            </div>
            <div class="mb-3" id="contenedor-datos-personales">
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
                <h2>Domicilio</h2>
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

        function validarFormulario() {
            let esValido = true;

            const rol = document.getElementById('<%= ddlRol.ClientID %>');
            const posicionEmpleado = document.getElementById('<%= posicionEmpleado.ClientID %>');
            const nombre = document.getElementById('<%= txtNombre.ClientID %>');
            const apellido = document.getElementById('<%= txtApellido.ClientID %>');
            const fechaNacimiento = document.getElementById('<%= txtFechaNacimiento.ClientID %>');
            const documento = document.getElementById('<%= txtDocumento.ClientID %>');
            const emailPersonal = document.getElementById('<%= txtEmailPersonal.ClientID %>');
            const telefono = document.getElementById('<%= txtTelefono.ClientID %>');
            const calle = document.getElementById('<%= txtCalle.ClientID %>');
            const numero = document.getElementById('<%= txtNumero.ClientID %>');
            const piso = document.getElementById('<%= txtPiso.ClientID %>');
            const depto = document.getElementById('<%= txtDepto.ClientID %>');
            const localidad = document.getElementById('<%= txtLocalidad.ClientID %>');
            const provincia = document.getElementById('<%= txtProvincia.ClientID %>');
            const codigoPostal = document.getElementById('<%= txtCodigoPostal.ClientID %>');
            const legajo = document.getElementById('<%= txtLegajo.ClientID %>');
            const matricula = document.getElementById('<%= txtMatricula.ClientID %>');
            const especialidad = document.getElementById('<%= ddlEspecialidad.ClientID %>');


            // Obtén referencias a los mensajes de error
            const errorRol = document.getElementById("errorRol");
            const errorNombre = document.getElementById("errorNombre");
            const errorApellido = document.getElementById("errorApellido");

            // Limpia mensajes previos
            errorRol.textContent = "";
            errorNombre.textContent = "";
            errorApellido.textContent = "";

            // Validar Rol
            if (!rol.value) {
                errorRol.textContent = "Debe seleccionar un rol.";
                esValido = false;
            }

            // Validar Nombre
            if (!nombre.value.trim()) {
                errorNombre.textContent = "El nombre es obligatorio.";
                esValido = false;
            }

            // Validar Apellido
            if (!apellido.value.trim()) {
                errorApellido.textContent = "El apellido es obligatorio.";
                esValido = false;
            }

            // Retorna si el formulario es válido
            return esValido;
        }

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
