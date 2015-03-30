using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace FlowerPriject
{
    class DAL
    {

        private SqlConnection conn;
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }


        //Konstruktor
        public DAL()
        {
            try
            {
                conn = new SqlConnection("Data Source=FRIDAS-DATOR;Initial Catalog=FlowerProjectDB;Integrated Security=True");
            }
            catch (SqlException ex) 
            
            { 
                throw ex; 
            
            }

        } 

        // CUSTOMER

        // Hämta alla kunder
        public DataSet GetCustomers()
        {
           
               conn.Open();

                SqlCommand sqlComm = new SqlCommand("spGetAllCustomers", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                DataSet GetCustomers = new DataSet();
                GetCustomers.Tables.Add("Customer");
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                da.Fill(GetCustomers, "Customer");

                conn.Close();
                return GetCustomers;
           
        }

        // Lägg till Customer
        public void SetCustomer(string ssn, string first_name, string last_name, string tel_nr)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spSetCustomer", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ssn", ssn);
                cmd.Parameters.AddWithValue("@first_name", first_name);
                cmd.Parameters.AddWithValue("@last_name", last_name);
                cmd.Parameters.AddWithValue("@tel_nr", tel_nr);
                cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (SqlException sqle)
            {
                throw sqle;
            }finally {
                conn.Close();
            }
        }

        // Deletea en kund
        public void DeleteCustomer(string ssn)
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("spDeleteCustomer", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ssn", ssn);
                command.ExecuteNonQuery();
                conn.Close();
            }

            catch (SqlException)
            {
                ErrorMessage = "Kan ej ta bort som har en order";
                throw new ArgumentException();
            }
        }

        // FLOWER

        // Hämta alla Flowers
        public DataSet GetFlowers()
        {
            conn.Open();

            SqlCommand sqlComm = new SqlCommand("spGetAllFlowers", conn);
            sqlComm.CommandType = CommandType.StoredProcedure;
            DataSet GetFlowers = new DataSet();
            GetFlowers.Tables.Add("Flower");
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = sqlComm;
            da.Fill(GetFlowers, "Flower");

            conn.Close();
            return GetFlowers;
        }

        // Lägg till Flower
        public void SetFlower(string flower_nr, string color, string name, int unit_price)
        {

            try
            {
                conn.Open();


                SqlCommand cmd = new SqlCommand("spSetFlower", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flower_nr", flower_nr);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@color", color);
                cmd.Parameters.AddWithValue("@unit_price", unit_price);
                cmd.ExecuteNonQuery();

                conn.Close();


            }
            catch (SqlException e)
            {
                throw e;
            }finally {
                conn.Close();
            }
        }

        // Deletea Flower
        public void DeleteFlower(string flower_nr)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("spDeleteFlower", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@flowerNr", flower_nr);
            command.ExecuteNonQuery();
            conn.Close();
        }
        // ORDER
        
        //hämta alla ordrar
        public DataTable GetOrders()
        {
            conn.Open();

            SqlCommand sqlComm = new SqlCommand("spGetOrders", conn);
            sqlComm.CommandType = CommandType.StoredProcedure;
            DataSet GetOrders = new DataSet();
            DataTable dt = new DataTable();
            dt = GetOrders.Tables.Add("OrderTable");
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = sqlComm;
            da.Fill(GetOrders, "OrderTable");

            conn.Close();
            return dt;
        }
        // lägg till order
        public void SetOrder(string order_nr, string ssn, string delivery_address, int total_price)
        {
            try
            {
                conn.Open();
   

                SqlCommand cmd = new SqlCommand("spSetOrder", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@order_nr", order_nr);
                cmd.Parameters.AddWithValue("@ssn", ssn);
                cmd.Parameters.AddWithValue("@delivery_address", delivery_address);
                cmd.Parameters.AddWithValue("@total_price", total_price);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
            
             catch(SqlException exc)
            
            {
               
            }

        }

        // deletea order
        public void DeleteOrder(string order_nr)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("spDeleteOrder", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@order_nr", order_nr);
            command.ExecuteNonQuery();
            conn.Close();
        }

        // --- eventuellt ändra order
      


        // ORDERLINE
        // set orderline

        public void SetOrderLine(string flower_nr, int quantity, string order_nr, int order_line_price) 
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("spSetOrderLine", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flower_nr", flower_nr);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@order_nr", order_nr);
            cmd.Parameters.AddWithValue("@order_line_price", order_line_price);


            cmd.ExecuteNonQuery();
            conn.Close();
        }


        // deletea order
        public void DeleteOrderLine(string order_line_nr)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("DELETE FROM Order_line2 WHERE order_line_nr = @order_line_nr", conn);

            command.Parameters.AddWithValue("@order_line_nr", order_line_nr);
            command.ExecuteNonQuery();
            conn.Close();
        }

        // Hämta totalpris
 
        public int totPrice(string flower_nr)
        {
            SqlDataReader resultset;
            int SUM = 0;
            conn.Open();
            SqlCommand command = new SqlCommand("spGetFlowerPrice", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@flower_nr", flower_nr);
            try
            {
                resultset = command.ExecuteReader();
                if (resultset.Read())
                {
                    SUM = resultset.GetInt32(0);
                }

                resultset.Close();
                conn.Close();
            }
            catch (Exception)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return SUM;
        }

        //Hämta ordrar för en person
        public DataSet GetCustomerOrders(string ssn) {

            conn.Open();

            SqlCommand sqlComm = new SqlCommand("spGetCustomerOrders", conn);
            sqlComm.CommandType = CommandType.StoredProcedure;
            sqlComm.Parameters.AddWithValue("@ssn", ssn);
            DataSet GetCustomerOrders = new DataSet();
            GetCustomerOrders.Tables.Add("OrderTable");
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = sqlComm;
            da.Fill(GetCustomerOrders, "OrderTable");

            conn.Close();
            return GetCustomerOrders;
 
            
        
        
        }

        //Hämta orderlines på en order

        public DataSet GetOrderLines(string orderNr)
        {
            conn.Open();

            SqlCommand sqlComm = new SqlCommand("spGetOrderLines", conn);
            sqlComm.CommandType = CommandType.StoredProcedure;
            sqlComm.Parameters.AddWithValue("@orderNr", orderNr);
            DataSet GetOrderLines = new DataSet();
            GetOrderLines.Tables.Add("OrderLine");
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = sqlComm;
            da.Fill(GetOrderLines, "OrderLine");

            conn.Close();
            return GetOrderLines;

        }
    }
         
}