using System;
using System.Collections.Generic;
//using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Configuration.ConfigurationManager;

namespace MvcApplicationReg.Models
{
  
    #region // Sales \\
    public class SalesData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public DateTime Orderdate { get; set; }
        public string ProductName { get; set; }
        public Decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public Decimal TotalPrice { get; set; }
        public Decimal TotalAmount { get; set; }
        public int TotalOrders { get; set; }
        public int OrderID { get; set; }
    }

    public class SalesModel
    {
        string objConn =  System.Configuration.ConfigurationManager.ConnectionStrings["testDBConn1"].ToString();

        public List<SalesData> getSalesData(int Searchtext)
        {
            List<SalesData> salesdata = new List<SalesData>();
            DataSet dsSalesDate = new DataSet();
            string sumObject;
            try
            {
                using (SqlConnection con = new SqlConnection(objConn))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SalesDate", con))
                    {
                        cmd.Parameters.AddWithValue("@id", Searchtext);
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dsSalesDate);
                            int count = dsSalesDate.Tables[0].Rows.Count;
                            sumObject = dsSalesDate.Tables[0].Compute("Sum(TOTALPRICE)", string.Empty).ToString();

                            //DataRow toInsert = dsSalesDate.Tables[0].NewRow();                            
                            //dsSalesDate.Tables[0].Rows.InsertAt(toInsert, count + 1);   
                            //toInsert["TOTALPRICE"] = sumObject;

                            DataTable dt = dsSalesDate.Tables[0];

                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    salesdata.Add(new SalesData
                                    {
                                        Name = dr["NAME"].ToString(),
                                        Id = Convert.ToInt16(dr["Id"]),
                                        Phone = dr["PHONE"].ToString(),
                                        Country = dr["ADRESS"].ToString(),
                                        TotalOrders = Convert.ToInt32(dr["TOTALORDERS"]),
                                        TotalPrice = Convert.ToDecimal(Convert.ToDecimal(dr["TOTALPRICE"]).ToString("#,##0.00")),
                                    });
                                }
                                salesdata.Add(new SalesData { TotalAmount = Convert.ToDecimal(Convert.ToDecimal(sumObject).ToString("#,##0.00")) });
                            }
                        }
                    }
                }
                return salesdata;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SalesData> getUserSalesData(int Searchtext)
        {
            List<SalesData> salesdata = new List<SalesData>();
            DataSet dsSalesDate = new DataSet();
            string sumObject;
            try
            {
                using (SqlConnection con = new SqlConnection(objConn))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SalesDate", con))
                    {
                        cmd.Parameters.AddWithValue("@id", Searchtext);
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dsSalesDate);
                            int count = dsSalesDate.Tables[0].Rows.Count;
                            sumObject = dsSalesDate.Tables[0].Compute("Sum(TOTALPRICE)", string.Empty).ToString();

                            //DataRow toInsert = dsSalesDate.Tables[0].NewRow();                            
                            //dsSalesDate.Tables[0].Rows.InsertAt(toInsert, count + 1);   
                            //toInsert["TOTALPRICE"] = sumObject;

                            DataTable dt = dsSalesDate.Tables[0];

                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    salesdata.Add(new SalesData
                                    {
                                        Name = dr["NAME"].ToString(),
                                        Country = dr["COUNTRY"].ToString(),
                                        OrderID = Convert.ToInt16(dr["ORDERID"]),
                                        Phone = dr["PHONE"].ToString(),
                                        Orderdate = Convert.ToDateTime(dr["DATEOFORDER"]),
                                        ProductName = dr["PRODUCTNAME"].ToString(),
                                        UnitPrice = Convert.ToDecimal(Convert.ToDecimal(dr["UNITPRICE"]).ToString()),
                                        Quantity = Convert.ToInt32(dr["QUANTITY"]),
                                        TotalPrice = Convert.ToDecimal(Convert.ToDecimal(dr["TOTALPRICE"]).ToString("#,##0.00")),
                                    });
                                }
                                salesdata.Add(new SalesData { TotalAmount = Convert.ToDecimal(Convert.ToDecimal(sumObject).ToString("#,##0.00")) });
                            }
                        }
                    }
                }
                return salesdata;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SalesData> getallusers()
        {
            List<SalesData> userslist = new List<SalesData>();
            DataSet dsusers = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(objConn))
                {
                    using (SqlCommand cmd = new SqlCommand("select id, FirstName + ' '+LastName as Name from  customer order by id asc", con))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dsusers);
                            foreach (DataRow dr in dsusers.Tables[0].Rows)
                            {
                                userslist.Add(new SalesData
                                {
                                    Id = Convert.ToInt16(dr["Id"]),
                                    Name = dr["Name"].ToString(),
                                });
                            }
                        }
                    }
                }
                return userslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    #endregion
}





