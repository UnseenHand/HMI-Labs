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
    public partial class DbTableCreate : Form
    {
        public DbTableCreate()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Create();
        }

        public async void Create()
        {
            var db = new SqlHelper();
            await db.CreateDbTable(textBox1.Text);
        }
    }
}
