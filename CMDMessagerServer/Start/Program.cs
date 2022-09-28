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
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting Server");
            handleUser = new UserHandler();

        }

       


    }
}
