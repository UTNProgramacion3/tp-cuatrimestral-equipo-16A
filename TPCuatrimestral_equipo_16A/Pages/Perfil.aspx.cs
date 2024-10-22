using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipo_16A.Views
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if(!IsPostBack)
            {
                pass.Attributes["type"] = "password";

                //Aca deberia leer la contraseña de la base de datos
                pass.Attributes["value"] = "";

            }

        }

        protected void ChkBoxChecked(object sender, EventArgs e)
        {

            Session.Add("tempPass", pass.Value.ToString());

            
            if (passCheck.Checked)
            {

                pass.Attributes["type"] = "text";
            }
            else
            {

                pass.Attributes["type"] = "password";

            }
            
            RestorePass();



        }

        protected void RestorePass()
        {
            pass.Attributes["value"] = Session["tempPass"].ToString();
        }

        protected void BtnModificar_OnClick(object sender, EventArgs e)
        {
            user.Attributes.Remove("disabled");
            pass.Attributes.Remove("disabled");
            nombre.Attributes.Remove("disabled");
            apellido.Attributes.Remove("disabled");
            dni.Attributes.Remove("disabled");
            mail.Attributes.Remove("disabled");
            btnGuardar.Attributes.Remove("disabled");
            btnCargarImagen.Attributes.Remove("disabled");
        }

        protected void BtnGuardar_OnClick(object sender, EventArgs e)
        {
            user.Attributes.Add("disabled", "disabled");
            pass.Attributes.Add("disabled", "disabled");
            nombre.Attributes.Add("disabled", "disabled");
            apellido.Attributes.Add("disabled", "disabled");
            dni.Attributes.Add("disabled", "disabled");
            mail.Attributes.Add("disabled", "disabled");
            btnGuardar.Attributes.Add("disabled", "disabled");
            btnCargarImagen.Attributes.Add("disabled", "disabled");
        }

    }
}