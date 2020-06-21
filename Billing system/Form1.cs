using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using Microsoft.Azure.Amqp.Framing;
using Grpc.Core;

namespace Billing_system
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if(txt_discount.Text.Length > 0)
            {
                txt_net.Text = (Convert.ToInt16(txt_sub.Text) - Convert.ToInt16(txt_discount.Text)).ToString();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.ForeColor = System.Drawing.Color.Green;
            radioButton2.ForeColor = System.Drawing.Color.Red;
            radioButton3.ForeColor = System.Drawing.Color.Red;
            radioButton4.ForeColor = System.Drawing.Color.Red;

            cmb_item.Items.Clear();
            cmb_item.Items.Add("Full Ice   500/-");
            cmb_item.Items.Add("Half Ice 300/-");
            cmb_item.Items.Add("1/4 Ice");
            cmb_item.Items.Add("1/8(Fanchar) Ice 80/-");
            cmb_item.Items.Add("50 Rs");
            cmb_item.Items.Add("20 Rs");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.ForeColor = System.Drawing.Color.Red;
            radioButton2.ForeColor = System.Drawing.Color.Green;
            radioButton3.ForeColor = System.Drawing.Color.Red;
            radioButton4.ForeColor = System.Drawing.Color.Red;

            cmb_item.Items.Clear();
            cmb_item.Items.Add("Own Jug Refill   20/-");
            cmb_item.Items.Add("Own Bottle Refill Cold 25/-");
            cmb_item.Items.Add("Own Bottle Refill Normal 20/-");
            cmb_item.Items.Add("Small own jug 10/-");
            
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.ForeColor = System.Drawing.Color.Red;
            radioButton2.ForeColor = System.Drawing.Color.Red;
            radioButton3.ForeColor = System.Drawing.Color.Green;
            radioButton4.ForeColor = System.Drawing.Color.Red;

            cmb_item.Items.Clear();
            cmb_item.Items.Add("Water bag   25/-");
            cmb_item.Items.Add("Aquafina  110/-");
            cmb_item.Items.Add("Other packed bottle 100/-");
           
        }

        private void cmb_item_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_item.SelectedItem.ToString() == "Full Ice   500/-")
            {
                text_price.Text = "500";
            }
            else if (cmb_item.SelectedItem.ToString() == "Half Ice 300/-")
            {
                text_price.Text = "300";
            }
            else if (cmb_item.SelectedItem.ToString() == "1/4 Ice")
            {
                text_price.Text = "160";
            }
            else if (cmb_item.SelectedItem.ToString() == "1/8(Fanchar) Ice 80/-")
            {
                text_price.Text = "80";
            }
            else if (cmb_item.SelectedItem.ToString() == "50 Rs")
            {
                text_price.Text = "50";
            }
            else if (cmb_item.SelectedItem.ToString() == "20 Rs" || cmb_item.SelectedItem.ToString() == "Own Jug Refill   20/-" || cmb_item.SelectedItem.ToString() == "Own Bottle Refill Normal 20/-")
            {
                text_price.Text = "20";
            }
            else if (cmb_item.SelectedItem.ToString() == "Own Bottle Refill Cold 25/-" || cmb_item.SelectedItem.ToString() == "Water bag   25/-")
            {
                text_price.Text = "25";
            }
            else if (cmb_item.SelectedItem.ToString() == "Small own jug 10/-")
            {
                text_price.Text = "10";
            }
            else if (cmb_item.SelectedItem.ToString() == "Aquafina  110/-")
            {
                text_price.Text = "110";
            }
            else if (cmb_item.SelectedItem.ToString() == "Other packed bottle 100/-")
            {
                text_price.Text = "100";
            }
            else
            {
                text_price.Text = "";
            }

            txt_qty.Text = "";
            txt_item_total.Text = "";
        }

        private void txt_qty_TextChanged(object sender, EventArgs e)
        {
            if (txt_qty.Text.Length > 0)
            {
                txt_item_total.Text = (Convert.ToInt16(text_price.Text) * Convert.ToInt16(txt_qty.Text)).ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] array = new string[4];
            array[0] = cmb_item.SelectedItem.ToString();
            array[1] = text_price.Text;
            array[2] = txt_qty.Text;
            array[3] = txt_item_total.Text;

            ListViewItem listView = new ListViewItem(array);
            listView1.Items.Add(listView);

            txt_sub.Text = (Convert.ToInt16(txt_item_total.Text) + Convert.ToInt16(txt_sub.Text)).ToString();

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.ForeColor = System.Drawing.Color.Red;
            radioButton2.ForeColor = System.Drawing.Color.Red;
            radioButton3.ForeColor = System.Drawing.Color.Red;
            radioButton4.ForeColor = System.Drawing.Color.Green;
            cmb_item.Items.Clear();
            cmb_item.Items.Add("Custom Product");
            cmb_item.SelectedItem = "Custom Product";
        }

        private void txt_paid_TextChanged(object sender, EventArgs e)
        {
            if (txt_net.Text.Length > 0)
            {
                txt_balance.Text = (Convert.ToInt16(txt_net.Text) - Convert.ToInt16(txt_paid.Text)).ToString();
                if(Convert.ToInt16(txt_paid.Text) <= Convert.ToInt16(txt_net.Text))
                {
                    txt_balance.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    txt_balance.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                for(int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].Selected)
                    {
                        txt_sub.Text = (Convert.ToInt16(txt_sub.Text) - Convert.ToInt16(listView1.Items[i].SubItems[3].Text)).ToString();
                        listView1.Items[i].Remove();
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                try
                {
                    string connetionString;
                    SqlConnection cnn;
                    connetionString = "Integrated Security = SSPI; Persist Security Info = False; Initial Catalog = master; Data Source = localhost' ";
                    cnn = new SqlConnection(connetionString) ;
                    cnn.Open();
                    MessageBox.Show("Connection Open  !");
                    cnn.Close();
                }
                catch (SqlException ex)
                {
                    StringBuilder errorMessages = new StringBuilder();
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                }
            }
            else
            {
                MessageBox.Show("Please add items to list!!");
            }
        }
    }
}
