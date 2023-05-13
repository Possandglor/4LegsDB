using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace _4LegsDB
{
    internal class ClientManipulator
    {
        public List<Client> foundClient = new List<Client>();
        public Client selectedClient;
        //public List<Visit> VisitList = new List<Visit>();
        //public List<Client> ClientList = new List<Client>();

        public DataBase DataList;

        public ClientManipulator() {
            DataList = JsonConvert.DeserializeObject<DataBase>(File.ReadAllText("db.json"));
            //Client first = JsonConvert.DeserializeObject<Client>(File.ReadAllText("client.json"));
            Client second = JsonConvert.DeserializeObject<Client>(File.ReadAllText("second.json"));

            //ClientList.Add(first);
           // ClientList.Add(second);
        }
        
        public List<Client> Search(string FIO, string petName, string phoneNumber)
        {
            List<Client> list = new List<Client>();
            foreach (Client client in DataList.ClientList)
            {
                if(client.FIO.ToLower().Contains(FIO.ToLower()) && client.petName.ToLower().Contains(petName.ToLower()) && client.phoneNumber.Contains(phoneNumber))
                    list.Add(client);
            }
            return list;
        }

        public void SaveDataList()
        {
            File.WriteAllText("db.json",JsonConvert.SerializeObject(DataList));
        }
    }
}
