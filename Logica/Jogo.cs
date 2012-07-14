using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;
using Logica;
using System.Media;

namespace Eukreukla
{

    [Serializable]
    public class Jogo : Base
    {
        //Delegate que recebe ponteiro para funções
        public delegate void MudaEstadoEventHandler();

        SoundPlayer simpleSound;

        private EstadoDoJogo m_enmEstado;
        private TipoDosJogadores m_enumTipoJogo;
        /// <summary>
        /// Retorna de quem é a vez
        /// </summary>
        public EstadoDoJogo VezDoJogador
        {
            get { return m_enmEstado; }
            set { m_enmEstado = value; }
        }

        private bool m_PodeComerSequencia = false;
        private bool m_IsComputadorUM = false;

        public bool IsComputadorUM
        {
            get { return m_IsComputadorUM; }
        }
        /// <summary>
        /// Booleano que indica se o jogador pode comer mais de uma peca, fazendo
        /// uma sequencia de comidas.
        /// </summary>
        public bool PodeComerSequencia
        {
            get { return m_PodeComerSequencia; }
            set { m_PodeComerSequencia = value; }
        }

        private int[] m_PecasRestantesDosJogadores = new int[2];

        public int[] PecasRestantesDosJogadores
        {
            get { return m_PecasRestantesDosJogadores; }
            set { m_PecasRestantesDosJogadores = value; }
        }


        public bool IsAdversarioVencedor { get; set; }

        public bool PecaFoiComida { get; set; }

        /// <summary>
        /// Model de Jogadores
        /// </summary>
        private JHumano m_HJogadorUm;
        private JComputador m_CJogadorUm;
        private JHumano m_HJogadorDois;
        private JComputador m_CJogadorDois;

        private int[] m_TabuleiroAtual;
        /// <summary>
        /// Estado atual do tabuleiro do jogo
        /// </summary>
        public int[] TabuleiroAtual
        {
            get { return m_TabuleiroAtual; }
        }

        private int[] m_TabuleiroProximosMovimentos;
        /// <summary>
        /// tabuleiro com proximos movimentos
        /// </summary>
        public int[] TabuleiroProximosMovimentos
        {
            get { return m_TabuleiroProximosMovimentos; }
            set { m_TabuleiroProximosMovimentos = value; }

        }

