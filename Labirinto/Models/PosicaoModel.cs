using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Labirinto.Models
{
    public class PosicaoModel
    {
        public int posY { get; set; }
        public int posX { get; set; }

        public PosicaoModel(int posicaoY, int posicaoX)
        {
            this.posY = posicaoY;
            this.posX = posicaoX;
        }
    }
}