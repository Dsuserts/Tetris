using System.Drawing;

namespace Tetris
{
    //хранит данные о всех ячейках и цвете заполнения этих ячеек
    public struct Component
    {
        public int x { get; set; }
        public int y { get; set; }
        public Brush brush { get; set; }

        public Component(int _x, int _y, Brush _brush)
        {
            x = _x;
            y = _y;
            brush = _brush;
        }
    }
}