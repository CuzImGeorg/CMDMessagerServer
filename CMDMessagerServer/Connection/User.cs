using CMDMessagerServer.Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace CMDMessagerServer.Connection
{
    public class User
    {
        public Socket handler { get; }
        public string username { get; set; }
        public Thread t { get; private set; }




        public User(Socket s, string name)
        {
            ValueWrapper<bool> ct = new(true);
            this.handler = s;
            this.username = name;
            t = new Thread(()=>
            {
                handeCMD(ct);

                
            });
            t.Start();
            Thread d = new Thread(() => {

                while (handler.Connected)
                {
                    Thread.Sleep(500);

                }

                handler.Close();
                ct.Value = false;
                Program.handleUser.users.Remove(this);
                

            });
            d.Start();
            
        }


        public void handeCMD(ValueWrapper<bool> ct)
        {

            while (ct.Value)
            {
                try
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
                            }
                            catch (SocketException e)
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

                    if (data.StartsWith("/"))
                    {
                        Program.handleCMDS.cmd(data.Replace("<EOF>", ""), this);
                    }
                    else
                    {
                        Program.handleUser.sendAll(data, username);
                    }
                }catch(Exception e)
                {

                }
            }
        }

    }

    public class ValueWrapper<T> where T : struct
    {
        public T Value { get; set; }
        public ValueWrapper(T value) { this.Value = value; }
    }
}
