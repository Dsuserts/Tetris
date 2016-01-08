using static System.Drawing.Brushes;
namespace Tetris
{
    public class fT : Figure
    {
        public fT(ITetris parent) : base(parent)
        {
            rotation = Rotation.rotation0;
            brush = QueueBrushes.GetBrush();
            a.X = 0;
            a.Y = 4;
            b.X = 0;
            b.Y = 5;
            c.X = 0;
            c.Y = 6;
            d.X = 1;
            d.Y = 5;
            InitFigure();
        }

        //поворот фигуры "T"
        public override void Up()
        {
            if (!checkUp()) return;
            switch (rotation)
            {
                case Rotation.rotation0:
                    ClearFigure();
                    a.X = b.X;
                    a.Y = a.Y + 1;
                    b.X = a.X + 1;
                    c.X = b.X + 1;
                    c.Y = b.Y;
                    d.X = b.X;
                    d.Y = b.Y - 1;
                    InitFigure();
                    rotation = Rotation.rotation90;
                    break;
                case Rotation.rotation90:
                    ClearFigure();
                    b.X = d.X;
                    b.Y = d.Y;
                    c.X = b.X;
                    c.Y = b.Y + 1;
                    d.X = c.X;
                    d.Y = c.Y + 1;
                    InitFigure();
                    rotation = Rotation.rotation180;
                    break;
                case Rotation.rotation180:
                    ClearFigure();
                    b.X = c.X;
                    b.Y = c.Y;
                    c.X = b.X + 1;
                    c.Y = b.Y;
                    d.X = b.X;
                    d.Y = b.Y + 1;
                    InitFigure();
                    rotation = Rotation.rotation270;
                    break;
                case Rotation.rotation270:
                    ClearFigure();
                    a.Y -= 1;
                    b.X = a.X;
                    c.Y += 1;
                    c.X -= 2;
                    d.Y -= 1;
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
            //проверка при сдвиге фигуры вниз, при повороте на 0
            if (rotation == Rotation.rotation0 && parent.field[a.X + 1, a.Y].brush == Black &&
                parent.field[c.X + 1, c.Y].brush == Black && parent.field[d.X + 1, d.Y].brush == Black)
                return true;
            //проверка при сдвиге фигуры вниз, при повороте на 90
            if (rotation == Rotation.rotation90 && parent.field[c.X + 1, c.Y].brush == Black &&
                parent.field[d.X + 1, d.Y].brush == Black)
                return true;
            //проверка при сдвиге фигуры вниз, при повороте на 180
            if (rotation == Rotation.rotation180 && parent.field[b.X + 1, b.Y].brush == Black &&
                parent.field[c.X + 1, c.Y].brush == Black && parent.field[d.X + 1, d.Y].brush == Black)
                return true;
            //проверка при сдвиге фигуры вниз, при повороте на 270
            if (rotation == Rotation.rotation270 && parent.field[c.X + 1, c.Y].brush == Black &&
                parent.field[d.X + 1, d.Y].brush == Black)
                return true;
            return false;
        }

        public override bool checkLeft()
        {
            //проверка при сдвиге фигуры влево, при повороте 0
            if (rotation == Rotation.rotation0 && a.Y > 0 && parent.field[a.X, a.Y - 1].brush == Black &&
                parent.field[d.X, d.Y - 1].brush == Black)
                return true;
            //проверка при сдвиге фигуры влево, при повороте 90
            if (rotation == Rotation.rotation90 && d.Y > 0 && parent.field[d.X, d.Y - 1].brush == Black &&
                parent.field[a.X, a.Y - 1].brush == Black && parent.field[c.X, c.Y - 1].brush == Black)
                return true;
            //проверка при сдвиге фигуры влево, при повороте 180
            if (rotation == Rotation.rotation180 && b.Y > 0 && parent.field[b.X, b.Y - 1].brush == Black &&
                parent.field[a.X, a.Y - 1].brush == Black)
                return true;
            //проверка при сдвиге фигуры влево, при повороте 270
            if (rotation == Rotation.rotation270 && b.Y > 0 && parent.field[a.X, a.Y - 1].brush == Black &&
                parent.field[b.X, b.Y - 1].brush == Black && parent.field[c.X, c.Y - 1].brush == Black)
                return true;
            return false;
        }

        public override bool checkRight()
        {
            //проверка при сдвиге фигуры вправо, при повороте 0
            if (rotation == Rotation.rotation0 && c.Y < 9 && parent.field[c.X, c.Y + 1].brush == Black &&
                parent.field[d.X, d.Y + 1].brush == Black)
                return true;
            //проверка при сдвиге фигуры вправо, при повороте 90
            if (rotation == Rotation.rotation90 && c.Y < 9 && parent.field[a.X, a.Y + 1].brush == Black &&
                parent.field[b.X, b.Y + 1].brush == Black && parent.field[c.X, c.Y + 1].brush == Black)
                return true;
            //проверка при сдвиге фигуры вправо, при повороте 180
            if (rotation == Rotation.rotation180 && d.Y < 9 && parent.field[a.X, a.Y + 1].brush == Black &&
                parent.field[d.X, d.Y + 1].brush == Black)
                return true;
            //проверка при сдвиге фигуры вправо, при повороте 270
            if (rotation == Rotation.rotation270 && d.Y < 9 && parent.field[a.X, a.Y + 1].brush == Black &&
                parent.field[d.X, d.Y + 1].brush == Black && parent.field[c.X, c.Y + 1].brush == Black)
                return true;
            return false;
        }

        public override bool checkUp()
        {
            try
            {
                //проверка при попытке перевернуть фигуру на 90 градусов
                if (rotation == Rotation.rotation0 && parent.field[d.X + 1, d.Y].brush == Black &&
                    parent.field[d.X, d.Y - 1].brush == Black)
                    return true;
                // --//-- 180
                if (rotation == Rotation.rotation90 && parent.field[b.X, b.Y + 1].brush == Black)
                    return true;
                // --//-- 270
                if (rotation == Rotation.rotation180 && parent.field[c.X + 1, c.Y].brush == Black)
                    return true;
                // --//-- исходное положение
                if (rotation == Rotation.rotation270 && parent.field[b.X, b.Y - 1].brush == Black)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}