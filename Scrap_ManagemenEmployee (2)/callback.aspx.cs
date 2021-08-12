using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class callback : System.Web.UI.Page
{
    private readonly string _ClientId = "E-Work Permit MX Test";
    private readonly string _redirectUri = "http://localhost:3000/callback.aspx";
    private readonly string _clientSecret = "0qFuurT7tp4s8VrKjmpxz06CrgCGiutRrLzHaAXVJlDmaR17lU5NSRqENoHwL94b";
    private readonly string _tokenurl = "https://fedauthtst.pg.com/as/token.oauth2";
    private readonly string openId = "https://fedauthtst.pg.com/idp/userinfo.openid";

    //private readonly string _ClientId = "TWS MX";
    ////private readonly string _redirectUri = "http://mdp-digital.na.pg.com:206/callback.aspx";
    //private readonly string _redirectUri = "https://mdp-digital.na.pg.com:206/callback.aspx";
    //private readonly string _clientSecret = "88eGGRVoRvJOZtrRgS7AnuwC1wrYc9bdtRBNO8A4KgEpB0zsRWFdxIHLG4sY7uMV";
    //private readonly string _tokenurl = "https://fedauth.pg.com/as/token.oauth2";
    //private readonly string openId = "https://fedauth.pg.com/idp/userinfo.openid";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var code = Request.QueryString["code"].ToString();
            var client = new RestClient(_tokenurl);
            //var client = new RestClient("https://fedauth.pg.com/as/token.oauth2");
            client.Timeout = -1;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Cookie", "PF=MZYJXEJDZqua7QoNjQFsrU; PF=MZYJXEJDZqua7QoNjQFsrU");
            request.AddHeader("Cookie", "PF=noJCiEym0ghK0RUjZdQfOU");
            request.AddParameter("client_id", _ClientId);
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("redirect_uri", _redirectUri);
            request.AddParameter("code", code);
            request.AddParameter("client_secret", _clientSecret);
            request.AddParameter("scope", "openid profile");
            IRestResponse response = client.Execute(request);
            Token token = JsonConvert.DeserializeObject<Token>(response.Content);
            var AcessToken = token.access_token;
            var value = getUserDate(AcessToken);
            if (value.groups.Contains("GDS-MDPE-PlantAdmin") || value.groups.Contains("GDS-MDPE-PlantUser"))
            {
                var group = string.Empty;
                foreach (var item in value.groups)
                {
                    if (item == "GDS-MDPE-PlantAdmin")
                    {
                        group = "GDS-MDPE-PlantAdmin";
                        //Session["EMail"] = value.email;
                        //Session["userName"] = value.ShortName;
                        // Getlogin(value.email, value.ShortName);
                    }
                    else if (item == "GDS-MDPE-PlantUser")
                    {
                        group = "GDS-MDPE-PlantUser";
                        //Session["EMail"] = value.email;
                        //Session["userName"] = value.ShortName;
                        //Getlogin(value.email, value.ShortName);
                    }
                }
            }
            else
            {
                //Response.Redirect("http://localhost:4200/login?userName=" + value.ShortName + "&IsaccessGroup=false");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public Root getUserDate(string tokenf)
    {
        var client = new RestClient(openId);
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("Cache-Control", "no-cache");
        request.AddHeader("Authorization", "Bearer " + tokenf);
        request.AddHeader("Cookie", "PF=MZYJXEJDZqua7QoNjQFsrU");
        IRestResponse response = client.Execute(request);
        //Console.WriteLine(response.Content);
        //user.InnerText = response1.Content;
        var a = response.Content;
        //a = a.Replace("}", "}]");
        //a = a.Replace("\r\n", "");
        JObject json = JObject.Parse(a);
        var replacevalue = json.ToString().Replace("{{", "");// json.ToString().Replace("{{", "").Replace("}}", "");
        var result = "";
        Root myDetails = JsonConvert.DeserializeObject<Root>(response.Content);

        return myDetails;
    }

}

public class Token
{
    public string access_token { get; set; }
    public string id_token { get; set; }
    public string token_type { get; set; }
    public int expires_in { get; set; }
}
public class Root
{
    public string Uid { get; set; }
    public string sub { get; set; }
    public string Mail { get; set; }
    public string FirstName { get; set; }
    public List<string> groups { get; set; }
    public string ShortName { get; set; }
    public string LastName { get; set; }
    public string email { get; set; }
}