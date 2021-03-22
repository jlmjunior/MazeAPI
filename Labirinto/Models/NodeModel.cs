using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Labirinto.Models
{
    public class NodeModel
    {
        public PosicaoModel Posicao { get; set; }
        public PosicaoModel Pai { get; set; }
        public int G { get; set; }
        public int H { get; set; }
        public int F 
        { 
            get { return H + G; } 
        }

        public NodeModel(PosicaoModel posicao, int g, int h)
        {
            this.Posicao = posicao;
            this.G = g;
            this.H = h;
        }
    }
}