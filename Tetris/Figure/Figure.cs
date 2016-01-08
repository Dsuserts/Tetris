using System.Collections;
using System.Drawing;

namespace Tetris
{
    public abstract class Figure
    {
        //каждая фигура состоит из 4 точек
        protected Point a, b, c, d;

        protected ITetris parent;

        //цвет отрисовки фигуры
        protected Brush brush;

        //градус вращения фигуры, по умолч. 0
        protected Rotation rotation = Rotation.rotation0;

        protected Figure(ITetris parent)
        {
            this.parent = parent;
        }

        #region движение фигур

        //падение фигуры вниз 
        public  bool Down()
        {
            if (checkDown())
            {
                ClearFigure();
                a.X++;
                b.X++;
                c.X++;
                d.X++;
                InitFigure();
                return true;
            }
            return false;
        }

        
        //движение фигуры влево
        public void Left()
        {
            if (checkLeft())
            {
                ClearFigure();
                a.Y--;
                b.Y--;
                c.Y--;
                d.Y--;
                InitFigure();
            }
        }

        //движение фигуры вправо
        public  void Right()
        {
            if (checkRight())
            {
                ClearFigure();
                a.Y++;
                b.Y++;
                c.Y++;
                d.Y++;
                InitFigure();
            }
        }

        //поворачивание фигуры
        public abstract void Up();

        #endregion


        #region изменение поля

        //вывод текущей фигуры на поле
        protected void InitFigure()
        {
            parent.field[a.X, a.Y].brush = brush;
            parent.field[b.X, b.Y].brush = brush;
            parent.field[c.X, c.Y].brush = brush;
            parent.field[d.X, d.Y].brush = brush;
        }

        //очистка текущей фигуры
        protected void ClearFigure()
        {
            parent.field[a.X, a.Y].brush = Brushes.Black;
            parent.field[b.X, b.Y].brush = Brushes.Black;
            parent.field[c.X, c.Y].brush = Brushes.Black;
            parent.field[d.X, d.Y].brush = Brushes.Black;
        }

        #endregion


        #region Проверки

        public abstract bool checkDown();

        public abstract bool checkLeft();

        public abstract bool checkRight();

        public abstract bool checkUp();

        #endregion

    }
}