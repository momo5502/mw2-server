using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MW2_Server
{
    public partial class Pannel : Form
    {
        private Main form;

        public Pannel(Main _form)
        {
            form = _form;
            InitializeComponent();
        }

        private void Pannel_Load(object sender, EventArgs e)
        {
            hostname.Text = form.hostname;
            gametype.Text = form.gametype;
            mapname.Text = form.mapname;
            error.Text = form.error;
            spam.Text = form.spam;
            maxclients.Text = form.sv_maxclients.ToString();
            clients.Text = form.clients.ToString();
            fs_game.Text = form.fs_game;
            port.Text = form.port.ToString();
        }

        private void Pannel_FormClosing(object sender, FormClosingEventArgs e)
        {
            form.hostname = hostname.Text;
            form.gametype = gametype.Text;
            form.mapname = mapname.Text;
            form.fs_game = fs_game.Text;
            form.error = error.Text;
            form.spam = spam.Text;

            try
            {
                form.sv_maxclients = Convert.ToUInt32(maxclients.Text);
            }
            catch { }
            try
            {
                form.clients = Convert.ToUInt32(clients.Text);
            }
            catch { }
            try
            {
                form.port = Convert.ToUInt16(port.Text);
            }
            catch { }

            form.Text = form.escapeColors(form.hostname);
            Config.Save(form);
        }
    }
}
