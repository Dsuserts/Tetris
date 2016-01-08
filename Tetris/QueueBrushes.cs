using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Tetris
{
    //очередь кисточек
    public static class QueueBrushes
    {
        private static readonly Queue<Brush> brush; 
        static QueueBrushes()
        {
            brush = new Queue<Brush>();
            brush.Enqueue(Brushes.OrangeRed);
            brush.Enqueue(Brushes.RoyalBlue);
            brush.Enqueue(Brushes.Yellow);
            brush.Enqueue(Brushes.BlueViolet);
            brush.Enqueue(Brushes.DeepSkyBlue);
            brush.Enqueue(Brushes.LawnGreen);
            brush.Enqueue(Brushes.Snow);
            brush.Enqueue(Brushes.DarkOrange);
        }

        //достать первую кисточку
        public static Brush GetBrush()
        {
            var temp = brush.Dequeue();
            brush.Enqueue(temp);
            return temp;
        }

        //peek
        public static Brush Peek() => brush.Peek();
    }
}