using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Labirinto.Util
{
    public class Maze
    {
        // 0 = Acessível / 1 = Inacessível
        public static int[,] CriarLabirinto()
        {
            int[,] tabuleiro = new int[10, 10] 
            { { 0, 1, 1, 1, 1, 1, 1, 1, 0, 1 }
            , { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }
            , { 1, 1, 1, 0, 1, 1, 1, 1, 1, 0 }
            , { 0, 0, 1, 0, 1, 0, 0, 0, 0, 0 }
            , { 0, 1, 1, 0, 1, 1, 0, 1, 1, 1 }
            , { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 }
            , { 0, 1, 1, 1, 1, 1, 0, 1, 0, 0 }
            , { 0, 1, 0, 0, 0, 0, 0, 0, 1, 1 }
            , { 0, 1, 1, 1, 1, 1, 1, 0, 0, 0 }
            , { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 } };

            return tabuleiro;
        }
    }
}