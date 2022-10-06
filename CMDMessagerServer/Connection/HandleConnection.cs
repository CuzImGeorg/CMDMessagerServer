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
          // IPAddress ipAddress = IPAddress.Parse("127.0.0.1");


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
                    Socket s = listener.Accept();
                    Thread t = new Thread(()=>
                    {
                        handleFirstConnection(s);
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
            socket.Send(Encoding.ASCII.GetBytes("Enter Username<EOF>"));
            string data = null;
            byte[] bytes = new Byte[2048];

            while (true)
            {
                int bytesRec = 0;
                try {
                     bytesRec = socket.Receive(bytes);
                } catch (SocketException e)
                {

                }
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
