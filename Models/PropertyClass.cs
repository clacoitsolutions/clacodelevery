using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DeliveryBoy.Models
{
    public class PropertyClass
    {
        public string otp {  get; set; }
        public string strId { get; set; }
        public string msg { get; set; }
        public string Action {  get; set; } 
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } 
        public string SallerId { get; set; }
        public string OrderId { get; set; }
        public string CustomerId { get; set; }

        public string CustomerName { get; set; }
        public string DeliveryStatus { get; set; }
        public string OrderStatus { get; set; }

        public string DeliveryTime { get; set; }
        public string PayMode { get; set; }
        public string paystatus { get; set; }
        public string PayableAmmount { get; set; }
        public string mDate { get; set; }
    
        public decimal deliverycharges { get;set; }
        public decimal GrossPayable { get; set; }
        public decimal NetTotal { get; set; }
        public decimal DiscAmt { get; set;}
        public decimal GrossTotal { get; set;}
        public string Status { get; set; }
        public string SSName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string eDate { get; set; }
        public string Description { get; set; }
        public string PaymentResponse { get; set; }
        public string ReturnDate { get; set; }
        public string ReturnReasondes { get; set; }
        public string EwayBillNo { get; set; }
        public decimal gstamount { get; set; }
        public decimal CoupenAmount { get; set;}
        public string ReturnAndRefundPolicy { get; set;}
        public  string deliveryboy {  get; set; }


        public string MobileNo {  get; set; }
        public string TotalQuantity {  get; set; }





        public DataTable dt1 { get; set; }
        public DataTable dtcombooffer { get; set; }


        // here  code for devlivery boy contact by muskan
        public class DeliveryContactUs
        {
            public string Role { get; set; }
            public string Action { get; set; }
            public string FullName { get; set; }
            public string MobileNo { get; set; }
            public string EmailAddress { get; set; }
            public string Message { get; set; }
            public string Subject { get; set; }
            public string OrderId { get; set; }
            public string ContactId { get; set; }
            public DataTable dtc { get; set; }

        }




    }
}