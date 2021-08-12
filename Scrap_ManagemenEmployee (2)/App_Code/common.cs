using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Net;
using System.Web.UI;
using System.Security.Cryptography;

/// <summary>
/// Summary description for common
/// </summary>
public abstract class AbstApiDB
{
    public abstract DataSet ByProcedure(string ProcedureName, string[] Parameter, string[] Values, string ByDataSetAlert);   
    public abstract DataSet LoginChk(string UserID, string Password, string UserType);
    public abstract string Alert(string icon, string AlertClass, string Heading, string Description);
    public abstract void EmptyTextBoxes(Control parent);
    public abstract string getDate(string InComingDate);
    public abstract string base64Encode(string sData);
    public abstract string base64Decod(string sData);
    public abstract DataSet ByQuery(string Query);
    public abstract bool isNumber(string s);
    public abstract bool isDecimal(string s);
    public abstract DataSet GetData(string sql);

    public abstract DataSet GetDatabase(string ProcedureName, string[] Parameter, string[] Value);
    public abstract string GetIPAddress();

    public string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI992";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
    public string SHA512_HASH(string rawData)
    {
        //Create a SHA512   
        using (SHA512 sha512Hash = SHA512.Create())
        {
            // ComputeHash - returns byte array  
            byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

   
}

public class common : AbstApiDB
{
    public string Cn;
    public DataSet ds;
    public DataSet dsnew;
    public DataTable dt;
    public SqlCommand cmd;
    public string _ErrorMessage;
    public string ErrorMessage
    {
        get { return _ErrorMessage; }
        set { _ErrorMessage = value; }
    }
    public string getconnection
    {
        get
        {
            try
            {
                Cn = System.Configuration.ConfigurationManager.ConnectionStrings["Conn"].ConnectionString.ToString();
            }
            catch { ErrorMessage = "Yes"; throw new Exception("Please Provide Connection Object Name:Conn"); }

            return Cn;
        }
    }
   
    public override string base64Encode(string sData)
    {
        try
        {
            byte[] encData_byte = new byte[sData.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
        catch (Exception ex)
        {
            throw new Exception("Error in base64Encode" + ex.Message);
        }
    }
    public override string base64Decod(string sData)
    {
        string result = "";
        try
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(sData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            result = new String(decoded_char);
        }
        catch (Exception ex)
        {
            throw new Exception("Error in base64Decode" + ex.Message);
        }
        return result;
    }
    public override void EmptyTextBoxes(Control parent)
    {

        // Loop through all the controls on the page

        foreach (Control c in parent.Controls)
        {

            // Check and see if it's a textbox

            if ((c.GetType() == typeof(TextBox)))
            {

                // Since its a textbox clear out the text  

                ((TextBox)(c)).Text = "";

            }
            else if ((c.GetType() == typeof(DropDownList)))
            {

                // Since its a textbox clear out the text  

                if (((DropDownList)(c)).Items.Count > 0)
                {
                    ((DropDownList)(c)).SelectedIndex = 0;
                }

            }
            else if ((c.GetType() == typeof(RadioButton)))
            {
                ((RadioButton)(c)).Checked = false;
            }


            // Now we need to call itself (recursive) because

            // all items (Panel, GroupBox, etc) is a container

            // so we need to check all containers for any

            // textboxes so we can clear them

            if (c.HasControls())
            {

                EmptyTextBoxes(c);

            }

        }
    }



    public override DataSet ByProcedure(string ProcedureName, string[] Parameter, string[] Values, string ByDataSetAlert)
    {

        try
        {
            using (SqlConnection cn = new SqlConnection(getconnection))
            {
                cmd = new SqlCommand(ProcedureName, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < Parameter.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@" + Parameter[i].ToString(), Values[i].ToString());
                }
                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {
                    ds = new DataSet();
                    adp.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (ErrorMessage != "Yes")
            {

                ds.Dispose();
                cmd.Dispose();
            }
        }
        return ds;
    }
    
    public common()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public override DataSet LoginChk(string UserID, string Password, string UserType)
    {

        //int result = 0;
        using (SqlConnection con = new SqlConnection(String.Format(getconnection)))
        {
            DataSet ds = new DataSet();

            using (SqlCommand cmd = new SqlCommand("CheckLogin", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = UserID;
                cmd.Parameters.Add("@UserPswd", SqlDbType.VarChar).Value = Password;
                cmd.Parameters.Add("@UserType", SqlDbType.VarChar).Value = UserType;

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(ds);
                return ds;

            }

        }
    }
    public override string Alert(string icon, string AlertClass, string Heading, string Description)
    {
        return "<div class='box-body'> <div class='alert " + AlertClass + " alert-dismissible'> <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>x</button> <h4><i class='icon fa " + icon + "'></i>" + Heading + "</h4>" + Description + "</div> </div>";

    }
    public override string getDate(string InComingDate)
    {
        string[] slipDate = InComingDate.Split("".ToCharArray());
        string FinalDate = slipDate[0];
        string[] FDate = FinalDate.Split("/".ToCharArray());
        string Date_Final = FDate[1] + "/" + FDate[0] + "/" + FDate[2];
        return Date_Final;

    }
    public override DataSet ByQuery(string Query)
    {
        try
        {

            using (SqlConnection cn = new SqlConnection(getconnection))
            {
                using (SqlDataAdapter adp = new SqlDataAdapter())
                {
                    adp.SelectCommand = new SqlCommand(Query, cn);
                    ds = new DataSet();
                    adp.Fill(ds);
                }
            }

        }
        catch (Exception ex)
        {
            //WriteLog(" Error Msg :" + ex.Message + "\n" + "Event Info :" + ex.StackTrace);
        }
        finally
        {
            if (ErrorMessage != "Yes")
            {
                //cmd.Dispose();
                ds.Dispose();
            }
        }
        return ds;
    }
    public override bool isNumber(string s)
    {
        int num;
        return Int32.TryParse(s, out num);
    }
    public override bool isDecimal(string s)
    {
        int num;
        double num1;
        if (Int32.TryParse(s, out num))
        {
            return Int32.TryParse(s, out num);
        }
        else
        {
            return double.TryParse(s, out num1);
        }
    }
    public override DataSet GetData(string sql)
    {
        DataSet ds1 = new DataSet();
        try
        {
            using (SqlConnection cn = new SqlConnection(getconnection))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds1);
            }
        }
        catch
        {
        }
        return ds1;
    }
    public override DataSet GetDatabase(string ProcedureName, string[] Parameter, string[] Value)
    {
        DataSet ds = new DataSet();
        try
        {
            using (SqlConnection cn = new SqlConnection(getconnection))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(ProcedureName, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < Parameter.Length; i++)
                {
                    cmd.Parameters.AddWithValue('@' + Parameter[i].ToString(), Value[i].ToString());
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
        }
        catch
        {
        }
        return ds;
    }
    public DataSet GetData(string ProcedureName, string[] Parameter, string[] Value)
    {
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(getconnection);
        con.Open();
        SqlCommand cmd = new SqlCommand(ProcedureName, con);
        cmd.CommandType = CommandType.StoredProcedure;
        for (int i = 0; i < Parameter.Length; i++)
        {
            cmd.Parameters.AddWithValue('@' + Parameter[i].ToString(), Value[i].ToString());
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return ds;
    }
    public void SetData(string ProcedureName, string[] Parameter, string[] Value)
    {
        SqlConnection con = new SqlConnection(getconnection);
        con.Open();
        SqlCommand cmd = new SqlCommand(ProcedureName, con);
        cmd.CommandType = CommandType.StoredProcedure;
        for (int i = 0; i < Parameter.Length; i++)
        {
            cmd.Parameters.AddWithValue('@' + Parameter[i].ToString(), Value[i].ToString());
        }
        cmd.ExecuteNonQuery();
        con.Close();
    }
    public override string GetIPAddress()
    {
        string IPAddress = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
        return IPAddress;
    }
   
}