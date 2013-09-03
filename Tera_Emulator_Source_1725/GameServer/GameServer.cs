using System;
using System.Diagnostics;
using System.Threading;
using Communication;
using Communication.Interfaces;
using Communication.Logic;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Service;
using Network;
using Tera.Services;
using Utils;
using Data.DAO;
using Configuration;

namespace Tera
{
    internal class GameServer : Global
    {
        public static TcpServer TcpServer;

        #region Main
        public static void Main()
        {
            //Set Title
            Console.Title = "[Tera-Online] {ProjectS1.GameServer}";
            //Cancel KeyEvent
            Console.CancelKeyPress += CancelEventHandler;

            try
            {
                RunServer();
            }
            catch (Exception ex)
            {
                Log.FatalException("Can't start server!", ex);
                return;
            }

            //Main Loop
            MainLoop();
            //Stop Server on Errors
            StopServer();
            //Kill Server Process
            Process.GetCurrentProcess().Kill();
        }
        #endregion Main

        private static void RunServer()
        {
            //Start ServerStartTime
            Stopwatch serverStartStopwatch = Stopwatch.StartNew();
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;

            //CheckServerMode
            CheckServerMode();

            //ConsoleOutput-Infos
            PrintServerLicence();
            PrintServerInfo();

            //Initialize TcpServer
            TcpServer = new TcpServer("*", Configuration.Network.GetServerPort(), Configuration.Network.GetServerMaxCon());
            Connection.SendAllThread.Start();

            //Initialize Server OpCodes
            OpCodes.Init();
            Console.WriteLine("----------------------------------------------------------------------------\n"
                +"---===== OpCodes - Revision: " + OpCodes.Version + " EU initialized!");
            
            //Global Services
            #region global_components
            //Services
            FeedbackService = new FeedbackService();
            AccountService = new AccountService();
            PlayerService = new PlayerService();
            MapService = new MapService();
            ChatService = new ChatService();
            VisibleService = new VisibleService();
            ControllerService = new ControllerService();
            CraftService = new CraftService();
            ItemService = new ItemService();
            AiService = new AiService();
            GeoService = new GeoService();
            StatsService = new StatsService();
            ObserverService = new ObserverService();
            AreaService = new AreaService();
            TeleportService = new TeleportService();
            PartyService = new PartyService();
            SkillsLearnService = new SkillsLearnService();
            CraftLearnService = new CraftLearnService();
            GuildService = new GuildService();
            EmotionService = new EmotionService();
            RelationService = new RelationService();
            DuelService = new DuelService();
            StorageService = new StorageService();
            TradeService = new TradeService();
            MountService = new MountService();

            //Engines
            ActionEngine = new ActionEngine.ActionEngine();
            AdminEngine = new AdminEngine.AdminEngine();
            SkillEngine = new SkillEngine.SkillEngine();
            QuestEngine = new QuestEngine.QuestEngine();
            #endregion

            //Set SqlDatabase Connection
            GlobalLogic.ServerStart("SERVER=" + DAOManager.MySql_Host + ";DATABASE=" + DAOManager.MySql_Database + ";UID=" + DAOManager.MySql_User + ";PASSWORD=" + DAOManager.MySql_Password + ";PORT=" + DAOManager.MySql_Port + ";charset=utf8");
            Console.ForegroundColor = ConsoleColor.Gray;

            //Start Tcp-Server Listening
            Console.WriteLine("----------------------------------------------------------------------------\n"
                + "---===== Loading GameServer Service.\n"
                + "----------------------------------------------------------------------------");
            TcpServer.BeginListening();

            //Stop ServerStartTime
            serverStartStopwatch.Stop();
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine("---===== GameServer start in {0}", (serverStartStopwatch.ElapsedMilliseconds / 1000.0).ToString("0.00s"));
            Console.WriteLine("----------------------------------------------------------------------------");
        }

        #region CheckServerMode
        //ServerMode
        public static void CheckServerMode()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            try
            {
                if (Configuration.Server.GetServerMode() == 0)
                {
                    Log.Info("ServerMode:" + Configuration.Server.GetServerMode() + "=DEBUG");
                    //ToDo: start functions if needed for this mode
                }
                else if (Configuration.Server.GetServerMode() == 1)
                {
                    Log.Info("ServerMode:" + Configuration.Server.GetServerMode() + "=RELEASE");
                    //ToDo: start functions if needed for this mode
                }
                else if (Configuration.Server.GetServerMode() == 2)
                {
                    Log.Info("ServerMode:" + Configuration.Server.GetServerMode() + "=TEST");
                    //ToDo: start functions if needed for this mode
                }
                else if (Configuration.Server.GetServerMode() != 0 || Configuration.Server.GetServerMode() != 1 || Configuration.Server.GetServerMode() != 2)
                {
                    Log.Error("ServerMode:" + Configuration.Server.GetServerMode() + "=UNKNOWN");
                    //ToDo: start functions if needed for this mode
                }

            }
            catch (Exception ex)
            {
                Log.FatalException("ServerMode not set to a correct Mode!", ex);
                return;
            }

        }
        #endregion CheckServerMode

        #region ConsoleOutput-Infos
        //License Text
        public static void PrintServerLicence()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            //Licence Info
            Console.WriteLine("----------------------------------------------------------------------------\n"
            + "ProjectS1.GameServer\n\n"
            + "This program is free software;\nyou can redistribute it and/or modify it under the terms of the \nGNU General Public License as published by the Free Software Foundation;\neither version 2 of the License, or (at your option) any later version.\n\n"
            + "This program is distributed in the hope that it will be useful,\nbut WITHOUT ANY WARRANTY; without even the implied warranty of\nMERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.\n\nSee the GNU General Public License for more details.\n\n"
            + "Copyright (C) 2012 (ProjectS1.GameServer)\n"
            + "Author: rework by P5yl0\n "
            + "----------------------------------------------------------------------------");

        }
        //Server Info Text
        public static void PrintServerInfo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            //Server Info
            Console.WriteLine("---===== Database IP: " + DAOManager.MySql_Host);
            Console.WriteLine("---===== Database Port: " + DAOManager.MySql_Port);
            Console.WriteLine("---===== Database User: " + DAOManager.MySql_User);
            Console.WriteLine("---===== Database Pass: ***");// + BaseDAO.MySql_Password);
            Console.WriteLine("---===== Database Name: " + DAOManager.MySql_Database);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        #endregion ConsoleOutput-Infos

        #region ServerEvents
        //Unhandled Server Exception Event
        protected static void UnhandledException(Object sender, UnhandledExceptionEventArgs args)
        {
            Log.FatalException("UnhandledException", (Exception)args.ExceptionObject);
            ShutdownServer();

            while (true)
            {
                Thread.Sleep(1);
            }
        }

        //Server Cancel Event
        protected static void CancelEventHandler(object sender, ConsoleCancelEventArgs args)
        {
            ShutdownServer();

            while (true)
            {
                Thread.Sleep(1);
            }
        }
        
        //Server Stop
        private static void StopServer()
        {
            TcpServer.ShutdownServer();
            GlobalLogic.OnServerShutdown();
        }

        #endregion ServerEvents

    }
}