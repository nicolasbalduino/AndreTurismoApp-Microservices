// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using AndreTurismoApp.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Data.SqlClient;
using System.Net.Http.Json;
using AndreTurismoApp.Consumer;

internal class Program
{
    //readonly string strConn = @"Server=(localdb)\\mssqllocaldb;Database=AndreTurismoApp;Trusted_Connection=True;MultipleActiveResultSets=true";
    //readonly SqlConnection conn;
    //public Program()
    //{
    //    conn = new SqlConnection(strConn);
    //    conn.Open();
    //}

    private static void Main(string[] args)
    {
        //string strConn = "Server=(localdb)\\mssqllocaldb;Database=AndreTurismoApp;Trusted_Connection=True;MultipleActiveResultSets=true";
        //SqlConnection conn;
        //conn = new SqlConnection(strConn);
        


        const string QUEUE_NAME = "city";

        var factory = new ConnectionFactory() { HostName = "localhost" };

        using (var connection = factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {


                channel.QueueDeclare(queue: QUEUE_NAME,
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);

                while (true)
                {
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        //conn.Open();
                        var body = ea.Body.ToArray();
                        var returnCity = Encoding.UTF8.GetString(body);
                        var city = JsonConvert.DeserializeObject<City>(returnCity);
                        Console.WriteLine("Description: " + city.Description);

                        new CityRabbitService().Insert(city);

                        //string strInsert = "insert into City (Description, DtCreated) values ( @Description, @DtCreated)";

                        //SqlCommand commandInsert = new SqlCommand(strInsert, conn);
                        //commandInsert.Parameters.Add(new SqlParameter("@Description", city.Description));
                        //commandInsert.Parameters.Add(new SqlParameter("@DtCreated", DateTime.UtcNow));

                        //commandInsert.ExecuteNonQuery();
                        //conn.Close();
                    };

                    channel.BasicConsume(queue: QUEUE_NAME,
                                         autoAck: true,
                                         consumer: consumer);

                    Thread.Sleep(2000);
                }
            }
        }
    }
}