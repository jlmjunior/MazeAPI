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

            PosicaoModel campo = new PosicaoModel(startY, startX);

            Stack pilha = new Stack();
            pilha.Push(campo);

            while (true)
            {
                campo = (PosicaoModel)pilha.Peek();

                if (campo.posY == endY && campo.posX == endX) break;

                maze[campo.posY, campo.posX] = 1;

                List<PosicaoModel> adjacentes = CamposAdjacentes(campo.posY, campo.posX);

                bool hasNext = false;

                foreach (PosicaoModel p in adjacentes)
                {
                    if (VerificaCampo(p.posY, p.posX))
                    {
                        pilha.Push(new PosicaoModel(p.posY, p.posX));
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
        #endregion
    }
}