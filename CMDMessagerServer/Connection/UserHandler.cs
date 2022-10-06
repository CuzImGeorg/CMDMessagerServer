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



        public void addUser(User user)
        {
            users.Add(user);
        }

        public void sendAll(string  msg, string username)
        {
            try
            {
                foreach (User user in users)
                {
                    try
                    {
                        user.handler.Send(Encoding.ASCII.GetBytes("[" + DateTime.Now.ToString("HH:mm:ss") + "]" + username + ":" + msg));

                    }
                    catch (SocketException e)
                    {
                        users.Remove(user);
                    }

                }
            } catch (System.InvalidOperationException e)
            {

            }
        }


    }
}
