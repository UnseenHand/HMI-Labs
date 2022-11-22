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
    public partial class ViewBase : Form
    {
        private readonly SqlHelper _db;

        public ViewBase()
        {
            InitializeComponent();
            _db = new SqlHelper();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void ViewBase_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = await _db.GetBases();
        }
    }
}
