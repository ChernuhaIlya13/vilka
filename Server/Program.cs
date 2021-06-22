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
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Data.SqlClient;

namespace Server
{
    class Program
    {
        public static List<Vilk> Vilks { get; set; } = new List<Vilk> { };
        public static async void JsonParser()
        {
            HttpClient httpClient = new HttpClient();
            string request = "https://positivebet.com/ru/bets/index?ajax=gridBets&au=0&crid=&fl%5B%5D=741851&isAjax=true&markNewEvent=true&perPage=15";
            HttpResponseMessage response = (await httpClient.GetAsync(request)).EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var parser = new HtmlParser();
            var document = parser.ParseDocument(responseBody);
            var trs = document.QuerySelectorAll("tbody tr");
            int VilkId = 1;
            foreach (var tr in trs)
            {
                var TimeOfLife = tr.QuerySelector("td:nth-child(1) div a")?.TextContent == null ? "отсутсвует" : tr.QuerySelector("td:nth-child(1) div a").TextContent;

                var Percent = tr.QuerySelector("td:nth-child(2) nobr")?.TextContent == null ? "отсутсвует" : tr.QuerySelector("td:nth-child(2) nobr").TextContent;
                var BookmakerFirst = tr.QuerySelector("td:nth-child(3) div:nth-child(1)")?.TextContent == null ? "отсутсвует" : tr.QuerySelector("td:nth-child(3) div:nth-child(1)").TextContent;
                var BookmakerSecond = tr.QuerySelector("td:nth-child(3) div:nth-child(2)")?.TextContent == null ? "отсутсвует" : tr.QuerySelector("td:nth-child(3) div:nth-child(2)").TextContent;
                string PlayersFirstEvent = tr.QuerySelector("td:nth-child(4) div")?.TextContent == null ? "отсутсвует" : tr.QuerySelector("td:nth-child(4) div").TextContent;
                var InfoAboutLeagueCountTimeFirstEvent = tr.QuerySelector("td:nth-child(4) small")?.TextContent == null ? "отсутсвует" : tr.QuerySelector("td:nth-child(4) small").TextContent;

                var PlayersSecondEvent = tr.QuerySelector("td:nth-child(4) br + a + div")?.TextContent == null ? "отсутсвует" : tr.QuerySelector("td:nth-child(4) div").TextContent;
                var InfoAboutLeagueCountTimeSecondEvent = tr.QuerySelector("td:nth-child(4) br + a + div + small")?.TextContent == null ? "отсутсвует" : tr.QuerySelector("td:nth-child(4) small").TextContent;

                var FirstBookmakerRate = tr.QuerySelector("td:nth-child(5) nobr a:nth-child(2)")?.TextContent == null ? "отсутсвует" : tr.QuerySelector("td:nth-child(5) nobr a:nth-child(2)").TextContent;
                var SecondBookmakerRate = tr.QuerySelector("td:nth-child(5)  br + nobr")?.TextContent == null ? "отсутсвует" : tr.QuerySelector("td:nth-child(5)  br + nobr").TextContent;

                var FirstBookmakerCoefficient = tr.QuerySelector("td:nth-child(6)  br + nobr")?.TextContent == null ? "отсутсвует" : tr.QuerySelector("td:nth-child(6)  br + nobr").TextContent;
                var SecondBookmakerCoefficient = tr.QuerySelector("td:nth-child(6) div:nth-child(2) br + nobr")?.TextContent == null ? "отсутсвует" : tr.QuerySelector("td:nth-child(6) div:nth-child(2) br + nobr").TextContent;

                Regex regex = new Regex(@"\((.+)\)");
                var MatchedInfo = regex.Matches(InfoAboutLeagueCountTimeSecondEvent);
                Match infoAboutLeague = null;

                if (MatchedInfo.Count != 0)
                {
                    infoAboutLeague = MatchedInfo[0];//Информация о Лиге
                }

                string firstPlayerFirstEvent = null;
                string secondPlayerFirstEvent = null;
                string firstPlayerSecondEvent = null;
                string secondPlayerSecondEvent = null;

                if (PlayersFirstEvent.Length > 0 && PlayersFirstEvent != "отсутсвует")
                {
                    //Первый игрок первого события
                    firstPlayerFirstEvent = PlayersFirstEvent.Split("vs")[0];
                    //Второй игрок второго события
                    secondPlayerFirstEvent = PlayersFirstEvent.Split("vs")[1];
                }
                if (PlayersSecondEvent.Length > 0 && PlayersSecondEvent != "отсутсвует")
                {
                    //Первый игрок первого события
                    firstPlayerSecondEvent = PlayersSecondEvent.Split("vs")[0];
                    //Второй игрок второго события
                    secondPlayerSecondEvent = PlayersSecondEvent.Split("vs")[1];
                }


                Vilk vilk = new Vilk(VilkId,TimeOfLife, Percent, BookmakerFirst, new PlayEvt(firstPlayerFirstEvent, secondPlayerFirstEvent, new ScoreGame(), 0), BookmakerSecond, new PlayEvt(firstPlayerSecondEvent, secondPlayerSecondEvent, new ScoreGame(), 0), new string[] { FirstBookmakerRate, SecondBookmakerRate }, new string[] { FirstBookmakerCoefficient, SecondBookmakerCoefficient });
                VilkId++;

                Vilks.Add(vilk);

                
                Console.WriteLine("TimeOfLife(Время жизни вилки) " + TimeOfLife);

                Console.WriteLine("Percent(Процент) " + Percent);

                Console.WriteLine("BookmakerFirst(Первый букмекер)  " + BookmakerFirst);


                Console.WriteLine("BookmakerSecond(Второй букмекер)  " + BookmakerSecond);

                Console.WriteLine("PlayersFirstEvent(Игроки(команды) первого события) " + PlayersFirstEvent);

                Console.WriteLine("InfoAboutLeagueCountTimeSecondEvent(Информация о лиге,счёте и прочее) первого события)  " + InfoAboutLeagueCountTimeFirstEvent);

                Console.WriteLine("PlayersSecondEvent(Игроки(команды) первого события) " + PlayersSecondEvent);

                Console.WriteLine("InfoAboutLeagueCountTimeSecondEvent(Информация о лиге,счёте и прочее) второго события)  " + InfoAboutLeagueCountTimeSecondEvent);

                Console.WriteLine("PlayersFirstEvent(ставка первого букмекера) " + FirstBookmakerRate);

                Console.WriteLine("SecondBookmakerRate(ставка первого букмекера)  " + SecondBookmakerRate);

                Console.WriteLine("FirstBookmakerCoefficient(Коэффициент у первого букмекера) " + FirstBookmakerCoefficient);

                Console.WriteLine("SecondBookmakerCoefficient(Коээфициент у второго букмекера) " + SecondBookmakerCoefficient);
                Console.WriteLine("*******NEXT ****************************************");
            }

            Console.WriteLine("Выводим список вилок");
            Console.WriteLine("Количество вилок " + Vilks.Count);
        }
        public static async Task ConnectToDatabase()
        {
            string connectionString = @"Server=DESKTOP-49F59OC;Database=Betting;Trusted_Connection=True;";

            // Создание подключения
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Vilks";
                command.Connection = connection;
                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    var coltimeOfLife = reader.GetName(1);
                    var colPercent = reader.GetName(2);
                    var colBookmakerFirst = reader.GetName(3);
                    var colBookmakerFirstEvent = reader.GetName(4);
                    var colBookmakerSecond = reader.GetName(5);
                    var colBookmakerSecondEvent = reader.GetName(6);
                    var colRate = reader.GetName(7);
                    var colCoefficient = reader.GetName(8);

                    Console.WriteLine($"{coltimeOfLife}\t {colPercent}\t {colBookmakerFirst}\t {colBookmakerFirstEvent}\t {colBookmakerSecond}\t {colBookmakerSecondEvent}\t {colRate}\t {colCoefficient}");

                    while (await reader.ReadAsync()) // построчно считываем данные
                    {
                        var timeOfLife = reader.GetValue(1);
                        var Percent = reader.GetValue(2);
                        var BookmakerFirst = reader.GetValue(3);
                        var BookmakerFirstEvent = reader.GetValue(4);
                        var BookmakerSecond = reader.GetValue(5);
                        var BookmakerSecondEvent = reader.GetValue(6);
                        var Rate = reader.GetValue(7);
                        var Coefficient = reader.GetValue(8);

                        Console.WriteLine($"{timeOfLife}\t {Percent}\t {BookmakerFirst}\t {BookmakerFirstEvent}\t {BookmakerSecond}\t {BookmakerSecondEvent}\t {Rate}\t {Coefficient}\t");
                    }
                }
            }
        }
        public static  void Main(string[] args)
        {
            //Подключается к базе данных и забирает выводит все вилки
            // ConnectToDatabase();
            JsonParser();
            while (true) {
                Thread.Sleep(15000);
                Console.Clear();
                JsonParser();
            }
            //Thread.Sleep(10000);
            /*#region Server
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000);

            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);

                while (true)
                {
                    Console.WriteLine("Ожидаем соединение через порт {0}", ipEndPoint);

                    Socket handler = sListener.Accept();
                    string data = null;


                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);

                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    Console.Write("Полученный текст: " + data + "\n\n");

                    string reply = "Спасибо за запрос в " + data.Length.ToString()
                            + " символов";
                    byte[] msg = Encoding.UTF8.GetBytes(reply);
                    //Получаем  вилки клиенту
                    BinaryFormatter formatter = new BinaryFormatter();
                    using (MemoryStream stream = new MemoryStream())
                    {
                        foreach (var vilk in Vilks)
                            formatter.Serialize(stream, vilk);
                        handler.Send(stream.GetBuffer());
                    }

                    //handler.Send(msg);

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
            */
        }

    }
}
