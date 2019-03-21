using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2dGame
{
    public partial class Form1 : Form
    {
        public static int score = 0;

        public Form1()
        {
            InitializeComponent();
            Cursor.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Move Drectly to Main Screen 
            MainScreen ms = new MainScreen();
            this.Controls.Add(ms);

            //put into center of screen 
            ms.Location = new Point((this.Width - ms.Width) / 2, (this.Height - ms.Height) / 2);
        }
    }
}
