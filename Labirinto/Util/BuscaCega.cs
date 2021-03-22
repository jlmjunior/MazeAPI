using Labirinto.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Labirinto.Util
{
    public class BuscaCega
    {
        private static int[,] maze;

        public Stack DepthSearch(int startY, int startX, int endY, int endX)
        {
            maze = Maze.CriarLabirinto();

            PosicaoModel node = new PosicaoModel(startY, startX);

            Stack pilha = new Stack();
            pilha.Push(node);

            while (IsAvailable())
            {
                node = (PosicaoModel)pilha.Peek(); 

                if (node.PosY == endY && node.PosX == endX) break; 

                maze[node.PosY, node.PosX] = 1; 

                List<PosicaoModel> adjacentes = CamposAdjacentes(node.PosY, node.PosX); 

                bool hasNext = false;

                foreach (PosicaoModel p in adjacentes)
                {
                    if (VerificaCampo(p.PosY, p.PosX))
                    {
                        pilha.Push(new PosicaoModel(p.PosY, p.PosX));
                        hasNext = true;

                        break;
                    }
                }

                if (!hasNext) pilha.Pop();
            }

            return pilha;
        }

        #region MÉTODOS AUXILIARES
        private bool VerificaCampo(int y, int x) 
        {
            try
            {
                if (maze[y, x] == 1) return false;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }

            return true;
        }

        private List<PosicaoModel> CamposAdjacentes(int y, int x)
        {
            List<PosicaoModel> adjacentes = new List<PosicaoModel>()
            { new PosicaoModel(y, x-1), new PosicaoModel(y, x+1)
            , new PosicaoModel(y-1, x), new PosicaoModel(y+1, x)};

            return adjacentes;
        }

        private bool IsAvailable()
        {
            foreach(int i in maze)
            {
                if (i == 0) return true;
            }

            return false;
        }
        #endregion
    }
}