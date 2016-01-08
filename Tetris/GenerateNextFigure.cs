using System;
using System.Collections;
using System.Collections.Generic;

namespace Tetris
{

    public static class GenerateNextFigure
    {
        //создает и возвращает новую фигуру
        public static Figure Get(ITetris tetris, int num)
        {
            switch (num)
            {
                case 0:
                    return new fI(tetris);
                case 1:
                    return new fQ(tetris);
                case 2:
                    return new fT(tetris);
                case 3:
                    return new fZ(tetris);
                case 4:
                    return new fJ(tetris);
                case 5:
                    return new fS(tetris);
                case 6:
                    return new fL(tetris);
            }
            return new fT(tetris);
        }
    }
}
