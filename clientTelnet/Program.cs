using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;

namespace clientTelnat
{
    internal class Program
    {
        private static TextReader r;
        private static TextWriter w;


        static void Main(string[] args)
        {
            Socket socket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1000);

            socket.Connect(ipEndPoint);

            NetworkStream ns = new NetworkStream(socket);

            r = new StreamReader(ns);
            w = new StreamWriter(ns);

            Task threadListening = Task.Factory.StartNew(() => Listening());
            Task threadWriting = Task.Factory.StartNew(() => Writing());
            Task.WaitAll(threadListening, threadWriting);










        }

        public static void Listening()
        {
            while (true)
            {
                Console.WriteLine(r.ReadLine());

            }





        }
        public static void Writing()
        {
            while (true)
            {

                w.WriteLine(Console.ReadLine());
                w.Flush();

            }




        }
    }
}

