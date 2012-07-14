using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eukreukla;
using System.Threading;
using System.Collections;

namespace Logica
{
    [Serializable]
    public class JComputador : Jogador
    {
        private Dictionary<int, int[]> dicPecasAComer;


        private int PONT_EXTERNA = 100;
        private int PONT_MEDIA = 30;
        private const int PONT_INTERNA = 10;

        private int m_ProfundidadeTotal = 3;
        private int m_casaDestinoAposComer = -1;

        public int[] ProximaJogada { get; set; }

        private bool m_ComeuPeca = false;
        public bool ComeuPeca
        {
            get
            {
                return m_ComeuPeca;
            }
            set
            {
                value = m_ComeuPeca;

            }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public JComputador()
        {
        //    Types.Position bla2 = 
        //    FCore.Fminmax bla = new FCore.Fminmax(Types.Piece.BLACK, 7, bla2);
        }

        /// <summary>
        /// Gera uma lista de proximas confiruações de tabuleiro possiveis a partir de um estado atual
        /// do tabuliero e do jogador que está jogando.
        /// </summary>
        /// <returns></returns>
        private List<BoardNode> GeraMovimentos(BoardNode _nodoTabuleiro, int _corJogador)
        {
            #region Coments...
            /*
             * Uma peca pode jogar:
             * - quando um vizinho estiver vazio
             * 
             * - quando um vizinho contiver uma peca do adversario e a proxima casa na reta que passa
             * por ambos estiver vazia. Essa proxima casa pode ser calculada pela soma/subtracao do 
             * modulo da diferenca da casa da peca que esta jogando para a peca adversaria vizinha.
             * --subtrae quando vizinho MENOR endereco_peca_atual
             * --soma quando vizinho MAIR endereco_peca_atual
             * Exemplo:
             *  Branca joga;
             *  Branca em 19; preta em 13;
             *  13 < 19? entao tabuleiro[13 - |19 - 13|] = 7; 7 vazia? entao branca_19 come preta_13
             *  
             *  Branca joga;
             *  Branca 11; preta 17;
             *  11 < 17? senao tabuleiro[17 + |13 - 17|] = 21; 21 vazia? senao nao pode comer preta_17
             *  
             *  Devemos bolar um geito (no pior dos casos muitos if's...) para tratar as bordas.
             *  Exemplo: se 19 for comer 15, será testado a casa 11, mas nao é possivel realizar esse movimento
             *  pelo tabuleiro.
             *  SOLUCAO: a casa onde a pedra que está comendo a adversario vai parar deve continuar sendo vizinha
             *  da casa onde estava a adversaria!!! :D
             *  
             * Com isso, "é facil ver que" precisamos de duas listas: uma com movimentos para casas vazias e
             * outra com movimentos apenas de "comer pecas". Caso a lista de comer pecas seja diferente de NULL,
             * descartamos a primeira lista. Com isso, todos os proximos movimentos possiveis retornados serao 
             * de comer. 
             * Caso a lista de comer seja vazia, descartamos ela e retornamos a lista apenas com os movimentos
             * possiveis para vizinhos vazios.
             */
            #endregion

            List<BoardNode> lstVizinhosVazios = new List<BoardNode>();
            List<BoardNode> lstPecasAComer = new List<BoardNode>();

            List<int> vizinhosDaPeca;
            List<int> vizinhosDoAdversario;
            int ProximaCasaLivre;
            int[] novoTabuleiro;

            for (int casa = 1; casa < _nodoTabuleiro.Tabuleiro.Length; casa++)
            {
                if (_nodoTabuleiro.Tabuleiro[casa] == _corJogador)
                {
                    //vizinhosDaPeca = new List<int>();
                    //Lista com os vizinhos da casa atual
                    vizinhosDaPeca = Vizinhos[casa].ToList<int>();

                    //Testa para cada vizinho da casa atual se ele esta vazio. Se estiver, cria um novo
                    //estado de Tabuleiro, ocupando o vizinho vazio, esvaziando a casa antiga e gerando
                    //um novo nodo na lista.
                    foreach (int vizinho in vizinhosDaPeca)
                    {
                        /*
                         * Vizinho tem tres possibilidades:
                         * - ou esta VAZIO e a peca pode ocupar --> ocupa
                         * - ou esta ocupado por peca do adversario e talvez possa comer --> tentar comer
                         * - ou esta ocupado com propria peca --> nao faz nada
                         */
                        if (_nodoTabuleiro.Tabuleiro[vizinho] == VAZIA)
                        {
                            //Aparentemente é necessario usar o CopyTo pois ao atribuir um array no outro, apenas
                            //a referencia na mem é copiada ai os dois apontam pro mesmo lugar e da muita merda...
                            novoTabuleiro = new int[26];
                            _nodoTabuleiro.Tabuleiro.CopyTo(novoTabuleiro, 0);

                            novoTabuleiro[0] = NAO_COMEU;
                            novoTabuleiro[casa] = VAZIA;
                            novoTabuleiro[vizinho] = _corJogador;

                            lstVizinhosVazios.Add(new BoardNode(_nodoTabuleiro, novoTabuleiro, this.CorDoJogador));
                        }
                        //como já nao é vazio, ou é do mesmo time (entao == _corJogador)
                        //ou entao != _corJogador
                        else if (_nodoTabuleiro.Tabuleiro[vizinho] != _corJogador)
                        {
                            #region Coments...
                            //Vizinho == Adversario
                            /*
                             * Re-explicando...
                             * Se o vizinho contem um adversario e a proxima casa na mesma direcao do adversario
                             * tambem estiver vazia, o jogador que esta jogando pode comer essa peca.
                             * 
                             * Para comela, entretanto, essa terceira casa (a que deve estar livre) tem que ser acessivel
                             * a partir da casa vizinha. Se nao, por exemplo, alguem que estiver na casa 19 pode tentar comer
                             * alguem da casa 15 e ir parar na 11, o q é impossivel pelo tabuleiro. 
                             * 
                             * Para resolver isso, esta sendo testado se a proxima casa vazia é vizinha da casa com
                             * a peca adversaria (a "passiva" q esta sendo comida...). Se for, entao o movimento
                             * pode ser realizado.
                             * 
                             * Para calcular o endereco dessa proxima casa basta acessar o indice do tabuleiro referente
                             * a [end_vizinho +- |end_casa - end_vizinho|] => endereco do vizinho adversario menos ou mais o
                             * modulo da diferenca do endereco da casa da peca atual com o endereco do vizinho. Menos se o 
                             * endereco do vizinho for menor que o da casa ("comendo para cima") ou mais se o endereco do vizinho
                             * for maior que o da casa ("comendo para baixo").
                             * 
                             * Entao, cria-se um novo estado de tabuleiro com a casa da peca e do vizinho comido vazios,
                             * a proxima casa depois do vizinho ocupada.
                             */
                            #endregion

                            vizinhosDoAdversario = Vizinhos[vizinho].ToList<int>();

                            if (vizinho < casa)
                            {
                                ProximaCasaLivre = vizinho - Math.Abs(casa - vizinho);

                                if (ProximaCasaLivre >= 1 && ProximaCasaLivre <= 25)
                                {
                                    if (_nodoTabuleiro.Tabuleiro[ProximaCasaLivre] == VAZIA &&
                                        vizinhosDoAdversario.Contains(ProximaCasaLivre))
                                    {
                                        //Aparentemente é necessario usar o CopyTo pois ao atribuir um array no outro, apenas
                                        //a referencia na mem é copiada ai os dois apontam pro mesmo lugar e da muita merda...
                                        novoTabuleiro = new int[26];
                                        _nodoTabuleiro.Tabuleiro.CopyTo(novoTabuleiro, 0);

                                        novoTabuleiro[0] = COMEU_PECA;
                                        novoTabuleiro[casa] = VAZIA;
                                        novoTabuleiro[vizinho] = PECA_COMIDA;
                                        novoTabuleiro[ProximaCasaLivre] = _corJogador;

                                        lstPecasAComer.Add(new BoardNode(_nodoTabuleiro, novoTabuleiro, this.CorDoJogador));
                                    }
                                }
                            }
                            else if (vizinho > casa)
                            {
                                ProximaCasaLivre = vizinho + Math.Abs(casa - vizinho);

                                if (ProximaCasaLivre >= 1 && ProximaCasaLivre <= 25)
                                {
                                    if (_nodoTabuleiro.Tabuleiro[ProximaCasaLivre] == VAZIA &&
                                        vizinhosDoAdversario.Contains(ProximaCasaLivre))
                                    {
                                        //Aparentemente é necessario usar o CopyTo pois ao atribuir um array no outro, apenas
                                        //a referencia na mem é copiada ai os dois apontam pro mesmo lugar e da muita merda...
                                        novoTabuleiro = new int[26];
                                        _nodoTabuleiro.Tabuleiro.CopyTo(novoTabuleiro, 0);

                                        novoTabuleiro[0] = COMEU_PECA;
                                        novoTabuleiro[casa] = VAZIA;
                                        novoTabuleiro[vizinho] = PECA_COMIDA;
                                        novoTabuleiro[ProximaCasaLivre] = _corJogador;

                                        lstPecasAComer.Add(new BoardNode(_nodoTabuleiro, novoTabuleiro, this.CorDoJogador));
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Aqui, o vizinho é do proprio time, nao faz nada. 
                        }
                    }
                }
            }

            if (lstPecasAComer.Count == 0)
                return lstVizinhosVazios;
            else
                return lstPecasAComer;
        }

        /// <summary>
        /// Funcao de avaliacao do algoritmo. Le um estado de tabuleiro e um jogador, analisa os dados
        /// e retorna um valor inteiro para tal estado; quanto maior, melhor.
        /// </summary>
        /// <returns></returns>
        public int CalculaValorJogada(int[] _TabuleiroParaAvaliar)
        {
            int valorDaJogada = 0;

            ThreadAvaliacao threadAvaliacaoCusto1 = new ThreadAvaliacao(
                 _TabuleiroParaAvaliar,
                Vizinhos[7].ToList<int>(),
                7);

            ThreadAvaliacao threadAvaliacaoCusto2 = new ThreadAvaliacao(
                _TabuleiroParaAvaliar,
                Vizinhos[9].ToList<int>(),
                9);

            ThreadAvaliacao threadAvaliacaoCusto3 = new ThreadAvaliacao(
                _TabuleiroParaAvaliar,
                Vizinhos[13].ToList<int>(),
                13);

            ThreadAvaliacao threadAvaliacaoCusto4 = new ThreadAvaliacao(
                _TabuleiroParaAvaliar,
                Vizinhos[17].ToList<int>(),
                17);
            ThreadAvaliacao threadAvaliacaoCusto5 = new ThreadAvaliacao(
                _TabuleiroParaAvaliar,
                Vizinhos[19].ToList<int>(),
                19);

            Thread t1 = new Thread(new ThreadStart(threadAvaliacaoCusto1.Avalia));
            Thread t2 = new Thread(new ThreadStart(threadAvaliacaoCusto2.Avalia));
            Thread t3 = new Thread(new ThreadStart(threadAvaliacaoCusto3.Avalia));
            Thread t4 = new Thread(new ThreadStart(threadAvaliacaoCusto4.Avalia));
            Thread t5 = new Thread(new ThreadStart(threadAvaliacaoCusto5.Avalia));

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();

            //Dominio dos cantos
            if (_TabuleiroParaAvaliar[1] == this.CorDoJogador)
                valorDaJogada += 100;
            if (_TabuleiroParaAvaliar[5] == this.CorDoJogador)
                valorDaJogada += 100;
            if (_TabuleiroParaAvaliar[21] == this.CorDoJogador)
                valorDaJogada += 100;
            if (_TabuleiroParaAvaliar[25] == this.CorDoJogador)
                valorDaJogada += 100;



            //Os testes aqui servem para que haja apenas um parametro jogador nessa funcao.

            //if (this.PecasRestantes > _pecasRestantesAdversario)
            //    valorDaJogada += 30;
            //else if (this.PecasRestantes == _pecasRestantesAdversario)
            //    valorDaJogada += 15;
            //else if (this.PecasRestantes < _pecasRestantesAdversario)
            //    valorDaJogada += -5;


            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();
            t5.Join();

            valorDaJogada += (threadAvaliacaoCusto1.ValorDaJogada +
                threadAvaliacaoCusto2.ValorDaJogada +
                threadAvaliacaoCusto3.ValorDaJogada +
                threadAvaliacaoCusto4.ValorDaJogada +
                threadAvaliacaoCusto5.ValorDaJogada) / 5;


            return valorDaJogada;
        }
        /// <summary>
        /// Calcula o valor da jogada. Quanto mais pecas na parte externa do tabuleiro, mais pontos.
        /// </summary>
        /// <param name="_Tabuleiro"></param>
        /// <returns></returns>
        public int CalculaValorJogada2(int[] _Tabuleiro, int _nroPecasJogadaAnteriorJogador, int _nroPecasJogadaAnteriorAdversario)
        {
            int valorDaJogada = 0;
            int pecasJogador = 0;
            int pecasAdversario = 0;

            for (int i = 0; i < _Tabuleiro.Length; i++)
            {
                if (_Tabuleiro[i] == this.CorDoJogador)
                    pecasJogador++;
                else if (_Tabuleiro[i] == CorJogadorAdversario(this.CorDoJogador))
                    pecasAdversario++;
            }

            //Se protege contra acabar o jogo (3 é aproximadamente o teto de 20% para 12 pecas)
            if (pecasJogador < pecasAdversario * 0.6)
                return valorDaJogada = int.MinValue;

            //Valoriza matar o adversario
            if (pecasAdversario < pecasJogador * 0.6)
                return valorDaJogada = int.MaxValue;

            //Valoriza comer pecas
            if (_nroPecasJogadaAnteriorAdversario - pecasAdversario > 0)
                valorDaJogada += 500;
            //Desvaloriza perder pecas
            if (_nroPecasJogadaAnteriorJogador - pecasJogador > 0)
                valorDaJogada += -500;

            if (pecasJogador >= pecasAdversario && pecasJogador - pecasAdversario >= 2)
            {
                PONT_EXTERNA = 50;
                PONT_MEDIA = 200;
            }
            else
            {
                PONT_EXTERNA = 100;
                PONT_MEDIA = 100;
            }



            for (int i = 0; i < 26; i++)
            {
                if (_Tabuleiro[i] == this.CorDoJogador)
                {
                    switch (i)
                    {
                        //casas externas
                        case 1:
                            valorDaJogada += PONT_EXTERNA + 100;
                            break;
                        case 2:
                            valorDaJogada += PONT_EXTERNA;
                            break;
                        case 3:
                            valorDaJogada += PONT_EXTERNA;
                            break;
                        case 4:
                            valorDaJogada += PONT_EXTERNA;
                            break;
                        case 5:
                            valorDaJogada += PONT_EXTERNA + 100;
                            break;
                        case 6:
                            valorDaJogada += PONT_EXTERNA;
                            break;
                        case 10:
                            valorDaJogada += PONT_EXTERNA;
                            break;
                        case 11:
                            valorDaJogada += PONT_EXTERNA;
                            break;
                        case 15:
                            valorDaJogada += PONT_EXTERNA;
                            break;
                        case 16:
                            valorDaJogada += PONT_EXTERNA;
                            break;
                        case 20:
                            valorDaJogada += PONT_EXTERNA;
                            break;
                        case 21:
                            valorDaJogada += PONT_EXTERNA + 100;
                            break;
                        case 22:
                            valorDaJogada += PONT_EXTERNA;
                            break;
                        case 23:
                            valorDaJogada += PONT_EXTERNA;
                            break;
                        case 24:
                            valorDaJogada += PONT_EXTERNA;
                            break;
                        case 25:
                            valorDaJogada += PONT_EXTERNA + 100;
                            break;
                        //casas do meio
                        case 7:
                            valorDaJogada += PONT_MEDIA;
                            break;
                        case 8:
                            valorDaJogada += PONT_MEDIA;
                            break;
                        case 9:
                            valorDaJogada += PONT_MEDIA;
                            break;
                        case 12:
                            valorDaJogada += PONT_MEDIA;
                            break;
                        case 14:
                            valorDaJogada += PONT_MEDIA;
                            break;
                        case 17:
                            valorDaJogada += PONT_MEDIA;
                            break;
                        case 18:
                            valorDaJogada += PONT_MEDIA;
                            break;
                        case 19:
                            valorDaJogada += PONT_MEDIA;
                            break;
                        //casa central
                        case 13:
                            valorDaJogada += AvaliaPecaCentral(_Tabuleiro, pecasJogador, i);
                            break;
                        default:
                            break;
                    }
                }
            }

            return valorDaJogada;
        }


        private int AvaliaPecaCentral(int[] _Tabuleiro, int _pecasJogador, int _casaCentral)
        {
            //Teste
            int proximaCasaLivreParaSerComido = 0;
            int proximaCasaLivreParaComer = 0;

            int somaDeSerComido = 0;
            int somaDeComer = 0;

            int COMER = 1000;
            int SER_COMID0 = -1000;

            int primeiraMedia = 0;
            int segundaMedia = 0;
            //teste

            foreach (int vizinho in Vizinhos[_casaCentral].ToList())
            {
                somaDeComer = somaDeSerComido = 0;

                if (vizinho < _casaCentral)
                {
                    proximaCasaLivreParaSerComido = _casaCentral + Math.Abs(_casaCentral - vizinho);
                    proximaCasaLivreParaComer = vizinho - Math.Abs(_casaCentral - vizinho);
                }
                else if (vizinho > _casaCentral)
                {
                    proximaCasaLivreParaSerComido = _casaCentral - Math.Abs(_casaCentral - vizinho);
                    proximaCasaLivreParaComer = vizinho + Math.Abs(_casaCentral - vizinho);
                }

                if (_Tabuleiro[vizinho] != _Tabuleiro[_casaCentral] && _Tabuleiro[vizinho] != VAZIA)
                {
                    if (proximaCasaLivreParaSerComido >= 1 && proximaCasaLivreParaSerComido <= 25)
                    {
                        if (_Tabuleiro[proximaCasaLivreParaSerComido] == VAZIA)
                            somaDeSerComido += SER_COMID0;
                    }
                    if (proximaCasaLivreParaComer >= 1 && proximaCasaLivreParaComer <= 25)
                    {
                        if (_Tabuleiro[proximaCasaLivreParaComer] == VAZIA)
                            somaDeComer += COMER;
                    }

                    primeiraMedia += Convert.ToInt32((somaDeSerComido + somaDeComer) / 2);
                }
            }
            segundaMedia = primeiraMedia / 8;
            return segundaMedia;
        }

        /// <summary>
        /// Algoritmo que realiza a jogada do computador.
        /// </summary>
        /// <param name="_Tabuleiro">aka Posição. Situacao atual do tabuleiro do jogo</param>
        /// <param name="_Profundidade">Profundidade da recursão. Esta ligada com a dificuldade
        /// do jogo (quanto mais profundo, mais dificil)</param>
        /// <param name="_udtJogador">Jogador que deve realizar a jogada.</param>
        /// <returns></returns>
        private int Minimax(BoardNode _Nodo, int _Profundidade,
            Jogador _udtJogadorMax, Jogador _udtJogadorMin, bool _isMax)
        {
            int numeroPecasJogadorAdverario = -1;
            //if (_Profundidade == 0)
            //{
            //    List<BoardNode> lstSucessores = new List<BoardNode>();
            //    lstSucessores = GeraMovimentos(_Nodo, _udtJogadorMax.CorDoJogador);
            //    for (int i = 0; i < lstSucessores.Count; i++)
            //    {
            //        lstSucessores[i] = Minimax(lstSucessores[i], _Profundidade + 1, _udtJogadorMax, _udtJogadorMin, false);
            //    }

            //    lstSucessores.Sort(Comparer<BoardNode>.Default);
            //    return lstSucessores[lstSucessores.Count - 1];
            //}
            if (_Profundidade == m_ProfundidadeTotal)
            {
                for (int i = 0; i < _Nodo.Tabuleiro.Length; i++)
                    if (_Nodo.Pai.Tabuleiro[i] == CorJogadorAdversario(this.CorDoJogador))
                        numeroPecasJogadorAdverario++;

                return CalculaValorJogada2(_Nodo.Tabuleiro, this.PecasRestantes, numeroPecasJogadorAdverario);

            }
            else
            {
                List<BoardNode> lstSucessores = new List<BoardNode>();
                lstSucessores = GeraMovimentos(_Nodo, _udtJogadorMin.CorDoJogador);
                if (lstSucessores.Count == 0)
                {
                    for (int i = 0; i < _Nodo.Tabuleiro.Length; i++)
                        if (_Nodo.Pai.Tabuleiro[i] == CorJogadorAdversario(this.CorDoJogador))
                            numeroPecasJogadorAdverario++;

                    return CalculaValorJogada2(_Nodo.Tabuleiro, this.PecasRestantes, numeroPecasJogadorAdverario);
                }
                else
                {
                    for (int i = 0; i < lstSucessores.Count; i++)
                    {
                        lstSucessores[i].StepCost = Minimax(lstSucessores[i], _Profundidade + 1, _udtJogadorMin, _udtJogadorMax, !_isMax);
                    }
                    lstSucessores.Sort(Comparer<BoardNode>.Default);

                    if (_isMax)
                    {
                        return lstSucessores[lstSucessores.Count - 1].StepCost;
                    }
                    else
                    {
                        return lstSucessores[0].StepCost;
                    }
                }
            }
        }

        private int MinimaxAlfaBeta(BoardNode _Nodo, int _Profundidade,
            Jogador _udtJogadorMax, Jogador _udtJogadorMin, bool _isMax, int _alpha, int _beta)
        {
            if (_Profundidade == m_ProfundidadeTotal)
            {
                return CalculaValorJogada2(_Nodo.Tabuleiro, _Nodo.NumeroPecasJogador, _Nodo.NumeroPecasJogadorAdverario);
            }
            else
            {
                List<BoardNode> lstSucessores = new List<BoardNode>();

                int i = 0;


                if (_isMax)
                {
                    lstSucessores = GeraMovimentos(_Nodo, _udtJogadorMax.CorDoJogador);
                }
                else
                {
                    lstSucessores = GeraMovimentos(_Nodo, _udtJogadorMin.CorDoJogador);
                }
                int nroSucessores = lstSucessores.Count;
                if (lstSucessores.Count == 0)
                {
                    return CalculaValorJogada2(_Nodo.Tabuleiro, _Nodo.NumeroPecasJogador, _Nodo.NumeroPecasJogadorAdverario);
                }
                else
                {
                    if (_isMax)
                    {
                        while (i < nroSucessores && _beta > _alpha)
                        {
                            lstSucessores[i].StepCost = MinimaxAlfaBeta(lstSucessores[i], _Profundidade + 1,
                                _udtJogadorMax, _udtJogadorMin, !_isMax, _alpha, _beta);

                            if (lstSucessores[i].StepCost > _alpha)
                            {
                                _alpha = lstSucessores[i].StepCost;
                            }
                            i++;
                        }
                        return _alpha;
                    }
                    else
                    {
                        while (i < nroSucessores && _beta > _alpha)
                        {
                            lstSucessores[i].StepCost = MinimaxAlfaBeta(lstSucessores[i], _Profundidade + 1,
                                 _udtJogadorMax, _udtJogadorMin, !_isMax, _alpha, _beta);

                            if (lstSucessores[i].StepCost < _beta)
                            {
                                _beta = lstSucessores[i].StepCost;
                            }
                            i++;

                        }
                        return _beta;
                    }
                }
            }
        }



        /// <summary>
        /// Retorna o máximo entre dois inteiros
        /// </summary>
        /// <param name="a">Primeiro Inteiro</param>
        /// <param name="b">Segundo Interio</param>
        /// <returns></returns>
        private int Max(int a, int b)
        {
            if (a >= b)
                return a;
            else
                return b;
        }

        public int[] RealizaJogada(int[] _TabuleiroAtual, JComputador _udtJogadorPC, Jogador _udtJogadorAdversario, int _ProfundidadeTotal)
        {
            m_ProfundidadeTotal = _ProfundidadeTotal;
            List<BoardNode> lstProximosTabuleiros = GeraMovimentos(new BoardNode(_TabuleiroAtual, this.CorDoJogador), this.CorDoJogador);

            if (lstProximosTabuleiros.Count > 1)
            {
                for (int i = 0; i < lstProximosTabuleiros.Count; i++)
                {
                    lstProximosTabuleiros[i].StepCost = MinimaxAlfaBeta(lstProximosTabuleiros[i], 1,
                        _udtJogadorPC, _udtJogadorAdversario, false, int.MinValue, int.MaxValue);
                }

                lstProximosTabuleiros.Sort(Comparer<BoardNode>.Default);
            }

            if (lstProximosTabuleiros[lstProximosTabuleiros.Count - 1].Tabuleiro[0] == COMEU_PECA)
            {
                m_ComeuPeca = true;
                for (int i = 0; i < 26; i++)
                {
                    if (_TabuleiroAtual[i] == VAZIA &&
                        lstProximosTabuleiros[lstProximosTabuleiros.Count - 1].Tabuleiro[i] == this.CorDoJogador)
                        m_casaDestinoAposComer = i;
                }
            }
            else
                m_ComeuPeca = false;

            this.ProximaJogada = lstProximosTabuleiros[lstProximosTabuleiros.Count - 1].Tabuleiro;

            return lstProximosTabuleiros[lstProximosTabuleiros.Count - 1].Tabuleiro;
        }



        /// <summary>
        /// Metodo para calcular se o jogador que acabou de comer uma peca pode comer mais uma, em
        /// sequencia
        /// </summary>
        /// <param name="_TabuleiroAtual">Tabuleiro atual</param>
        /// <param name="_corDoJogador">Cor do jogador que acabou de cor uma peca</param>
        /// <param name="_casa">Casa onde ele parou depois mais recente comida de pecas</param>
        /// <returns></returns>
        public bool ComeMaisUmPeca(int[] _TabuleiroAtual, int _corDoJogador, int _nroPecasJogadaAnteriorJogador, int _nroPecasJogadaAnteriorAdversario)
        {
            dicPecasAComer = new Dictionary<int, int[]>();

            List<int> vizinhosDaPeca;
            List<int> vizinhosDoAdversario;

            int ProximaCasaLivre;
            int[] novoTabuleiro;
            int valorDaJogada;


            //Lista com os vizinhos da casa atual
            vizinhosDaPeca = Vizinhos[m_casaDestinoAposComer].ToList<int>();

            //Testa para cada vizinho da casa atual se ele esta vazio. Se estiver, cria um novo
            //estado de Tabuleiro, ocupando o vizinho vazio, esvaziando a casa antiga e gerando
            //um novo nodo na lista.
            foreach (int vizinho in vizinhosDaPeca)
            {
                /*
                 * Vizinho tem tres possibilidades:
                 * - ou esta VAZIO e a peca pode ocupar --> ocupa
                 * - ou esta ocupado por peca do adversario e talvez possa comer --> tentar comer
                 * - ou esta ocupado com propria peca --> nao faz nada
                 */
                if (_TabuleiroAtual[vizinho] == VAZIA || _TabuleiroAtual[vizinho] == PECA_COMIDA)
                {
                    //nao faz nada
                }
                //como já nao é vazio, ou é do mesmo time (entao == m_corJogador)
                //ou entao != m_corJogador
                else if (_TabuleiroAtual[vizinho] != _corDoJogador)
                {
                    vizinhosDoAdversario = Vizinhos[vizinho].ToList<int>();

                    if (vizinho < m_casaDestinoAposComer)
                    {
                        ProximaCasaLivre = vizinho - Math.Abs(m_casaDestinoAposComer - vizinho);

                        if (ProximaCasaLivre >= 1 && ProximaCasaLivre <= 25)
                        {
                            if (_TabuleiroAtual[ProximaCasaLivre] == VAZIA && vizinhosDoAdversario.Contains(ProximaCasaLivre))
                            {
                                //Aparentemente é necessario usar o CopyTo pois ao atribuir um array no outro, apenas
                                //a referencia na mem é copiada ai os dois apontam pro mesmo lugar e da muita merda...
                                novoTabuleiro = new int[26];
                                _TabuleiroAtual.CopyTo(novoTabuleiro, 0);

                                novoTabuleiro[0] = COMEU_PECA;
                                novoTabuleiro[m_casaDestinoAposComer] = VAZIA;
                                novoTabuleiro[vizinho] = PECA_COMIDA;
                                novoTabuleiro[ProximaCasaLivre] = _corDoJogador;
                                m_casaDestinoAposComer = ProximaCasaLivre;
                                m_ComeuPeca = true;

                                valorDaJogada = CalculaValorJogada2(novoTabuleiro,
                                    _nroPecasJogadaAnteriorJogador, _nroPecasJogadaAnteriorAdversario);

                                if (!dicPecasAComer.Keys.Contains(valorDaJogada))
                                    dicPecasAComer.Add(valorDaJogada, novoTabuleiro);

                            }
                        }
                    }
                    else if (vizinho > m_casaDestinoAposComer)
                    {
                        ProximaCasaLivre = vizinho + Math.Abs(m_casaDestinoAposComer - vizinho);

                        if (ProximaCasaLivre >= 1 && ProximaCasaLivre <= 25)
                        {
                            if (_TabuleiroAtual[ProximaCasaLivre] == VAZIA && vizinhosDoAdversario.Contains(ProximaCasaLivre))
                            {
                                //Aparentemente é necessario usar o CopyTo pois ao atribuir um array no outro, apenas
                                //a referencia na mem é copiada ai os dois apontam pro mesmo lugar e da muita merda...
                                novoTabuleiro = new int[26];
                                _TabuleiroAtual.CopyTo(novoTabuleiro, 0);

                                novoTabuleiro[0] = COMEU_PECA;
                                novoTabuleiro[m_casaDestinoAposComer] = VAZIA;
                                novoTabuleiro[vizinho] = PECA_COMIDA;
                                novoTabuleiro[ProximaCasaLivre] = _corDoJogador;
                                m_casaDestinoAposComer = ProximaCasaLivre;
                                m_ComeuPeca = true;

                                valorDaJogada = CalculaValorJogada2(novoTabuleiro,
                                     _nroPecasJogadaAnteriorJogador, _nroPecasJogadaAnteriorAdversario);

                                if (!dicPecasAComer.Keys.Contains(valorDaJogada))
                                    dicPecasAComer.Add(valorDaJogada, novoTabuleiro);


                            }
                        }
                    }
                }
                else
                {
                    //Aqui, o vizinho é do proprio time, nao faz nada.
                }

            }//Fim do foreach de vizinhos

            if (dicPecasAComer.Count == 0)
            {
                m_ComeuPeca = false;
                return false;
            }
            else
            {
                List<int> lstChaves = dicPecasAComer.Keys.ToList();
                lstChaves.Sort();
                this.ProximaJogada = dicPecasAComer[lstChaves[lstChaves.Count - 1]];
                m_ComeuPeca = true;
                return true;
            }
        }

    }

    public class OrdenacaoPorValor : IComparer
    {
        int IComparer.Compare(Object x, Object y)
        {
            BoardNode objetoA = (BoardNode)x;
            BoardNode objetoB = (BoardNode)y;
            return objetoA.StepCost.CompareTo(objetoB.StepCost);
        }
    }
}
