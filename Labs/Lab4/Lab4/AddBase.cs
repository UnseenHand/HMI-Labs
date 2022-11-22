using Lab4.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class AddBase : Form
    {
        private readonly SqlHelper _db;

        public AddBase()
        {
            InitializeComponent();
            _db = new SqlHelper();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(textBox4.Text, out decimal price))
            {
                await _db.AddNewBase(new Base()
                {
                    BaseName = textBox2.Text,
                    City = textBox3.Text,
                    RoomPrice = price,
                    SeaDistance = double.Parse(textBox5.Text)
                });
            }
            else
            {
                MessageBox.Show("Invalid input format!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }
    }
}
