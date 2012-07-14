using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eukreukla;

namespace Logica
{
    [Serializable]
    public class JHumano : Jogador
    {
        private int m_OrigemDoMovimento = -1;
        private List<int> m_lstPossiveisDestinos;

        private Dictionary<int, List<int[]>> dicVizinhosVazios;
        private Dictionary<int, List<int[]>> dicPecasAComer;

        public Dictionary<int, List<int[]>> ProximaJogada { get; set; }

        //Atributos utilizados para GerarMovimentos
        private int[] m_TabuleiroAtualInterno = new int[26];

        public int[] NovoTabuleiroAtual
        {
            get { return m_TabuleiroAtualInterno; }
            set { m_TabuleiroAtualInterno = value; }
        }
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
        public JHumano()
        {

        }

        /// <summary>
        /// Sobrecarga do método de gerar movimentos. Aqui, apenas sao procuradas proximas jogadas
        /// para uma peça especifica, a qual o jogador humano está jogando.
        /// </summary>
        /// <param name="_tabuleiro">Estado atual do tabuleiro</param>
        /// <param name="m_corJogador">Cor das pecas do jogador</param>
        /// <returns></returns>
        public void GeraMovimentos(int[] _TabuleiroAtual, int _corDoJogador)
        {
            dicVizinhosVazios = new Dictionary<int, List<int[]>>();
            dicPecasAComer = new Dictionary<int, List<int[]>>();

            List<int> vizinhosDaPeca;
            List<int> vizinhosDoAdversario;

            int ProximaCasaLivre;
            int[] novoTabuleiro;

            for (int casa = 1; casa < _TabuleiroAtual.Length; casa++)
            {
                if (_TabuleiroAtual[casa] == _corDoJogador)
                {
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
                        if (_TabuleiroAtual[vizinho] == VAZIA || _TabuleiroAtual[vizinho] == PECA_COMIDA)
                        {
                            //Aparentemente é necessario usar o CopyTo pois ao atribuir um array no outro, apenas
                            //a referencia na mem é copiada ai os dois apontam pro mesmo lugar e da muita merda...
                            novoTabuleiro = new int[26];
                            _TabuleiroAtual.CopyTo(novoTabuleiro, 0);

                            novoTabuleiro[casa] = VAZIA;
                            novoTabuleiro[vizinho] = PROXIMA_JOGADA;

                            if (!dicVizinhosVazios.Keys.Contains(casa))
                                dicVizinhosVazios.Add(casa, new List<int[]>());

                            dicVizinhosVazios[casa].Add(novoTabuleiro);
                        }
                        //como já nao é vazio, ou é do mesmo time (entao == m_corJogador)
                        //ou entao != m_corJogador
                        else if (_TabuleiroAtual[vizinho] != _corDoJogador)
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
                                    if (_TabuleiroAtual[ProximaCasaLivre] == VAZIA && vizinhosDoAdversario.Contains(ProximaCasaLivre))
                                    {
                                        //Aparentemente é necessario usar o CopyTo pois ao atribuir um array no outro, apenas
                                        //a referencia na mem é copiada ai os dois apontam pro mesmo lugar e da muita merda...
                                        novoTabuleiro = new int[26];
                                        _TabuleiroAtual.CopyTo(novoTabuleiro, 0);

                                        novoTabuleiro[casa] = VAZIA;
                                        novoTabuleiro[vizinho] += PECA_A_COMER;
                                        novoTabuleiro[ProximaCasaLivre] = PROXIMA_JOGADA;

                                        if (!dicPecasAComer.Keys.Contains(casa))
                                            dicPecasAComer.Add(casa, new List<int[]>());

                                        dicPecasAComer[casa].Add(novoTabuleiro);
                                    }
                                }
                            }
                            else if (vizinho > casa)
                            {
                                ProximaCasaLivre = vizinho + Math.Abs(casa - vizinho);

                                if (ProximaCasaLivre >= 1 && ProximaCasaLivre <= 25)
                                {
                                    if (_TabuleiroAtual[ProximaCasaLivre] == VAZIA && vizinhosDoAdversario.Contains(ProximaCasaLivre))
                                    {
                                        //Aparentemente é necessario usar o CopyTo pois ao atribuir um array no outro, apenas
                                        //a referencia na mem é copiada ai os dois apontam pro mesmo lugar e da muita merda...
                                        novoTabuleiro = new int[26];
                                        _TabuleiroAtual.CopyTo(novoTabuleiro, 0);

                                        novoTabuleiro[casa] = VAZIA;
                                        novoTabuleiro[vizinho] += PECA_A_COMER;
                                        novoTabuleiro[ProximaCasaLivre] = PROXIMA_JOGADA;

                                        if (!dicPecasAComer.Keys.Contains(casa))
                                            dicPecasAComer.Add(casa, new List<int[]>());

                                        dicPecasAComer[casa].Add(novoTabuleiro);
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Aqui, o vizinho é do proprio time, nao faz nada.
                        }

                    }//Fim do foreach de vizinhos


                }//Fim do teste se a peca que esta sendo iterado é do jogador q esta executando o metodo


            }//Fim do For sobre o Tabuleiro

