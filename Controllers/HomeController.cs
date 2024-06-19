using DeliveryBoy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Net;
using System.Reflection;
using System.Xml.Linq;
using static DeliveryBoy.Models.PropertyClass;

namespace DeliveryBoy.Controllers
{
    public class HomeController : Controller
    {
        //  Dbmanager dbManager = new Dbmanager();
        private readonly Dbmanager dbManager;

        PropertyClass objp = new PropertyClass();
        LogicClass objL = new LogicClass();




        public ActionResult Index()
        {
            List<PropertyClass> dataList = new List<PropertyClass>();

            try
            {

                if (Session["Username"] == null || Session["ContactNumber"] == null || Session["CompanyName"] == null)
                {
                    // Redirect the user to the SignIn action if session values are not set
                    return RedirectToAction("SignIn", "Home");
                }

                else
                {
                    objp.Action = "1";
                    objp.SallerId = Session["Username"] as string;

                    DataTable dt = objL.GetSalerDetails(objp, "GetSalerAssignDetails");
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        ViewBag.name = dt.Rows[0]["Name"].ToString();
                        ViewBag.mobile = dt.Rows[0]["ContactNo"].ToString();
                        ViewBag.address = dt.Rows[0]["Address"].ToString();
                        ViewBag.aadhar = dt.Rows[0]["AadharNo"].ToString();
                        ViewBag.pincode = dt.Rows[0]["Pincode"].ToString();
                        ViewBag.totalpayment = dt.Rows[0]["TotalNetPayable"].ToString();
                       



                        foreach (DataRow row in dt.Rows)
                        {
                            PropertyClass objp = new PropertyClass();
                            objp.OrderId = row["Orderid"].ToString();
                            objp.CustomerId = row["CustomerId"].ToString();
                            objp.DeliveryStatus = row["deliveryStatus"].ToString();
                            objp.OrderStatus = row["OrderStatus"].ToString();
                            objp.PayMode = row["PaymentMode"].ToString();
                            objp.paystatus = row["Paymentstatus"].ToString();
                            objp.DeliveryTime = row["DeliveryTime"].ToString();
                            objp.PayableAmmount = row["NetPayable"].ToString();

                            if (row.Table.Columns.Contains("TotalQuantity"))
                            {
                                // Check if TotalQuantity column has a non-null value
                                if (row["TotalQuantity"] != DBNull.Value)
                                {
                                    objp.TotalQuantity = row["TotalQuantity"].ToString();
                                }
                                else
                                {
                                    // Handle null value case
                                    // For example, you can assign a default value or handle it based on your requirement
                                    objp.TotalQuantity = "N/A";
                                }
                            }
                            else
                            {
                                // Handle case where TotalQuantity column doesn't exist in DataRow
                                // For example, log an error or handle it based on your requirement
                                Console.WriteLine("TotalQuantity column not found in DataRow.");
                            }

                            objp.GrossPayable = Convert.ToDecimal(row["GrossAmount"]);



                            dataList.Add(objp);
                        }
                      


                    }


                    else

                    {

                        ViewBag.msg = "Invalid User";

                    }




                }
            }

            catch (Exception ex)

            {



            }






            return View(dataList);
        }

        public ActionResult About()
        {

            if (Session["Username"] == null || Session["ContactNumber"] == null || Session["CompanyName"] == null)
            {
                // Redirect the user to the SignIn action if session values are not set
                return RedirectToAction("SignIn", "Home");
            }

            else
            {
                ViewBag.Message = "Your application description page.";
            }

            return View();
        }




