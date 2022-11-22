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
        string conString = "Server=(localdb)\\MSSQLLocalDB;Database=RecreationBase;Trusted_Connection=True;";

        public Form1()
        {
            InitializeComponent();
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

        private void displayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var viewBase = new ViewBase
            {
                MdiParent = this
            };
            var db = new SqlHelper();
            viewBase.Text = "В БД " + db.Count().ToString() + " записів!!!!";
            viewBase.Show();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void createTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dbTable = new DbTableCreate
            {
                MdiParent = this
            };
            dbTable.Text = "Create Table For Your Database";
            dbTable.Show();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dbTable = new AddBase
            {
                MdiParent = this
            };
            dbTable.Text = "Add new record";
            dbTable.Show();
        }
    }
}
