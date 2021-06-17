using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.Http;
using System.Collections.Generic;
using AngleSharp.Html.Parser;
using AngleSharp.Dom;
using PlayEvent;
using System.Threading.Tasks;
using System.Threading;

namespace Server
{
    class Program
    {
        public List<Vilk> Vilks;
        public static async void JsonParser()
        {
            HttpClient httpClient = new HttpClient();
            string request = "https://positivebet.com/ru/bets/index?ajax=gridBets&au=0&crid=&fl%5B%5D=741851&isAjax=true&markNewEvent=true&perPage=15";
            HttpResponseMessage response = (await httpClient.GetAsync(request)).EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var parser = new HtmlParser();
            var document = parser.ParseDocument(responseBody);
            var trs = document.QuerySelectorAll("tbody tr");
            foreach(var tr in trs)
            {
                var TimeOfLife = tr.QuerySelector("td:nth-child(1) div a")?.TextContent == null ? "отсутсвует": tr.QuerySelector("td:nth-child(1) div a").TextContent;

                var Percent = tr.QuerySelector("td:nth-child(2) nobr")?.TextContent == null ? "отсутсвует" : tr.QuerySelector("td:nth-child(2) nobr").TextContent;
                var BookmakerFirst = tr.QuerySelector("td:nth-child(3) div:nth-child(1)")?.TextContent == null ? "отсутсвует" : tr.QuerySelector("td:nth-child(3) div:nth-child(1)").TextContent;
                var BookmakerSecond = tr.QuerySelector("td:nth-child(3) div:nth-child(2)")?.TextContent == null ? "отсутсвует" : tr.QuerySelector("td:nth-child(3) div:nth-child(2)").TextContent;
                var BookmakerFirstEvent = tr.QuerySelector("td:nth-child(4) div")?.TextContent == null ? "отсутсвует" : tr.QuerySelector("td:nth-child(4) div").TextContent;
                var BookmakerSecondEvent = tr.QuerySelector("td:nth-child(4) small")?.TextContent == null ? "отсутсвует" : tr.QuerySelector("td:nth-child(4) small").TextContent;

                var FirstBookmakerRate = tr.QuerySelector("td:nth-child(5) nobr a:nth-child(2)")?.TextContent == null ? "отсутсвует" : tr.QuerySelector("td:nth-child(5) nobr a:nth-child(2)").TextContent;
                var SecondBookmakerRate = tr.QuerySelector("td:nth-child(5)  br + nobr")?.TextContent == null ? "отсутсвует" : tr.QuerySelector("td:nth-child(5)  br + nobr").TextContent;

                var FirstBookmakerCoefficient = tr.QuerySelector("td:nth-child(6)  br + nobr")?.TextContent == null ? "отсутсвует" : tr.QuerySelector("td:nth-child(6)  br + nobr").TextContent;
                var SecondBookmakerCoefficient = tr.QuerySelector("td:nth-child(6) div:nth-child(2) br + nobr")?.TextContent == null ? "отсутсвует" : tr.QuerySelector("td:nth-child(6) div:nth-child(2) br + nobr").TextContent;

                Console.WriteLine("TimeOfLife(Время жизни вилки) " + TimeOfLife);
                Console.WriteLine("Percent(Процент) " + Percent );
                Console.WriteLine("BookmakerFirst(Первый букмекер)  " + BookmakerFirst);
                Console.WriteLine("BookmakerSecond(Второй букмекер)  " + BookmakerSecond );
                Console.WriteLine("BookmakerFirstEvent(Подробности события первого букмекера) " + BookmakerFirstEvent );
                Console.WriteLine("BookmakerSecondEvent(Подробности события второго букмекера)  " + BookmakerSecondEvent );
                Console.WriteLine("FirstBookmakerRate(Ставка у первого букмекера) " + FirstBookmakerRate);
                Console.WriteLine("SecondBookmakerRate(Ставка у второго букмекера) " + SecondBookmakerRate);
                Console.WriteLine("FirstBookmakerCoefficient(Коэффициент у первого букмекера) " + FirstBookmakerCoefficient );
                Console.WriteLine("SecondBookmakerCoefficient(Коээфициент у второго букмекера) " + SecondBookmakerCoefficient );
                Console.WriteLine("*******NEXT ****************************************");
            }
        }
        static void Main(string[] args)
        {
            JsonParser();
            while (true) {
                Thread.Sleep(10000);
                Console.Clear();
                JsonParser();
            }
            
            #region Server
            // Устанавливаем для сокета локальную конечную точку
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000);

            // Создаем сокет Tcp/Ip
            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Назначаем сокет локальной конечной точке и слушаем входящие сокеты
            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);

                // Начинаем слушать соединения
                while (true)
                {
                    Console.WriteLine("Ожидаем соединение через порт {0}", ipEndPoint);

                    // Программа приостанавливается, ожидая входящее соединение
                    Socket handler = sListener.Accept();
                    string data = null;

                    // Мы дождались клиента, пытающегося с нами соединиться

                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);

                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    // Показываем данные на консоли
                    Console.Write("Полученный текст: " + data + "\n\n");

                    // Отправляем ответ клиенту\
                    string reply = "Спасибо за запрос в " + data.Length.ToString()
                            + " символов";
                    byte[] msg = Encoding.UTF8.GetBytes(reply);
                    handler.Send(msg);

                    if (data.IndexOf("<TheEnd>") > -1)
                    {
                        Console.WriteLine("Сервер завершил соединение с клиентом.");
                        break;
                    }

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
            #endregion
        }
        
    }
}
