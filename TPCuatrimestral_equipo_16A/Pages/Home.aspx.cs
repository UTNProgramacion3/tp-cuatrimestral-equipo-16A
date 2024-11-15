using Business.Managers;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipo_16A.Pages
{
	public partial class Home : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

        protected void Button1_Click(object sender, EventArgs e)
        {
			var email = new EmailManager(new DBManager());

			email.EnviarEmail("escuderopablo.m@gmail.com", Title, "Hola, este es un mensaje de prueba");
        }
    }
}