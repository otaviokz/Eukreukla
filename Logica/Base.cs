using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eukreukla;

namespace Logica
{
    [Serializable]
    public abstract class Base
    {
        #region Constantes
        //Constantes utilizadas no tabuleiro
        protected const int VAZIA = -1;
        protected const int BRANCAS = 1;
        protected const int PRETAS = 0;
        protected const int PROXIMA_JOGADA = 2;
        protected const int PECA_COMIDA = 3;
        protected const int COMEU_PECA = 9;
        protected const int NAO_COMEU = -9;
        /*
         * Ao adicionar 4, fugimos de todas as outras constantes e podemos utilizar o número resultante
         * para calcular a cor da peça a ser marcada.
         */
        protected const int PECA_A_COMER = 4;
        #endregion


        private Dictionary<int, int[]> m_dicVizinhos;
        /// <summary>
        /// Dicionário indexado por endereco da casa contendo todos os vizinhos
        /// dessa casa
        /// </summary>
        public Dictionary<int, int[]> Vizinhos
        {
            get { return m_dicVizinhos; }
        }


        /// <summary>
        /// Construtor
        /// </summary>
        public Base()
        {
            m_dicVizinhos = new Dictionary<int, int[]>();
            CriaVizinhos(m_dicVizinhos);
        }



        #region Metodos

        protected static int CorJogadorAdversario(int  _corDoJogador)
        {
            if (_corDoJogador == BRANCAS)
                return PRETAS;
            else
                return BRANCAS;
        }
        /// <summary>
        /// Cria um dicionario de vizinhos onde:
        /// CHAVE: endereco da casa no tabuleiro
        /// VALOR: array de int com as casas vizinhas
        /// </summary>
        /// <param name="_dicVizinhos">Dicionario null</param>
        /// <returns>um objeto Dicionario carregado com vizinhos com chaves de 1 a 25.</returns>
        protected static void CriaVizinhos(Dictionary<int, int[]> dicVizinhos)
        {
            for (int i = 1; i < 26; i++)
            {
                switch (i)
                {
                    case 1:
                        dicVizinhos.Add(i, new int[] { 2, 6, 7 });
                        break;
                    case 2:
                        dicVizinhos.Add(i, new int[] { 1, 3, 7 });
                        break;
                    case 3:
                        dicVizinhos.Add(i, new int[] { 2, 4, 7, 8, 9 });
                        break;
                    case 4:
                        dicVizinhos.Add(i, new int[] { 3, 5, 9 });
                        break;
                    case 5:
                        dicVizinhos.Add(i, new int[] { 4, 9, 10 });
                        break;
                    case 6:
                        dicVizinhos.Add(i, new int[] { 1, 7, 11 });
                        break;
                    case 7:
                        dicVizinhos.Add(i, new int[] { 1, 2, 3, 6, 8, 11, 12, 13 });
                        break;
                    case 8:
                        dicVizinhos.Add(i, new int[] { 3, 7, 9, 13 });
                        break;
                    case 9:
                        dicVizinhos.Add(i, new int[] { 3, 4, 5, 8, 10, 13, 14, 15 });
                        break;
                    case 10:
                        dicVizinhos.Add(i, new int[] { 5, 9, 15 });
                        break;
                    case 11:
                        dicVizinhos.Add(i, new int[] { 6, 7, 12, 16, 17 });
                        break;
                    case 12:
                        dicVizinhos.Add(i, new int[] { 7, 11, 13, 17 });
                        break;
                    case 13:
                        dicVizinhos.Add(i, new int[] { 7, 8, 9, 12, 14, 17, 18, 19 });
                        break;
                    case 14:
                        dicVizinhos.Add(i, new int[] { 9, 13, 15, 19 });
                        break;
                    case 15:
                        dicVizinhos.Add(i, new int[] { 9, 10, 14, 19, 20 });
                        break;
                    case 16:
                        dicVizinhos.Add(i, new int[] { 11, 17, 21 });
                        break;
                    case 17:
                        dicVizinhos.Add(i, new int[] { 11, 12, 13, 16, 18, 21, 22, 23 });
                        break;
                    case 18:
                        dicVizinhos.Add(i, new int[] { 13, 17, 19, 23 });
                        break;
                    case 19:
                        dicVizinhos.Add(i, new int[] { 13, 14, 15, 18, 20, 23, 24, 25 });
                        break;
                    case 20:
                        dicVizinhos.Add(i, new int[] { 15, 19, 25 });
                        break;
                    case 21:
                        dicVizinhos.Add(i, new int[] { 16, 17, 22 });
                        break;
                    case 22:
                        dicVizinhos.Add(i, new int[] { 17, 21, 23 });
                        break;
                    case 23:
                        dicVizinhos.Add(i, new int[] { 17, 18, 19, 22, 24 });
                        break;
                    case 24:
                        dicVizinhos.Add(i, new int[] { 19, 23, 25 });
                        break;
                    case 25:
                        dicVizinhos.Add(i, new int[] { 19, 20, 24 });
                        break;
                    default:
                        break;
                }
            }

        }
        #endregion
    }
}
