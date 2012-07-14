using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eukreukla
{
    public class ThreadAvaliacao
    {

        private int[] Tabuleiro;
        private List<int> vizinhos;
        private int casaEmAvaliacao;

        private const int BRANCAS = 1;
        private const int PRETAS = 0;
        private const int VAZIA = -1;

        private int proximaCasaLivreParaSerComido = 0;
        private int proximaCasaLivreParaComer = 0;

        private int somaDeSerComido = 0;
        private int somaDeComer = 0;

        private int valorDaJogada = 0;

        public int ValorDaJogada
        {
            get { return valorDaJogada; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public ThreadAvaliacao(int[] _Tabuleiro, List<int> _vizinhos, int _casaEmAvaliacao)
        {
            Tabuleiro = _Tabuleiro;
            vizinhos = _vizinhos;
            casaEmAvaliacao = _casaEmAvaliacao;
        }

        /// <summary>
        /// Função que avalia as casas 7, 9, 13, 17 e 19 - casas que tem uma ampla possibilidade de
        /// movimentos (8 vizinhos). 
        /// </summary>
        public void Avalia()
        {
            /*
             * A avaliacao está ocorrendo da seguinte forma:
             * 
             * Sao calculados dois enderecos: o anterior a uma peca vizinha e o anterior a propria peca.
             * Testa se a peca pode ser comida ou nao. Se ela puder (proximaCasaLivreParaSerComido == VAZIA)
             * aumenta o peso negativo. Se puder comer (proximaCasaLivreParaComer == VAZIA), aumenta o peso 
             * positivo.
             */
            foreach (int vizinho in vizinhos)
            {

                somaDeComer = somaDeSerComido = 0;


                if (vizinho < casaEmAvaliacao)
                {
                    proximaCasaLivreParaSerComido = casaEmAvaliacao + Math.Abs(casaEmAvaliacao - vizinho);
                    proximaCasaLivreParaComer = vizinho - Math.Abs(casaEmAvaliacao - vizinho);
                }
                else if (vizinho > casaEmAvaliacao)
                {
                    proximaCasaLivreParaSerComido = casaEmAvaliacao - Math.Abs(casaEmAvaliacao - vizinho);
                    proximaCasaLivreParaComer = vizinho + Math.Abs(casaEmAvaliacao - vizinho);
                }

                if (Tabuleiro[vizinho] != Tabuleiro[casaEmAvaliacao] && Tabuleiro[vizinho] != VAZIA)
                {
                    if (proximaCasaLivreParaSerComido >= 1 && proximaCasaLivreParaSerComido <= 25)
                    {
                        if (Tabuleiro[proximaCasaLivreParaSerComido] == VAZIA)
                            somaDeSerComido -= 100;
                        else
                            somaDeSerComido += 200;
                    }
                    if (proximaCasaLivreParaComer >= 1 && proximaCasaLivreParaComer <= 25)
                    {
                        if (Tabuleiro[proximaCasaLivreParaComer] == VAZIA)
                            somaDeComer += 100;
                        else
                            somaDeComer -= 200;
                    }

                }

                valorDaJogada = Convert.ToInt32(valorDaJogada + ((0.2 * somaDeSerComido + 0.8 * somaDeComer) / 2));

            }
            
        }
    }
}
