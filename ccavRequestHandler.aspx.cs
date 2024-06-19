using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace DeliveryBoy
{
    public partial class ccavRequestHandler : System.Web.UI.Page
    {

        //CCACrypto ccaCrypto = new CCACrypto();
        string workingKey = "92A4E7045F525F426B94913122D361E5";//put in the 32bit alpha numeric key in the quotes provided here 	
        string ccaRequest = "";
        public string strEncRequest = "";
        public string strAccessCode = "AVKK82LD94CL83KKLC";// put the access key in the quotes provided here.
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //  1714823441426
                //var amt = Convert.ToInt64(Request.Form["amount"]);
                //var orderid= Request.Form["order_id"];
                //var amt = Convert.ToInt64(Request.Form["amount"]);
                //var orderid = Request.Form["order_id"];
                string amt = Request.QueryString["NetTotal"];
                string orderid = Request.QueryString["OrderId"];

                // Construct the query string properly
                var cc = "tid=&merchant_id=3396203&order_id=" + orderid + "&amount=" + amt + "&currency=INR&redirect_url=http://192.168.0.89/MCPG.ASP.net.2.0.kit/ccavResponseHandler.aspx&cancel_url=http://192.168.0.96/mcpg_new/iframe/ccavResponseHandler.aspx";



                ccaRequest = cc;
                

                //strEncRequest = ccaCrypto.Encrypt(ccaRequest, workingKey);
            }
        }
    }
}