using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class setting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            Txtfirstname.Text = Session["Name"].ToString();
            TxtEmail.Text = Session["Email"].ToString();
            TxtPhone.Text = Session["ContactNo"].ToString();        
            First.Attributes.Add("class", "active");
            TxtEmail.Attributes.Add("class", "active");
            Email.Attributes.Add("class", "active");
            Phone.Attributes.Add("class", "active");
            //Address.Attributes.Add("class", "active");
        }
    }
}