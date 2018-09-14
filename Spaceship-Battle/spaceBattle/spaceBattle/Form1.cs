using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Media; // Reference: PresentationCore, WindowsBase

namespace spaceBattle
{
    public partial class Form1 : Form
    {

        public class Bullet : PictureBox
        {
            public Bullet()
            {
                this.Image = Properties.Resources.bullet;
                this.BackColor = System.Drawing.Color.Transparent;
                this.SizeMode = PictureBoxSizeMode.AutoSize;

                this.Top = SpaceShip.Top - 10;
                this.Left = SpaceShip.Left + SpaceShip.Width / 2 - this.Width / 2;

                GameSpace.Controls.Add(this);
            }

            public void Step()
            {
                this.Top -= MOVESTEP;

                if (GameSpace.Controls.OfType<Enemie>().Count() > 0)
                {
                    foreach (Enemie enemie in GameSpace.Controls.OfType<Enemie>())
                    {
                        if (enemie.isAlive && enemie.Bottom > this.Top && (this.Left > enemie.Left && this.Left < enemie.Left + enemie.Width))
                        {
                            GameSpace.Controls.Remove(this);
                            enemie.Destroy();
                            break;
                        }
                        else if (this.Top < 0) { GameSpace.Controls.Remove(this); }
                    }
                }
                else if (this.Top < 0) { GameSpace.Controls.Remove(this); }
            }
        }

        public class Enemie : PictureBox
        {
            private static int totalEnemies;

            public bool isAlive = true;

            private static int oldX, oldY;

            public static int TotalEnemies
            {
                get { return totalEnemies; }
                set
                {
                    totalEnemies = value;

                    if (totalEnemies == 0)
                    {
                        Spawn();
                    }
                }
            }


            public static void Spawn()
            {
                int enemieWidth, enemieHeight;

                if (TotalEnemies < MAXENEMIES)
                {
                    for (int i = 0; i < MAXENEMIES; i++)
                    {
                        Random rx = new Random(DateTime.Now.Second);
                        Random ry = new Random(DateTime.Now.Second + DateTime.Now.Hour * 2);
                        Random mob = new Random(DateTime.Now.Second + DateTime.Now.Minute);

                        int enemieType = mob.Next(1, 10);

                        switch (enemieType)
                        {
                            case 2: enemieWidth = 100; enemieHeight = 64; break;
                            default: enemieWidth = 60; enemieHeight = 52; break;
                        }

                        /* int x = rx.Next(100, GameSpace.Width - enemieWidth - 200);   
                         int y = ry.Next(100, GameSpace.Height - enemieHeight - 200);*/
                        int x;
                        int y;

                        do
                        {
                            y = ry.Next(50, GameSpace.Height - enemieHeight - 100);
                        }
                        while (Math.Abs(y - oldY) < 50);

                        do
                        {
                            x = rx.Next(100, GameSpace.Width - enemieWidth - 200);
                        }
                        while (Math.Abs(x - oldX) < 80);

                        oldX = x;
                        oldY = y;


                        new Enemie(x, y, (enemieType == 2) ? true : false);
                    }
                } 
            }



            public Enemie(int x, int y, bool Boss = false)
            {
                if (Boss)
                {
                    this.Image = Properties.Resources.enemie2;
                }
                else
                {
                    this.Image = Properties.Resources.enemie1;
                }

                this.BackColor = System.Drawing.Color.Transparent;
                this.SizeMode = PictureBoxSizeMode.AutoSize;

                this.Top = y;
                this.Left = x;

                TotalEnemies++;

                GameSpace.Controls.Add(this);
            }

            public void Destroy()
            {
                isAlive = false;

                MediaPlayer mp = new MediaPlayer();
               
                if (this.Width > 99) // boss ship
                {
                    mp.Open(new System.Uri(@"Audio\\expLarge.wav", UriKind.Relative));
                    Score += 20;
                }
                else
                {
                    mp.Open(new System.Uri(@"Audio\\expSmall.wav", UriKind.Relative));
                    Score += 10;
                }

                this.Image = Properties.Resources.explodeAnim;

                if (SOUND) { mp.Play(); }


                Timer t = new Timer();
                t.Interval = 1000;
                t.Start();
                t.Tick += destructionDelay;

                TotalEnemies--;

            }

