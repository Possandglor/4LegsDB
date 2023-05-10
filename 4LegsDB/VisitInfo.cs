using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4LegsDB
{
    public partial class VisitInfo : Form
    {
        public Client client;
        public VisitInfo(Client client)
        {
            InitializeComponent();
            this.client = client;
            try
            {
                richTextBox1.Text = client.visits[0].Split(' ')[0];
                richTextBox2.Text = client.visits[0].Split(' ')[1];
            }
            catch { }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textChanged(object sender, EventArgs e)
        {
            client.visits[0] = richTextBox1.Text;
            client.visits[0] += " " + richTextBox2.Text;

        }
    }
}
