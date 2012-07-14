using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logica;

namespace Eukreukla
{
    [Serializable]
    public class Jogador : Base
    {
        public int CorDoJogador { get; set; }
        public int PecasRestantes { get; set; }

        public bool IsEmbaixo { get; set; }
        public bool IsHumano { get; set; }
        public bool IsPlaying { get; set; }
        public bool IsPrimeiroJogador { get; set; }
        public bool IsVencedor { get; set; }
        public bool IsFuncional { get; set; }


        public Jogador()
        {
            this.IsEmbaixo = false;
            this.IsHumano = false;
            this.IsPlaying = false;
            this.IsPrimeiroJogador = false;
            this.IsVencedor = false;
            this.IsFuncional = false;

            this.PecasRestantes = 12;
        }

        /// <summary>
        /// Remove uma peca do jogador, ao ser comida.
        /// </summary>
        protected void RemoveUmaPeca()
        {
            this.PecasRestantes--;
        }



        

    }
}
