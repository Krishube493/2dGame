using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace _2dGame
{
    public partial class GameScreen : UserControl
    {
        //Global Variables 
        Boolean leftArrowDown, rightArrowDown, spaceBarDown, time = true, escapePress;

        int boxSpeed = 13;
        int jump = 0;
        int none = 0;

        int xValue, nonevalue;
        string directionValue;
        int colourChange;

        //list 
        List<Box> drawBox = new List<Box>();

        //player 
        Box player;

        //sounds 
        SoundPlayer MoveSound = new SoundPlayer(Properties.Resources.MoveSound);
        SoundPlayer DieSound = new SoundPlayer(Properties.Resources.DeathSound);

        public GameScreen()
        {
            InitializeComponent();
            OnStart();
            Cursor.Hide();
        }

        public void OnStart()
        {
            //What to do when the game starts 
            Box b1 = new Box(53, 300, 20, 20, "Left", Properties.Resources.blueGhost);
            drawBox.Add(b1);

            Box b2 = new Box(80, 230, 20, 20, "Right", Properties.Resources.redGhost);
            drawBox.Add(b2);

            Box b3 = new Box(750, 160, 20, 20, "Left", Properties.Resources.yellowGhost);
            drawBox.Add(b3);

            Box b4 = new Box(500, 90, 20, 20, "Left", Properties.Resources.pinkGhost);
            drawBox.Add(b4);

            Box b5 = new Box(1, 20, 20, 20, "Right", Properties.Resources.blueGhost);
            drawBox.Add(b5);

            player = new Box(this.Width / 2, 375, 15, 15);
            
            //sound playing on repeat 
            MoveSound.PlayLooping();
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Space:
                    spaceBarDown = true;
                    break;
                case Keys.Escape:
                    escapePress = true;
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                 case Keys.Left:
                    leftArrowDown = false;
                    break;
                 case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Space:
                    spaceBarDown = false;
                    time = true;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //update location of all boxes
            foreach (Box b in drawBox)
            {
                //Move boxes
                b.BoxMove(boxSpeed, b.direction);
            }

            //Move player
            if (leftArrowDown)
            {
                player.Move(10, "left");
            }

            if (rightArrowDown)
            {
                player.Move(10, "right");
            }
            
            //to move down if its time
            if (spaceBarDown && time)
            {
                //increase score
                Form1.score++;
                jump++;
                none++;
                scoreLabel.Text = "Score = " + Form1.score;
                Refresh();

                //move boxes
                foreach (Box b in drawBox)
                {
                    b.MoveBox(70);
                }

                //add new box and direction of box
                Random randGen = new Random();
                xValue = randGen.Next(1, 801);

                if (xValue > 300)
                {
                    directionValue = "Right";
                }
                else
                {
                    directionValue = "Left";
                }

                //put a break in boxes
                Random rand = new Random();
                nonevalue = randGen.Next(1, 26);
                if (none == nonevalue)
                {
                    time = false;
                    nonevalue = randGen.Next(1, 26);
                }
                else
                {
                    Random randColour = new Random();
                    colourChange = randColour.Next(1, 5);
                    if (colourChange == 1)
                    {
                        Box b1 = new Box(xValue, 20, 20, 20, directionValue, Properties.Resources.blueGhost);
                        drawBox.Add(b1);
                        time = false;
                    }
                    else if (colourChange == 2)
                    {
                        Box b1 = new Box(xValue, 20, 20, 20, directionValue, Properties.Resources.redGhost);
                        drawBox.Add(b1);
                        time = false;
                    }
                    else if (colourChange == 3)
                    {
                        Box b1 = new Box(xValue, 20, 20, 20, directionValue, Properties.Resources.yellowGhost);
                        drawBox.Add(b1);
                        time = false;
                    }
                    else if (colourChange == 4)
                    {
                        Box b1 = new Box(xValue, 20, 20, 20, directionValue, Properties.Resources.pinkGhost);
                        drawBox.Add(b1);
                        time = false;
                    }
                }

                Refresh();
            }

            //Collision
            foreach (Box b in drawBox)
            {
                if (player.Collision(b))
                {
                    //play die sound 
                    DieSound.Play();

                    //Remove this screen
                    gameTimer.Enabled = false;
                    Form f = this.FindForm();
                    f.Controls.Remove(value: this);

                    //Move Drectly to Game Over Screen 
                    GameOverScreen gos = new GameOverScreen();
                    f.Controls.Add(gos);

                    //put into the middle of the screen
                    gos.Location = new Point((f.Width - gos.Width) / 2, (f.Height - gos.Height) / 2);
                    this.Dispose();
                }
            }

            //remove box if it has gone of screen
            if (drawBox[0].y > this.Height)
            {
                drawBox.RemoveAt(0);
            }

            //change the direction if it hits end
            foreach (Box b in drawBox)
            {
                if (b.x > this.Width)
                {
                    b.direction = "Left";
                }

                if (b.x < 0)
                {
                    b.direction = "Right";
                }
            }

            //increases speed of boxes 
            if (jump == 10)
            {
                jump = 0;
                boxSpeed++;
                speedLabel.Text = "Speed = " + boxSpeed;
                Refresh();
            }

            //Win when you hit a certain score
            if (Form1.score == 250)
            {
                gameTimer.Enabled = false;
                //Remove this screen 
                Form f = this.FindForm();
                f.Controls.Remove(value: this);

                //Move Drectly to Win Screen 
                WinScreen ws = new WinScreen();
                f.Controls.Add(ws);

                //put into the middle of the screen
                ws.Location = new Point((f.Width - ws.Width) / 2, (f.Height - ws.Height) / 2);
                ws.Focus();
            }

            //escape or end game or exit
            if (escapePress)
            {
                //Remove this screen
                gameTimer.Enabled = false;
                Form f = this.FindForm();
                f.Controls.Remove(value: this);

                //Move Drectly to Game Over Screen 
                GameOverScreen gos = new GameOverScreen();
                f.Controls.Add(gos);

                //put into the middle of the screen
                gos.Location = new Point((f.Width - gos.Width) / 2, (f.Height - gos.Height) / 2);
                gos.Focus();
                this.Dispose();
            }

            Refresh();
        }


        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush playerBrush = new SolidBrush(Color.Black);
            //To draw everything in the list 
            foreach (Box b in drawBox)
            {
                e.Graphics.DrawImage(b.image, b.x, b.y, b.size, b.size2);
            }

            //Draw Player
            e.Graphics.DrawImage(Properties.Resources.playerImage, player.x, player.y, player.size, player.size);
        }
    }
}
