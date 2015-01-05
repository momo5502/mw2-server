﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Mono.Nat;

namespace MW2_Server
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        class Master
        {
            public string name;
            public string ip;
            public ushort port;
            public IPEndPoint endpoint;
        }

        // Thread and socket stuff
        private Log log;
        private Thread listening_thread;
        private Thread heartbeat_thread;
        private Socket socket;
        private Dictionary<int, Master> masters = new Dictionary<int, Master>();

        // Game config
        public ushort port = 28960;
        public string hostname = "Cod4host";
        public string gametype = "war";
        public string mapname = "mp_afghan";
        public string error = "^5Sorry, momo5502 doesn't want you to play on this server!";
        public int sv_maxclients = 18;
        public int clients = 0;
        public string fs_game = "";
        public string spam = "";
        public ulong npid;

        // Engine stuff
        private ushort protocol = 61586;
        private string gamename = "IW4";
        private string version = "3.0-1"; // aiw3: 4.0-{rev}

        private void Main_Load(object sender, EventArgs e)
        {
            log = new Log(logBox, this);

            log.Print(level.None, "MW2 Server spoofer by momo5502");
            log.Print(level.None, "------------------------------");

            Config.Read(this);

            this.Text = escapeColors(hostname);

            log.Print(level.System, "Starting server.");

            // Print config
            log.Print(level.System, "Hostname: " + hostname);
            log.Print(level.System, "Gametype: " + gametype);
            log.Print(level.System, "Mapname: " + mapname);
            log.Print(level.System, "Maxclients: " + sv_maxclients);
            log.Print(level.System, "Clients: " + clients);
            log.Print(level.System, "Mod: " + fs_game);
            log.Print(level.System, "Spam: " + (spam == "" ? "No" : "Yes"));
            log.Print(level.System, "Error: " + error);

            npid = 0x120000100000000 | 0x555 /*(ulong)(new Random().Next() % 1024)*/; // Fuck, that fixes it for now
            log.Print(level.System, "NPID: " + npid.ToString("X"));

            bool success = false;

            while (!success)
            {
                try
                {
                    // Our endpoint
                    IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, port);

                    // Bind socket
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    socket.Bind(serverEndPoint);
                    success = true;
                }
                catch (System.Exception ex)
                {
                    // Increment port if already bound
                    success = false;
                    port++;
                }
            }

            log.Print(level.System, "Server starting on port " + port + ".");

            // Send UPNP packet to forward the port dynamically
            log.Print(level.System, "Sending UPNP packet.");
            NatUtility.DeviceFound += DeviceFound;
            NatUtility.StartDiscovery();

            // UDP Listerner thread
            log.Print(level.System, "Starting listener thread.");
            listening_thread = new Thread(ListeningThread);
            listening_thread.Start();

            // Heartbeat thread
            log.Print(level.System, "Starting heartbeat thread.");
            heartbeat_thread = new Thread(HeartbeatThread);
            heartbeat_thread.Start();
        }

        // Open ports if UPNP device found
        private void DeviceFound(object sender, DeviceEventArgs args)
        {
            INatDevice device = args.Device;

            // Free TCP and UDP slots
            device.DeletePortMap(new Mapping(Protocol.Tcp, port, port));
            device.DeletePortMap(new Mapping(Protocol.Udp, port, port));

            // Open the slots
            device.CreatePortMap(new Mapping(Protocol.Tcp, port, port));
            device.CreatePortMap(new Mapping(Protocol.Udp, port, port));

            log.Print(level.System, "UPNP device found, mapping port " + port + ".");
        }

        // Remove color codes from a string
        public string escapeColors(string blah)
        {
            return Regex.Replace(blah, @"\^.", "");
        }

        // Listen for incoming packets
        private void ListeningThread()
        {
            log.Print(level.System, "Listener thread started.");

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, port);
            EndPoint Remote = (EndPoint)(sender);

            byte[] data = new byte[8192];

            while (true)
            {
                int recv = socket.ReceiveFrom(data, ref Remote);

                try
                {
                    HandlePacketData(data, recv, Remote);
                }
                catch{} // Ignore that for now

                Thread.Sleep(30);
            }
        }

        // Send print packet with predefined spam message ( client will use Com_Printf to echo to the console )
        private void HandleSpamResponse(EndPoint sender)
        {
            if (spam == "")
                return;

            log.Print(level.Output, sender.ToString() + " <-- spam");

            string printResponse = "print\n" + spam;

            sendData(Encoding.ASCII.GetBytes(printResponse), (IPEndPoint)sender);
        }

        // Send error packet with predefined error message ( client will use Com_Error to print the message )
        private void HandleErrorResponse(EndPoint sender)
        {
            log.Print(level.Output, sender.ToString() + " <-- error");

            string errorResponse = "error\n" + error;

            sendData(Encoding.ASCII.GetBytes(errorResponse), (IPEndPoint)sender);
        }

        // Stats response incomplete...
        private void HandleStatsResponse(EndPoint sender, byte[] _data)
        {
            log.Print(level.Output, " <-- statsResponse");

            byte[] value = new byte[4];
            value[0] = _data[10];
            value[1] = _data[11];
            value[2] = _data[12];
            value[3] = _data[13];

            int response = BitConverter.ToInt32(value, 0);
            response &= 127;

            string statsResponse = sender.ToString() + "statsResponse" + response;

            sendData(Encoding.ASCII.GetBytes(statsResponse), (IPEndPoint)sender);
        }

        // Send connect reponse for client to be able to connect to server
        private void HandleConnectResponse(EndPoint sender)
        {
            log.Print(level.Output, sender.ToString() + " <-- connectResponse");

            string connectResponse = "connectResponse";

            sendData(Encoding.ASCII.GetBytes(connectResponse), (IPEndPoint)sender);
        }

        // Handle challenge requests sent by the client upon connecting
        private void HandleChallengeResponse(string challenge, EndPoint sender)
        {
            log.Print(level.Output, sender.ToString() + " <-- challengeResponse: " + challenge);

            string challengeResponse = "challengeResponse " + challenge;

            sendData(Encoding.ASCII.GetBytes(challengeResponse), (IPEndPoint)sender);
        }

        // Send fake server information
        private void HandleInfoResponse(string challenge, EndPoint sender)
        {
            string optionalMasterSuffix = "";

            for (int i = 0; i < masters.Count; i++)
            {
                if (masters[i].endpoint.Equals(sender))
                {
                    optionalMasterSuffix = " (" + masters[i].name + ")";
                    break;
                }
            }

            log.Print(level.Output, sender.ToString() + " <-- infoResponse: " + challenge + optionalMasterSuffix);

            // Build fake inforesponse
            string inforesponse = "infoResponse\n\\";
            inforesponse += "hc\\0\\";
            inforesponse += "npid\\ " + npid.ToString("X") + "\\";
            inforesponse += "shortversion\\" + version + "\\";
            inforesponse += "pure\\1\\";
            inforesponse += "gametype\\" + gametype + "\\";
            inforesponse += "sv_maxclients\\" + sv_maxclients + "\\";
            inforesponse += "sv_privateClients\\0\\";
            inforesponse += "clients\\" + clients + "\\";
            inforesponse += "mapname\\" + mapname + "\\";
            inforesponse += "protocol\\" + protocol + "\\";
            inforesponse += "gamename\\" + gamename + "\\";
            inforesponse += "hostname\\" + hostname + "\\";
            inforesponse += "fs_game\\" + (fs_game == "" ? "" : "momo " + fs_game) + "\\";

            if (challenge != "")
                inforesponse += "challenge\\" + challenge + "\\";

            sendData(Encoding.ASCII.GetBytes(inforesponse), (IPEndPoint)sender);

            // Send spam message if client updates the serverlist
            if (challenge.Substring(0, 3) == "xxx") // Challenge sent when fetching servers for serverlist
                HandleSpamResponse(sender);
        }

        private void HandleJoinPartyRepsone(string challenge, EndPoint sender)
        {
            string challengeResponse = "challengeResponse " + challenge;

            sendData(Encoding.ASCII.GetBytes(challengeResponse), (IPEndPoint)sender);
        }

        // Evaluate packets and send answers
        private void HandlePacketData(byte[] data, int length, EndPoint sender)
        {
            string message = Encoding.ASCII.GetString(data, 0, length);
            if (length > 4)
                message = message.Substring(4);
            string[] split = message.Split((" ").ToCharArray());
            log.Print(level.Input, sender.ToString() + " --> " + split[0]);

            if(split[0] == "2joinParty" || split[0] == "R")
            {
                log.Print(level.None, message);
            }

            // Handle getinfo requests
            if (message.Contains("getinfo"))
            {
                HandleInfoResponse((split.Length > 1 ? split[1] : ""), sender);
            }

            // Handle getchallenge requests
            else if (message.Contains("getchallenge"))
            {
                HandleChallengeResponse((split.Length > 1 ? split[1] : ""), sender);
            }

            // Handle connect requests
            else if (message.Contains("connect"))
            {
                HandleConnectResponse(sender);
            }

            // Handle stats requests (not finished yet, send error message instead)
            else if (message.Contains("stats"))
            {
                HandleErrorResponse(sender);
                //HandleStatsResponse(sender, data);
            }
        }

        // Add master servers to the heartbeat list
        private void addMaster(string name, string ip, ushort port)
        {
            Master master = new Master();
            master.ip = ip;
            master.port = port;
            master.name = name;

            try
            {
                IPAddress[] addresses = Dns.GetHostAddresses(master.ip);
                master.endpoint = new IPEndPoint(addresses[0], master.port);
            }
            catch
            {
                log.Print(level.System, "Failed to resolve master: " + ip + " (" + master.name + ")");
                return;
            }

            masters.Add(masters.Count, master);

            log.Print(level.System, "Master added: " + master.endpoint.ToString() + " (" + master.name + ")");
        }

        // Handle heartbeats
        private void HeartbeatThread()
        {
            log.Print(level.System, "Heartbeat thread started.");

            addMaster("IW4Play", "server.iw4play.de", 20810);

            byte[] data = new byte[8192];

            Thread.Sleep(5000);
            while (true)
            {
                log.Print(level.System, "Sending heartbeat" + (masters.Count > 1 ? "s" : "") + " to master" + (masters.Count > 1 ? "s" : "") + "."); // Haha
                data = Encoding.ASCII.GetBytes("heartbeat " + gamename + " " + protocol);

                // Send heartbeats to masters
                for (int i = 0; i < masters.Count; i++)
                {
                    log.Print(level.Output, masters[i].endpoint.ToString() + " <-- heartbeat (" + masters[i].name + ")");
                    sendData(data, masters[i].endpoint);
                }

                Thread.Sleep(120000); // 2 minutes
            }
        }

        // Wrapper to send data with correct headers and length
        private void sendData(byte[] data, IPEndPoint server)
        {
            // Add header
            byte[] _data = new byte[data.Length + 4];

            data.CopyTo(_data, 4);

            _data[0] = 0xFF;
            _data[1] = 0xFF;
            _data[2] = 0xFF;
            _data[3] = 0xFF;

            try
            {
                socket.SendTo(_data, (_data.Length > 0x2000 ? 0x2000 : _data.Length), SocketFlags.None, server);
            }
            catch{}

        }

        private void Main_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            // Popup config
            new Panel(this).ShowDialog();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            listening_thread.Abort();
            heartbeat_thread.Abort();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void logBox_TextChanged(object sender, EventArgs e)
        {
            logBox.SelectionStart = logBox.Text.Length;
            logBox.ScrollToCaret();
        }
    }
}