using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int TickRate = 500;
        PictureBox Field = new PictureBox();
        string Cool;
        int FieldSize = 10;
        int col = 100;
        int row = 70;
        int[,] FriendList;
        int AmmountOfFriends = 0;
        bool[,] GridCounting;
        Button NextStep = new Button();
        Button StartTheGame = new Button();
        Button ResetTheGame = new Button();
        Button SpeedUp = new Button();
        bool StartedTheGame = false;
        bool SpeedTheShitUp = false;
        Button EndTheGame = new Button();
        ComboBox ShapeToUse = new ComboBox();
        Button PlaceTheShape = new Button();
        public Form1()
        {
            InitializeComponent();
            CreateField();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }
        private void CreateField()
        {

            StartTheGame.Click += new EventHandler(RunTheGame);
            StartTheGame.Size = new Size(50, 20);
            StartTheGame.Text = "Start";

            ResetTheGame.Text = "Reset";
            ResetTheGame.Click += new EventHandler(ResetIt);
            ResetTheGame.Size = new Size(StartTheGame.Width, StartTheGame.Height);
            ResetTheGame.Location = new Point(StartTheGame.Width, 0);

            SpeedUp.Text = "Speed Up";
            SpeedUp.Click += new EventHandler(SpeedingGoBrr);
            SpeedUp.Size = new Size(ResetTheGame.Width, ResetTheGame.Height);
            SpeedUp.Location = new Point(SpeedUp.Width + StartTheGame.Width, 0);

            NextStep.Click += new EventHandler(NextStepPlease);
            NextStep.Size = new Size(SpeedUp.Width, SpeedUp.Height);
            NextStep.Location = new Point(SpeedUp.Location.X + SpeedUp.Width, 0);
            NextStep.Text = "Step Over";

            Field.Size = new Size(col * FieldSize, row * FieldSize);
            Field.BorderStyle = BorderStyle.FixedSingle;
            Field.Location = new Point(5, StartTheGame.Height+10);
            Field.Paint += new PaintEventHandler(DrawTheCells);

            EndTheGame.Click += new EventHandler(EndIt);
            EndTheGame.Size = new Size(50, 20);
            EndTheGame.Text = "Stop";
            EndTheGame.Visible = false;
            EndTheGame.Enabled = false;

            ShapeToUse.Location = new Point(NextStep.Location.X + NextStep.Width + 10, 0);
            ShapeToUse.Items.Add("Pentagon");

            PlaceTheShape.Click += new EventHandler(PlaceASelectedShape);
            PlaceTheShape.Size = new Size(SpeedUp.Width, SpeedUp.Height);
            PlaceTheShape.Location = new Point(ShapeToUse.Location.X + ShapeToUse.Width + 10, 0);
            PlaceTheShape.Text = "Place Shape";

            Controls.Add(Field);
            Controls.Add(SpeedUp); 
            Controls.Add(NextStep);
            Controls.Add(EndTheGame);
            Controls.Add(StartTheGame);
            Controls.Add(ResetTheGame);
            Controls.Add(ShapeToUse); 
            Controls.Add(PlaceTheShape);

            FriendList = new int[col, row];
            GridCounting = new bool[col, row];

            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    GridCounting[i, j] = false;
                    FriendList[i, j] = 0;
                }
            }

        }
        private void PlaceASelectedShape(object sender, EventArgs e)
        {
            if (ShapeToUse.Text == "")
            {
                ShapeToUse.Text = "Pentagon";
            }
            Cool = File.ReadAllText(ShapeToUse.Text + ".txt");
            string[] Lines = Cool.Split('\n');
            for (int RepRow = 0; RepRow < Lines.Length; RepRow++)
            {
                string Line = Lines[RepRow];
                if (Line.Length > 0 && Line[0] != '!')
                {
                    for (int Column = 0; Column < Line.Length; Column++)
                    {
                        if (Line[Column] == 'O')
                        {
                            GridCounting[Column, RepRow] = true;
                        }
                    }
                }
            }
            Field.Invalidate();
        }
        private void DrawTheCells(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush ImaBrush = new SolidBrush(Color.Black);
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (GridCounting[i, j])
                    {
                        g.FillRectangle(ImaBrush, new Rectangle(new Point(i * FieldSize, j * FieldSize), new Size(FieldSize, FieldSize)));
                    }

                }
            }

        }
        private async void RunTheGame(object sender, EventArgs e)
        {
            StartedTheGame = true;
            EndTheGame.Visible = true;
            EndTheGame.Enabled = true;
            StartTheGame.Visible = false;
            StartTheGame.Enabled = false;
            while (StartedTheGame)
            {
                await UpdateTheCells();

                Application.DoEvents();

                await Task.Delay(16);
            }

        }
        private async Task UpdateTheCells()
        {
            NextStep.PerformClick();

            await Task.Delay(TickRate);
        }
        private void CheckTheFriends()
        {
            for (int kol = 0; kol < col; kol++)
            {
                for (int rij = 0; rij < row; rij++)
                {
                    AmmountOfFriends = 0;
                    int pRow = rij == 0 ? row - 1 : rij - 1;
                    int pCol = kol == 0 ? col - 1 : kol - 1;
                    int nCol = kol == col - 1 ? 0 : kol + 1;
                    int nRow = rij == row - 1 ? 0 : rij + 1;
                    if (GridCounting[pCol, pRow]) AmmountOfFriends++;
                    if (GridCounting[pCol, nRow]) AmmountOfFriends++;
                    if (GridCounting[pCol, rij]) AmmountOfFriends++;
                    if (GridCounting[nCol, pRow]) AmmountOfFriends++;
                    if (GridCounting[nCol, nRow]) AmmountOfFriends++;
                    if (GridCounting[nCol, rij]) AmmountOfFriends++;
                    if (GridCounting[kol, pRow]) AmmountOfFriends++;
                    if (GridCounting[kol, nRow]) AmmountOfFriends++;
                    FriendList[kol, rij] = AmmountOfFriends;
                }
            }
        }
        private void NextStepPlease(object sender, EventArgs e)
        {
            CheckTheFriends();
            for (int Columns = 0; Columns < col; Columns++)
            {
                for (int Rowing = 0; Rowing < row; Rowing++)
                {
                    //MessageBox.Show(Grid[Columns, Rowing].Name + " has " + FriendList[Columns, Rowing].ToString() + " friends.");
                    if (FriendList[Columns, Rowing] < 2 || FriendList[Columns, Rowing] > 3)
                    {

                        GridCounting[Columns, Rowing] = false;
                    }
                    else if (FriendList[Columns, Rowing] == 3)
                    {
                        GridCounting[Columns, Rowing] = true;
                    }
                }
            }
            Field.Invalidate();
        }
        //new
        private void SpeedingGoBrr(object sender, EventArgs e)
        {
            if (!SpeedTheShitUp)
            {
                TickRate = 250;
                SpeedTheShitUp = true;
                SpeedUp.Text = "Slow down";
            }
            else
            {
                TickRate = 500;
                SpeedTheShitUp = false;
                SpeedUp.Text = "Speed up";
            }
        }
        private void ResetIt(object sender, EventArgs e)
        {

            StartedTheGame = false;
            EndTheGame.Visible = false;
            EndTheGame.Enabled = false;
            StartTheGame.Visible = true;
            StartTheGame.Enabled = true;
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    GridCounting[i, j] = false;
                }
            }
            Field.Invalidate();
        }
        private void EndIt(object sender, EventArgs e)
        {
            StartedTheGame = false;
            EndTheGame.Visible = false;
            EndTheGame.Enabled = false;
            StartTheGame.Visible = true;
            StartTheGame.Enabled = true;
        }
    }
}
