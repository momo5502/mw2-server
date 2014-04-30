using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace MW2_Server
{
    // Worst way of handling this, but meh :P
    class Config
    {
        private static string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MW2 Server\\";

        public static void Read(Main form)
        {
            if (!Directory.Exists(folder))
            {
                return;
            }

            if (File.Exists(folder + "spam.txt"))
            {
                form.spam = File.ReadAllText(folder + "spam.txt");
            }

            if (File.Exists(folder + "error.txt"))
            {
                form.error = File.ReadAllText(folder + "error.txt");
            }

            if (File.Exists(folder + "hostname.txt"))
            {
                form.hostname = File.ReadAllText(folder + "hostname.txt");
            }

            if (File.Exists(folder + "gametype.txt"))
            {
                form.gametype = File.ReadAllText(folder + "gametype.txt");
            }

            if (File.Exists(folder + "mapname.txt"))
            {
                form.mapname = File.ReadAllText(folder + "mapname.txt");
            }

            if (File.Exists(folder + "fs_game.txt"))
            {
                form.fs_game = File.ReadAllText(folder + "fs_game.txt");
            }

            if (File.Exists(folder + "sv_maxclients.txt"))
            {
                try
                {
                    form.sv_maxclients = Convert.ToUInt32(File.ReadAllText(folder + "sv_maxclients.txt"));
                }
                catch { }
            }

            if (File.Exists(folder + "clients.txt"))
            {
                try
                {
                    form.clients = Convert.ToUInt32(File.ReadAllText(folder + "clients.txt"));
                }
                catch { }
            }

            if (File.Exists(folder + "port.txt"))
            {
                try
                {
                    form.port = Convert.ToUInt16(File.ReadAllText(folder + "port.txt"));
                }
                catch { }
            }
        }

        public static void Save(Main form)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            Write("hostname.txt", form.hostname);
            Write("gametype.txt", form.gametype);
            Write("mapname.txt", form.mapname);
            Write("fs_game.txt", form.fs_game);
            Write("error.txt", form.error);
            Write("spam.txt", form.spam);
            Write("sv_maxclients.txt", form.sv_maxclients.ToString());
            Write("clients.txt", form.clients.ToString());
            Write("port.txt", form.port.ToString());
        }

        private static void Write(string filename, string content)
        {
            var outFile = File.Open(folder + filename, FileMode.Create, FileAccess.Write);
            var writer = new BinaryWriter(outFile);
            writer.Write(ASCIIEncoding.ASCII.GetBytes(content));
            writer.Dispose();
            outFile.Dispose();
        }
    }
}