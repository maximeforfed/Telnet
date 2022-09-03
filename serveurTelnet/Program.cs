using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Diagnostics;

namespace ServerTelnet
{

    class Config
    {
        public const string openAdress = "0.0.0.0";
        public const int port = 1000;
        public const int MAX_CONNECTION = 1;



    }


    class ClientCommunication
    {
        public Socket socket { get; set; }
        public int numero { get; set; }

        public ClientCommunication(Socket socket, int numero)
        {
            this.socket = socket;
            this.numero = numero;
        }
    }



    internal class Program
    {
        private static int nbrclient;
        static void Main(string[] args)
        {

            Socket socket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(Config.openAdress), Config.port);
            socket.Bind(ipEndPoint);
            socket.Listen(Config.MAX_CONNECTION);
            while (true)
            {


                Socket socketClient = socket.Accept();
                nbrclient = nbrclient + 1;
                ClientCommunication clientCommunication = new ClientCommunication(socketClient, nbrclient);
                Thread thread = new Thread(Communication);
                thread.Start(clientCommunication);
            }







        }

        public static void Communication(object pclientCommunication)

        {
            ClientCommunication clientCommunication = pclientCommunication as ClientCommunication;

            NetworkStream networkStream = new NetworkStream(clientCommunication.socket);
            TextReader reader = new StreamReader(networkStream);
            TextWriter writer = new StreamWriter(networkStream);
            writer.WriteLine("coucou utilisateur");
            writer.Flush();

            while (true)
            {





                writer.WriteLine("taper votre commandes :  ");
                writer.Flush();
                string res = reader.ReadLine();
                writer.WriteLine("votre commande :   " + res);
                writer.Flush();




            }





        }

    }
}
