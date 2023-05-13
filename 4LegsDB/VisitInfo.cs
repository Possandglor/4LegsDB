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
        private Visit visit;
        private Client client;
        HttpUtil httpUtil = new HttpUtil();
        public VisitInfo(Visit visit,Client client)
        {
            InitializeComponent();
            this.visit = visit;
            this.client = client;
            try
            {
                richTextBox1.Text = visit.anamnez;
                textBox1.Text = visit.vid;
                richTextBox2.Text = visit.appointment;
                dateTimePicker1.Text = visit.date;
            }
            catch { }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textChanged(object sender, EventArgs e)
        {
            if(dateTimePicker1.Text!="")
            visit.date = dateTimePicker1.Text;
            visit.vid = textBox1.Text;
            visit.anamnez = richTextBox1.Text;
            visit.appointment = richTextBox2.Text;

        }

        private async void VisitInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            string url = Parameters.settings.url; 
            string title = visit.vid + " " + client.petName +" "+client.phoneNumber ;
            string datn = visit.date;
            Dictionary<string,string> keyValuePairs = new Dictionary<string,string>();
            keyValuePairs.Add("title", title);
            keyValuePairs.Add ("datn", datn);
            keyValuePairs.Add("event", "create");
            string response = await httpUtil.SendPostRequest(url, keyValuePairs);


        }

        private void VisitInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
