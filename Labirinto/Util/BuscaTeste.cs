using Labirinto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Labirinto.Util
{
    public class BuscaTeste
    {
        private static int[,] maze;

        public List<PosicaoModel> AStarSearch(int startY, int startX, int endY, int endX)
        {
            List<PosicaoModel> lista = new List<PosicaoModel>();
            lista.Add(new PosicaoModel(startY, startX));

            BlocoModel[,] mazeMapeado = MapearBlocos(endY, endX);
            BlocoModel blocoAtual = mazeMapeado[startY, startX];

            while (true)
            {
                if (blocoAtual.distancia == 0) break;


            }

            return null;
        }

        #region MÉTODOS AUXILIARES
        private BlocoModel[,] MapearBlocos(int y, int x)
        {
            List<PosicaoModel> lista = new List<PosicaoModel>();
            lista.Add(new PosicaoModel(y, x));

            int distancia = 1;

            BlocoModel[,] mazeMapeado = new BlocoModel[10, 10];
            maze = Maze.CriarLabirinto();
            mazeMapeado[y, x] = new BlocoModel(0, 0);

            while (HasNull(mazeMapeado))
            {
                List<PosicaoModel> novaLista = new List<PosicaoModel>();

                foreach (PosicaoModel l in lista)
                {
                    List<PosicaoModel> camposAdjacentes = CamposAdjacentes(l.posY, l.posX);
                    
                    foreach (PosicaoModel p in camposAdjacentes)
                    {
                        int tipo = TipoCampo(p.posY, p.posX);

                        if (tipo == 0 && mazeMapeado[p.posY, p.posX] == null)
                        {
                            mazeMapeado[p.posY, p.posX] = new BlocoModel(tipo, distancia);

                            novaLista.Add(new PosicaoModel(p.posY, p.posX));
                        } 
                        else if (tipo == 1)
                        {
                            mazeMapeado[p.posY, p.posX] = new BlocoModel(1, -1);
                        }
                    }
                }

                lista = novaLista;
                distancia++;
            }

            return mazeMapeado;
        }

        private bool HasNull(BlocoModel[,] maze)
        {
            foreach(BlocoModel m in maze)
            {
                if (m == null) return true;
            }

            return false;
        }

        private List<PosicaoModel> CamposAdjacentes(int y, int x)
        {
            List<PosicaoModel> adjacentes = new List<PosicaoModel>()
            { new PosicaoModel(y, x-1), new PosicaoModel(y, x+1)
            , new PosicaoModel(y-1, x), new PosicaoModel(y+1, x)};

            return adjacentes;
        }

        private int TipoCampo(int y, int x)
        {
            try
            {
                if (maze[y, x] >= 0 ) return maze[y, x];
            }
            catch (IndexOutOfRangeException)
            {
                return -1;
            }

            return -1;
        }
        #endregion
    }
}