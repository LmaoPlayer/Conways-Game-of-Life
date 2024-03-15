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
        List<int> CoordsX = new List<int>();
        List<int> CoordsY = new List<int>();
        public Form1()
        {
            InitializeComponent();
            CreateField();
            Read();
            RunTheGame();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }
        private void CreateField()
        {
            

            Field.Size = new Size(col*FieldSize, row*FieldSize);
            Field.BorderStyle = BorderStyle.FixedSingle;
            Field.Location = new Point(5, 5);
            Field.Paint += new PaintEventHandler(DrawTheCells);

            Controls.Add(Field);

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
        private void Read()
        {
            Cool = File.ReadAllText("e.txt");
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
            for (int i = 0; i<col; i++)
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
        private async void RunTheGame()
        {
            while (true)
            {
                await UpdateTheCells();

                Application.DoEvents();

                await Task.Delay(16);
            }

        }
        private async Task UpdateTheCells()
        {
            NextStepPlease();

            await Task.Delay(1000);
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
        private void NextStepPlease()
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
/*        private void SpeedingGoBrr(object sender, EventArgs e)
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
            Button HowWillIReset = (Button)sender;

            StartedTheGame = false;
            EndTheGame.Visible = false;
            EndTheGame.Enabled = false;
            StartTheGame.Visible = true;
            StartTheGame.Enabled = true;
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    GridCounting[i, j] = true;
                    if (HowWillIReset.Text == "Reset")
                    {
                        Grid[i, j].BackColor = Color.Transparent;
                    }
                    else
                    {
                        Controls.Remove(Grid[i, j]);
                    }

                }
            }
        }
        private void EndIt(object sender, EventArgs e)
        {
            StartedTheGame = false;
            EndTheGame.Visible = false;
            EndTheGame.Enabled = false;
            StartTheGame.Visible = true;
            StartTheGame.Enabled = true;
        }
    }*/
}
