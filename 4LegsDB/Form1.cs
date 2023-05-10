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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ClientManipulator CM = new ClientManipulator();

        private void Form1_Load(object sender, EventArgs e)
        {
            //ShowClientList(CM);
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

        private void ShowClientList(Client client)
        {
            if (CM != null)
            {
                CM.selectedClient = CM.ClientList.IndexOf(client);
                FIOTxt.Text = client.FIO;
                phoneNumberTxt.Text = client.phoneNumber;
                petNameTxt.Text = client.petName;
                petSexTxt.Text = client.petSex;
                petBreedTxt.Text = client.petBreed;
                petBirthDateTxt.Text = client.petBirthDate;
                commentTxt.Text = client.comment;
            }
        }

        private void SearchClients(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            List<Client> foundClient = CM.Search(FIOSearchTxt.Text, petNameSearchTxt.Text, phoneNumberSearchTxt.Text);
            foreach (Client client in foundClient)
            {
                listBox1.Items.Add(client.FIO+"; "+client.petName+"; "+client.phoneNumber);
            }
            CM.foundClient = foundClient;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex != -1)
            {
                string info = listBox1.GetItemText(listBox1.SelectedItem);
                foreach(Client client in CM.foundClient)
                {
                    if(client.FIO == info.Split(';')[0] && client.petName == info.Split(';')[1].Trim() && client.phoneNumber == info.Split(';')[2].Trim() )
                     ShowClientList(client);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VisitInfo vi = new VisitInfo(CM.ClientList[CM.selectedClient]);
            vi.Show();
        }
    }
}
