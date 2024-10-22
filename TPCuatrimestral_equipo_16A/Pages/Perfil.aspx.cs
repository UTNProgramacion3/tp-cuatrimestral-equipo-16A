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

    }
}