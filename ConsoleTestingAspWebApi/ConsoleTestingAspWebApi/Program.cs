using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ConsoleTestingAspWebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient client = new WebClient();

            client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36");

            Student student = new Student();

            Stream data = client.OpenRead("https://localhost:44376/Student/GetAllApi");

            StreamReader reader = new StreamReader(data);

            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
           

            var obj = serializer.Deserialize(reader, typeof(IEnumerable<Student>)) as IEnumerable<Student>;
            foreach (var item in obj)
            {
                Console.WriteLine($"First name - {item.FirstName}");
                Console.WriteLine($"Last name - {item.LastName}");
                Console.WriteLine($"Group name - {item.GroupName}");
                Console.WriteLine("---------------------------");
            }

            string s = reader.ReadToEnd();
            //Console.WriteLine(s);
            data.Close();
            reader.Close();
        }
    }
}
