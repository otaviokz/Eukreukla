using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logica;

namespace Estatistica
{
    [Serializable]
    public class InfoPartida
    {
        public Dictionary<int[],RegistroTabuleiro> DicEstatisticas { get; set; }
        public List<int[]> ListaTabuleirosJogados { get; set; }
        public List<int> ListaValoresAvaliados { get; set; }

        public TipoDosJogadores TipoDosJogadores { get; set; }
        public bool IsAdversarioVencedor { get; set; }


        /// <summary>
        /// Construtor
        /// </summary>
        public InfoPartida()
        {
            ListaTabuleirosJogados = new List<int[]>();
            ListaValoresAvaliados = new List<int>();
        }
    }

    [Serializable]
    public class RegistroTabuleiro
    {
        public int nroVezesJogado { get; set; }
        public int nroVezesVitoria { get; set; }
        public double Chance { get; set; }
    }
}
