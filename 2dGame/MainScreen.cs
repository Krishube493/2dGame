﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2dGame
{
    public partial class MainScreen : UserControl
    {
        public MainScreen()
        {
            InitializeComponent();
            instructions.Text = "Right Arrow = Move Player Right \nLeft Arrow = Move Player Left \nSpace = Jump \nEsc = End Game \n \nWin When You Get A Score Of 250";
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            //Remove this screen 
            Form f = this.FindForm();
            f.Controls.Remove(value: this);

            //Move Drectly to Game Screen 
            GameScreen gs = new GameScreen();
            f.Controls.Add(gs);

            //put into the middle of the screen
            gs.Location = new Point((f.Width - gs.Width) / 2, (f.Height - gs.Height) / 2);
            gs.Focus();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            //end game
            Application.Exit();
        }
    }
}
