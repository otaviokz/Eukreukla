using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logica;

namespace Eukreukla
{
    public class BoardNode : IComparable
    {
        protected const int BRANCAS = 1;
        protected const int PRETAS = 0;

        private int[] tabuleiro;
        /// <summary>
        /// Uma situação atual do tabuleiro do jogo.
        /// </summary>
        public int[] Tabuleiro
        {
            get { return tabuleiro; }
            set { tabuleiro = value; }
        }

        private BoardNode pai;
        /// <summary>
        /// Nodo-pai: o nodo na arvore que gerou este nodo
        /// </summary>
        public BoardNode Pai
        {
            get { return pai; }
        }

        //private int profundidade;
        ///// <summary>
        ///// DEPTH: the number of steps along the path from the initial state.
        ///// </summary>
        //public int Profundidade
        //{
        //    get { return profundidade; }
        //    set { profundidade = value; }
        //}

        private int stepCost;
        /// <summary>
        /// Custo da jogada, avaliado pela função de avaliação.
        /// </summary>
        public int StepCost
        {
            get { return stepCost; }
            set { stepCost = value; }
        }

        //private List<BoardNode> lstNodosFilhos;

        //public List<BoardNode> NodosFilhos
        //{
        //    get { return lstNodosFilhos; }
        //    set { lstNodosFilhos = value; }
        //}

        private int numeroPecasJogadorAdverario;

        public int NumeroPecasJogadorAdverario
        {
            get { return numeroPecasJogadorAdverario; }
            set { numeroPecasJogadorAdverario = value; }
        }

        private int numeroPecasJogador;

        public int NumeroPecasJogador
        {
            get { return numeroPecasJogador; }
            set { numeroPecasJogador = value; }
        }

        /// <summary>
        /// Construtor do nodo pai
        /// </summary>
        /// <param name="state"></param>
        public BoardNode(int[] _Tabuleiro, int _corDoJogador)
        {
            this.tabuleiro = _Tabuleiro;
            //this.profundidade = 0;
            this.stepCost = 0;
            this.pai = null;

            for (int j = 0; j < _Tabuleiro.Length; j++)
                if (_Tabuleiro[j] == CorJogadorAdversario(_corDoJogador))
                    numeroPecasJogadorAdverario++;
                else if (_Tabuleiro[j] == _corDoJogador)
                    numeroPecasJogador++;

        }
        /// <summary>
        /// Construtor dos nodos filhos
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="state"></param>
        public BoardNode(BoardNode parent, int[] _Tabuleiro, int _corDoJogador)
        {
            this.tabuleiro = _Tabuleiro;
            this.pai = parent;

            for (int j = 0; j < _Tabuleiro.Length; j++)
                if (_Tabuleiro[j] == CorJogadorAdversario(_corDoJogador))
                    numeroPecasJogadorAdverario++;
                else if (_Tabuleiro[j] == _corDoJogador)
                    numeroPecasJogador++;

            //this.profundidade = parent.profundidade + 1;
        }

        private static int CorJogadorAdversario(int _corDoJogador)
        {
            if (_corDoJogador == BRANCAS)
                return PRETAS;
            else
                return BRANCAS;
        }
        
        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is BoardNode)
            {
                BoardNode bn2 = (BoardNode)obj;

                return this.StepCost.CompareTo(bn2.stepCost);
            }
            else
                throw new ArgumentException("Não é BoadNode");
        }

        #endregion
    }
}
public enum TipoEstadoNodo : int
{
    Visiting = 0,
    Visited,
    Candidate,
    Evaluating,
    Discarded,
    Approved
}