        // here  code for devlivery boy contact by muskan
        public ActionResult Contact()
        {
            if (Session["Username"] == null || Session["ContactNumber"] == null || Session["CompanyName"] == null)
            {

                return RedirectToAction("SignIn", "Home");
            }

            else
            {




            }

            return View();
        }
        [HttpPost]
        public ActionResult Contact(DeliveryContactUs objCls)
        {

            try
            {
                string role = Convert.ToString(Session["Role"]);
                objCls.Role = role;

                string ContactId = Convert.ToString(Session["Username"]);
                objCls.ContactId = ContactId;
                //string Role = Session["Role"]?.ToString();
                //objCls.Role = Role;

                objCls.Action = "1";

                DataTable dt = objL.deliveryContactUs(objCls, "proc_AllContactDetails");

                if (dt != null && dt.Rows.Count > 0)
                {
                    string message = dt.Rows[0]["msg"].ToString();
                    if (message == "Successfully submitted your Query We will contact you soon")
                    {
                        TempData["SuccessMessage"] = message;
                        //  Response.Write("<script>alert('" + message + "')</script>");
                        return RedirectToAction("Contact", "Home");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Some error occurred: " ;
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Some error occurred: No data returned from the database.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An exception occurred: ";  // Log the exception
            }

            // Pass the object as a single-item list to the view
            return View(new List<DeliveryContactUs> { objCls });
        }









        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult SignIn()
        {

            return View();
        }
        [HttpPost]
        public ActionResult SignIn(string txtname, string txtpassword)


        {
            try
            {
                objp.Action = "9";
                objp.UserName = txtname;
                objp.Password = txtpassword;

                //objp.Role="9";

                DataTable dt = objL.getLoginDetails(objp, "Proc_GetLoginDetails");

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    // Assuming dt has only one row for simplicity, adjust this accordingly if needed

                    if (row["msg"].ToString() == "Password Not matched")
                    {
                        ViewBag.msg = "Invalid user";

                    }

                    else
                    {

                        // Store values in session variables
                        Session["Username"] = row["username"].ToString();
                        Session["Password"] = row["password"].ToString();
                        Session["ContactNumber"] = row["ContactNo"].ToString();
                        Session["CompanyName"] = row["companyname"].ToString();
                        Session["address"] = row["company_Address"].ToString();
                        ViewBag.msg = row["msg"].ToString(); ;

                        //here session role get by muskan
                        Session["Role"] = row["Role"].ToString();


                        // Redirect to another action method or view
                        return RedirectToAction("index", "Home");



                    }

                }
                else
                {



                    //Handle case where login details are not found
                    return RedirectToAction("Sign In", "Home");
                }
            }
            catch (Exception ex)
            {


            }




            return View();
        }


        public ActionResult ManageOrder(string OrderId)
        {


            List<PropertyClass> propertyList = new List<PropertyClass>();
            PropertyClass objp = new PropertyClass();

            if (Session["Username"] == null || Session["ContactNumber"] == null || Session["CompanyName"] == null)
            {
                // Redirect the user to the SignIn action if session values are not set
                return RedirectToAction("SignIn", "Home");
            }

            else
            {


                try
                {
                    if (!string.IsNullOrEmpty(OrderId))
                    {
                        if (Convert.ToString(Session["Role"]) == "9")
                        {
                            // Code for Role 9
                        }
                        else
                        {
                            // Code for other roles
                        }

                        objp.Action = "3";
                        objp.CustomerId = OrderId;
                        DataSet ds = objL.GetCustomerDashBoard(objp, "proc_BindCustomerDashBoard", OrderId);
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            ViewBag.OrderId = ds.Tables[0].Rows[0]["OrderId"].ToString();
                            ViewBag.mDate = ds.Tables[0].Rows[0]["OrdDate"].ToString();
                            ViewBag.GrossPayable = Convert.ToDecimal(ds.Tables[0].Rows[0]["GrossAmount"].ToString());
                            ViewBag.deliverycharges = Convert.ToDecimal(ds.Tables[0].Rows[0]["DeliveryCharges"].ToString());
                            ViewBag.NetTotal = Convert.ToDecimal(ds.Tables[0].Rows[0]["NetPayable"].ToString());
                            ViewBag.DiscAmt = Convert.ToDecimal(ds.Tables[0].Rows[0]["DiscountAmount"].ToString());
                            ViewBag.PayMode = ds.Tables[0].Rows[0]["PaymentMode"].ToString();
                            ViewBag.Status = ds.Tables[0].Rows[0]["DeliveryStatus"].ToString();
                            ViewBag.SSName = ds.Tables[0].Rows[0]["Name"].ToString();
                            ViewBag.Address = ds.Tables[0].Rows[0]["FullAddress"].ToString();
                            ViewBag.ContactNo = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                            ViewBag.paystatus = ds.Tables[0].Rows[0]["Paystatus"].ToString();
                            ViewBag.eDate = ds.Tables[0].Rows[0]["CancelDate"].ToString();
                            ViewBag.Description = ds.Tables[0].Rows[0]["CancelReason"].ToString();
                            ViewBag.ReturnDate = ds.Tables[0].Rows[0]["ReturnDate"].ToString();
                            ViewBag.ReturnReasondes = ds.Tables[0].Rows[0]["ReturnReason"].ToString();
                            ViewBag.EwayBillNo = ds.Tables[0].Rows[0]["DeliveryDate"].ToString();
                            ViewBag.gstamount = Convert.ToDecimal(ds.Tables[0].Rows[0]["GstAmount"].ToString());
                            ViewBag.CoupenAmount = Convert.ToDecimal(ds.Tables[0].Rows[0]["CoupenAmount"].ToString());

                            // this Code  is Created By Abhimanyu Singh
                            ViewBag.ReturnAndRefundPolicy = ds.Tables[0].Rows[0]["ReturnAndRefundPolicy"].ToString();
                            ViewBag.DeliveryTime = ds.Tables[0].Rows[0]["DeliveryTime"].ToString();

                            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                            {
                                objp.dt1 = ds.Tables[1];
                            }
                            if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                            {
                                objp.dtcombooffer = ds.Tables[2];
                            }
                            propertyList.Add(objp);
                        }
                    }
                }
                catch (Exception ex)
                { }
            }
            // Return propertyList to the view
            return View(propertyList);
        }


        public ActionResult PaymentDetails()
        {
            return View();
        }

        public JsonResult updateOrderStatus(PropertyClass p)
        {
            try
            {
                //if (Session["Username"] == null || Session["ContactNumber"] == null || Session["CompanyName"] == null)
                //{
                //    // Redirect the user to the SignIn action if session values are not set
                //    return RedirectToAction("SignIn", "Home");
                //}



                p.SallerId = Session["Username"] as string;
                //p.OrderId=
                DataTable dt = objL.InsertCancelRequest(p, "Proc_InsertUpdateOrderstatus");
                if (dt != null && dt.Rows.Count > 0)
                {

                    objp.strId = "1";
                    objp.msg = dt.Rows[0]["msg"].ToString();
                    if (objp.strId == "1")
                    {
                        // objp.PayMode = dt.Rows[0]["payMode"].ToString();
                    }
                }
                else
                {
                    //objp.strId = "0";
                }

            }
            catch (Exception ex)
            {
                //{
                //    objp.strId = "0";
                //    objp.msg = ex.Message;
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {



            Session.Abandon();

            // Return JSON response indicating successful logout
            return RedirectToAction("SignIn", "Home");
        }

        public ActionResult customersupport()
        {
            return View();
        }
        public ActionResult privacypolicy()
        {
            return View();
        }
        public ActionResult termscondition() 
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult UnderMaintenance()
        {
            return View();
        }








    }
}