using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

public struct ParamList
{
    public string url { get; set; }
    // Информация о почтовом сервере
    public string smtpServer { get; set; }
    public int smtpPort { get; set; }
    public string smtpUsername { get; set; }
    public string smtpPassword { get; set; }

    // Отправитель, получатель и тема письма
    public string from { get; set; }
    public string to { get; set; }
    public string subject { get; set; }

    // Путь к файлу, который нужно отправить
    public string filePath { get; set; }
}

namespace _4LegsDB
{
    public static class Parameters
    {


        public static ParamList settings = JsonConvert.DeserializeObject<ParamList>(File.ReadAllText("params.json"));

        
    }
}
