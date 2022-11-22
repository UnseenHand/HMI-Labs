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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab4
{
    public partial class Form1 : Form
    {
        private readonly SqlHelper _db;

        public Form1()
        {
            InitializeComponent();
            _db = new SqlHelper();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            IsMdiContainer = true;
            Width = 1000;
            Height = 600;
            await SqlHelper.CreateDb();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void displayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var viewBase = new ViewBase
            {
                MdiParent = this,
                Text = $"There is {await _db.Count()} records in DB!"
            };
            viewBase.Show();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void createTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dbTable = new DbTableCreate
            {
                MdiParent = this,
                Text = "Create Table For Your Database"
            };
            dbTable.Show();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dbTable = new AddBase
            {
                MdiParent = this,
                Text = "Add new record"
            };
            dbTable.Show();
        }

        private void знищитиToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
