using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
    
/// <summary>
/// Summary description for AutoComplete
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class AutoComplete : System.Web.Services.WebService
{
  //  Product p = new Product();
    [WebMethod]

    public string[] GetConsignee(string prefixText)
    {
        string strSql = "select Patient_Name, Patient_Id from Tbl_PatientRegistration where Patient_Name LIKE '" + prefixText + "%'  ORDER BY Patient_Name";
        DataSet dtst = new DataSet();
        SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Conn"].ConnectionString.ToString());

        SqlCommand sqlComd = new SqlCommand(strSql, cn);
        cn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(dtst);
        string[] cntName = new string[dtst.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in dtst.Tables[0].Rows)
            {
                cntName.SetValue(rdr["Patient_Name"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            cn.Close();
        }
        return cntName;
    }
}