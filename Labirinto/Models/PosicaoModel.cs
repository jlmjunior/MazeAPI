using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Labirinto.Models
{
    public class PosicaoModel
    {
        public int PosY { get; set; }
        public int PosX { get; set; }

        public PosicaoModel(int posicaoY, int posicaoX)
        {
            this.PosY = posicaoY;
            this.PosX = posicaoX;
        }
    }
}