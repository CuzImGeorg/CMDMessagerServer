using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDMessagerServer.Connection
{
    public class UserHandler
    {
        private List<User> users = new List<User>();



        public void addUser(User user)
        {
            users.Add(user);
        }

        public void sendAll(string  msg)
        {
            foreach (User user in users)
            {
                 user.handler.Send(Encoding.ASCII.GetBytes(msg));

            }
        }


    }
}
