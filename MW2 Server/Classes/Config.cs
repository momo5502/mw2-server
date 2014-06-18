using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Threading.Tasks;

namespace MW2_Server
{
    // much better way of handling it
    class Config
    {
        // defining the xmlreader and writer
        private static XmlReader xmlre;
        private static XmlWriter xmlwr;
        // defining the folder where we house all the files
        private static string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MW2 Server\\";

        public static void Read(Main form)
        {
            try
            {
                // defining the previous xmlreader instance we created to point to our config file (it'll crash if it 
                // doesn't exist, but we've got that covered)

                // creating settings!
                XmlReaderSettings xmlreaderse = new XmlReaderSettings();
                // allows for crashy characters!
                xmlreaderse.CheckCharacters = false;

                xmlre = XmlReader.Create(folder + "config.xml", xmlreaderse);

                // reads through the entire xml file
                while (xmlre.Read())
                {
                    // handles the spam option
                    if ((xmlre.NodeType == XmlNodeType.Element) && (xmlre.Name == "spam"))
                    {
                        // fills the user input with the xml content
                        form.spam = xmlre.ReadElementContentAsString();
                    }

                    // same as above, but with error instead 
                    // same applies until the end of the xmlreader loop
                    if ((xmlre.NodeType == XmlNodeType.Element) && (xmlre.Name == "error"))
                    {
                        form.error = xmlre.ReadElementContentAsString();
                    }

                    if ((xmlre.NodeType == XmlNodeType.Element) && (xmlre.Name == "hostname"))
                    {
                        form.hostname = xmlre.ReadElementContentAsString();
                    }

                    if ((xmlre.NodeType == XmlNodeType.Element) && (xmlre.Name == "mapname"))
                    {
                        form.mapname = xmlre.ReadElementContentAsString();
                    }

                    if ((xmlre.NodeType == XmlNodeType.Element) && (xmlre.Name == "fs_game"))
                    {
                        form.fs_game = xmlre.ReadElementContentAsString();
                    }

                    if ((xmlre.NodeType == XmlNodeType.Element) && (xmlre.Name == "sv_maxclients"))
                    {
                        form.sv_maxclients = xmlre.ReadElementContentAsInt();
                    }

                    if ((xmlre.NodeType == XmlNodeType.Element) && (xmlre.Name == "clients"))
                    {
                        form.clients = xmlre.ReadElementContentAsInt();
                    }

                    if ((xmlre.NodeType == XmlNodeType.Element) && (xmlre.Name == "port"))
                    {
                        form.port = Convert.ToUInt16(xmlre.ReadElementContentAsInt());
                    }
                }

                // stops it looking at the input so other parts of the process can use the file
                xmlre.Dispose();
            }

            // crashes if the file doesn't exist/is damaged, so we create a new file
            catch
            {
                // stops it looking at the input so other parts of the process can use the file
                xmlre.Dispose();
                // calls the xmlcreate function, which creates our new xml file
                XmlCreate(form);
            }

            if (!Directory.Exists(folder))
            {
                return;
            }
        }

        public static void XmlCreate(Main form)
        {
            // creates an instance of the filstreame
            using (FileStream fs = new FileStream(folder + "config.xml", FileMode.Create, FileAccess.Write))
            {
                // setting up the settings
                XmlWriterSettings xmlwritersettings1 = new XmlWriterSettings();
                // makes the xml file user readable
                xmlwritersettings1.Indent = true;
                // allows for crashy characters!
                xmlwritersettings1.CheckCharacters = false;

                // sets up our xmlwriter function to point to our filesystem instance
                using (xmlwr = XmlWriter.Create(fs, xmlwritersettings1))
                {
                    // let's start the document
                    xmlwr.WriteStartDocument();
                    // creates the settings element
                    xmlwr.WriteStartElement("settings");

                    // gets all the user inputted data, if there is any
                    xmlwr.WriteElementString("hostname", form.hostname);
                    xmlwr.WriteElementString("gametype", form.gametype);
                    xmlwr.WriteElementString("mapname", form.mapname);
                    xmlwr.WriteElementString("fs_game", form.fs_game);
                    xmlwr.WriteElementString("error", form.error);
                    xmlwr.WriteElementString("spam", form.spam);
                    xmlwr.WriteElementString("sv_maxclients", form.sv_maxclients.ToString());
                    xmlwr.WriteElementString("clients", form.clients.ToString());
                    xmlwr.WriteElementString("port", form.port.ToString());

                    // we end the settings element
                    xmlwr.WriteEndElement();
                    // ending the document
                    xmlwr.WriteEndDocument();
                    // writes it to the filesystem
                    xmlwr.Flush();
                }
                // and finally, we dispose it so other parts of the program can look at the same file
                fs.Dispose();

            }


        }
    }
}