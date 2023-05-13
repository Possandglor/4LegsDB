using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4LegsDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ClientManipulator CM = new ClientManipulator();
        HttpUtil httpUtil = new HttpUtil();
        private void Form1_Load(object sender, EventArgs e)
        {
            //ShowClientList(CM);
            FillClientList();
            FillDataTable(DateTime.Today);
        }

        public void FillDataTable(DateTime dt)
        {
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].HeaderText = "Дата";
            dataGridView1.Columns[1].HeaderText = "Вид приема";
            dataGridView1.Columns[2].HeaderText = "ФИО и телефон";
            dataGridView1.Columns[3].HeaderText = "Кличка питомца";
            dataGridView1.Rows.Clear(); 
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            List<Visit> list = new List<Visit>();
            foreach (Visit visit in CM.DataList.VisitList)
            {
                if (DateTime.Parse(visit.date).Date == dt)
                {
                    list.Add(visit);
                }
            }
            list.Sort((x, y) => DateTime.Parse(x.date).CompareTo(DateTime.Parse(y.date)));

            foreach (Visit visit in list)
            {
                foreach (Client client in CM.DataList.ClientList)
                {
                    if (visit.phoneNumber == client.phoneNumber)
                        dataGridView1.Rows.Add(visit.date, visit.vid, client.FIO + " " + visit.phoneNumber, client.petName);
                }
            }
            //httpUtil.sendMessage();
        }
        private void FillClientList()
        {
            FoundClientListBox.Items.Clear();
            List<Client> foundClient = CM.Search(FIOSearchTxt.Text, petNameSearchTxt.Text, phoneNumberSearchTxt.Text);
            foreach (Client client in foundClient)
            {
                FoundClientListBox.Items.Add(client.FIO + "; " + client.petName + "; " + client.phoneNumber);
            }
            CM.foundClient = foundClient;
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ShowClientInfo(Client client)
        {
            if (CM != null)
            {
                CM.selectedClient = client;
                FIOTxt.Text = client.FIO;
                phoneNumberTxt.Text = client.phoneNumber;
                petNameTxt.Text = client.petName;
                petSexTxt.Text = client.petSex;
                petBreedTxt.Text = client.petBreed;
                petBirthDateTxt.Text = client.petBirthDate;
                commentTxt.Text = client.comment;
            }
            VisitListBox.Items.Clear();
            List<Visit> visitList = new List<Visit>();
            foreach (Visit visit in CM.DataList.VisitList)
            {
                if (visit.phoneNumber == CM.selectedClient.phoneNumber)
                {
                    visitList.Add(visit);
                }
            }
            visitList.Sort((x,y)=> DateTime.Parse(x.date).CompareTo(DateTime.Parse(y.date)));

            foreach(Visit visit in visitList)
            {
                VisitListBox.Items.Add(visit.date + "; " + visit.phoneNumber);

            }
        }

        private void SearchClients(object sender, EventArgs e)
        {
            FoundClientListBox.Items.Clear();
            List<Client> foundClient = CM.Search(FIOSearchTxt.Text, petNameSearchTxt.Text, phoneNumberSearchTxt.Text);
            foreach (Client client in foundClient)
            {
                FoundClientListBox.Items.Add(client.FIO+"; "+client.petName+"; "+client.phoneNumber);
            }
            CM.foundClient = foundClient;
        }

        private void FoundClientListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(FoundClientListBox.SelectedIndex != -1)
            {
                string info = FoundClientListBox.GetItemText(FoundClientListBox.SelectedItem);
                foreach(Client client in CM.foundClient)
                {
                    if(client.FIO == info.Split(';')[0] && 
                        client.petName == info.Split(';')[1].Trim() && 
                        client.phoneNumber == info.Split(';')[2].Trim() )
                     ShowClientInfo(client);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(Visit visit in CM.DataList.VisitList)
            {
                if(visit.phoneNumber==CM.selectedClient.phoneNumber)
                {
                    visit.phoneNumber = phoneNumberTxt.Text;
                }
            }

            CM.selectedClient.FIO = FIOTxt.Text;
            CM.selectedClient.phoneNumber = phoneNumberTxt.Text;
            CM.selectedClient.petName = petNameTxt.Text;
            CM.selectedClient.petSex = petSexTxt.Text;
            CM.selectedClient.petBreed = petBreedTxt.Text;
            CM.selectedClient.petBirthDate = petBirthDateTxt.Text;
            CM.selectedClient.comment = commentTxt.Text;
            ShowClientInfo(CM.selectedClient);
            FillClientList();

        }

        private void VisitListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (VisitListBox.SelectedIndex != -1)
            {
                string info = VisitListBox.GetItemText(VisitListBox.SelectedItem);
                foreach (Visit visit in CM.DataList.VisitList)
                {
                    string num = info.Split('-')[1].Trim();
                    if (visit.date == info.Split(';')[0] && visit.phoneNumber == info.Split(';')[1].Trim())
                    {
                        VisitInfo vi = new VisitInfo(visit,CM.selectedClient);
                        vi.Show();
                        break;
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CM.SaveDataList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Visit visit = new Visit();
            visit.date = dateTimePicker1.Text;
            visit.phoneNumber = CM.selectedClient.phoneNumber;
            CM.DataList.VisitList.Add(visit); 
            ShowClientInfo(CM.selectedClient);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex != -1) {
                FillDataTable(DateTime.Parse(dateTimePicker2.Text));
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (VisitListBox.SelectedIndex != -1)
            {
                string info = VisitListBox.GetItemText(VisitListBox.SelectedItem);
                foreach (Visit visit in CM.DataList.VisitList)
                {
                    string num = info.Split('-')[1].Trim();
                    if (visit.date == info.Split(';')[0] && visit.phoneNumber == info.Split(';')[1].Trim())
                    {
                        string url = Parameters.settings.url;
                        Dictionary<string,string> data = new Dictionary<string,string>();
                        string title = visit.vid + " " + CM.selectedClient.petName + " " + CM.selectedClient.phoneNumber;
                        string datn = visit.date;
                        data.Add("title",title);
                        data.Add("datn",datn);
                        data.Add("event","delete");
                        string response = await httpUtil.SendPostRequest(url, data);


                        CM.DataList.VisitList.Remove(visit);
                        break;
                    }
                }
            }

            ShowClientInfo(CM.selectedClient);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            FillDataTable(DateTime.Parse(dateTimePicker2.Text));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dateTimePicker2.Text = DateTime.Parse(dateTimePicker2.Text).AddDays(-1).ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dateTimePicker2.Text = DateTime.Parse(dateTimePicker2.Text).AddDays(1).ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var server = new RestServer("http://192.168.1.2/");
            server.Start();
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            Dictionary<string,string> dict = new Dictionary<string,string>();
            dict.Add("param1", "value1");
            dict.Add("param2", "value21");
            dict.Add("param3", "value3");
            await httpUtil.SendPostRequest("http://192.168.1.2/hello", dict);
        }
    }
}
