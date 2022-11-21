using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CMDMessagerServer.Connection
{
    public class UserHandler
    {
        public  List<User> users = new List<User>();
        public List<String> msgs = new List<string>();


        public void addUser(User user)
        {
            users.Add(user);
        }

        public void sendAll(string  msg, string username)
        {

            msgs.Add(("[" + DateTime.Now.ToString("HH:mm:ss") + "]" + username + ":" + msg));
            try
            {
                foreach (User user in users)
                {
                    try
                    {
                        if (user.handler.Connected)
                        {
                            user.handler.Send(Encoding.ASCII.GetBytes("[" + DateTime.Now.ToString("HH:mm:ss") + "]" + username + ":" + msg));
                        }

                    }
                    catch (Exception e)
                    {

                    }

                }
            } catch (System.InvalidOperationException e)
            {

            }
        }


    }
}
