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

            // Cria uma nova pilha e empilha o nó inicial
            Stack pilha = new Stack();
            pilha.Push(node);

            // Loop até encontrar o nó desejado ou até verificar todos os nós
            while (IsAvailable())
            {
                node = (PosicaoModel)pilha.Peek(); // Retorna a ponta da pilha

                if (node.posY == endY && node.posX == endX) break; // Caso tenha encontrado o nó de interesse

                maze[node.posY, node.posX] = 1; // Marca nó como visitado

                List<PosicaoModel> adjacentes = CamposAdjacentes(node.posY, node.posX); 

                bool hasNext = false;

                // Percorre os campos adjacentes, caso seja um campo válido adiciona na pilha
                foreach (PosicaoModel p in adjacentes)
                {
                    if (VerificaCampo(p.posY, p.posX))
                    {
                        pilha.Push(new PosicaoModel(p.posY, p.posX)); // Caso o campo seja válido, adiciona na pilha e força a saída do loop atual
                        hasNext = true;

                        break;
                    }
                }

                if (!hasNext) pilha.Pop(); // Remove da pilha caso esteja em um beco sem saída
            }

            return pilha;
        }

        #region MÉTODOS AUXILIARES
        // Verifica se o campo não é uma parede ou está fora dos limites da matriz
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

        // Verifica se a matriz ainda possui campos que não foram explorados
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