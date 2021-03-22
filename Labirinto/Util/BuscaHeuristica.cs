using Labirinto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Xml;

namespace Labirinto.Util
{
    public class BuscaHeuristica
    {
        #region VARIÁVEIS
        private static int[,] maze;

        private static int eY;
        private static int eX;
        #endregion

        public List<NodeModel> AStarSearch(int startY, int startX, int endY, int endX)
        {
            maze = Maze.CriarLabirinto();

            eY = endY;
            eX = endX;

            List<NodeModel> fechada = new List<NodeModel>(); // Lista para nodes promissores
            List<NodeModel> aberta = new List<NodeModel>(); // Lista para nodes já verificados

            aberta.Add(new NodeModel(new PosicaoModel(startY, startX), 0, BuscaH(new PosicaoModel(startY, startX))));

            while (aberta.Count != 0) // Loop enquanto a lista aberta não estiver vazia
            {
                NodeModel nodeAtual = BuscaMenor(aberta);

                aberta.Remove(nodeAtual);
                fechada.Add(nodeAtual);

                if (nodeAtual.Posicao.PosY == eY && nodeAtual.Posicao.PosX == eX) return fechada; // Retorna o resultado caso o node atual seja o desejado

                // Campos ajacentes ao nó atual
                List<PosicaoModel> camposAdjacentes = CamposAdjacentes(nodeAtual.Posicao.PosY, nodeAtual.Posicao.PosX);

                // Percorre os campos adjacentes e verifica se é um campo válido para incluir na lista aberta
                foreach(PosicaoModel p in camposAdjacentes)
                {
                    if (VerificaCampo(p.PosY, p.PosX)) 
                    {
                        if (!HasNode(p, fechada)) // Adiciona na lista aberta caso o node seja promissor
                        {
                            NodeModel novoNode = new NodeModel(p, nodeAtual.G + 1, BuscaH(p));
                            novoNode.Pai = nodeAtual.Posicao;

                            aberta.Add(novoNode);
                        }
                        else
                        {
                            foreach(NodeModel node in fechada) // Verificação para casos em que se encotrou um F menor que o antigo
                            {
                                if (node.Posicao.PosY == p.PosY && node.Posicao.PosX == p.PosX && node.G > nodeAtual.G + 1)
                                {
                                    node.Pai = nodeAtual.Posicao;
                                    node.G = nodeAtual.G + 1;
                                }
                            }
                        }

                    }
                }
            }

            return null;
        }

        #region MÉTODOS AUXILIARES
        private NodeModel BuscaMenor(List<NodeModel> nodeList)
        {
            List<NodeModel> lista = nodeList.OrderBy(e => e.F).ToList();

            return lista[0];
        }

        private int BuscaH(PosicaoModel p)
        {
            int y = eY - p.PosY;
            y = (y < 0 ? -(y) : y);

            int x = eX - p.PosX;
            x = (x < 0 ? -(x) : x);

            return y + x;
        }

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

        // Verifica se determinado nó existe na lista
        private bool HasNode(PosicaoModel p, List<NodeModel> nodeList)
        {
            var node = nodeList.Find(e => e.Posicao.PosX == p.PosX && e.Posicao.PosY == p.PosY);

            return (node != null);
        }
        #endregion
    }
}