using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logica;
using Eukreukla;

namespace Estatistica
{
    [Serializable]
    public class Dados
    {
        public TimeSpan TempoDecorrido { get; set; }

        public List<int[]> TabuleirosJogados { get; set; }

        public TipoDosJogadores TipoDosJogadores { get; set; }

        public Jogador JogadorUM { get; set; }

        public Jogador JogadorDOIS { get; set; }

        public Jogo Jogo { get; set; }

        public Dados()
        {
            TabuleirosJogados = new List<int[]>();
        }

    }
}
