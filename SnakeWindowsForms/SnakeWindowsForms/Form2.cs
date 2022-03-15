using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace SnakeWindowsForms
{
    public partial class Form2 : Form
    {
        private List<Class1> Snake = new List<Class1>();
        private Class1 food = new Class1();

        int maxWidth, maxHeight;
        Random random = new Random();

        bool goLeft, goRight, goUp, goDown;

        
        public Form2()
        {
            InitializeComponent();

            new Class2();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && Class2.directions != "right") 
            {
                goLeft = true; 
            }
            if (e.KeyCode == Keys.Right && Class2.directions != "left")
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Up && Class2.directions != "down")
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Down && Class2.directions != "up")
            {
                goDown = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
        }

        private void StartGame(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void StopGame(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GameTimer(object sender, EventArgs e)
        {
            // setting the directions


            if (goLeft)
            {
                Class2.directions = "left";
            }
            if (goRight)
            {
                Class2.directions = "right";
            }
            if (goDown)
            {
                Class2.directions = "down";
            }
            if (goUp)
            {
                Class2.directions = "up";
            }
            // end of directions

            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {

                    switch (Class2.directions)
                    {
                        case "left":
                            Snake[i].x--;
                            break;
                        case "right":
                            Snake[i].x++;
                            break;
                        case "down":
                            Snake[i].y++;
                            break;
                        case "up":
                            Snake[i].y--;
                            break;
                    }

                    if (Snake[i].x < 0)
                    {
                        Snake[i].x = maxWidth;
                    }
                    if (Snake[i].x > maxWidth)
                    {
                        Snake[i].x = 0;
                    }
                    if (Snake[i].y < 0)
                    {
                        Snake[i].y = maxHeight;
                    }
                    if (Snake[i].y > maxHeight)
                    {
                        Snake[i].y = 0;
                    }


                    if (Snake[i].x == food.x && Snake[i].y== food.y)
                    {
                        EatFood();
                    }

                    for (int j = 1; j < Snake.Count; j++)
                    {

                        if (Snake[i].x == Snake[j].x && Snake[i].y == Snake[j].y)
                        {
                            GameOver();
                        }

                    }


                }
                else
                {
                    Snake[i].x = Snake[i - 1].x;
                    Snake[i].y = Snake[i - 1].y;
                }
            }
            picCanvas.Invalidate();

        }

        private void UpdatePictureBox(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            Brush snakeColour;

            for (int i = 0; i < Snake.Count; i++)
            {
                if (i == 0)
                {
                    snakeColour = Brushes.Black;
                }
                else
                {
                    snakeColour = Brushes.Green;
                }

                canvas.FillEllipse(snakeColour, new Rectangle
                    (
                    Snake[i].x * Class2.Width,
                    Snake[i].y * Class2.Height,
                    Class2.Width, Class2.Height
                    ));
            }


            canvas.FillEllipse(Brushes.Red, new Rectangle
            (
            food.x * Class2.Width,
            food.y * Class2.Height,
            Class2.Width, Class2.Height
            ));
        }

        private void RestartGame() 
        {
            maxWidth = picCanvas.Width / Class2.Width - 1;
            maxHeight = picCanvas.Height / Class2.Height - 1;

            Snake.Clear();

            btnStart.Enabled = false;
            btnStop.Enabled = false;
            Class1.score = 0;
            label1.Text = "Score: " + Class1.score;

            Class1 head = new Class1 { x = 10, y = 5 };
            Snake.Add(head); // adding the head part of the snake to the list

            for (int i = 0; i < 5; i++)
            { 
                Class1 body = new Class1();
                Snake.Add(body);
            }

            food = new Class1 { x = random.Next(2, maxWidth), y = random.Next(2, maxHeight) };

            GameTimer1.Start();

        }
        private void EatFood() 
        {
            Class1.score += 1;

            label1.Text = "Score: " + Class1.score;

            Class1 body = new Class1
            {
                x = Snake[Snake.Count - 1].x,
                y = Snake[Snake.Count - 1].y
            };

            Snake.Add(body);

            food = new Class1 { x = random.Next(2, maxWidth), y = random.Next(2, maxHeight) };
        }

        private void GameOver()
        {
            GameTimer1.Stop();
            btnStart.Enabled = true;
            btnStop.Enabled = true;
            if (Class1.score > Class1.highScore)
            { 
                Form3 f3 = new Form3();
                f3.Show();
                this.Hide();
            }
        }
    }
}
