using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CMDMessagerServer.Start;

namespace CMDMessagerServer.Connection
{
    public class HandleConnection
    {
        public Socket listener;
        public Socket handler;

        public void StartListening()
        {

            IPAddress ipAddress = IPAddress.Parse("212.87.212.36");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 12000);

            listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.  
                while (true)
                {

                    Console.WriteLine("Waiting for a connection...");
                    listener.Listen(32);
                    Thread t = new Thread(()=>
                    {
                        handleFirstConnection(listener.Accept());
                    });
                    t.Start();
                                              




                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
            }

        }

        public void handleFirstConnection(Socket socket)
        {
            socket.Send(Encoding.ASCII.GetBytes("Enter Username"));
            string data = null;
            byte[] bytes = new Byte[2048];

            while (true)
            {
                int bytesRec = socket.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                if (data.IndexOf("<EOF>") > -1)
                {

                    break;
                }

            }

            Console.WriteLine("Text received : {0}", data);
            Program.handleUser.addUser(new User(socket ,data));
            

        }


    }
}
