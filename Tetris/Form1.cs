using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Tetris
{
    public partial class Form1 : Form, ITetris, INextFigure
    {
        //цвет отрисовки подквадратов
        private Pen myPen;
        //количество заполненных рядов
        private int lines;
        //ширина/высота клетки
        private int width = 25;
        //поле, массив 20*10
        public Component[,] field { get; } = new Component[20, 10];
        //picture box на котором будет выводиться следущая фигура
        public PictureBox PictureNextFigure { get; }
        //текущая фигура
        private Figure figure;
        //таймер
        private Timer timer;
        //скорость таймера/движения фигур
        private int speed = 800;
        //текущий уровень сложности
        private int level = 1;
        //генератор следущей фигуры
        private NextFigure nextFigure;
        //игра запущена
        private bool enter = false;
        //поле для следущей фигуры
        public Component[,] smallField { get; } = new Component[3, 4];
        //звуки
        SoundControl soundControl = new SoundControl();



        //конструктор
        public Form1()
        {
            
            InitializeComponent();

            nextFigure = new NextFigure(this);

            //инициализация таймера
            timer = new Timer {Interval = speed};
            timer.Tick += (sender, args) =>
            {
                if (figure.Down()) ;
                else
                {
                    HorizontalRow();
                    GameOver();
                    figure = nextFigure.GetNext(this);
                }
                pictureBox1.Invalidate();
                PictureNextFigure.Invalidate();
            };

            //инициализация цвета отрисовки подквадрата
            myPen = new Pen(Color.Black)
            {
                LineJoin = LineJoin.Miter,
                Width = 2.0F
            };

            //отрисовка стакана и сетки
            pictureBox1.Image = (Image) new Bitmap(pictureBox1.Width, pictureBox1.Height);
            var g = Graphics.FromImage(pictureBox1.Image);
            var p = new Pen(Color.DarkSlateGray);
            for (var i = 0; i < 10; i++)
                g.DrawLine(p, pictureBox1.Width/10*(i + 1), 0, pictureBox1.Width/10*(i + 1), pictureBox1.Height);
            for (var i = 0; i < 20; i++)
                g.DrawLine(p, 0, pictureBox1.Height/20*(i + 1), pictureBox1.Width, pictureBox1.Height/20*(i + 1));

            //красная линия
            g.DrawLine(Pens.Red, 0, pictureBox1.Height/20*3, pictureBox1.Width, pictureBox1.Height/20*3);

            //инициализация массива/поля
            InitializeField();

            //текущий уровень сложности
            labelLevel.Text = $"Level: {level}";

            //инициализация массива координат следущей фигуры
            for (var i = 0; i < 3; i++)
                for (var j = 0; j < 4; j++)
                    smallField[i, j] = new Component((j * width) + 1, (i * width) + 1, Brushes.Black);

            //pictureBox на котором будет отображаться следущая фигура
            PictureNextFigure = new PictureBox
            {
                Location = new Point(324, 91),
                Size = new Size(101, 76),
                BackColor = Color.Black
            };

            //добавляем контрол к форме
            this.Controls.Add(PictureNextFigure);

            //обработчик перерисовки поля
            PictureNextFigure.Paint += (s, e) =>
            {
                for (var i = 0; i < 3; i++)
                    for (var j = 0; j < 4; j++)
                    {
                        //если в текущей клетке стоит фигура рисуем квадрат
                        if (smallField[i, j].brush != Brushes.Black)
                        {
                            e.Graphics.FillRectangle(smallField[i, j].brush
                            , smallField[i, j].x, smallField[i, j].y, width - 1, width - 1);
                            //и подквадрат
                            e.Graphics.DrawRectangle(myPen, smallField[i, j].x + 3, smallField[i, j].y + 3, 
                                width - 7, width - 7);
                        }
                    }
            };

            labelPause.Text = @"TETRIS";
            labelPause.Visible = true;
            soundControl.playMain();
        }

        //инициализация массива/поля
        private void InitializeField()
        {
            for (var i = 0; i < 20; i++)
                for (var j = 0; j < 10; j++)
                    field[i, j] = new Component((j * width) + 1, (i * width) + 1, Brushes.Black);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            for (var i = 0; i < 20; i++)
                for (var j = 0; j < 10; j++)
                {
                    //перерисовка клеток
                    e.Graphics.FillRectangle(field[i, j].brush, field[i, j].x, field[i, j].y, width - 1, width - 1);

                    //если в текущей клетке стоит фигура рисуем подквадрат
                    if (field[i, j].brush != Brushes.Black)
                        e.Graphics.DrawRectangle(myPen, field[i, j].x + 3, field[i, j].y + 3, width - 7, width - 7);
                }
        }

        //новая игра
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Stop();
            labelPause.Text = @"PAUSE";
            labelPause.Visible = false;
            enter = true;
            //очищаем поле
            InitializeField();
            labelLevel.Text = @"Level: 1";
            labelLines.Text = @"Lines: 0";
            lines = 0;
            figure = nextFigure.GetNext(this);
            pictureBox1.Invalidate();
            //таймер запускающий игру
            timer.Start();
            PictureNextFigure.Invalidate();
        }

        //обработчик нажатия клавиш
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (enter)
            {
                //пауза
                if (e.KeyCode == Keys.P)
                {
                    if (!timer.Enabled)
                    {
                        timer.Start();
                        labelPause.Visible = false;
                    }
                    else
                    {
                        timer.Stop();
                        labelPause.Visible = true;
                    }
                }
                //стрелки
                if (timer.Enabled)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.A:
                            figure.Left();
                            break;
                        case Keys.D:
                            figure.Right();
                            break;
                        case Keys.W:
                            figure.Up();
                            break;
                        case Keys.S:
                            figure.Down();
                            break;
                    }
                }
                pictureBox1.Invalidate();
            }
        }

        //проверка сложилась ли линия
        private void HorizontalRow()
        {
            try
            {
                var k = 0;
                var curLine = 0;
                for (var i = 0; i < 20; i++)
                {
                    k = 0;
                    curLine = i;
                    for (var j = 0; j < 10; j++)
                        if (field[i, j].brush != Brushes.Black)
                            k++;
                    if (k == 10)
                        break;
                }
                if (k == 10)
                {
                    for (int i = curLine; i > 0; i--)
                        for (int j = 0; j < 10; j++)
                            field[i, j].brush = field[i - 1, j].brush;
                    lines++;
                    ChangeLevel();
                    labelLines.Text = $"Lines: {lines}";
                    //если линия сложилась запускаем проверку еще раз
                    HorizontalRow();
                    soundControl.playSuccess();
                }
            }
            catch { }
        }

        //смена уровня сложности
        private void ChangeLevel()
        {
            switch (lines)
            {
                case 10:
                    timer.Interval = 700;
                    labelLevel.Text = @"Level: 2";
                    break;
                case 20:
                    timer.Interval = 600;
                    labelLevel.Text = @"Level: 3";
                    break;
                case 30:
                    timer.Interval = 500;
                    labelLevel.Text = @"Level: 4";
                    break;
                case 40:
                    timer.Interval = 400;
                    labelLevel.Text = @"Level: 5";
                    break;
                case 50:
                    timer.Interval = 300;
                    labelLevel.Text = @"Level: 6";
                    break;
                case 60:
                    timer.Interval = 250;
                    labelLevel.Text = @"Level: 7";
                    break;
                case 70:
                    timer.Interval = 200;
                    labelLevel.Text = @"Level: 8";
                    break;
                case 80:
                    timer.Interval = 150;
                    labelLevel.Text = @"Level: 9";
                    break;
                case 90:
                    timer.Interval = 100;
                    labelLevel.Text = @"Level: 10";
                    break;
            }
        }

        //проверка на проиграш
        //если фигура вышла за красную линию => Game Over
        private void GameOver()
        {
            for (var i = 0; i < 10; i++)
            {
                if (field[2, i].brush != Brushes.Black)
                {
                    timer.Stop();
                    labelPause.Text = @"GAME OVER!";
                    labelPause.Visible = true;
                }
            }
        }

        //обработчик отключения звука
        private void button1_Click(object sender, EventArgs e) =>
                button1.Text = soundControl.mute() ? "ON" : "OFF";

        //help
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        //about programm
        private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("                   Tetris version 1.01. \n" +
                             "                    Written in C # 6.0.\n" +
                             "           .NET Framework 4.6.1 used.\n\n" +
                             "    E-mail:mazurkostya93@gmail.com", "About program");
        }
    }
}
