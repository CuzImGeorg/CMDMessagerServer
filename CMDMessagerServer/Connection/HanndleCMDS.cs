using CMDMessagerServer.Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDMessagerServer.Connection
{
    public class HanndleCMDS
    {
        public Dictionary<string, string> commands { get; set; }

        public HanndleCMDS()
        {
            commands = new Dictionary<string, string>();
            commands.Add("clear", "Clears the Cosole");
            commands.Add("cls", "as clear");
            commands.Add("hide", "as clear");
            commands.Add("help", " as ?");
            commands.Add("?", "get all Commands");
            commands.Add("lu", "List all user");
            commands.Add("show", "Empfängt alle MSg neu");
            commands.Add("clearmsgs", "Löscht den Msg Cashe");

        }

        public string cmd(string cmd, User u)
        {
            switch(cmd.Substring(1))
            {
                case "lu": 
                {
                    foreach(User user in Program.handleUser.users) {

                        u.handler.Send(Encoding.ASCII.GetBytes(user.username + "<EOF>"));
                    }

                    break;
                }
                case "?":
                {
                    foreach (string s in commands.Keys)
                    {
                            u.handler.Send(Encoding.ASCII.GetBytes("/" + s + "                     " + commands[s] + "<EOF>" ));
                            Thread.Sleep(100);

                        }
                        break;
                }
                case "help":
                {
                        this.cmd("/?", u);
                    break;
                }
                case "show" : {
                    foreach(string s in Program.handleUser.msgs) {
                        u.handler.Send(Encoding.ASCII.GetBytes(s));
                        Thread.Sleep(100);
                    }


                    break;
                }
                case "clearmsg": {
                    Program.handleUser.msgs.Clear();

                    break;
                }
            }
            if(cmd.Substring(1).StartsWith("rename "))
            {
                u.username = cmd[8..];

            }
      





            return null;
        }
        
    }
}
