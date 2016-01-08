using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static System.Drawing.Brushes;

namespace Tetris
{
    // класс генерирует новую фигуру и заполняет координаты следущей фигуры
    public class NextFigure
    {
        //координаты следущей фигуры
        private Point a, b, c, d;
        //номер следущей фигуры
        private int n;

        private readonly Queue<int> queue;

        private readonly Random rand;

        private readonly INextFigure figure;

        //конструктор
        public NextFigure(INextFigure figure)
        {
            this.figure = figure;
            queue = new Queue<int>();
            rand = new Random();
            queue.Enqueue(rand.Next(0,7));
        }

        //получить новую фигуру
        public Figure GetNext(ITetris field)
        {
            n = rand.Next(0, 7);
            queue.Enqueue(n);
            Figure f = GenerateNextFigure.Get(field, queue.Dequeue());
            PaintSmallFigure(n);
            return f;
        }

        //заполнение поля следущей фигуры
        public void PaintSmallFigure(int num)
        {
            //зачищаем старую фигуру
            figure.smallField[a.X, a.Y].brush = Black;
            figure.smallField[b.X, b.Y].brush = Black;
            figure.smallField[c.X, c.Y].brush = Black;
            figure.smallField[d.X, d.Y].brush = Black;

            switch (num)
            {
                case 0:// "I"
                    a.X = 0;
                    a.Y = 0;
                    b.X = 0;
                    b.Y = 1;
                    c.X = 0;
                    c.Y = 2;
                    d.X = 0;
                    d.Y = 3;
                    break;
                case 1:// "Q"
                    a.X = 0;
                    a.Y = 1;
                    b.X = 0;
                    b.Y = 2;
                    c.X = 1;
                    c.Y = 1;
                    d.X = 1;
                    d.Y = 2;
                    break;
                case 2:// "T"
                    a.X = 0;
                    a.Y = 1;
                    b.X = 0;
                    b.Y = 2;
                    c.X = 0;
                    c.Y = 3;
                    d.X = 1;
                    d.Y = 2;
                    break;
                case 3:// "Z"
                    a.X = 0;
                    a.Y = 1;
                    b.X = 0;
                    b.Y = 2;
                    c.X = 1;
                    c.Y = 2;
                    d.X = 1;
                    d.Y = 3;
                    break;
                case 4:// "J"
                    a.X = 0;
                    a.Y = 2;
                    b.X = 1;
                    b.Y = 2;
                    c.X = 2;
                    c.Y = 1;
                    d.X = 2;
                    d.Y = 2;
                    break;
                case 5:// "S"
                    a.X = 0;
                    a.Y = 2;
                    b.X = 0;
                    b.Y = 3;
                    c.X = 1;
                    c.Y = 1;
                    d.X = 1;
                    d.Y = 2;
                    break;
                case 6:// "L"
                    a.X = 0;
                    a.Y = 1;
                    b.X = 1;
                    b.Y = 1;
                    c.X = 2;
                    c.Y = 1;
                    d.X = 2;
                    d.Y = 2;
                    break;
            }

            //заполняем координаты новой фигуры
            figure.smallField[a.X, a.Y].brush = QueueBrushes.Peek();
            figure.smallField[b.X, b.Y].brush = QueueBrushes.Peek();
            figure.smallField[c.X, c.Y].brush = QueueBrushes.Peek();
            figure.smallField[d.X, d.Y].brush = QueueBrushes.Peek();
        }
    }
}