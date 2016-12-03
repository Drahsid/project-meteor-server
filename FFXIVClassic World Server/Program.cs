﻿using MySql.Data.MySqlClient;
using NLog;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVClassic_World_Server
{
    class Program
    {
        public static Logger Log;

        static void Main(string[] args)
        {
            // set up logging
            Log = LogManager.GetCurrentClassLogger();
#if DEBUG
            TextWriterTraceListener myWriter = new TextWriterTraceListener(System.Console.Out);
            Debug.Listeners.Add(myWriter);

            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Threading.Thread.Sleep(15000);
            }

#endif
            bool startServer = true;

            Log.Info("==================================");
            Log.Info("FFXIV Classic World Server");
            Log.Info("Version: 0.0.1");            
            Log.Info("==================================");

            //Load Config
            if (!ConfigConstants.Load())
                startServer = false;

            //Test DB Connection
            Log.Info("Testing DB connection... ");
            using (MySqlConnection conn = new MySqlConnection(String.Format("Server={0}; Port={1}; Database={2}; UID={3}; Password={4}", ConfigConstants.DATABASE_HOST, ConfigConstants.DATABASE_PORT, ConfigConstants.DATABASE_NAME, ConfigConstants.DATABASE_USERNAME, ConfigConstants.DATABASE_PASSWORD)))
            {
                try
                {
                    conn.Open();
                    conn.Close();

                    Log.Info("Connection ok.");
                }
                catch (MySqlException e)
                {
                    Log.Error(e.ToString());
                    startServer = false;
                }
            }
          
            //Start server if A-OK
            if (startServer)
            {
                Server server = new Server();                
                server.StartServer();

                while (startServer)
                {
                    String input = Console.ReadLine();
                    Log.Info("[Console Input] " + input);
                    //cp.DoCommand(input, null);
                }
            }

            Program.Log.Info("Press any key to continue...");
            Console.ReadKey();
        }
    }
}