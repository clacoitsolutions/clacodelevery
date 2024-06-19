using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using static DeliveryBoy.Models.PropertyClass;

namespace DeliveryBoy.Models
{
    public class LogicClass
    {

        dbHelper objdb = new dbHelper();

        public object Session { get; private set; }

        public DataTable getLoginDetails(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UserName",Objp.UserName),
                new SqlParameter("@Password",Objp.Password),
                new SqlParameter("@Action",Objp.Action),
               
            };

            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable GetSalerDetails(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@Deliveryid",Objp.SallerId),

            };
            dtt = objdb.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        internal DataSet GetCustomerDashBoard(PropertyClass Objp, string ProcName, string OrderId)
        {
            DataSet dt = new DataSet();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@CustomerId",Objp.CustomerId),
                new SqlParameter("@deliveryboy",Objp.deliveryboy),
                new SqlParameter("@MobileNo",Objp.MobileNo),
                new SqlParameter("@orderId",OrderId)
            };
            dt = objdb.ExecProcDataSet(ProcName, parm);
            return dt;
        }

        internal DataTable InsertCancelRequest(PropertyClass Objp, string ProcName)
        {
            string otp = Objp.otp;
           // string a = Objp.PaymentResponse;
            DateTime? delDate = null;
            if (!string.IsNullOrEmpty(Objp.mDate))
            {
                delDate = Convert.ToDateTime(Objp.mDate);
            }
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@OrderId",Objp.OrderId),
                 new SqlParameter("@deliveryboy",Objp.SallerId),
                 new SqlParameter("@Paymode",Objp.PayMode),
                  new SqlParameter("@Deliverystatus",Objp.Status),
                new SqlParameter("@CancelReason",Objp.Description),
              new SqlParameter("@Otp",Objp.otp),
               // new SqlParameter("@updatedate",delDate),
              
            };
            dt = objdb.ExecProcDataTable(ProcName, parm);
            return dt;
        }


        // here  code for devlivery boy contact by muskan
        public DataTable deliveryContactUs(DeliveryContactUs DCon, string ProcName)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
               new SqlParameter("@Role",DCon.Role),
       new SqlParameter("@Action", DCon.Action),
       new SqlParameter("@FullName", DCon.FullName),
       new SqlParameter("@Mobile", DCon.MobileNo),
       new SqlParameter("@Email", DCon.EmailAddress),
       new SqlParameter("@Subject",DCon.Subject),
       new SqlParameter("@OrderId",DCon.OrderId),
       new SqlParameter("@Msg", DCon.Message),
       new SqlParameter("@ContactId",DCon.ContactId)
                };
                return objdb.ExecProcDataTable(ProcName, param);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw;
            }
        }



    }
}