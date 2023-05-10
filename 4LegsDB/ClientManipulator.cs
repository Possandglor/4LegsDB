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
        public int selectedClient;
        public List<Client> ClientList = new List<Client>();

        public ClientManipulator() {
            Client first = JsonConvert.DeserializeObject<Client>(File.ReadAllText("client.json"));
            Client second = JsonConvert.DeserializeObject<Client>(File.ReadAllText("second.json"));

            ClientList.Add(first);
            ClientList.Add(second);
        }
        
        public List<Client> Search(string FIO, string petName, string phoneNumber)
        {
            List<Client> list = new List<Client>();
            foreach (Client client in ClientList)
            {
                if(client.FIO.Contains(FIO) && client.petName.Contains(petName) && client.phoneNumber.Contains(phoneNumber))
                    list.Add(client);
            }
            return list;
        }
    }
}
