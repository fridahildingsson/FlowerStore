using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace FlowerPriject
{
    class Controller
    {
        DAL dal = new DAL();


        //Konstruktor
        public Controller()
        {
        }

        public string ErrorMessage
        {
            get { return dal.ErrorMessage; }
            set { dal.ErrorMessage = value; }
        }


        // CUSTOMER

        public DataSet GetControllersCust()
        {
            DataSet DS;
            DS = dal.GetCustomers();
            return DS;
        }

        public void SetCustomer(string ssn, string first_name, string last_name, string tel_nr)
        {

            dal.SetCustomer(ssn, first_name, last_name, tel_nr);
        }

        public void DeleteCustomer(string ssn)
        {
            dal.DeleteCustomer(ssn);
        }

        // FLOWERS

        public DataSet GetControllersFlowers()
        {
            DataSet ds;
            ds = dal.GetFlowers();
            return ds;
        }

        public void SetFlower(string flower_nr, string name, string color, int unit_price)
        {

            dal.SetFlower(flower_nr, color, name, unit_price);
        }

        public void DeleteFlower(string flower_nr)
        {
            dal.DeleteFlower(flower_nr);
        }

        // ORDER

        public DataTable GetOrders()
        {
            DataTable dt;
            dt = dal.GetOrders();
            return dt;

        }

        public DataSet GetOrderLines(string orderNr)
        {
            DataSet dt;
            dt = dal.GetOrderLines(orderNr);
            return dt;
        }

        public void SetOrder(string order_nr, string ssn, string delivery_address, int total_price)
        {
            dal.SetOrder(order_nr, ssn, delivery_address, total_price);
        }


        public void DeleteOrder(string order_nr)
        {
            dal.DeleteOrder(order_nr);

        }


        public DataSet GetCustomerOrders(string ssn)
        {
            DataSet ds;
            ds = dal.GetCustomerOrders(ssn);
            return ds;
        }


        // ORDERLINE

        public void SetOrderLine(string flower_nr, int quantity, string order_nr, int order_line_price)
        {

            dal.SetOrderLine(flower_nr, quantity, order_nr, order_line_price);

        }

        public void DeleteOrderLine(string order_line_nr)
        {
            dal.DeleteOrderLine(order_line_nr);

        }

        // TotPris
        public int totPrice(string flower_nr)
        {
            return dal.totPrice(flower_nr);
        }
    }
}
    

    

    

