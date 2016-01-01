using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace TcpGuy
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Console.Write("Gib ma hostname: ");
                string addr = Console.ReadLine();
                DoStuff(addr);
            }
            catch(Exception e)
            {
                Console.WriteLine("Ohhh D: -> \n" + e.Message);
            }
            Console.ReadLine();
        }

        static void DoStuff(string address)
        {
            Console.WriteLine("Hallo Welt. Ich bin dumm.");
            List<TcpClient> clients = new List<TcpClient>();

            for(int i = 0; i < 50; i++)
            {
                TcpClient client = new TcpClient();
                client.ReceiveTimeout = 5000;

                try
                {
                    client.Connect(address, 29920);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                clients.Add(client);
                StreamWriter writer = new StreamWriter(client.GetStream());
                sendDt(writer, @"\auth\\gamename\swbfront2ps2\response\f11f999a8f79bb080663a697c87adde5\port\0\id\1\final\");
                Thread.Sleep(50);
                sendDt(writer, @"\newgame\\sesskey\788355045\\challenge\370025987\final\");
                Console.Write("Client #" + i.ToString());
                Console.ReadLine();
            }
        }

        static void sendDt(StreamWriter w, string d)
        {
            d = xorBytes(d, "GameSpy3D");
            w.WriteLine(d);
            w.Flush();
        }

        static string xorBytes(string str, string keystr)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(str);
            byte[] key = System.Text.Encoding.ASCII.GetBytes(keystr);
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(data[i] ^ key[i % key.Length]);
            }
            return System.Text.Encoding.ASCII.GetString(data); ;
        }

    }
}
