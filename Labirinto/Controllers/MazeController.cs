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

            // Valida se as posições escolhidas não são paredes
            if (maze[startY, startX] == 1 || maze[endY, endX] == 1)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Message: Error", "application/json");
            }

            BuscaCega buscaCega = new BuscaCega();

            Stack pilha = buscaCega.DepthSearch(startY, startX, endY, endX);

            List<PosicaoModel> lista = new List<PosicaoModel>();

            int size = pilha.Count;

            // Desempilha e transfere para lista do tipo Posicao que vai ser retornada no JSON
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

            // Valida se as posições escolhidas não são paredes
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
            
            // Retrocede o caminho através das posições dos nós pais, até encontrar o nó inicial (sem pai)
            while(nodeAtual.pai != null)
            {
                lista.Add(nodeAtual.posicao);

                PosicaoModel p = nodeAtual.pai;
                nodeAtual = nodeList.Find(e => e.posicao.posX == p.posX && e.posicao.posY == p.posY);
            }

            return Request.CreateResponse(HttpStatusCode.OK, lista, "application/json");
        }
    }
}
