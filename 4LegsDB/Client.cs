using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace _4LegsDB
{
    public class Client
    {
        public string FIO {get; set;}
        public string phoneNumber {get; set;}
        public string petName {get; set;}
        public string petSex {get; set; }
        public string petBreed {get; set;}
        public string petBirthDate {get; set;}
        public string comment {get; set;}
        public string[] analyses { get; set; }
    }
}
