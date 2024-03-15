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
        PictureBox Field = new PictureBox();
        string Cool;
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
/*            RunTheGame();*/
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }
        private void CreateField()
        {
            

            Field.Size = new Size(col*10, row*10);
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
                    GridCounting[i, j] = true;
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
                            GridCounting[RepRow, Column] = true;
                            CoordsX.Add(Column);
                            CoordsY.Add(RepRow);
                        }
                    }
                }
            }
            Field.Invalidate();
        }
        private void DrawTheCells(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 10);
            for (int i = 0; i<CoordsY.Count; i++)
            {
                for (int j = 0; j < CoordsX.Count; j++)
                {
                    g.DrawRectangle(pen, new Rectangle(new Point(CoordsX[j]*10+5, CoordsY[i]*10+5), new Size(10, 10)));
                }
            }
            
        }
        private async void RunTheGame()
        {
            await Task.Delay(1);
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
                    if (!GridCounting[pCol, pRow]) AmmountOfFriends++;
                    if (!GridCounting[pCol, nRow]) AmmountOfFriends++;
                    if (!GridCounting[pCol, rij]) AmmountOfFriends++;
                    if (!GridCounting[nCol, pRow]) AmmountOfFriends++;
                    if (!GridCounting[nCol, nRow]) AmmountOfFriends++;
                    if (!GridCounting[nCol, rij]) AmmountOfFriends++;
                    if (!GridCounting[kol, pRow]) AmmountOfFriends++;
                    if (!GridCounting[kol, nRow]) AmmountOfFriends++;
                    FriendList[kol, rij] = AmmountOfFriends;
                }
            }
        }
    }
}