            if (dicPecasAComer.Count == 0)
                this.ProximaJogada = dicVizinhosVazios;
            else
                this.ProximaJogada = dicPecasAComer;
        }

        /// <summary>
        /// Metodo para calcular se o jogador que acabou de comer uma peca pode comer mais uma, em
        /// sequencia
        /// </summary>
        /// <param name="_TabuleiroAtual">Tabuleiro atual</param>
        /// <param name="_corDoJogador">Cor do jogador que acabou de cor uma peca</param>
        /// <param name="_casa">Casa onde ele parou depois mais recente comida de pecas</param>
        /// <returns></returns>
        public bool ComeMaisUmPeca(int[] _TabuleiroAtual, int _corDoJogador, int _casa)
        {
            dicPecasAComer = new Dictionary<int, List<int[]>>();

            List<int> vizinhosDaPeca;
            List<int> vizinhosDoAdversario;

            int ProximaCasaLivre;
            int[] novoTabuleiro;


            //Lista com os vizinhos da casa atual
            vizinhosDaPeca = Vizinhos[_casa].ToList<int>();

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

                    if (vizinho < _casa)
                    {
                        ProximaCasaLivre = vizinho - Math.Abs(_casa - vizinho);

                        if (ProximaCasaLivre >= 1 && ProximaCasaLivre <= 25)
                        {
                            if (_TabuleiroAtual[ProximaCasaLivre] == VAZIA && vizinhosDoAdversario.Contains(ProximaCasaLivre))
                            {
                                //Aparentemente é necessario usar o CopyTo pois ao atribuir um array no outro, apenas
                                //a referencia na mem é copiada ai os dois apontam pro mesmo lugar e da muita merda...
                                novoTabuleiro = new int[26];
                                _TabuleiroAtual.CopyTo(novoTabuleiro, 0);

                                novoTabuleiro[_casa] = VAZIA;
                                novoTabuleiro[vizinho] += PECA_A_COMER;
                                novoTabuleiro[ProximaCasaLivre] = PROXIMA_JOGADA;

                                if (!dicPecasAComer.Keys.Contains(_casa))
                                    dicPecasAComer.Add(_casa, new List<int[]>());

                                dicPecasAComer[_casa].Add(novoTabuleiro);
                            }
                        }
                    }
                    else if (vizinho > _casa)
                    {
                        ProximaCasaLivre = vizinho + Math.Abs(_casa - vizinho);

                        if (ProximaCasaLivre >= 1 && ProximaCasaLivre <= 25)
                        {
                            if (_TabuleiroAtual[ProximaCasaLivre] == VAZIA && vizinhosDoAdversario.Contains(ProximaCasaLivre))
                            {
                                //Aparentemente é necessario usar o CopyTo pois ao atribuir um array no outro, apenas
                                //a referencia na mem é copiada ai os dois apontam pro mesmo lugar e da muita merda...
                                novoTabuleiro = new int[26];
                                _TabuleiroAtual.CopyTo(novoTabuleiro, 0);

                                novoTabuleiro[_casa] = VAZIA;
                                novoTabuleiro[vizinho] += PECA_A_COMER;
                                novoTabuleiro[ProximaCasaLivre] = PROXIMA_JOGADA;

                                if (!dicPecasAComer.Keys.Contains(_casa))
                                    dicPecasAComer.Add(_casa, new List<int[]>());

                                dicPecasAComer[_casa].Add(novoTabuleiro);
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
                return false;
            else
            {
                this.ProximaJogada = dicPecasAComer;
                return true;
            }
        }

        /// <summary>
        /// Faz a uniao de todas as possiveis jogadas a partir de um casa em
        /// diferentes tabuleiros em um tabuleiro só.
        /// </summary>
        /// <param name="_lstTabuleiro"></param>
        public int[] CalculaProximasJogadas(int _casaSendoAvaliada, int[] _TabuleiroAtual)
        {
            m_OrigemDoMovimento = _casaSendoAvaliada;
            m_lstPossiveisDestinos = new List<int>();
            int[] novoTabuleiro = new int[26];
            _TabuleiroAtual.CopyTo(novoTabuleiro, 0);

            if (this.ProximaJogada.Keys.Contains(_casaSendoAvaliada))
            {
                foreach (int[] tabuleiro in this.ProximaJogada[_casaSendoAvaliada])
                {
                    for (int i = 0; i < tabuleiro.Length; i++)
                    {
                        switch (tabuleiro[i])
                        {
                            case PROXIMA_JOGADA:
                                novoTabuleiro[i] = PROXIMA_JOGADA;
                                m_lstPossiveisDestinos.Add(i);
                                break;
                            case PECA_A_COMER: //Se for preta
                                novoTabuleiro[i] = PECA_A_COMER;
                                break;
                            case (PECA_A_COMER + 1): //Se for branca
                                novoTabuleiro[i] = PECA_A_COMER + 1;
                                break;
                            default:
                                break;
                        }
                    }
                }

                return novoTabuleiro;
            }
            return _TabuleiroAtual;
        }

        public bool MovimentaPeca(int _casaDestino, int[] _TabuleiroAtual)
        {
            _TabuleiroAtual.CopyTo(m_TabuleiroAtualInterno, 0);

            if (m_OrigemDoMovimento != -1 && m_lstPossiveisDestinos != null)
            {
                //!Vizinhos[m_OrigemDoMovimento].Contains(_casaDestino) -> se a casa de destino nao
                //é vizinha da propria peca, entao é pq ela esta pulando uma (comendo)
                if (m_lstPossiveisDestinos.Contains(_casaDestino) &&
                    !Vizinhos[m_OrigemDoMovimento].Contains(_casaDestino))
                {
                    int casaPecaComida;

                    //Se deve comer, a casa entre a origem e o destino devem ficar vazias
                    //e deve subtrair uma peca do outro jogador
                    casaPecaComida = Math.Abs(m_OrigemDoMovimento + _casaDestino) / 2;

                    m_TabuleiroAtualInterno[casaPecaComida] = VAZIA;
                    m_TabuleiroAtualInterno[m_OrigemDoMovimento] = VAZIA;
                    m_TabuleiroAtualInterno[_casaDestino] = this.CorDoJogador;
                    m_ComeuPeca = true;

                    return true;
                }
                else if (m_lstPossiveisDestinos.Contains(_casaDestino))
                {
                    m_TabuleiroAtualInterno[m_OrigemDoMovimento] = VAZIA;
                    m_TabuleiroAtualInterno[_casaDestino] = this.CorDoJogador;
                    m_ComeuPeca = false;

                    return true;
                }
            }

            return false;
        }
    }
}
