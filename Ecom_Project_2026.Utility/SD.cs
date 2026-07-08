using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Project_2026.Utility
{
    public static class SD
    {
        //SP Cover Type
        public const string SP_GetCoverTypes = "GetCoverTypes";
        public const string SP_GetCoverType = "GetCoverType";
        public const string SP_CreateCoverType = "CreateCoverType";
        public const string SP_UpdateCoverType = "UpdateCoverType";
        public const string SP_DeleteCoverType = "DeleteCoverType";

        //SP Category
        public const string SP_GetCategories = "GetCategories";
        public const string SP_GetCategory = "GetCategory";
        public const string SP_CreateCategory = "CreateCategory";
        public const string SP_UpdateCategory = "UpdateCategory";
        public const string SP_DeleteCategory = "DeleteCategory";

        //SP Products
        public const string SP_GetProducts = "GetProducts";
        public const string SP_GetProduct = "GetProduct";
        public const string SP_CreateProduct = "CreateProduct";
        public const string SP_UpdateProduct = "UpdateProduct";
        public const string SP_DeleteProduct = "DeleteProduct";


        //Roles
        public const string Role_Admin = "Admin";
        public const string Role_Individual = "Individual";
        public const string Role_Employee = "Employee User";
        public const string Role_Company = "Company User";

        //Order Status
        public const string OrderStatusPending = "Pending";
        public const string OrderStatusApproved = "Approved";
        public const string OrderStatusInProgress = "Processing";
        public const string OrderStatusShipped = "Shipped";
        public const string OrderStatusCancelled = "Cancelled";
        public const string OrderStatusRefunded = "Refunded";

        //Payment Status
        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusDelayPayment = "PaymentStatusDelay";
        public const string PaymentStatusRejected = "Rejected";

        //Session
        public const string Ss_CartSessionCount = "Cart Count Session";

        public static double GetPriceBasedOnQuantity(double quantity, double price, double price50, double price100)
        {
            if(quantity < 50)
                return price;
            else if(quantity >100)
                return price50;
            else return price100;
        }
    }
}
