using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Logica;
using System.Media;
using System.Threading;
using Estatistica;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace Eukreukla
{
    public partial class FrmPrincipal : Form
    {
        #region Variaveis
        private const int BRANCAS = 1;
        private const int PRETAS = 0;
        private const int VAZIA = -1;
        private const int PROXIMA_JOGADA = 2;
        protected const int PECA_COMIDA = 3;

        SoundPlayer move = new SoundPlayer(Eukreukla.Resource.move);
        SoundPlayer drop = new SoundPlayer(Eukreukla.Resource.drop);
        SoundPlayer captue = new SoundPlayer(Eukreukla.Resource.capture);

        Jogador m_udtJogadorUm;
        Jogador m_udtJogadorDois;

        InfoPartida coletaDados;
        Dados gravaJogo;
        Graphics canvas;
        Jogo novoJogo;
        Dictionary<int, Point> m_dicCoordenadas;
        private TipoDosJogadores m_enumTipoJogadores;

        DateTime t_inicio;
        DateTime t_fim;
        TimeSpan t_diferenca;
        private bool m_blnIsTabuleiroAtual = true;
        private bool m_PodeMover = false;
        private int m_casaOrigem = -1;
        private int m_NumeroDeMovimentos;
        private int m_NumeroDePecasComidas;
        private int m_Dificuldade = 3;

        #endregion

        /// <summary>
        /// Construtor
        /// </summary>
        public FrmPrincipal()
        {
            InitializeComponent();

            m_dicCoordenadas = CriaCoordenadas();
            canvas = pnlTabuleiro.CreateGraphics();
            LimpaTabuleiro();
            menuFacil.Checked = true;
        }

        #region Eventos
        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void menuSalvarJogo_Click(object sender, EventArgs e)
        {
            Stream stream = null;
            const int VERSION = 1;
            gravaJogo = new Dados();
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "MeuEukreuklaSave";
            saveFileDialog.Filter = "Eukreukla files |*.euk|All files|*.*";
            saveFileDialog.InitialDirectory =
                Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);

            try
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    gravaJogo.TipoDosJogadores = m_enumTipoJogadores;
                    gravaJogo.JogadorUM = m_udtJogadorUm;
                    gravaJogo.JogadorDOIS = m_udtJogadorDois;
                    gravaJogo.Jogo = novoJogo;
                    gravaJogo.TempoDecorrido = t_diferenca;

                    IFormatter formatter = new BinaryFormatter();
                    stream = new FileStream(saveFileDialog.FileName, FileMode.Create,
                        FileAccess.Write, FileShare.None);
                    formatter.Serialize(stream, VERSION);
                    formatter.Serialize(stream, gravaJogo);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (null != stream)
                    stream.Close();

                saveFileDialog.Dispose();
                saveFileDialog = null;
                gravaJogo = null;
            }
        }

        private void menuCarregarJogo_Click(object sender, EventArgs e)
        {
            Stream stream = null;
            Dados carregaJogo = null;
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Eukreukla files |*.euk";

            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    IFormatter formatter = new BinaryFormatter();
                    stream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.None);
                    int version = (int)formatter.Deserialize(stream);
                    carregaJogo = (Dados)formatter.Deserialize(stream);

                    m_enumTipoJogadores = carregaJogo.TipoDosJogadores;
                    novoJogo = carregaJogo.Jogo;
                    m_udtJogadorUm = carregaJogo.JogadorUM;
                    m_udtJogadorDois = carregaJogo.JogadorDOIS;
                    t_diferenca = carregaJogo.TempoDecorrido;

                    IniciarJogo(true);
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (null != stream)
                    stream.Close();
                openFileDialog.Dispose();
                openFileDialog = null;
                carregaJogo = null;
            }
        }

        private void menuColetaDados_Click(object sender, EventArgs e)
        {
            Stream stream = null;
            const int VERSION = 1;


            saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = string.Empty;
            saveFileDialog.Filter = "Eukreukla Data files |*.edt|All files|*.*";
            saveFileDialog.InitialDirectory =
                Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);

            try
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    IFormatter formatter = new BinaryFormatter();
                    stream = new FileStream(saveFileDialog.FileName, FileMode.Create,
                        FileAccess.Write, FileShare.None);
                    formatter.Serialize(stream, VERSION);
                    formatter.Serialize(stream, coletaDados);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (null != stream)
                    stream.Close();

                saveFileDialog.Dispose();
                saveFileDialog = null;
            }
        }

        private void pnlTabuleiro_Paint(object sender, PaintEventArgs e)
        {
            DesenhaTabuleiro();

            if (novoJogo != null)
            {
                if (m_blnIsTabuleiroAtual)
                    DesenhaPecas(novoJogo.TabuleiroAtual);
                else
                    DesenhaPecas(novoJogo.TabuleiroProximosMovimentos);
            }
        }

        private void configuraçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuracao();
        }

        private void btnIniciarJogo_Click(object sender, EventArgs e)
        {
            IniciarJogo(false);
        }

        private void btnNovoJogo_Click(object sender, EventArgs e)
        {
            NovoJogo();
        }

        private void pnlTabuleiro_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                lblX.Text = e.X.ToString();
                lblY.Text = e.Y.ToString();
                int casa = ConverteCoordenadaEmCasa(e.Location);
                lblCasa.Text = casa.ToString();

                if (casa != VAZIA)
                {
                    if (!novoJogo.PodeComerSequencia)
                        MostraMovimentosPossiveis(casa);

                    if (novoJogo.TabuleiroAtual[casa] == VAZIA && m_PodeMover)
                        MovimentaPeca(casa);
                }
            }
        }
        private void Tempo_Tick(object sender, EventArgs e)
        {
            t_fim = DateTime.Now;
            t_diferenca = t_fim.Subtract(t_inicio);

            lblTempo.Text =
                t_diferenca.Hours.ToString("00") + ":" +
                t_diferenca.Minutes.ToString("00") + ":" +
                t_diferenca.Seconds.ToString("00");
        }

        private void menuDificuldade_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;

            menuFacil.Checked = false;
            menuFacilMedio.Checked = false;
            menuMedio.Checked = false;
            menuMedioDificil.Checked = false;
            menuDificil.Checked = false;

            menu.Checked = true;
            m_Dificuldade = Convert.ToInt32(menu.Tag);

        }

        private void menuSair_Click(object sender, EventArgs e)
        {
            Tempo.Enabled = false;

            if (novoJogo == null)
            {
                this.Close();
            }
            else
            {
                if (MessageBox.Show("Deseja cancelar o jogo atual?", "Atenção", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    this.Close();
                }
                else
                {
                    Tempo.Enabled = true;
                }
            }
        }
        #endregion

        #region Métodos de Controle
        /// <summary>
        /// Gera os movimentos possiveis no tabuleiro atual
        /// </summary>
        /// <param name="_casaJogada"></param>
        /// <returns></returns>
        private bool MostraMovimentosPossiveis(int _casaJogada)
        {

            if (novoJogo.CliqueEsquerdo(_casaJogada))
            {
                if (ckbSom.Checked)
                    move.Play();
                LimpaTabuleiro();
                DesenhaPecas(novoJogo.TabuleiroProximosMovimentos);
                m_blnIsTabuleiroAtual = false;
                m_casaOrigem = _casaJogada;
                m_PodeMover = true;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Realiza o deslocamento da peca
        /// </summary>
        /// <param name="_casaJogada"></param>
        private void MovimentaPeca(int _casaJogada)
        {
            if (m_casaOrigem != _casaJogada)
            {
                if (novoJogo.CliqueDireito(_casaJogada))
                {
                    m_NumeroDeMovimentos++;
                    m_casaOrigem = _casaJogada;
                    LimpaTabuleiro();

                    if (!novoJogo.PodeComerSequencia)
                    {
                        m_blnIsTabuleiroAtual = true;

                        DesenhaPecas(novoJogo.TabuleiroAtual);
                        m_PodeMover = false;
                    }
                    else
                    {
                        m_blnIsTabuleiroAtual = false;
                        DesenhaPecas(novoJogo.TabuleiroProximosMovimentos);
                        m_PodeMover = true;
                    }

                    if (novoJogo.PecaFoiComida)
                    {
                        if (ckbSom.Checked)
                            captue.PlaySync();
                    }
                    else if (ckbSom.Checked)
                        move.Play();

                    AtualizaMenuInferior();

                    if (novoJogo != null && novoJogo.VezDoJogador == EstadoDoJogo.ComputadorJogando)
                        ChamaPC();
                }
            }
        }

        private void IniciarJogo(bool _IsJogoCarregado)
        {
            if (m_udtJogadorUm == null || m_udtJogadorDois == null)
            {
                MessageBox.Show("Configure os jogadores no menu 'Configurações'.", "Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                if (!_IsJogoCarregado)
                {
                    novoJogo = new Jogo(m_udtJogadorUm, m_udtJogadorDois, m_enumTipoJogadores);
                    coletaDados = new InfoPartida();
                    coletaDados.TipoDosJogadores = TipoDosJogadores.ComputadorXComputador;
                }

                btnIniciarJogo.Enabled = false;
                menuDificuldade.Enabled = false;

                DesenhaTabuleiro();
                DesenhaPecas(novoJogo.TabuleiroAtual);
                AtualizaMenuInferior();

                t_inicio = DateTime.Now;
                Tempo.Start();

                if (m_enumTipoJogadores == TipoDosJogadores.ComputadorXHumano)
                    ChamaPC();
                else if (m_enumTipoJogadores == TipoDosJogadores.ComputadorXComputador)
                    ChamaDoisPCs();
            }
        }

        /// <summary>
        /// Método que inicia um novo jogo, sendo isso abrir a janela de configuracao e perguntar
        /// se o jogador quer cancelar a partida atual, se estiver ocorrendo
        /// </summary>
        private void NovoJogo()
        {
            Tempo.Enabled = false;

            if (novoJogo == null)
            {
                btnIniciarJogo.Enabled = true;
                menuDificuldade.Enabled = true;

                Configuracao();
            }
            else
            {
                if (MessageBox.Show("Deseja cancelar o jogo atual?", "Atenção", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    DesenhaTabuleiro();

                    btnIniciarJogo.Enabled = true;
                    menuDificuldade.Enabled = true;

                    Configuracao();
                }
                else
                {
                    Tempo.Enabled = true;
                }
            }
        }
        /// <summary>
        /// Chama a IA, para jogos do tipo humano contra computador
        /// </summary>
        private void ChamaPC()
        {

            if (novoJogo.JogadaDoComputador(m_Dificuldade))
            {
                if (ckbSom.Checked)
                    move.Play();
                for (int i = 0; i < 100000000; i++)
                {
                }
                m_NumeroDeMovimentos++;
                LimpaTabuleiro();
                m_blnIsTabuleiroAtual = true;
                DesenhaPecas(novoJogo.TabuleiroAtual);
                if (ckbSom.Checked)
                    drop.Play();
                AtualizaMenuInferior();
            }

        }

        /// <summary>
        /// IA jogando contra ela mesma
        /// </summary>
        private void ChamaDoisPCs()
        {
            while (novoJogo != null)
            {
                if (novoJogo != null)
                {
                    if (novoJogo.JogadaDoComputador(m_Dificuldade))
                    {
                        if (ckbSom.Checked)
                            move.Play();
                        for (int i = 0; i < 100000000; i++)
                        {
                        }
                        m_NumeroDeMovimentos++;
                        LimpaTabuleiro();
                        m_blnIsTabuleiroAtual = true;
                        DesenhaPecas(novoJogo.TabuleiroAtual);
                        if (ckbSom.Checked)
                            drop.Play();
                        AtualizaMenuInferior();
                    }
                }

            }
        }

        /// <summary>
        /// Abre form de configuracao e recebe jogadores e tipo de jogo.
        /// </summary>
        private void Configuracao()
        {
            FrmConfig frmConfig = new FrmConfig();

            if (frmConfig.ShowDialog() == DialogResult.OK)
            {
                m_udtJogadorUm = frmConfig.JogadorUM;
                m_udtJogadorDois = frmConfig.JogadorDOIS;
                m_enumTipoJogadores = frmConfig.TipoDosJogadores;
            }
        }
        #endregion

        #region Métodos de Desenho

        /// <summary>
        /// Desenha a figura do tabuleiro. Chamar no evento de Load do form.
        /// </summary>
        public void DesenhaTabuleiro()
        {
            //this.pnlTabuleiro.BackgroundImage = (System.Drawing.Image)Eukreukla.Resource.board;
            //this.pnlTabuleiro.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            canvas.Clear(Color.Honeydew);

            Pen linhaTabuleiro = new Pen(Color.DarkOrange, 4);

            //canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Point[] quadradoExterno = new Point[4] 
                {new Point(30,25),
                 new Point(470,25),
                 new Point(470,385),
                 new Point(30,385)};

            Point[] quadradoInterno = new Point[4]
                {new Point(250,25),
                 new Point(30,205),
                 new Point(250,385),
                 new Point(470,205)};

            canvas.DrawPolygon(linhaTabuleiro, quadradoExterno);
            canvas.DrawPolygon(linhaTabuleiro, quadradoInterno);

            //Linhas horizontais
            canvas.DrawLine(linhaTabuleiro, new Point(30, 115), new Point(470, 115));
            canvas.DrawLine(linhaTabuleiro, new Point(30, 205), new Point(470, 205));
            canvas.DrawLine(linhaTabuleiro, new Point(30, 295), new Point(470, 295));
            //Linhas verticais
            canvas.DrawLine(linhaTabuleiro, new Point(140, 25), new Point(140, 385));
            canvas.DrawLine(linhaTabuleiro, new Point(250, 25), new Point(250, 385));
            canvas.DrawLine(linhaTabuleiro, new Point(360, 25), new Point(360, 385));
            //Linhas diagonais
            canvas.DrawLine(linhaTabuleiro, new Point(30, 25), new Point(470, 385));
            canvas.DrawLine(linhaTabuleiro, new Point(30, 385), new Point(470, 25));

        }

        /// <summary>
        /// Desenha na tela a disposicao das pecas num estado atual do tabuleiro.
        /// </summary>
        /// <param name="_Tabuleiro"></param>
        private void DesenhaPecas(int[] _Tabuleiro)
        {
            //Faz coleta de dados
            if (coletaDados != null)
                coletaDados.ListaTabuleirosJogados.Add(novoJogo.TabuleiroAtual);
            Rectangle rectangle;
            Pen pen;
            Brush brush = new LinearGradientBrush(new Point(1, 1), new Point(2, 2), Color.White, Color.White);
            Point pontoXY;
            Size tamanho = new Size(40, 40);

            if (_Tabuleiro != null)
                for (int i = 1; i < _Tabuleiro.Length; i++)
                {
                    switch (_Tabuleiro[i])
                    {
                        case PRETAS:
                            pontoXY = m_dicCoordenadas[i];
                            brush = new LinearGradientBrush(pontoXY, new Point(pontoXY.X + 40, pontoXY.Y + 40), Color.Black, Color.SkyBlue);
                            rectangle = new Rectangle(pontoXY, tamanho);
                            canvas.FillEllipse(brush, rectangle);
                            break;
                        case BRANCAS:
                            pontoXY = m_dicCoordenadas[i];
                            brush = new LinearGradientBrush(pontoXY, new Point(pontoXY.X + 40, pontoXY.Y + 40), Color.Red, Color.Orange);
                            rectangle = new Rectangle(pontoXY, tamanho);
                            canvas.FillEllipse(brush, rectangle);
                            break;
                        case PROXIMA_JOGADA:
                            pen = new Pen(Color.Yellow, 4);
                            rectangle = new Rectangle(m_dicCoordenadas[i], tamanho);
                            canvas.DrawEllipse(pen, rectangle);
                            break;
                        case VAZIA:
                            break;
                        case PECA_COMIDA:
                            pontoXY = m_dicCoordenadas[i];
                            pen = new Pen(Color.Brown, 4);
                            canvas.DrawLine(pen, pontoXY, new Point(pontoXY.X + 40, pontoXY.Y + 40));
                            canvas.DrawLine(pen, new Point(pontoXY.X, pontoXY.Y + 40),
                                                 new Point(pontoXY.X + 40, pontoXY.Y));
                            break;
                        default:
                            //caso de peça a ser comida (consultar documentacao do arquivo Game.cs linha ~41
                            if (_Tabuleiro[i] % 2 == 0)
                            {
                                //Se for par, peca preta
                                pontoXY = m_dicCoordenadas[i];
                                brush = new LinearGradientBrush(pontoXY, new Point(pontoXY.X + 40, pontoXY.Y + 40), Color.Black, Color.SkyBlue);
                                rectangle = new Rectangle(pontoXY, tamanho);
                                canvas.FillEllipse(brush, rectangle);

                                pen = new Pen(Color.Red, 3);
                                canvas.DrawLine(pen, pontoXY, new Point(pontoXY.X + 40, pontoXY.Y + 40));
                                canvas.DrawLine(pen, new Point(pontoXY.X, pontoXY.Y + 40),
                                                     new Point(pontoXY.X + 40, pontoXY.Y));
                            }
                            else
                            {
                                //Se for impar, peca branca
                                pontoXY = m_dicCoordenadas[i];
                                brush = new LinearGradientBrush(pontoXY, new Point(pontoXY.X + 40, pontoXY.Y + 40), Color.Red, Color.Orange);
                                rectangle = new Rectangle(pontoXY, tamanho);
                                canvas.FillEllipse(brush, rectangle);

                                pen = new Pen(Color.Red, 3);
                                canvas.DrawLine(pen, pontoXY, new Point(pontoXY.X + 40, pontoXY.Y + 40));
                                canvas.DrawLine(pen, new Point(pontoXY.X, pontoXY.Y + 40),
                                                     new Point(pontoXY.X + 40, pontoXY.Y));
                            }
                            break;

                    }
                }
        }
        /// <summary>
        /// Limpa o tabuleiro e redesenha ele
        /// </summary>
        private void LimpaTabuleiro()
        {
            canvas.Clear(Color.Plum);
            DesenhaTabuleiro();
        }
        #endregion

        #region Métodos Auxiliares
        /// <summary>
        /// Cria um dicionario de pontos (Points) X e Y para converter o indice do array da casa
        /// em uma coordenada
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, Point> CriaCoordenadas()
        {
            Dictionary<int, Point> dicCoordenadas = new Dictionary<int, Point>();

            for (int i = 0; i < 26; i++)
            {
                switch (i)
                {
                    case 1:
                        dicCoordenadas.Add(i, new Point(10, 10));
                        break;
                    case 2:
                        dicCoordenadas.Add(i, new Point(120, 10));
                        break;
                    case 3:
                        dicCoordenadas.Add(i, new Point(230, 10));
                        break;
                    case 4:
                        dicCoordenadas.Add(i, new Point(340, 10));
                        break;
                    case 5:
                        dicCoordenadas.Add(i, new Point(450, 10));
                        break;
                    case 6:
                        dicCoordenadas.Add(i, new Point(10, 100));
                        break;
                    case 7:
                        dicCoordenadas.Add(i, new Point(120, 100));
                        break;
                    case 8:
                        dicCoordenadas.Add(i, new Point(230, 100));
                        break;
                    case 9:
                        dicCoordenadas.Add(i, new Point(340, 100));
                        break;
                    case 10:
                        dicCoordenadas.Add(i, new Point(450, 100));
                        break;
                    case 11:
                        dicCoordenadas.Add(i, new Point(10, 190));
                        break;
                    case 12:
                        dicCoordenadas.Add(i, new Point(120, 190));
                        break;
                    case 13:
                        dicCoordenadas.Add(i, new Point(230, 190));
                        break;
                    case 14:
                        dicCoordenadas.Add(i, new Point(340, 190));
                        break;
                    case 15:
                        dicCoordenadas.Add(i, new Point(450, 190));
                        break;
                    case 16:
                        dicCoordenadas.Add(i, new Point(10, 280));
                        break;
                    case 17:
                        dicCoordenadas.Add(i, new Point(120, 280));
                        break;
                    case 18:
                        dicCoordenadas.Add(i, new Point(230, 280));
                        break;
                    case 19:
                        dicCoordenadas.Add(i, new Point(340, 280));
                        break;
                    case 20:
                        dicCoordenadas.Add(i, new Point(450, 280));
                        break;
                    case 21:
                        dicCoordenadas.Add(i, new Point(10, 370));
                        break;
                    case 22:
                        dicCoordenadas.Add(i, new Point(120, 370));
                        break;
                    case 23:
                        dicCoordenadas.Add(i, new Point(230, 370));
                        break;
                    case 24:
                        dicCoordenadas.Add(i, new Point(340, 370));
                        break;
                    case 25:
                        dicCoordenadas.Add(i, new Point(450, 370));
                        break;
                    default:
                        break;
                }


            }
            return dicCoordenadas;
        }

        /// <summary>
        /// Converte coordenadas (nao exatas) no indice de uma casa. Avalia intervalos.
        /// </summary>
        /// <param name="coordX">Coordenada X clicada pelo usuario</param>
        /// <param name="coordY">Coordenada y clicada pelo usuario</param>
        /// <returns></returns>
        private int ConverteCoordenadaEmCasa(Point mouseXY)
        {
            int casa = -1;

            for (int indiceCasa = 1; indiceCasa < 26; indiceCasa++)
            {
                Point coord = m_dicCoordenadas[indiceCasa];

                if (mouseXY.X >= coord.X && mouseXY.X <= coord.X + 40)
                    if (mouseXY.Y >= coord.Y && mouseXY.Y <= coord.Y + 40)
                    {
                        casa = indiceCasa;
                        break;
                    }
            }

            return casa;
        }


        /// <summary>
        /// Atualiza dados do jogo.
        /// </summary>
        private void AtualizaMenuInferior()
        {
            if (novoJogo != null)
            {
                if (m_udtJogadorUm.CorDoJogador == BRANCAS)
                {
                    lblPecasRestantesBrancas.Text = novoJogo.PecasRestantesDosJogadores[0].ToString();
                    lblPecasRestantesPretas.Text = novoJogo.PecasRestantesDosJogadores[1].ToString();
                }
                else
                {
                    lblPecasRestantesBrancas.Text = novoJogo.PecasRestantesDosJogadores[1].ToString();
                    lblPecasRestantesPretas.Text = novoJogo.PecasRestantesDosJogadores[0].ToString();
                }

                m_NumeroDePecasComidas = 24 - (novoJogo.PecasRestantesDosJogadores[0] + novoJogo.PecasRestantesDosJogadores[1]);
                lblNumeroMovimentos.Text = m_NumeroDeMovimentos.ToString();
                lblNumeroPecasComidas.Text = m_NumeroDePecasComidas.ToString();

                if (m_NumeroDeMovimentos > 0)
                    lblMediaComidasPorMovimentos.Text = ((double)m_NumeroDePecasComidas / (double)m_NumeroDeMovimentos).ToString("0.00");
                else
                    lblMediaComidasPorMovimentos.Text = "--";

                switch (novoJogo.VezDoJogador)
                {
                    case EstadoDoJogo.JogadorUm:
                        lblEstadoDoJogo.Text = "Vez do jogador UM, peças " +
                    (m_udtJogadorUm.CorDoJogador == BRANCAS ? "vermelhas." : "pretas.");
                        break;

                    case EstadoDoJogo.JogadorDois:
                        lblEstadoDoJogo.Text = "Vez do jogador DOIS, peças " +
                    (m_udtJogadorDois.CorDoJogador == BRANCAS ? "vermelhas." : "pretas.");
                        break;

                    case EstadoDoJogo.Fim:
                        coletaDados.IsAdversarioVencedor = novoJogo.IsAdversarioVencedor;
                        novoJogo = null;
                        lblEstadoDoJogo.Text = "Fim de jogo.";
                        Tempo.Stop();
                        break;

                    case EstadoDoJogo.ComputadorJogando:
                        switch (m_enumTipoJogadores)
                        {
                            case TipoDosJogadores.ComputadorXHumano:
                                lblEstadoDoJogo.Text = "Vez do Computador, peças " +
                                (m_udtJogadorUm.CorDoJogador == BRANCAS ? "vermelhas." : "pretas.");
                                break;

                            case TipoDosJogadores.HumanoXComputador:
                                lblEstadoDoJogo.Text = "Vez do Computador, peças " +
                                (m_udtJogadorDois.CorDoJogador == BRANCAS ? "vermelhas." : "pretas.");
                                break;

                            case TipoDosJogadores.ComputadorXComputador:

                                if (novoJogo.IsComputadorUM)
                                    lblEstadoDoJogo.Text = "Vez do Computador UM, peças " +
                                        (m_udtJogadorDois.CorDoJogador == BRANCAS ? "vermelhas." : "pretas.");
                                else
                                    lblEstadoDoJogo.Text = "Vez do Computador DOIS, peças " +
                                        (m_udtJogadorDois.CorDoJogador == BRANCAS ? "vermelhas." : "pretas.");

                                break;
                            default:
                                break;
                        }

                        break;
                    default:
                        break;
                }

                //Testa impate
                if (m_NumeroDeMovimentos > 70)
                {
                    lblEstadoDoJogo.Text = "Empate por número de movimentos";
                    Tempo.Stop();
                }

            }

        }
        #endregion



        

    }
}

