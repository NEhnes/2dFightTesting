using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Media;
using System.Timers;
using System.IO;

namespace _2dFightTesting
{
    public partial class Form1 : Form
    {
        System.Windows.Media.MediaPlayer backMedia = new System.Windows.Media.MediaPlayer();

        public Form1()
        {
            InitializeComponent();
            ChangeScreen(this, new MenuScreen());

            backMedia.Open(new Uri(Application.StartupPath + "/Resources/backgroundMusic.mp3"));

            backMedia.MediaEnded += new EventHandler(backMedia_MediaEnded);
            backMedia.Play();


        }

        public static void ChangeScreen(object sender, UserControl next)
        {
            Form f; // will either be the sender or parent of sender

            if (sender is Form)
            {
                f = (Form)sender;                          //f is sender
            }
            else
            {
                UserControl current = (UserControl)sender;  //create UserControl from sender
                f = current.FindForm();                     //find Form UserControl is on
                f.Controls.Remove(current);                 //remove current UserControl
            }

            // add the new UserControl to the middle of the screen and focus on it
            next.Location = new Point((f.ClientSize.Width - next.Width) / 2, (f.ClientSize.Height - next.Height) / 2);
            f.Controls.Add(next);
            next.Focus();
        }

        private void backMedia_MediaEnded(object sender, EventArgs e)
        {
            backMedia.Stop();
            backMedia.Play();
        }
    }
}