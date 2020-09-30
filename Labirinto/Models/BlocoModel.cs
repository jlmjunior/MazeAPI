using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Labirinto.Models
{
    public class BlocoModel
    {
        public int tipo { get; set; }
        public int distancia { get; set; }

        public BlocoModel(int tipo, int distancia)
        {
            this.tipo = tipo;
            this.distancia = distancia;
        }
    }
}