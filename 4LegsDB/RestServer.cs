using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4LegsDB
{
    public class RestServer
    {
        private HttpListener _listener;
        private readonly string _url;
        private bool _isRunning;

        public RestServer(string url)
        {
            _url = url;
            _listener = new HttpListener();
            _listener.Prefixes.Add(_url);
        }

        public void Start()
        {
            if (!_isRunning)
            {
                _isRunning = true;
                _listener.Start();
                MessageBox.Show("Сервер REST API запущен.");
                // Запускаем обработку запросов в отдельном потоке
                Task.Run(() => ProcessRequests());
            }
        }

        public void Stop()
        {
            if (_isRunning)
            {
                _isRunning = false;
                _listener.Stop();
                MessageBox.Show("Сервер REST API остановлен.");
            }
        }

        private void ProcessRequests()
        {
            while (_isRunning)
            {
                try
                {
                    // Ожидаем входящий запрос
                    var context = _listener.GetContext();
                    var request = context.Request;
                    var response = context.Response;
                    
                    //HttpMethod - POST, GET
                    //request.RawUrl - точка, куда приходит
                    using (var stream = request.InputStream)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            var body = reader.ReadToEnd();
                            MessageBox.Show(body);
                        }
                    }
                    // Обработка запроса и формирование ответа
                    // В данном примере возвращается "Hello, World!"
                    var responseData = "Hello, World!";
                    var buffer = System.Text.Encoding.UTF8.GetBytes(responseData);
                    response.ContentLength64 = buffer.Length;
                    var output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    output.Close();
                }
                catch (Exception ex)
                {
                    // Обработка ошибок при обработке запроса
                    MessageBox.Show("Ошибка при обработке запроса: " + ex.Message);
                }
            }
        }
    }
}
