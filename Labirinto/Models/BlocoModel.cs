using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Labirinto.Models
{
    public class BlocoModel
    {
        public int Tipo { get; set; }
        public int Distancia { get; set; }

        public BlocoModel(int tipo, int distancia)
        {
            this.Tipo = tipo;
            this.Distancia = distancia;
        }
    }
}