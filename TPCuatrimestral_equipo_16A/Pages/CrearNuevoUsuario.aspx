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
        <div class="card-popup-header bg-primary text-white">
            <span id="mensajeTitulo"></span>
            <button onclick="cerrarMensaje()" class="close-button">&times;</button>
        </div>
        <div class="card-popup-body">
            <p id="mensajeTexto"></p>
        </div>
    </div>

    <div class="container mt-5">
        <div class="card shadow">
            <div class="card-header bg-secondary text-white">
                <h2>Crear Usuario</h2>
            </div>
            <div class="card-body">
                <asp:Panel ID="PanelCrearUsuario" runat="server">
                    <div class="mb-3">
                        <label for="ddlRol" class="form-label">Rol</label>
                        <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-select"
                            OnSelectedIndexChanged="ddlRol_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="1" Text="Administrador"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Empleado"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Paciente"></asp:ListItem>
                        </asp:DropDownList>
                        <span class="text-danger" id="errorDdlRol"></span>
                    </div>

                    <!-- Campos dinámicos según el Rol -->
                    <div id="legajoDiv" class="mb-3" style="display: none;">
                        <label for="txtLegajo" class="form-label">Legajo</label>
                        <asp:TextBox ID="txtLegajo" runat="server" CssClass="form-control" />
                        <span class="text-danger" id="errorLegajo"></span>
                    </div>

                    <div id="matriculaDiv" class="mb-3" style="display: none;">
                        <label for="txtMatricula" class="form-label">Matrícula</label>
                        <asp:TextBox ID="txtMatricula" runat="server" CssClass="form-control" />
                        <span class="text-danger" id="errorMatricula"></span>
                    </div>

                    <div id="especialidadDiv" class="mb-3" style="display: none;">
                        <label for="ddlEspecialidad" class="form-label">Especialidad</label>
                        <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select">
                            <asp:ListItem Value="1">Especialidad 1</asp:ListItem>
                            <asp:ListItem Value="2">Especialidad 2</asp:ListItem>
                            <asp:ListItem Value="3">Especialidad 3</asp:ListItem>
                        </asp:DropDownList>
                        <span class="text-danger" id="errorEspecialidad"></span>
                    </div>

                    <div id="obraSocialDiv" class="mb-3" style="display: none;">
                        <label for="txtObraSocial" class="form-label">Obra Social</label>
                        <asp:TextBox ID="txtObraSocial" runat="server" CssClass="form-control" />
                        <span class="text-danger" id="errorObraSocial"></span>
                    </div>
                    <div id="nroAfiliadoDiv" class="mb-3" style="display: none;">
                        <label for="txtNroAfiliado" class="form-label">Número Afiliado</label>
                        <asp:TextBox ID="txtNroAfiliado" runat="server" CssClass="form-control" />
                        <span class="text-danger" id="errorNroAfiliado"></span>
                    </div>

                    <!-- Jornada Laboral -->
                    <div id="jornadaLaboralDiv" class="border-top mt-4 pt-3" style="display: none;">
                        <asp:DropDownList ID="ddlSede" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Seleccionar Sede" Value="" />

                    </asp:DropDownList>
                        <h2>Registrar Jornada Laboral Semanal</h2>
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label class="form-label">Lunes</label>
                                <div class="small">
                                    <label for="ddlInicioLunes" class="form-label">Inicio</label>
                                    <asp:DropDownList ID="ddlInicioLunes" runat="server" CssClass="form-select">
                                        <asp:ListItem Value="" Text="--:--" />
                                    </asp:DropDownList>
                                </div>
                                <div class="small">
                                    <label for="ddlFinLunes" class="form-label">Fin</label>
                                    <asp:DropDownList ID="ddlFinLunes" runat="server" CssClass="form-select">
                                        <asp:ListItem Value="" Text="--:--" />
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-6 mb-3">
                                <label class="form-label">Martes</label>
                                <div class="small">
                                    <label for="ddlInicioMartes" class="form-label">Inicio</label>
                                    <asp:DropDownList ID="ddlInicioMartes" runat="server" CssClass="form-select">
                                        <asp:ListItem Value="" Text="--:--" />
                                    </asp:DropDownList>
                                </div>
                                <div class="small">
                                    <label for="ddlFinMartes" class="form-label">Fin</label>
                                    <asp:DropDownList ID="ddlFinMartes" runat="server" CssClass="form-select">
                                        <asp:ListItem Value="" Text="--:--" />
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-6 mb-3">
                                <label class="form-label">Miércoles</label>
                                <div class="small">
                                    <label for="ddlInicioMiercoles" class="form-label">Inicio</label>
                                    <asp:DropDownList ID="ddlInicioMiercoles" runat="server" CssClass="form-select">
                                        <asp:ListItem Value="" Text="--:--" />
                                    </asp:DropDownList>
                                </div>
                                <div class="small">
                                    <label for="ddlFinMiercoles" class="form-label">Fin</label>
                                    <asp:DropDownList ID="ddlFinMiercoles" runat="server" CssClass="form-select">
                                        <asp:ListItem Value="" Text="--:--" />
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-6 mb-3">
                                <label class="form-label">Jueves</label>
                                <div class="small">
                                    <label for="ddlInicioJueves" class="form-label">Inicio</label>
                                    <asp:DropDownList ID="ddlInicioJueves" runat="server" CssClass="form-select">
                                        <asp:ListItem Value="" Text="--:--" />
                                    </asp:DropDownList>
                                </div>
                                <div class="small">
                                    <label for="ddlFinJueves" class="form-label">Fin</label>
                                    <asp:DropDownList ID="ddlFinJueves" runat="server" CssClass="form-select">
                                        <asp:ListItem Value="" Text="--:--" />
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-6 mb-3">
                                <label class="form-label">Viernes</label>
                                <div class="small">
                                    <label for="ddlInicioViernes" class="form-label">Inicio</label>
                                    <asp:DropDownList ID="ddlInicioViernes" runat="server" CssClass="form-select">
                                        <asp:ListItem Value="" Text="--:--" />
                                    </asp:DropDownList>
                                </div>
                                <div class="small">
                                    <label for="ddlFinViernes" class="form-label">Fin</label>
                                    <asp:DropDownList ID="ddlFinViernes" runat="server" CssClass="form-select">
                                        <asp:ListItem Value="" Text="--:--" />
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-6 mb-3">
                                <label class="form-label">Sábado</label>
                                <div class="small">
                                    <label for="ddlInicioSabado" class="form-label">Inicio</label>
                                    <asp:DropDownList ID="ddlInicioSabado" runat="server" CssClass="form-select">
                                        <asp:ListItem Value="" Text="--:--" />
                                    </asp:DropDownList>
                                </div>
                                <div class="small">
                                    <label for="ddlFinSabado" class="form-label">Fin</label>
                                    <asp:DropDownList ID="ddlFinSabado" runat="server" CssClass="form-select">
                                        <asp:ListItem Value="" Text="--:--" />
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-6 mb-3">
                                <label class="form-label">Domingo</label>
                                <div class="small">
                                    <label for="ddlInicioDomingo" class="form-label">Inicio</label>
                                    <asp:DropDownList ID="ddlInicioDomingo" runat="server" CssClass="form-select">
                                        <asp:ListItem Value="" Text="--:--" />
                                    </asp:DropDownList>
                                </div>
                                <div class="small">
                                    <label for="ddlFinDomingo" class="form-label">Fin</label>
                                    <asp:DropDownList ID="ddlFinDomingo" runat="server" CssClass="form-select">
                                        <asp:ListItem Value="" Text="--:--" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                    </div>


                    <!-- Información personal -->
                    <div class="mb-3">
                        <label for="txtNombre" class="form-label">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                        <span class="text-danger" id="errorNombre"></span>
                    </div>

                    <div class="mb-3">
                        <label for="txtApellido" class="form-label">Apellido</label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
                        <span class="text-danger" id="errorApellido"></span>
                    </div>

                    <div class="mb-3">
                        <label for="txtFechaNacimiento" class="form-label">Fecha Nacimiento</label>
                        <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control" TextMode="Date" />
                        <span class="text-danger" id="errorFechaNac"></span>
                    </div>

                    <div class="mb-3">
                        <label for="txtDocumento" class="form-label">Documento</label>
                        <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" />
                        <span class="text-danger" id="errorDni"></span>
                    </div>

                    <div class="mb-3">
                        <label for="txtEmailPersonal" class="form-label">Email Personal</label>
                        <asp:TextBox ID="txtEmailPersonal" runat="server" CssClass="form-control" />
                        <span class="text-danger" id="errorEmail"></span>
                    </div>

                    <div class="mb-3">
                        <label for="txtTelefono" class="form-label">Teléfono</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                    </div>

                    <!-- Sección de Domicilio -->
                    <div class="border-top mt-4 pt-3">
                        <h4 class="mb-3">Domicilio</h4>
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
                    </div>

                    <div class="mt-4 text-center">
                        <asp:Button ID="btnCrear" runat="server" Text="Crear Usuario" CssClass="btn btn-primary btn-lg"
                            OnClientClick="return validarFormulario(event);" OnClick="btnCrear_Click" />
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>


    <script type="text/javascript">

        function mostrarMensaje(titulo, texto) {
            const mensajeDiv = document.getElementById("mensaje");
            const mensajeTitulo = document.getElementById("mensajeTitulo");
            const mensajeTexto = document.getElementById("mensajeTexto");

            mensajeTitulo.textContent = titulo;
            mensajeTexto.textContent = texto;

            mensajeDiv.style.display = "block";

            setTimeout(() => {
                mensajeDiv.style.display = "none";
            }, 3000);
        }

        function cerrarMensaje() {
            document.getElementById("mensaje").style.display = "none";
        }

        function validarFormulario(event) {
            let esValido = true;

            const rol = document.getElementById('<%= ddlRol.ClientID %>');
            const nombre = document.getElementById('<%= txtNombre.ClientID %>');
            const apellido = document.getElementById('<%= txtApellido.ClientID %>');
            const fechaNacimiento = document.getElementById('<%= txtFechaNacimiento.ClientID %>');
            const documento = document.getElementById('<%= txtDocumento.ClientID %>');
            const emailPersonal = document.getElementById('<%= txtEmailPersonal.ClientID %>');
            const obraSocial = document.getElementById('<%= txtObraSocial.ClientID %>');
            const numeroAfiliado = document.getElementById('<%= txtNroAfiliado.ClientID %>');
           <%-- const calle = document.getElementById('<%= txtCalle.ClientID %>');
            const numero = document.getElementById('<%= txtNumero.ClientID %>');
            const piso = document.getElementById('<%= txtPiso.ClientID %>');
            const depto = document.getElementById('<%= txtDepto.ClientID %>');
            const localidad = document.getElementById('<%= txtLocalidad.ClientID %>');
            const provincia = document.getElementById('<%= txtProvincia.ClientID %>');
            const codigoPostal = document.getElementById('<%= txtCodigoPostal.ClientID %>');--%>
            const matricula = document.getElementById('<%= txtMatricula.ClientID %>');
            const especialidad = document.getElementById('<%= ddlEspecialidad.ClientID %>');

            const errorRol = document.getElementById("errorDdlRol");
            const errorNombre = document.getElementById("errorNombre");
            const errorApellido = document.getElementById("errorApellido");
            const errorFechaNac = document.getElementById("errorFechaNac");
            const errorDni = document.getElementById("errorDni");
            const errorEmail = document.getElementById("errorEmail");
            const errorNumeroAfiliado = document.getElementById("errorNroAfiliado");
            const errorObraSocial = document.getElementById("errorObraSocial");
            //const errorCalle = document.getElementById("errorCalle");
            //const errorNumero = document.getElementById("errorNumero");
            //const errorLocalidad = document.getElementById("errorLocalidad");
            //const errorProvincia = document.getElementById("errorProvincia");
            //const errorCodigoPostal = document.getElementById("errorCodigoPostal");

            errorRol.textContent = "";
            errorNombre.textContent = "";
            errorApellido.textContent = "";
            errorFechaNac.textContent = "";
            errorDni.textContent = "";
            errorEmail.textContent = "";
            errorNumeroAfiliado.textContent = "";
            errorObraSocial.textContent = "";
            //errorCalle.textContent = "";
            //errorNumero.textContent = "";
            //errorLocalidad.textContent = "";
            //errorProvincia.textContent = "";
            //errorCodigoPostal.textContent = "";

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

            // Validar Fecha de Nacimiento
            if (!fechaNacimiento.value) {
                errorFechaNac.textContent = "La fecha de nacimiento es obligatoria.";
                esValido = false;
            }

            // Validar Documento
            if (!documento.value) {
                errorDni.textContent = "El documento es obligatorio.";
                esValido = false;
            } else if (documento.value.length != 8) {
                errorDni.textContent = "El documento debe contener 8 dígitos.";
                esValido = false;
            }

            // Validar Email
            if (!emailPersonal.value.trim()) {
                errorEmail.textContent = "El correo electrónico es obligatorio.";
                esValido = false;
            } else if (!emailPersonal.value.includes('@') || !emailPersonal.value.includes('.')) {
                errorEmail.textContent = "El correo electrónico debe tener un formato válido.";
                esValido = false;
            }

            // Validar Matrícula (solo si la posición es "Médico")
            if (rol.value === "2" && !matricula.value.trim()) {
                document.getElementById("errorMatricula").textContent = "La matrícula es obligatoria para médicos.";
                esValido = false;
            }

            // Validar Especialidad (solo si la posición es "Médico")
            if (rol.value === "2" && !especialidad.value) {
                document.getElementById("errorEspecialidad").textContent = "La especialidad es obligatoria para médicos.";
                esValido = false;
            }

            if (rol.value === "5" && !obraSocial.value) {
                document.getElementById("errorObraSocial").textContent = "La obra social es obligatoria para pacientes.";
                esValido = false;
            }
            if (rol.value === "5" && !numeroAfiliado.value) {
                document.getElementById("errorNroAfiliado").textContent = "El número de afiliado es obligatorio para pacientes.";
                esValido = false;
            }

            if (!esValido) {
                event.preventDefault();
                return esValido
            }
            return esValido;
        }

        function mostrarMensaje(mensaje, titulo) {
            document.getElementById("mensajeTexto").textContent = mensaje;
            document.getElementById("mensajeTitulo").textContent = titulo;
            document.getElementById("mensaje").style.display = "block"; // Mostrar el mensaje
        }

        function cerrarMensaje() {
            document.getElementById("mensaje").style.display = "none"; // Ocultar el mensaje
        }


        function toggleFields() {
            debugger
            var role = document.getElementById('<%= ddlRol.ClientID %>').value;
            document.getElementById('matriculaDiv').style.display = 'none';
            document.getElementById('especialidadDiv').style.display = 'none';

            if (role == "2") { // 2 es Medico

                document.getElementById('matriculaDiv').style.display = 'block';
                document.getElementById('especialidadDiv').style.display = 'block';
                document.getElementById('jornadaLaboralDiv').style.display = 'block';
                
            }
            

            //document.getElementById('legajoDiv').style.display = 'block';
            if (role == "5") { //5 es paciente
                document.getElementById('obraSocialDiv').style.display = 'block';
                document.getElementById('nroAfiliadoDiv').style.display = 'block';
            }
        }

        window.onload = function () {
            document.getElementById('<%= ddlRol.ClientID %>').addEventListener('change', toggleFields);
            document.getElementById('<%= ddlRol.ClientID %>').addEventListener('change', toggleFields);

            toggleFields();
        };

    </script>

</asp:Content>