        /// <summary>
        /// Construtor
        /// </summary>
        public Jogo(object _udtJogadorUm, object _udtJogadorDois, TipoDosJogadores _enumTipoJogo)
        {
            ////Chama o método que realiza jogada do computador para P1
            //m_cp1 = new MudaEstado(CP1);
            ////Chama o método que realiza jogada do computador para P2
            //m_cp2 = new MudaEstado(CP2);
            ////Chama o método que mostra o resultado do jogo
            //m_fim = new MudaEstado(Fim);

            m_enumTipoJogo = _enumTipoJogo;
            switch (m_enumTipoJogo)
            {

                case TipoDosJogadores.HumanoXComputador:
                    m_HJogadorUm = new JHumano();
                    m_CJogadorDois = new JComputador();
                    CopiaAtributosJogadores((Jogador)_udtJogadorUm, (Jogador)_udtJogadorDois);
                    break;
                case TipoDosJogadores.HumanoXFComputador:
                    m_HJogadorUm = new JHumano();
                    m_CJogadorDois = new JComputador();
                    CopiaAtributosJogadores((Jogador)_udtJogadorUm, (Jogador)_udtJogadorDois);
                    break;
                case TipoDosJogadores.HumanoXHumano:
                    m_HJogadorUm = new JHumano();
                    m_HJogadorDois = new JHumano();
                    CopiaAtributosJogadores((Jogador)_udtJogadorUm, (Jogador)_udtJogadorDois);
                    break;
                case TipoDosJogadores.ComputadorXHumano:
                    m_CJogadorUm = new JComputador();
                    m_HJogadorDois = new JHumano();
                    CopiaAtributosJogadores((Jogador)_udtJogadorUm, (Jogador)_udtJogadorDois);
                    break;
                case TipoDosJogadores.ComputadorXComputador:
                    m_CJogadorUm = new JComputador();
                    m_CJogadorDois = new JComputador();
                    CopiaAtributosJogadores((Jogador)_udtJogadorUm, (Jogador)_udtJogadorDois);
                    break;
                case TipoDosJogadores.ComputadorXFComputador:
                    m_CJogadorUm = new JComputador();
                    m_CJogadorDois = new JComputador();
                    CopiaAtributosJogadores((Jogador)_udtJogadorUm, (Jogador)_udtJogadorDois);
                    break;
                default:
                    break;
            }

            //Instancia o Tabuleiro
            //A posicao 0 do array nao sera usada para facilitar varios calculos dos algoritmos
            //que envolvem os indices dele, por isso 26 posicoes para o jogo com 25 casas
            m_TabuleiroAtual = new int[26];
            m_TabuleiroProximosMovimentos = new int[26];

            Jogador jogadorConfig = (Jogador)_udtJogadorUm;

            if (jogadorConfig.IsPrimeiroJogador)
            {
                if (m_enumTipoJogo == TipoDosJogadores.HumanoXComputador
                    || m_enumTipoJogo == TipoDosJogadores.HumanoXHumano
                    || m_enumTipoJogo == TipoDosJogadores.HumanoXFComputador)
                    m_enmEstado = EstadoDoJogo.JogadorUm;
                else if (m_enumTipoJogo == TipoDosJogadores.ComputadorXHumano
                      || m_enumTipoJogo == TipoDosJogadores.ComputadorXComputador
                      || m_enumTipoJogo == TipoDosJogadores.ComputadorXFComputador)
                {
                    m_enmEstado = EstadoDoJogo.ComputadorJogando;
                    m_IsComputadorUM = true;
                }
            }
            else
            {
                if (m_enumTipoJogo == TipoDosJogadores.ComputadorXHumano || m_enumTipoJogo == TipoDosJogadores.HumanoXHumano)
                    m_enmEstado = EstadoDoJogo.JogadorDois;
                else if (
                       m_enumTipoJogo == TipoDosJogadores.HumanoXComputador
                    || m_enumTipoJogo == TipoDosJogadores.ComputadorXComputador
                    || m_enumTipoJogo == TipoDosJogadores.ComputadorXFComputador)
                {
                    m_enmEstado = EstadoDoJogo.ComputadorJogando;
                    m_IsComputadorUM = false;
                }
            }

            //Inicializa o tabuleiro
            for (int i = 1; i < 26; i++)
            {
                /*
                 * Preenche de 1 a 12 com pecas do jogador NOT(Embaixo).
                 * 14 a 25 com pecas do jogador Embaixo.
                 * A casa 13 inicia vazia.
                 */
                if (jogadorConfig.IsEmbaixo)
                {
                    if (i < 13)
                        m_TabuleiroAtual[i] = CorJogadorAdversario(jogadorConfig.CorDoJogador);
                    else
                        m_TabuleiroAtual[i] = jogadorConfig.CorDoJogador;
                }
                else
                {
                    if (i < 13)
                        m_TabuleiroAtual[i] = jogadorConfig.CorDoJogador;
                    else
                        m_TabuleiroAtual[i] = CorJogadorAdversario(jogadorConfig.CorDoJogador);
                }

                m_TabuleiroProximosMovimentos[i] = VAZIA;

            }
            m_TabuleiroAtual[13] = VAZIA;
        }


