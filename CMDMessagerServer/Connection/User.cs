using CMDMessagerServer.Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CMDMessagerServer.Connection
{
    public class User
    {
        public Socket handler { get; }
        public string username { get; set; }

        
        

        public User(Socket s, string name)
        {
            this.handler = s;
            this.username = name;
            Thread t = new Thread(()=>
            {
                handeCMD();

                
            });
            t.Start();
        }


        public void handeCMD()
        {
            string data = null;
            byte[] bytes = new Byte[2048];

            while (true)
            {
                int bytesRec = 0;
                if (handler != null)
                {

                    try
                    {
                        bytesRec = handler.Receive(bytes);
                    }catch (SocketException e)
                    {

                    }

                }

                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                if (data.IndexOf("<EOF>") > -1)
                {

                    break;
                }

            }

            Console.WriteLine("Text received : {0}", data);

            if(data.StartsWith("/"))
            {
                Program.handleCMDS.cmd(data.Replace("<EOF>", ""), this);
            } else
            {
                Program.handleUser.sendAll(data, username);
            }



            handeCMD();
        }

    }
}
