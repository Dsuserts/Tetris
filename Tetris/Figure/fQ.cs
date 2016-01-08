using static System.Drawing.Brushes;

namespace Tetris
{
    public class fQ: Figure
    {
        public fQ(ITetris parent) : base(parent)
        {
            brush = QueueBrushes.GetBrush();
            a.X = 0;
            a.Y = 4;
            b.X = 0;
            b.Y = 5;
            c.X = 1;
            c.Y = 4;
            d.X = 1;
            d.Y = 5;
            InitFigure();
        }

        public override void Up()
        {
        }

        #region Проверки

        public override bool checkDown()
        {
            if (c.X == 19 || d.X == 19)
                return false;
            return parent.field[c.X + 1, c.Y].brush == Black && parent.field[d.X + 1, d.Y].brush == Black;
        }

        public override bool checkLeft() =>
            a.Y > 0 && c.Y > 0 && parent.field[a.X, a.Y - 1].brush == Black &&
            parent.field[c.X, c.Y - 1].brush == Black;

        public override bool checkRight() =>
            b.Y < 9 && d.Y < 9 && parent.field[b.X, b.Y + 1].brush == Black &&
            parent.field[d.X, d.Y + 1].brush == Black;

        public override bool checkUp() => false;

        #endregion
    }
}