        /// <summary>
        /// Este método faz o chaveamento entre as diferentes IA do computador: imperativa e funcional.
        /// Se o tipo de jogadores for ComputadorXComputador, simplesmente chama o método tradicional para jogar.
        /// Se o tipo de jogadores for ComputadorXFComputador, chama o método tradicional para o jogador um
        /// e os métodos funcionais para o jogador dois (pois o funcional será sempre o jogador dois).
        /// </summary>
        /// <param name="_ProfundidadeTotal"></param>
        /// <returns></returns>
        public bool JogadaDoComputador(int _ProfundidadeTotal)
        {
            for (int i = 0; i < m_TabuleiroAtual.Length; i++)
            {
                if (m_TabuleiroAtual[i] == PECA_COMIDA)
                    m_TabuleiroAtual[i] = VAZIA;
            }

            switch (m_enumTipoJogo)
            {
                //Apenas IA imperativas, joga totalmente imperativo
                case TipoDosJogadores.ComputadorXComputador:
                    return JogadaDoComputadorImperativo(_ProfundidadeTotal);
                //Apenas UMA IA, a qual deve ser funcional
                case TipoDosJogadores.HumanoXFComputador:
                    bool result = JogadaDoComputadorFuncional(_ProfundidadeTotal);
                    m_enmEstado = EstadoDoJogo.JogadorUm;
                    return result;
                //IA Imperativa X IA Funcional                                    
                case TipoDosJogadores.ComputadorXFComputador:
                    if (m_IsComputadorUM)
                        return JogadaDoComputadorImperativo(_ProfundidadeTotal);
                    else
                        return JogadaDoComputadorFuncional(_ProfundidadeTotal);
                default:
                    break;
            }
            return false;
        }

        private bool JogadaDoComputadorFuncional(int _ProfundidadeTotal)
        {
            Types.Piece JogadorFPieceColor;
            if (m_CJogadorDois.CorDoJogador == 0) JogadorFPieceColor = Types.Piece.BLACK;
            else JogadorFPieceColor = Types.Piece.WHITE;

            FCore.Fminmax minimaxIA = new FCore.Fminmax(JogadorFPieceColor, _ProfundidadeTotal);

            m_TabuleiroAtual = minimaxIA.ReturnBestMove(this.TabuleiroAtual);

            m_IsComputadorUM = true;

            return true;
        }

        private bool JogadaDoComputadorImperativo(int _ProfundidadeTotal)
        {
            if (m_enumTipoJogo == TipoDosJogadores.HumanoXComputador)
            {
                m_TabuleiroAtual = m_CJogadorDois.RealizaJogada(this.TabuleiroAtual, m_CJogadorDois, m_HJogadorUm, _ProfundidadeTotal);

                if (m_CJogadorDois.ComeuPeca)
                {
                    m_HJogadorUm.PecasRestantes--;
                    this.PecasRestantesDosJogadores[0] = m_HJogadorUm.PecasRestantes;
                    this.PecasRestantesDosJogadores[1] = m_CJogadorDois.PecasRestantes;

                    while (m_CJogadorDois.ComeMaisUmPeca(m_TabuleiroAtual, m_CJogadorDois.CorDoJogador,
                        m_CJogadorDois.PecasRestantes, m_HJogadorUm.PecasRestantes))
                    {
                        m_HJogadorUm.PecasRestantes--;
                        this.PecasRestantesDosJogadores[0] = m_HJogadorUm.PecasRestantes;
                        this.PecasRestantesDosJogadores[1] = m_CJogadorDois.PecasRestantes;
                        m_CJogadorDois.ProximaJogada.CopyTo(m_TabuleiroAtual, 0);
                    }
                    m_CJogadorDois.ProximaJogada.CopyTo(m_TabuleiroAtual, 0);
                }
                m_enmEstado = EstadoDoJogo.JogadorUm;

                Fim();
                return true;

            }
            else if (m_enumTipoJogo == TipoDosJogadores.ComputadorXHumano)
            {
                m_TabuleiroAtual = m_CJogadorUm.RealizaJogada(this.TabuleiroAtual, m_CJogadorUm, m_HJogadorDois, _ProfundidadeTotal);


                if (m_CJogadorUm.ComeuPeca)
                {
                    m_HJogadorDois.PecasRestantes--;
                    this.PecasRestantesDosJogadores[0] = m_CJogadorUm.PecasRestantes;
                    this.PecasRestantesDosJogadores[1] = m_HJogadorDois.PecasRestantes;

                    while (m_CJogadorUm.ComeMaisUmPeca(m_TabuleiroAtual, m_CJogadorUm.CorDoJogador,
                        m_CJogadorUm.PecasRestantes, m_HJogadorDois.PecasRestantes))
                    {
                        m_HJogadorDois.PecasRestantes--;
                        this.PecasRestantesDosJogadores[0] = m_CJogadorUm.PecasRestantes;
                        this.PecasRestantesDosJogadores[1] = m_HJogadorDois.PecasRestantes;
                        m_CJogadorUm.ProximaJogada.CopyTo(m_TabuleiroAtual, 0);
                    }
                    m_CJogadorUm.ProximaJogada.CopyTo(m_TabuleiroAtual, 0);
                }
                m_enmEstado = EstadoDoJogo.JogadorDois;

                Fim();
                return true;

            }
            else if (m_enumTipoJogo == TipoDosJogadores.ComputadorXComputador)
            {
                if (m_IsComputadorUM)
                {
                    m_TabuleiroAtual = m_CJogadorUm.RealizaJogada(this.TabuleiroAtual, m_CJogadorUm, m_CJogadorDois, _ProfundidadeTotal);
                    m_IsComputadorUM = false;

                    Fim();
                    return true;
                }
                else //PC Dois jogando
                {
                    m_TabuleiroAtual = m_CJogadorDois.RealizaJogada(this.TabuleiroAtual, m_CJogadorDois, m_CJogadorUm, _ProfundidadeTotal);
                    m_IsComputadorUM = true;

                    Fim();
                    return true;
                }

            }
            else if (m_enumTipoJogo == TipoDosJogadores.ComputadorXFComputador)
            //Os testes de adversários estao separados por questoes de organizacao, pois quando for
            // computador x Fcomputador NAO HA JOGADA AQUI DO COMPUTADOR DOIS
            {
                if (m_IsComputadorUM)
                {
                    m_TabuleiroAtual = m_CJogadorUm.RealizaJogada(this.TabuleiroAtual, m_CJogadorUm, m_CJogadorDois, _ProfundidadeTotal);
                    m_IsComputadorUM = false;

                    Fim();
                    return true;
                }
                else
                {
                    throw new Exception();
                }
            }
            return false;
        }

