using static System.Drawing.Brushes;

namespace Tetris
{
    public class fI : Figure
    {
        public fI(ITetris parent) : base(parent)
        {
            rotation = Rotation.rotation0;
            brush = QueueBrushes.GetBrush();
            a.X = 0;
            a.Y = 3;
            b.X = 0;
            b.Y = 4;
            c.X = 0;
            c.Y = 5;
            d.X = 0;
            d.Y = 6;
            InitFigure();
        }

        //поворот фигуры "I"
        public override void Up()
        {
            if (!checkUp()) return;
            switch (rotation)
            {
                case Rotation.rotation0:
                    ClearFigure();
                    a.X = c.X;
                    a.Y = c.Y;
                    b.X = a.X + 1;
                    c.X = a.X + 2;
                    d.X = a.X + 3;
                    b.Y = a.Y;
                    c.Y = a.Y;
                    d.Y = a.Y;
                    InitFigure();
                    rotation = Rotation.rotation90;
                    break;
                case Rotation.rotation90:
                    ClearFigure();
                    b.X = a.X;
                    c.X = a.X;
                    d.X = a.X;
                    a.Y -= 2;
                    b.Y = a.Y + 1;
                    c.Y = a.Y + 2;
                    d.Y = a.Y + 3;
                    InitFigure();
                    rotation = Rotation.rotation180;
                    break;
                case Rotation.rotation180:
                    ClearFigure();
                    a.Y += 1;
                    b.Y = a.Y;
                    c.Y = a.Y;
                    d.Y = a.Y;
                    b.X = a.X + 1;
                    c.X = b.X + 1;
                    d.X = c.X + 1;
                    InitFigure();
                    rotation = Rotation.rotation270;
                    break;
                case Rotation.rotation270:
                    ClearFigure();
                    b.X = a.X;
                    c.X = a.X;
                    d.X = a.X;
                    a.Y -= 1;
                    b.Y = a.Y + 1;
                    c.Y = a.Y + 2;
                    d.Y = a.Y + 3;
                    InitFigure();
                    rotation = Rotation.rotation0;
                    break;
            }
        }

        #region проверки

        public override bool checkDown()
        {
            //если фигура на дне => false
            if (a.X == 19 || d.X == 19) 
                return false;
            //проверка при сдвиге фигуры вниз, при повороте на 0 или 180
            if ((rotation == Rotation.rotation0  || rotation == Rotation.rotation180) &&
                parent.field[a.X + 1, a.Y].brush == Black && parent.field[b.X + 1, b.Y].brush == Black &&
                parent.field[c.X + 1, c.Y].brush == Black && parent.field[d.X + 1, d.Y].brush == Black)
                return true;
            //проверка при сдвиге фигуры вниз, при повороте на 90 или 270
            return (rotation == Rotation.rotation90 || rotation == Rotation.rotation270 ) &&
                   parent.field[d.X + 1, d.Y].brush == Black;
        }

        public override bool checkLeft()
        {
            //проверка при сдвиге фигуры влево, при повороте на 0 или 180
            if ((rotation == Rotation.rotation0 || rotation == Rotation.rotation180) && a.Y > 0&&
                parent.field[a.X, a.Y - 1].brush == Black)
                return true;
            //проверка при сдвиге фигуры влево, при повороте на 90 или 270
            return (rotation == Rotation.rotation90 || rotation == Rotation.rotation270) && a.Y > 0 &&
                   parent.field[a.X, a.Y - 1].brush == Black && parent.field[b.X, b.Y - 1].brush == Black && 
                   parent.field[c.X, c.Y - 1].brush == Black && parent.field[d.X, d.Y - 1].brush == Black;
        }

        public override bool checkRight()
        {
            //проверка при сдвиге фигуры вправо, при повороте на 0 или 180
            if ((rotation == Rotation.rotation0 || rotation == Rotation.rotation180) && d.Y < 9 &&
                parent.field[d.X, d.Y + 1].brush == Black)
                return true;
            //проверка при сдвиге фигуры вправо, при повороте на 90 или 270
            return (rotation == Rotation.rotation90 || rotation == Rotation.rotation270) && a.Y < 9 &&
                    parent.field[a.X, a.Y + 1].brush == Black && parent.field[b.X, b.Y + 1].brush == Black &&
                    parent.field[c.X, c.Y + 1].brush == Black && parent.field[d.X, d.Y + 1].brush == Black;
        }

        public override bool checkUp()
        {
            try
            {
                //проверка при попытке повернуть фигуру на 90 градусов
                if (rotation == Rotation.rotation0 && parent.field[c.X + 1, c.Y].brush == Black && 
                    parent.field[c.X + 2, c.Y].brush == Black && parent.field[c.X + 3, c.Y].brush == Black)
                    return true;
                // --//-- 180
                if (rotation == Rotation.rotation90 && parent.field[a.X, a.Y - 2].brush == Black &&
                    parent.field[a.X, a.Y - 1].brush == Black && parent.field[a.X, a.Y + 1].brush == Black)
                    return true;
                // --//-- 270
                if (rotation == Rotation.rotation180 && parent.field[b.X + 1, b.Y].brush == Black &&
                    parent.field[b.X + 2, b.Y].brush == Black && parent.field[b.X + 3, b.Y].brush == Black)
                    return true;
                // --//-- исходное положение
                if (rotation == Rotation.rotation270 && parent.field[a.X, a.Y - 1].brush == Black &&
                    parent.field[a.X, a.Y + 1].brush == Black && parent.field[a.X, a.Y + 2].brush == Black)
                    return true;
            }
            catch
            {
                return false;
            }
            return false;
        }
        #endregion
    }
}