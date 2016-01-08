using static System.Drawing.Brushes;
namespace Tetris
{
    public class fJ: Figure
    {
        public fJ(ITetris parent) : base(parent)
        {
            rotation = Rotation.rotation0;
            brush = QueueBrushes.GetBrush();
            a.X = 0;
            a.Y = 5;
            b.X = 1;
            b.Y = 5;
            c.X = 2;
            c.Y = 4;
            d.X = 2;
            d.Y = 5;
            InitFigure();
        }

        //поворот фигуры "J"
        public override void Up()
        {
            if (!checkUp()) return;
            switch (rotation)
            {
                case Rotation.rotation0:
                    ClearFigure();
                    a.Y -= 1;
                    b.Y -= 1;
                    c.X = b.X;
                    c.Y = b.Y+1;
                    d.X = c.X;
                    d.Y = c.Y + 1;
                    InitFigure();
                    rotation = Rotation.rotation90;
                    break;
                case Rotation.rotation90:
                    ClearFigure();
                    a.X = c.X - 1;
                    a.Y = c.Y;
                    b.X = a.X;
                    b.Y = a.Y + 1;
                    d.X = c.X + 1;
                    d.Y = c.Y;
                    InitFigure();
                    rotation = Rotation.rotation180;
                    break;
                case Rotation.rotation180:
                    ClearFigure();
                    a.Y -= 1;
                    b.Y = a.Y + 1;
                    c.X = b.X;
                    c.Y = b.Y + 1;
                    d.X = c.X + 1;
                    d.Y = c.Y;
                    InitFigure();
                    rotation = Rotation.rotation270;
                    break;
                case Rotation.rotation270:
                    ClearFigure();
                    a.Y = b.Y;
                    b.X = a.X + 1;
                    b.Y = a.Y;
                    d.X = b.X + 1;
                    d.Y = b.Y;
                    c.X = d.X;
                    c.Y = d.Y - 1;
                    InitFigure();
                    rotation = Rotation.rotation0;
                    break;
            }
        }

        #region проверки
        public override bool checkDown()
        {
            //если фигура на дне => false
            if (d.X == 19)
                return false;
            //проверка при сдвиге фигуры вниз, текущий поворот 0
            if (rotation == Rotation.rotation0 && parent.field[d.X + 1, d.Y].brush == Black && 
                parent.field[c.X + 1, c.Y].brush == Black)
                return true;
            //проверка при сдвиге фигуры вниз, текущий поворот 90
            if (rotation == Rotation.rotation90 && parent.field[d.X + 1, d.Y].brush == Black &&
                parent.field[c.X + 1, c.Y].brush == Black && parent.field[b.X + 1, b.Y].brush == Black)
                return true;
            //проверка при сдвиге фигуры вниз, текущий поворот 180
            if (rotation == Rotation.rotation180 && parent.field[d.X + 1, d.Y].brush == Black && 
                parent.field[b.X + 1, b.Y].brush == Black)
                return true;
            //проверка при сдвиге фигуры вниз, текущий поворот 270
            return rotation == Rotation.rotation270 && parent.field[a.X + 1, a.Y].brush == Black && 
                parent.field[b.X + 1, b.Y].brush == Black && parent.field[d.X + 1, d.Y].brush == Black;
        }

        public override bool checkLeft()
        {
            //проверка при сдвиге фигуры влево, текущий поворот 0 
            if (rotation == Rotation.rotation0 && c.Y > 0 && parent.field[a.X, a.Y - 1].brush == Black && 
                parent.field[b.X, b.Y - 1].brush == Black && parent.field[c.X, c.Y - 1].brush == Black)
                return true;
            //проверка при сдвиге фигуры влево, текущий поворот 90 
            if (rotation == Rotation.rotation90 && b.Y > 0 && parent.field[a.X, a.Y - 1].brush == Black && 
                parent.field[b.X, b.Y - 1].brush == Black)
                return true;
            //проверка при сдвиге фигуры влево, текущий поворот 180 
            if (rotation == Rotation.rotation180 & d.Y > 0 && parent.field[a.X, a.Y - 1].brush == Black && 
                parent.field[c.X, c.Y - 1].brush == Black && parent.field[d.X, d.Y - 1].brush == Black)
                return true;
            //проверка при сдвиге фигуры влево, текущий поворот 270 
            return rotation == Rotation.rotation270 && a.Y > 0 && parent.field[a.X, a.Y - 1].brush == Black && 
                parent.field[d.X, d.Y - 1].brush == Black;
        }

        public override bool checkRight()
        {
            //проверка при сдвиге фигуры вправо, текущий поворот 0 
            if (rotation == Rotation.rotation0 && d.Y < 9 && parent.field[a.X, a.Y + 1].brush == Black && 
                parent.field[b.X, b.Y + 1].brush == Black && parent.field[d.X, d.Y + 1].brush == Black)
                return true;
            //проверка при сдвиге фигуры вправо, текущий поворот 90 
            if (rotation == Rotation.rotation90 && d.Y < 9 && parent.field[a.X, a.Y + 1].brush == Black &&
                parent.field[d.X, d.Y + 1].brush == Black)
                return true;
            //проверка при сдвиге фигуры вправо, текущий поворот 180 
            if (rotation == Rotation.rotation180 && b.Y < 9 && parent.field[c.X, c.Y + 1].brush == Black && 
                parent.field[b.X, b.Y + 1].brush == Black && parent.field[d.X, d.Y + 1].brush == Black)
                return true;
            //проверка при сдвиге фигуры вправо, текущий поворот 0 
            return rotation == Rotation.rotation270 && d.Y < 9 && parent.field[c.X, c.Y + 1].brush == Black &&
                parent.field[d.X, d.Y + 1].brush == Black;
        }

        public override bool checkUp()
        {
            try
            {
                //проверка при попытке повернуть фигуру, текущий поворот 0
                if (rotation == Rotation.rotation0 && parent.field[a.X, a.Y - 1].brush == Black && 
                    parent.field[b.X, b.Y - 1].brush == Black && parent.field[b.X, b.Y + 1].brush == Black)
                    return true;
                //проверка при попытке повернуть фигуру, текущий поворот 90
                if (rotation == Rotation.rotation90 && parent.field[a.X, a.Y + 1].brush == Black && 
                    parent.field[a.X, a.Y + 2].brush == Black && parent.field[c.X + 1, c.Y].brush == Black)
                    return true;
                //проверка при попытке повернуть фигуру, текущий поворот 180
                if (rotation == Rotation.rotation180 && parent.field[a.X, a.Y - 1].brush == Black &&
                    parent.field[b.X + 1, b.Y].brush == Black)
                    return true;
                //проверка при попытке повернуть фигуру, текущий поворот 270
                return rotation == Rotation.rotation270 && parent.field[b.X + 1, b.Y].brush == Black && 
                    parent.field[b.X + 2, b.Y].brush == Black && parent.field[a.X + 2, a.Y].brush == Black;
            }
            catch
            {
                return false;
            }
        }
        #endregion 
    }
}