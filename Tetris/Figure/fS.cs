using static System.Drawing.Brushes;    
namespace Tetris
{
    public class fS : Figure
    {
        public fS(ITetris parent) : base(parent)
        {
            rotation = Rotation.rotation0;
            brush = QueueBrushes.GetBrush();
            a.X = 0;
            a.Y = 5;
            b.X = 0;
            b.Y = 6;
            c.X = 1;
            c.Y = 4;
            d.X = 1;
            d.Y = 5;
            InitFigure();
        }

        //поворот фигуры "S"
        public override void Up()
        {
            if (!checkUp()) return;
            switch (rotation)
            {
                case Rotation.rotation0:
                    ClearFigure();
                    b.X = a.X + 1;
                    b.Y = a.Y;
                    c.X = b.X;
                    c.Y = b.Y + 1;
                    d.X = c.X + 1;
                    d.Y = c.Y;
                    InitFigure();
                    rotation = Rotation.rotation90;
                    break;
                case Rotation.rotation90:
                    ClearFigure();
                    b.X = a.X;
                    b.Y = a.Y + 1;
                    c.X = a.X + 1;
                    c.Y = a.Y - 1;
                    d.X = c.X;
                    d.Y = c.Y + 1;
                    InitFigure();
                    rotation = Rotation.rotation0;
                    break;
            }
        }

        #region Проверки

        public override bool checkDown()
        {
            //если фигура на дне => false
            if (d.X == 19 || c.X == 19)
                return false;
            //проверка при сдвиге фигуры вниз, текущий поворот 0 или 180
            if ((rotation == Rotation.rotation0 || rotation == Rotation.rotation180) && 
                parent.field[b.X + 1, b.Y].brush == Black &&
                parent.field[c.X + 1, c.Y].brush == Black && parent.field[d.X + 1, d.Y].brush == Black)
                return true;
            //проверка при сдвиге фигуры вниз, текущий поворот 90 или 270
            return (rotation == Rotation.rotation90 || rotation == Rotation.rotation270) &&
                   parent.field[d.X + 1, d.Y].brush == Black && parent.field[b.X + 1, b.Y].brush == Black;
        }

        public override bool checkLeft()
        {
            //проверка при сдвиге фигуры влево, текущий поворот 0 или 180
            if (rotation == Rotation.rotation0 && c.Y > 0 &&
                parent.field[a.X, a.Y - 1].brush == Black && parent.field[c.X, c.Y - 1].brush == Black)
                return true;
            //проверка при сдвиге фигуры влево, текущий поворот 90 или 270
            return rotation == Rotation.rotation90 && b.Y > 0 &&
                   parent.field[b.X, b.Y - 1].brush == Black && parent.field[a.X, a.Y - 1].brush == Black;
        }

        public override bool checkRight()
        {
            //проверка при сдвиге фигуры вправо, текущий поворот 0 или 180
            if (rotation == Rotation.rotation0 &&
                b.Y < 9 && parent.field[d.X, d.Y + 1].brush == Black && parent.field[b.X, b.Y + 1].brush == Black)
                return true;
            //проверка при сдвиге фигуры вправо, текущий поворот 90 или 270
            return rotation == Rotation.rotation90 &&
                   c.Y < 9 && parent.field[d.X, d.Y + 1].brush == Black && parent.field[c.X, c.Y + 1].brush == Black;
        }

        public override bool checkUp()
        {
            try
            {
                //проверка при попытке повернуть фигуру. текущий поворот 0 или 180
                if (rotation == Rotation.rotation0
                    && parent.field[d.X, d.Y + 1].brush == Black &&
                    parent.field[d.X + 1, d.Y + 1].brush == Black)
                    return true;
                // --//-- 90 или 270
                return rotation == Rotation.rotation90 && parent.field[b.X, b.Y - 1].brush == Black &&
                       parent.field[a.X, a.Y + 1].brush == Black;
            }
            catch
            {
                //выход за пределы массива => false
                return false;
            }
        }

        #endregion
    }
}