            private void destructionDelay(object sender, EventArgs e)
            {
                GameSpace.Controls.Remove(this);
                ((Timer)sender).Stop();
            }
        }


        const int SPEED = 10;
        const int MOVESTEP = 10;
        const int INTERVAL = 10;
        const int RELOAD = 200;
        const bool COLLISION = true;
        const bool SOUND = true;
        const byte MAXENEMIES = 1;

        private static int score = 0;

        public static int Score
        {
            get { return score; }
            set
            {
                score = value;
                ((Label)GameSpace.Controls.OfType<Label>().First()).Text = "Score: " + score;
            }
        }


        public static Form GameSpace;
        public static PictureBox SpaceShip;

        System.Media.SoundPlayer soundPlayer;
        Stream sound = Properties.Resources.laser;



        public Form1()
        {
            InitializeComponent();
            gameloop.Interval = INTERVAL;
            reload.Interval = RELOAD;

            soundPlayer = new System.Media.SoundPlayer(sound);

            GameSpace = this;
            SpaceShip = spaceship;

            Enemie.Spawn();

           /* new Enemie(50, 40);
            new Enemie(300, 100);
            new Enemie(500, 200);
            new Enemie(200, 300, true);*/
        }

        private void Fire()
        {
            if (!reload.Enabled)
            {
                reload.Enabled = true;
                if (SOUND) { soundPlayer.Play(); }

                Bullet b = new Bullet();

               /* b.Image = Properties.Resources.bullet;
                b.BackColor = Color.Transparent;
                b.SizeMode = PictureBoxSizeMode.AutoSize;

                b.Top = spaceship.Top - 10;
                b.Left = spaceship.Left + spaceship.Width / 2 - b.Width / 2;

            */
                

                gameloop.Enabled = true;
            }
            
        }

        private void MoveShip(byte direction)
        {
            spaceship.BackgroundImage = Properties.Resources.shipt1;

            if (COLLISION)
            {
                switch (direction)
                {
                    case 0: // up
                        if (spaceship.Top - MOVESTEP > 0)
                        {
                            spaceship.Top -= MOVESTEP;
                        }
                        break;

                    case 1: // down
                        if (spaceship.Bottom + MOVESTEP < this.Height)
                        {
                            spaceship.Top += MOVESTEP;
                        }
                        break;

                    case 2: // left
                        if (spaceship.Left - MOVESTEP > 0)
                        {
                            spaceship.Left -= MOVESTEP;
                        }
                        break;

                    case 3: // right
                        if (spaceship.Right + MOVESTEP < this.Width)
                        {
                            spaceship.Left += MOVESTEP;
                        }
                        break;
                }
            }
            /*else // no collision
            {
                switch (direction)
                {
                    case 0: spaceship.Top -= 10; break;

                    case 1: spaceship.Top += 10; break;

                    case 2: spaceship.Left -= 10; break;

                    case 3: spaceship.Left += 10; break;
                }
            }*/






        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) { Fire(); }

            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W) { MoveShip(0); }
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S) { MoveShip(1); }
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A) { MoveShip(2); }
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D) { MoveShip(3); }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            spaceship.BackgroundImage = Properties.Resources.shipt0;
        }

        private void gameloop_Tick(object sender, EventArgs e)
        {
            foreach (Bullet bullet in this.Controls.OfType<Bullet>())
            {
                bullet.Step();
            }

            if (this.Controls.OfType<Bullet>().Count() == 0)
            {
                gameloop.Enabled = false;
            }
        }

        private void reload_Tick(object sender, EventArgs e)
        {
            reload.Enabled = false;
        }




    }
}
