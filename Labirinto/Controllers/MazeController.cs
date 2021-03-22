using Labirinto.Models;
using Labirinto.Util;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Labirinto.Controllers
{
    public class MazeController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage BuscaProfundidade(int startY, int startX, int endY, int endX)
        {
            int[,] maze = Maze.CriarLabirinto();

            if (maze[startY, startX] == 1 || maze[endY, endX] == 1)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Message: Error", "application/json");
            }

            BuscaCega buscaCega = new BuscaCega();

            Stack pilha = buscaCega.DepthSearch(startY, startX, endY, endX);

            List<PosicaoModel> lista = new List<PosicaoModel>();

            int size = pilha.Count;

            for (int i = 0; i < size; i++) 
            {
                lista.Add((PosicaoModel)pilha.Peek());
                pilha.Pop();
            }

            return Request.CreateResponse(HttpStatusCode.OK, lista, "application/json");
        }

        [HttpGet]
        public HttpResponseMessage BuscaHeuristica(int startY, int startX, int endY, int endX)
        {
            int[,] maze = Maze.CriarLabirinto();

            if (maze[startY, startX] == 1 || maze[endY, endX] == 1)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Message: Error", "application/json");
            }

            BuscaHeuristica busca = new BuscaHeuristica();

            List<PosicaoModel> lista = new List<PosicaoModel>();
            List<NodeModel> nodeList = busca.AStarSearch(startY, startX, endY, endX);

            if (nodeList == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Message: Not Found", "application/json");
            }

            NodeModel nodeAtual = nodeList[nodeList.Count - 1];
            
            while(nodeAtual.Pai != null)
            {
                lista.Add(nodeAtual.Posicao);

                PosicaoModel p = nodeAtual.Pai;
                nodeAtual = nodeList.Find(e => e.Posicao.PosX == p.PosX && e.Posicao.PosY == p.PosY);
            }

            return Request.CreateResponse(HttpStatusCode.OK, lista, "application/json");
        }
    }
}
