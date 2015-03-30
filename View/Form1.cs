using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Data;


namespace FlowerPriject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Controller control = new Controller();

        // Registrera kund

        private void ButtonRegisterCustomer(object sender, EventArgs e)
        {

            try
            {
               

                if ((textBox1_CustNr.Text == "") || (textBox2_firstName.Text == "") || (textBox3_LastName.Text == "") || (textBox4_PhoneNr.Text == ""))
                {

                    MessageBox.Show("All fields must hold a value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    control.SetCustomer(textBox1_CustNr.Text, textBox2_firstName.Text, textBox3_LastName.Text, textBox4_PhoneNr.Text);

                    MessageBox.Show("Customer registered");

                    textBox1_CustNr.Text = "";
                    textBox2_firstName.Text = "";
                    textBox3_LastName.Text = "";
                    textBox4_PhoneNr.Text = "";
                    DataSet DsGetCustomers = control.GetControllersCust();
                    dataGridView1_ShowCust.DataMember = "Customer";
                    dataGridView1_ShowCust.DataSource = DsGetCustomers;


                    comboBox1_OrderCusomer.Items.Clear();
                    DataSet ds = control.GetControllersCust();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        comboBox1_OrderCusomer.Items.Add(dr["Ssn"]);
                    }

                    comboBox1_customer_id.Items.Clear();
                    DataSet ds2 = control.GetControllersCust();
                    foreach (DataRow dr in ds2.Tables[0].Rows)
                    {
                        comboBox1_customer_id.Items.Add(dr["Ssn"]);
                    }
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        // Radera Kund

        private void ButtonDeleteCustomer(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = control.GetOrders();
                bool custBool = false;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr.ToString() == (String)dataGridView1_ShowCust.SelectedCells[0].OwningRow.Cells[0].Value.ToString())
                    {
                        custBool = true;
                    }
                }

                if (custBool == true)
                {
                    MessageBox.Show("Customer allready exists", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                else
                {
                    string ssn_toDelete = (String)dataGridView1_ShowCust.SelectedCells[0].OwningRow.Cells[0].Value.ToString();
                    control.DeleteCustomer(ssn_toDelete);

                    foreach (DataGridViewRow row in dataGridView1_ShowCust.SelectedRows)
                    {
                        dataGridView1_ShowCust.Rows.Remove(row);
                        MessageBox.Show("Customer deleted");
                    }


                    DataSet dsGetCust = control.GetControllersCust();
                    dataGridView1_ShowCust.DataMember = "Customer";
                    dataGridView1_ShowCust.DataSource = dsGetCust;


                    comboBox1_OrderCusomer.Items.Clear();
                    DataSet ds = control.GetControllersCust();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        comboBox1_OrderCusomer.Items.Add(dr["Ssn"]);
                    }

                    comboBox1_customer_id.Items.Clear();
                    DataSet ds2 = control.GetControllersCust();
                    foreach (DataRow dr in ds2.Tables[0].Rows)
                    {
                        comboBox1_customer_id.Items.Add(dr["Ssn"]);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Sorry, something went wrong.." + exc.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        
        }

        //FORM_LOAD



        private void Form1_Load(object sender, EventArgs e)
        {
            //show flowers
            DataSet dsGetFlowers = control.GetControllersFlowers();
            dataGridView1_Flowers.DataMember = "Flower";
            dataGridView1_Flowers.DataSource = dsGetFlowers;

            //show customer
            DataSet getCustomers = control.GetControllersCust();

            dataGridView1_ShowCust.DataMember = "Customer";
            dataGridView1_ShowCust.DataSource = getCustomers;

            //tolatprice 0, form start
            int defalutTotPrice = 0;
            textBox1_TotPrice.Text = defalutTotPrice.ToString();

            //fill combobox, customer
            DataSet ds = control.GetControllersCust();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                comboBox1_OrderCusomer.Items.Add(dr["Ssn"]);
            }

            // fyll cmbobox i tab 4 med customers
            DataSet ds2 = control.GetControllersCust();
            foreach (DataRow dr in ds2.Tables[0].Rows)
            {
                comboBox1_customer_id.Items.Add(dr["Ssn"]);
            }
            //Fyll på combobox FlowerNr
            DataSet dsf = control.GetControllersFlowers();
            foreach (DataRow dr in dsf.Tables[0].Rows)
            {
                comboBox1_OrderFlowerNr.Items.Add(dr["flowerNr"]);
            }

            dataGridView1_ShowOrders.Columns.Add("Flower", "Flower");
            dataGridView1_ShowOrders.Columns.Add("Quantity", "Quantity");
            dataGridView1_ShowOrders.Columns.Add("Order nr", "Order nr");
            dataGridView1_ShowOrders.Columns.Add("Line price", "Line price");
            dataGridView1_ShowOrders.Columns[0].Width = 50;
            dataGridView1_ShowOrders.Columns[1].Width = 50;
            dataGridView1_ShowOrders.Columns[2].Width = 50;
            dataGridView1_ShowOrders.Columns[3].Width = 50;


        }



        // lägg till Flower
        private void ButtonRegisterFlower(object sender, EventArgs e)
        {

            try
            {

                if ((textBox1_FlowerNr.Text == "") || (textBox2_FlowerName.Text == "") || (textBox3_FlowerColour.Text == "") || (textBox4_FlowerPrice.Text == ""))
                {
                    MessageBox.Show("All fields must hold a value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                }


                else
                {
                    control.SetFlower(textBox1_FlowerNr.Text, textBox2_FlowerName.Text, textBox3_FlowerColour.Text, int.Parse(textBox4_FlowerPrice.Text));
                    MessageBox.Show("Flower registered");
                    textBox1_FlowerNr.Text = "";
                    textBox2_FlowerName.Text = "";
                    textBox3_FlowerColour.Text = "";
                    textBox4_FlowerPrice.Text = "";

                    DataSet dsGetFlowers = control.GetControllersFlowers();
                    // dataGridView1_Flowers.DataMember = "Flowers"; // KOlla upp detta, den kraschar programmet
                    dataGridView1_Flowers.DataSource = dsGetFlowers;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        // Ta bort flower

        private void ButtonDeleteFlower(object sender, EventArgs e)
        {

            
                string flower_nr_toDelete = (String)dataGridView1_Flowers.SelectedCells[0].OwningRow.Cells[0].Value.ToString();
                control.DeleteFlower(flower_nr_toDelete);

                foreach (DataGridViewRow row in dataGridView1_Flowers.SelectedRows)
                {
                    dataGridView1_Flowers.Rows.Remove(row);
                    MessageBox.Show("Flower deleted");
                }

                DataSet dsGetFlowers = control.GetControllersFlowers();
                //dataGridView1_Flowers.DataMember = "Flowers";
                dataGridView1_Flowers.DataSource = dsGetFlowers;
          

        }



        // Slut På flower

        //ORDER

        // ORDERLINEKNAPPEN lägg till orderline
        private void ButtonAddOrderLine(object sender, EventArgs e)
        {
            
                bool dublettOL = false;
                int parsedValue;

                // För att inte  få med dubletter i ordern

                foreach (DataGridViewRow g1 in dataGridView1_ShowOrders.Rows)
                {


                    if (g1.Cells[0].Value.ToString() == comboBox1_OrderFlowerNr.Text)
                    {
                        dublettOL = true;
                    }


                }
                if (dublettOL == true)
                {

                    MessageBox.Show("Flower allready in order", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

                else if (comboBox1_OrderFlowerNr.Text == "" || textBox1_OrderQuantity.Text == "" || textBox1_OrderNr.Text == "")
                {

                    MessageBox.Show("All fields must hold a value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                }


                else if (!int.TryParse(textBox1_OrderQuantity.Text, out parsedValue))
                {

                    MessageBox.Show("This is a number only field", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

                else
                {


                    String temp_fl_no = comboBox1_OrderFlowerNr.Text;
                    int sum = control.totPrice(temp_fl_no);
                    int previousTotal = int.Parse(textBox1_TotPrice.Text);
                    int quantityTimesPrice = (sum * int.Parse(textBox1_OrderQuantity.Text));


                    dataGridView1_ShowOrders.Rows.Add(comboBox1_OrderFlowerNr.Text, textBox1_OrderQuantity.Text, textBox1_OrderNr.Text, quantityTimesPrice.ToString());


                    int totSum = 0;
                    for (int i = 0; i < dataGridView1_ShowOrders.Rows.Count; ++i)
                    {
                        totSum += Convert.ToInt32(dataGridView1_ShowOrders.Rows[i].Cells[3].Value);
                    }
                    textBox1_TotPrice.Text = totSum.ToString();

                }
          
        }




        // genererar ordernr
        private void ButtonOrderNr(object sender, EventArgs e)
        {
            textBox1_OrderNr.Text = (DateTime.Now.ToString().GetHashCode().ToString("x"));
            button1_OrderNr.Enabled = false;
        }

        // ORDERKNAPPEN skicka order till databasen 
        private void ButtonSendOrder(object sender, EventArgs e)
        {
            //skickar ordern

            //felhanterande om ingen orderline är tillagd

           

                if (dataGridView1_ShowOrders.Rows.Count == 0)
                {

                    MessageBox.Show("Add orderlines to order", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (comboBox1_OrderCusomer.Text == "" || textBox1_setDeliAddress.Text == "" || textBox1_OrderNr.Text == "")
                {
                    MessageBox.Show("All fields must hold a value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {


                    control.SetOrder(textBox1_OrderNr.Text, comboBox1_OrderCusomer.Text, textBox1_setDeliAddress.Text, Convert.ToInt32(textBox1_TotPrice.Text));


                    dataGridView1_ShowOrders.AllowUserToAddRows = false;


                    foreach (DataGridViewRow g1 in dataGridView1_ShowOrders.Rows)
                    {

                        control.SetOrderLine(g1.Cells[0].Value.ToString(), int.Parse(g1.Cells[1].Value.ToString()), g1.Cells[2].Value.ToString(), int.Parse(g1.Cells[3].Value.ToString()));
                    }


                    MessageBox.Show("Order registered");

                    comboBox1_OrderCusomer.ResetText();
                    textBox1_setDeliAddress.ResetText();
                    textBox1_OrderNr.ResetText();
                    comboBox1_OrderFlowerNr.ResetText();
                    textBox1_OrderQuantity.ResetText();
                    dataGridView1_ShowOrders.Rows.Clear();
                    textBox1_TotPrice.ResetText();
                    button1_OrderNr.Enabled = true;


                }
           
            
        }

        // Ta bort orderline
        private void ButtonDelOrderLine(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dataGridView1_ShowOrders.SelectedRows)
            {
                dataGridView1_ShowOrders.Rows.RemoveAt(row.Index);
            }
            int totSum = 0;
            for (int i = 0; i < dataGridView1_ShowOrders.Rows.Count; ++i)
            {
                totSum += Convert.ToInt32(dataGridView1_ShowOrders.Rows[i].Cells[3].Value);
            }
            textBox1_TotPrice.Text = totSum.ToString();

        }

        //Lägger customers orders i datagridview
        private void button1_show_orders_Click(object sender, EventArgs e)
        {
            DataSet dsGetCustomerOrders = control.GetCustomerOrders(comboBox1_customer_id.Text);
            dataGridView_orders.DataMember = "OrderTable";
            dataGridView_orders.DataSource = dsGetCustomerOrders;
        }
        
        //Fyller dataGridView med Orderlines för en order
        private void dataGridView_orders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string orderNr = (String)dataGridView_orders.SelectedCells[0].OwningRow.Cells[0].Value.ToString();
            DataSet dtGetOrderLines = control.GetOrderLines(orderNr);
            dataGridView_order_lines.DataMember = "OrderLine";
            dataGridView_order_lines.DataSource = dtGetOrderLines;
        }

        //delete order
        private void button_delete_order_Click(object sender, EventArgs e)
        {
            string orderNr = (String)dataGridView_orders.SelectedCells[0].OwningRow.Cells[0].Value.ToString();
            control.DeleteOrder(orderNr);

            foreach (DataGridViewRow row in dataGridView_orders.SelectedRows)
            {
                dataGridView_orders.Rows.Remove(row);
                MessageBox.Show("Order deleted");
                DataSet dtGetOrderLines = control.GetOrderLines(orderNr);
                dataGridView_order_lines.DataMember = "OrderLine";
                dataGridView_order_lines.DataSource = dtGetOrderLines;
            }
        }

    }
}


// slut på klass