        /// <summary>
        /// Primeira jogada de um ser humano, gera os movimentos possiveis para a peca que ele clicou
        /// </summary>
        /// <param name="_casaOrigem">Casa clicada com o botão esquerdo.</param>
        /// <returns>
        /// True: se gera moviementos válidos a partir dessa casa (atribui um novo (possivel) novo
        /// estado de tabuleiro a ser desenha na propriedade TabuleiroProximasJogadas
        /// False: se não é possivel mover aquela peca</returns>
        public bool CliqueEsquerdo(int _casaOrigem)
        {
            this.PecaFoiComida = false;

            for (int i = 0; i < m_TabuleiroAtual.Length; i++)
            {
                if (m_TabuleiroAtual[i] == PECA_COMIDA)
                    m_TabuleiroAtual[i] = VAZIA;
            }

            if (m_enmEstado != EstadoDoJogo.ComputadorJogando)
            {
                if (m_enmEstado == EstadoDoJogo.JogadorUm &&
                    m_TabuleiroAtual[_casaOrigem] == m_HJogadorUm.CorDoJogador)
                {
                    m_HJogadorUm.GeraMovimentos(this.TabuleiroAtual, m_HJogadorUm.CorDoJogador);
                    m_TabuleiroProximosMovimentos =
                         m_HJogadorUm.CalculaProximasJogadas(_casaOrigem, this.TabuleiroAtual);

                    Fim();
                    return true;
                }
                else if (m_enmEstado == EstadoDoJogo.JogadorDois &&
                    m_TabuleiroAtual[_casaOrigem] == m_HJogadorDois.CorDoJogador)
                {
                    m_HJogadorDois.GeraMovimentos(this.TabuleiroAtual, m_HJogadorDois.CorDoJogador);
                    m_TabuleiroProximosMovimentos =
                        m_HJogadorDois.CalculaProximasJogadas(_casaOrigem, this.TabuleiroAtual);

                    Fim();
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// Move efetivamente um peca, indo para a casa clicada com o botão direito (destino).
        /// </summary>
        /// <param name="_casaDestino">Casa para onde a peca devera se mover</param>
        /// <returns>
        /// True: se o movimento for valido e ocorrer (atribui um novo estado de tabuleiro efetivo
        /// a propriedade TabuleiroAtual)
        /// False: se ela nao puder se mover naquela direcao</returns>
        public bool CliqueDireito(int _casaDestino)
        {
            if (m_enmEstado != EstadoDoJogo.ComputadorJogando)
            {
                m_PodeComerSequencia = false;

                if (m_enmEstado == EstadoDoJogo.JogadorUm)
                {
                    if (m_HJogadorUm.MovimentaPeca(_casaDestino, this.TabuleiroAtual))
                    {
                        m_HJogadorUm.NovoTabuleiroAtual.CopyTo(m_TabuleiroAtual, 0);

                        //Passa para o proximo jogador
                        if (m_enumTipoJogo == TipoDosJogadores.HumanoXComputador
                         || m_enumTipoJogo == TipoDosJogadores.ComputadorXHumano
                         || m_enumTipoJogo == TipoDosJogadores.HumanoXFComputador)
                            m_enmEstado = EstadoDoJogo.ComputadorJogando;
                        else
                            m_enmEstado = EstadoDoJogo.JogadorDois;

                        //Atualiza as pecas restantes
                        if (m_HJogadorUm.ComeuPeca)
                        {
                            this.PecaFoiComida = true;
                            if (m_enumTipoJogo == TipoDosJogadores.HumanoXHumano)
                            {
                                m_HJogadorDois.PecasRestantes--;
                                this.PecasRestantesDosJogadores[0] = m_HJogadorUm.PecasRestantes;
                                this.PecasRestantesDosJogadores[1] = m_HJogadorDois.PecasRestantes;
                            }
                            else if (m_enumTipoJogo == TipoDosJogadores.HumanoXComputador)
                            {
                                m_CJogadorDois.PecasRestantes--;
                                this.PecasRestantesDosJogadores[0] = m_HJogadorUm.PecasRestantes;
                                this.PecasRestantesDosJogadores[1] = m_CJogadorDois.PecasRestantes;
                            }

                            if (m_HJogadorUm.ComeMaisUmPeca(this.TabuleiroAtual, m_HJogadorUm.CorDoJogador, _casaDestino))
                            {
                                m_TabuleiroProximosMovimentos =
                                    m_HJogadorUm.CalculaProximasJogadas(_casaDestino, this.TabuleiroAtual);
                                m_PodeComerSequencia = true;
                                m_enmEstado = EstadoDoJogo.JogadorUm;
                            }
                        }

                        Fim();

                        return true;
                    }
                }
                else if (m_enmEstado == EstadoDoJogo.JogadorDois)
                {
                    if (m_HJogadorDois.MovimentaPeca(_casaDestino, this.TabuleiroAtual))
                    {
                        m_HJogadorDois.NovoTabuleiroAtual.CopyTo(m_TabuleiroAtual, 0);

                        //Passa para o proximo jogador
                        if (m_enumTipoJogo == TipoDosJogadores.HumanoXComputador
                         || m_enumTipoJogo == TipoDosJogadores.ComputadorXHumano
                         || m_enumTipoJogo == TipoDosJogadores.HumanoXFComputador)
                            m_enmEstado = EstadoDoJogo.ComputadorJogando;
                        else
                            m_enmEstado = EstadoDoJogo.JogadorUm;

                        //Atualiza as pecas restantes
                        if (m_HJogadorDois.ComeuPeca)
                        {
                            this.PecaFoiComida = true;
                            if (m_enumTipoJogo == TipoDosJogadores.HumanoXHumano)
                            {
                                m_HJogadorUm.PecasRestantes--;
                                this.PecasRestantesDosJogadores[0] = m_HJogadorUm.PecasRestantes;
                                this.PecasRestantesDosJogadores[1] = m_HJogadorDois.PecasRestantes;
                            }
                            else if (m_enumTipoJogo == TipoDosJogadores.ComputadorXHumano)
                            {
                                m_CJogadorUm.PecasRestantes--;
                                this.PecasRestantesDosJogadores[0] = m_CJogadorUm.PecasRestantes;
                                this.PecasRestantesDosJogadores[1] = m_HJogadorDois.PecasRestantes;
                            }

                            if (m_HJogadorDois.ComeMaisUmPeca(this.TabuleiroAtual, m_HJogadorDois.CorDoJogador, _casaDestino))
                            {
                                m_TabuleiroProximosMovimentos =
                                    m_HJogadorDois.CalculaProximasJogadas(_casaDestino, this.TabuleiroAtual);

                                m_PodeComerSequencia = true;
                                m_enmEstado = EstadoDoJogo.JogadorDois;
                            }

                        }
                        Fim();
                        return true;
                    }
                }
            }
            return false;
        }

        public void Fim()
        {
            switch (m_enumTipoJogo)
            {
                case TipoDosJogadores.ComputadorXHumano:
                    if (m_CJogadorUm.PecasRestantes == 0)
                    {
                        IsAdversarioVencedor = true;
                        m_enmEstado = EstadoDoJogo.Fim;
                    }
                    else if (m_HJogadorDois.PecasRestantes == 0)
                    {
                        IsAdversarioVencedor = false;
                        m_enmEstado = EstadoDoJogo.Fim;
                    }
                    break;
                case TipoDosJogadores.HumanoXComputador:
                    if (m_HJogadorUm.PecasRestantes == 0)
                    {
                        IsAdversarioVencedor = false;
                        m_enmEstado = EstadoDoJogo.Fim;
                    }
                    else if (m_CJogadorDois.PecasRestantes == 0)
                    {
                        IsAdversarioVencedor = true;
                        m_enmEstado = EstadoDoJogo.Fim;
                    }
                    break;
                case TipoDosJogadores.HumanoXFComputador:
                    if (m_HJogadorUm.PecasRestantes == 0)
                    {
                        IsAdversarioVencedor = false;
                        m_enmEstado = EstadoDoJogo.Fim;
                    }
                    else if (m_CJogadorDois.PecasRestantes == 0)
                    {
                        IsAdversarioVencedor = true;
                        m_enmEstado = EstadoDoJogo.Fim;
                    }
                    break;
                case TipoDosJogadores.HumanoXHumano:
                    if (m_HJogadorUm.PecasRestantes == 0)
                    {
                        m_enmEstado = EstadoDoJogo.Fim;
                    }
                    else if (m_HJogadorDois.PecasRestantes == 0)
                    {
                        m_enmEstado = EstadoDoJogo.Fim;
                    }
                    break;
                case TipoDosJogadores.ComputadorXComputador:
                    if (m_CJogadorUm.PecasRestantes == 0)
                    {
                        m_enmEstado = EstadoDoJogo.Fim;
                    }
                    else if (m_CJogadorDois.PecasRestantes == 0)
                    {
                        m_enmEstado = EstadoDoJogo.Fim;
                    }
                    break;
                case TipoDosJogadores.ComputadorXFComputador:
                    if (m_CJogadorUm.PecasRestantes == 0)
                    {
                        m_enmEstado = EstadoDoJogo.Fim;
                    }
                    else if (m_CJogadorDois.PecasRestantes == 0)
                    {
                        m_enmEstado = EstadoDoJogo.Fim;
                    }
                    break;
                default:
                    break;
            }
            if (m_enumTipoJogo == TipoDosJogadores.HumanoXHumano)
            {

            }
        }

        private void CopiaAtributosJogadores(Jogador _UM, Jogador _DOIS)
        {
            switch (m_enumTipoJogo)
            {
                case TipoDosJogadores.ComputadorXHumano:
                    m_CJogadorUm.CorDoJogador = _UM.CorDoJogador;
                    m_CJogadorUm.IsEmbaixo = _UM.IsEmbaixo;
                    m_CJogadorUm.IsHumano = _UM.IsHumano;
                    m_CJogadorUm.IsPlaying = _UM.IsPlaying;
                    m_CJogadorUm.IsPrimeiroJogador = _UM.IsPrimeiroJogador;
                    m_CJogadorUm.IsVencedor = _UM.IsVencedor;
                    m_CJogadorUm.PecasRestantes = _UM.PecasRestantes;

                    m_HJogadorDois.CorDoJogador = _DOIS.CorDoJogador;
                    m_HJogadorDois.IsEmbaixo = _DOIS.IsEmbaixo;
                    m_HJogadorDois.IsHumano = _DOIS.IsHumano;
                    m_HJogadorDois.IsPlaying = _DOIS.IsPlaying;
                    m_HJogadorDois.IsPrimeiroJogador = _DOIS.IsPrimeiroJogador;
                    m_HJogadorDois.IsVencedor = _DOIS.IsVencedor;
                    m_HJogadorDois.PecasRestantes = _DOIS.PecasRestantes;

                    break;
                case TipoDosJogadores.HumanoXFComputador:
                    m_HJogadorUm.CorDoJogador = _UM.CorDoJogador;
                    m_HJogadorUm.IsEmbaixo = _UM.IsEmbaixo;
                    m_HJogadorUm.IsHumano = _UM.IsHumano;
                    m_HJogadorUm.IsPlaying = _UM.IsPlaying;
                    m_HJogadorUm.IsPrimeiroJogador = _UM.IsPrimeiroJogador;
                    m_HJogadorUm.IsVencedor = _UM.IsVencedor;
                    m_HJogadorUm.PecasRestantes = _UM.PecasRestantes;

                    m_CJogadorDois.CorDoJogador = _DOIS.CorDoJogador;
                    m_CJogadorDois.IsEmbaixo = _DOIS.IsEmbaixo;
                    m_CJogadorDois.IsHumano = _DOIS.IsHumano;
                    m_CJogadorDois.IsPlaying = _DOIS.IsPlaying;
                    m_CJogadorDois.IsPrimeiroJogador = _DOIS.IsPrimeiroJogador;
                    m_CJogadorDois.IsVencedor = _DOIS.IsVencedor;
                    m_CJogadorDois.IsFuncional = _DOIS.IsFuncional;
                    m_CJogadorDois.PecasRestantes = _DOIS.PecasRestantes;
                    break;
                case TipoDosJogadores.HumanoXComputador:
                    m_HJogadorUm.CorDoJogador = _UM.CorDoJogador;
                    m_HJogadorUm.IsEmbaixo = _UM.IsEmbaixo;
                    m_HJogadorUm.IsHumano = _UM.IsHumano;
                    m_HJogadorUm.IsPlaying = _UM.IsPlaying;
                    m_HJogadorUm.IsPrimeiroJogador = _UM.IsPrimeiroJogador;
                    m_HJogadorUm.IsVencedor = _UM.IsVencedor;
                    m_HJogadorUm.PecasRestantes = _UM.PecasRestantes;

                    m_CJogadorDois.CorDoJogador = _DOIS.CorDoJogador;
                    m_CJogadorDois.IsEmbaixo = _DOIS.IsEmbaixo;
                    m_CJogadorDois.IsHumano = _DOIS.IsHumano;
                    m_CJogadorDois.IsPlaying = _DOIS.IsPlaying;
                    m_CJogadorDois.IsPrimeiroJogador = _DOIS.IsPrimeiroJogador;
                    m_CJogadorDois.IsVencedor = _DOIS.IsVencedor;
                    m_CJogadorDois.PecasRestantes = _DOIS.PecasRestantes;

                    break;
                case TipoDosJogadores.HumanoXHumano:
                    m_HJogadorUm.CorDoJogador = _UM.CorDoJogador;
                    m_HJogadorUm.IsEmbaixo = _UM.IsEmbaixo;
                    m_HJogadorUm.IsHumano = _UM.IsHumano;
                    m_HJogadorUm.IsPlaying = _UM.IsPlaying;
                    m_HJogadorUm.IsPrimeiroJogador = _UM.IsPrimeiroJogador;
                    m_HJogadorUm.IsVencedor = _UM.IsVencedor;
                    m_HJogadorUm.PecasRestantes = _UM.PecasRestantes;

                    m_HJogadorDois.CorDoJogador = _DOIS.CorDoJogador;
                    m_HJogadorDois.IsEmbaixo = _DOIS.IsEmbaixo;
                    m_HJogadorDois.IsHumano = _DOIS.IsHumano;
                    m_HJogadorDois.IsPlaying = _DOIS.IsPlaying;
                    m_HJogadorDois.IsPrimeiroJogador = _DOIS.IsPrimeiroJogador;
                    m_HJogadorDois.IsVencedor = _DOIS.IsVencedor;
                    m_HJogadorDois.PecasRestantes = _DOIS.PecasRestantes;
                    break;
                case TipoDosJogadores.ComputadorXComputador:
                    m_CJogadorUm.CorDoJogador = _UM.CorDoJogador;
                    m_CJogadorUm.IsEmbaixo = _UM.IsEmbaixo;
                    m_CJogadorUm.IsHumano = _UM.IsHumano;
                    m_CJogadorUm.IsPlaying = _UM.IsPlaying;
                    m_CJogadorUm.IsPrimeiroJogador = _UM.IsPrimeiroJogador;
                    m_CJogadorUm.IsVencedor = _UM.IsVencedor;
                    m_CJogadorUm.PecasRestantes = _UM.PecasRestantes;

                    m_CJogadorDois.CorDoJogador = _DOIS.CorDoJogador;
                    m_CJogadorDois.IsEmbaixo = _DOIS.IsEmbaixo;
                    m_CJogadorDois.IsHumano = _DOIS.IsHumano;
                    m_CJogadorDois.IsPlaying = _DOIS.IsPlaying;
                    m_CJogadorDois.IsPrimeiroJogador = _DOIS.IsPrimeiroJogador;
                    m_CJogadorDois.IsVencedor = _DOIS.IsVencedor;
                    m_CJogadorDois.PecasRestantes = _DOIS.PecasRestantes;
                    break;
                case TipoDosJogadores.ComputadorXFComputador:
                    m_CJogadorUm.CorDoJogador = _UM.CorDoJogador;
                    m_CJogadorUm.IsEmbaixo = _UM.IsEmbaixo;
                    m_CJogadorUm.IsHumano = _UM.IsHumano;
                    m_CJogadorUm.IsPlaying = _UM.IsPlaying;
                    m_CJogadorUm.IsPrimeiroJogador = _UM.IsPrimeiroJogador;
                    m_CJogadorUm.IsVencedor = _UM.IsVencedor;
                    m_CJogadorUm.PecasRestantes = _UM.PecasRestantes;

                    m_CJogadorDois.CorDoJogador = _DOIS.CorDoJogador;
                    m_CJogadorDois.IsEmbaixo = _DOIS.IsEmbaixo;
                    m_CJogadorDois.IsHumano = _DOIS.IsHumano;
                    m_CJogadorDois.IsPlaying = _DOIS.IsPlaying;
                    m_CJogadorDois.IsPrimeiroJogador = _DOIS.IsPrimeiroJogador;
                    m_CJogadorDois.IsVencedor = _DOIS.IsVencedor;
                    m_CJogadorDois.IsFuncional = _DOIS.IsFuncional;
                    m_CJogadorDois.PecasRestantes = _DOIS.PecasRestantes;
                    break;
                default:
                    break;
            }
        }
    }
}
[Serializable]
public enum EstadoDoJogo
{
    [Description("Jogador Um")]
    JogadorUm,
    [Description("Jogador Dois")]
    JogadorDois,
    [Description("Fim de jogo")]
    Fim,
    [Description("Computador")]
    ComputadorJogando
};