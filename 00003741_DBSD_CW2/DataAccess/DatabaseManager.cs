using _00003741_DBSD_CW2.Models;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace _00003741_DBSD_CW2.DataAccess
{
    public class DatabaseManager
    {

        public static string ConnStr
        {
            get
            {
                return WebConfigurationManager
                    .ConnectionStrings["connectionToTester"]
                    .ConnectionString;
            }
        }

        public List<Products> GetAllProducts()
        {
            List<Products> products = new List<Products>();

            using (DbConnection conn = new SqlConnection(ConnStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Products";
                    conn.Open();

                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Products p = new Products()
                            {
                                Id = rdr.GetInt32(0),
                                Name = rdr.GetString(1),
                                Description = rdr.GetString(2),
                                Price = rdr.GetInt32(3),
                                InStock = rdr.GetInt32(4)
                            };
                            products.Add(p);
                        }
                    }

                }
            }
            return products;
        }
        public Products GetProductById(int Id)
        {
            Products result = null;

            using (DbConnection conn = new SqlConnection(ConnStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Products WHERE product_id = @product_id";

                    DbParameter pProductId = cmd.CreateParameter();
                    pProductId.ParameterName = "@product_id";
                    pProductId.DbType = DbType.Int32;
                    pProductId.Value = Id;
                    cmd.Parameters.Add(pProductId);

                    conn.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = new Products()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                Price = reader.GetInt32(3),
                                InStock = reader.GetInt32(4),
                            };
                        }
                    }
                    conn.Close();

                }
                return result;
            }








        }
        public int Order(ProductOrder OrderItem)
        {
            int result = 10;

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                using (SqlCommand cmd = new SqlCommand(@"[dbo].[ordermake]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.AddParameter("@customer_id", DbType.Int32, OrderItem.CustomerId);
                    cmd.AddParameter("@product_id", DbType.Int32, OrderItem.ProductId);
                    cmd.AddParameter("@order_amount", DbType.Int32, OrderItem.Quantity);
                    cmd.AddParameter("@order_date", DbType.Date, DateTime.Now.Date);


                    var p = cmd.CreateParameter();
                    p.ParameterName = "@msg";
                    p.Direction = ParameterDirection.ReturnValue;

                    cmd.Parameters.Add(p);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    result = (int)cmd.Parameters["@msg"].Value;

                    Debug.WriteLine("Connection result " + result);
                }
                conn.Close();
            }

            return result;
        }

        public List<Suggestion> GetAllSuggestion(int customerId)
        {
            List<Suggestion> suggestions = new List<Suggestion>();

            using (DbConnection conn = new SqlConnection(ConnStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * from suggestion(@customerid)";
                    cmd.AddParameter("customerid", DbType.Int32, customerId);
                    conn.Open();

                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Suggestion p = new Suggestion()
                            {
                                Id = rdr.GetInt32(0),
                                ProductName = rdr.GetString(1),
                                ProductDescription = rdr.GetString(2),
                                Price = rdr.GetInt32(3),
                                LeftInStock = rdr.GetInt32(4),
                                NumberOfCustomerBought = rdr.GetInt32(5)
                            };
                            suggestions.Add(p);
                        }
                    }

                }
            }
            return suggestions;
        }

        public int Register(RegistrationModel customer)
        {
            int result = 10;

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                using (SqlCommand cmd = new SqlCommand(@"[dbo].[register]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.AddParameter("@title", DbType.String, customer.Title);
                    cmd.AddParameter("@firstname", DbType.String, customer.FirstName);
                    cmd.AddParameter("@lastName", DbType.String, customer.LastName);
                    cmd.AddParameter("@email", DbType.String, customer.Email);
                    cmd.AddParameter("@pass", DbType.String, customer.Password);
                    cmd.AddParameter("@dob", DbType.Date, customer.Dob);

                    var p = cmd.CreateParameter();
                    p.ParameterName = "@msg";
                    p.Direction = ParameterDirection.ReturnValue;

                    cmd.Parameters.Add(p);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    result = (int)cmd.Parameters["@msg"].Value;

                    Debug.WriteLine("Connection result " + result);
                }
                conn.Close();
            }

            return result;
        }


        public int Login(LoginModel customer)
        {
            int result = 10;

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                using (SqlCommand cmd = new SqlCommand(@"[dbo].[signin]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.AddParameter("@email", DbType.String, customer.Email);
                    cmd.AddParameter("@pass", DbType.String, customer.Password);

                    var p = cmd.CreateParameter();
                    p.ParameterName = "@msg";
                    p.Direction = ParameterDirection.ReturnValue;

                    cmd.Parameters.Add(p);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    result = (int)cmd.Parameters["@msg"].Value;

                    Debug.WriteLine("Connection result " + result);
                }
                conn.Close();
            }

            return result;
        }
        
        public void UpdateCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                using (SqlCommand cmd = new SqlCommand(@"[dbo].[update_customer]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.AddParameter("@id", DbType.Int32, customer.Id);
                    cmd.AddParameter("@title", DbType.String, customer.Title);
                    cmd.AddParameter("@firstname", DbType.String, customer.FirstName);
                    cmd.AddParameter("@lastname", DbType.String, customer.LastName);
                    cmd.AddParameter("@email", DbType.String, customer.Email);
                    cmd.AddParameter("@pass", DbType.String, customer.Password);
                    cmd.AddParameter("@dob", DbType.Date, customer.Dob);
                    Debug.WriteLine("" + customer.Password);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                }
                conn.Close();
            }
        }

        public Customer GetCustomerById(int Id)
        {
            Customer result = null;

            using (DbConnection conn = new SqlConnection(ConnStr))
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Customers WHERE customer_id = @id";

                    DbParameter param = cmd.CreateParameter();
                    param.ParameterName = "@id";
                    param.DbType = DbType.Int32;
                    param.Value = Id;
                    cmd.Parameters.Add(param);

                    conn.Open();
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = new Customer()
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                FirstName = reader.GetString(2),
                                LastName = reader.GetString(3),
                                Email = reader.GetString(4),
                                Password = reader.GetString(5),
                                Dob = reader.GetDateTime(6)
                            };
                        }
                    }
                    conn.Close();

                }
                return result;
            }

        }

    }

    public static class DbCommandExtensionMethods
    {
        public static IDbDataParameter AddParameter(this IDbCommand cmd, string name, System.Data.DbType type, object value, ParameterDirection direction = ParameterDirection.Input)
        {
            var p = cmd.CreateParameter();
            p.ParameterName = name;
            p.Direction = direction;
            if (value == null)
            {
                p.Value = DBNull.Value;
            }
            else
            {
                p.DbType = type;
                p.Value = value;
            }

            cmd.Parameters.Add(p);
            return p;
        }

    }
}