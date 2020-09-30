using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Labirinto.Models
{
    public class NodeModel
    {
        public PosicaoModel posicao { get; set; }
        public PosicaoModel pai { get; set; }
        public int g { get; set; }
        public int h { get; set; }
        public int f 
        { 
            get { return h + g; } 
        }

        public NodeModel(PosicaoModel posicao, int g, int h)
        {
            this.posicao = posicao;
            this.g = g;
            this.h = h;
        }
    }
}