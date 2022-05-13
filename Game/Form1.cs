using System;

namespace Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        bool GameState = false;
        string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        char CurrentChar = 'A';

        System.Timers.Timer NewTimer = new System.Timers.Timer();

        Graphics Graph;

        private void Form1_Load(object sender, EventArgs e)
        {
            NewTimer.Interval = 1500;
            NewTimer.Elapsed += InGame;

            KeyUp += Form1_KeyUP;
            KeyPreview = true;
            Graph = pictureBox1.CreateGraphics();
        }

        bool Next;

        int Count = 0;
        int Skipped = 0;

        private void Form1_KeyUP(object? sender, KeyEventArgs e)
        {
            if (Next) return;

            if (e.KeyValue == CurrentChar && GameState)
            {
                Next = true;
                Count++;
                return;
            }
        }

        private void InGame(object? sender, System.Timers.ElapsedEventArgs e)
        {
            
            if (GameState)
            {

                Graph.Clear(Color.White);

                if (!Next)
                {

                    Skipped++;
                    textBox1.Text = "Всего пропущено: " + Skipped.ToString();
                }
                else
                {
                    textBox2.Text = "Нажато вовремя: " + Count.ToString();
                }

                Next = false;
                Random rnd = new Random();
                CurrentChar = Enumerable.Repeat(Chars, 1).Select(s => s[rnd.Next(s.Length)]).ToArray()[0];


                

                Graph.DrawString(CurrentChar.ToString(), new Font("Courier New", 30.0F), new SolidBrush(Color.Red), new Point(10, 10));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (NewTimer.Enabled)
            {
                Count = 0;
                Skipped = 0;
                NewTimer.Stop();
                GameState = false;
                return;
            }

            NewTimer.Start();
            GameState = true;
        }
    }
}