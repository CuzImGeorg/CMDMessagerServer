using CMDMessagerServer.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDMessagerServer.Start
{
    public class Program
    {
        public static UserHandler handleUser { get; set; }
        public static HanndleCMDS handleCMDS { get; private set; }
        public static HandleConnection handleConnection { get; set; }

        
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting Server");
            handleUser = new UserHandler();
            handleCMDS = new HanndleCMDS(); 
            handleConnection = new HandleConnection();
            handleConnection.StartListening();

        }


        


    }
}
