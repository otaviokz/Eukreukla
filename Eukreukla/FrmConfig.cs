using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Logica;

namespace Eukreukla
{
    public partial class FrmConfig : Form
    {
        public Jogador JogadorUM { get; set; }
        public Jogador JogadorDOIS { get; set; }

        private const int BRANCAS = 1;
        private const int PRETAS = 0;
        private const int VAZIA = -1;

        public TipoDosJogadores TipoDosJogadores { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        public FrmConfig()
        {
            InitializeComponent();
            ckbIsJogadorUmHumano.Checked = true;
            ckbCorPecaJogadorUM.Checked = true;
            ckbIniciaJogoEmbaixo.Checked = true;
            ckbIsPrimeiroJogar.Checked = true;
            ckbIsJogadorDoisFuncional.Checked = true;
        }



        private void btnOK_Click(object sender, EventArgs e)
        {
            JogadorUM = new Jogador();
            JogadorDOIS = new Jogador();

            try
            {
                //Teste se é humano
                if (ckbIsJogadorUmHumano.Checked)
                    JogadorUM.IsHumano = true;
                if (ckbIsJogadorDoisHumano.Checked)
                    JogadorDOIS.IsHumano = true;

                if (ckbCorPecaJogadorUM.Checked)
                {
                    JogadorUM.CorDoJogador = BRANCAS;
                    JogadorDOIS.CorDoJogador = PRETAS;
                }
                else
                {
                    JogadorUM.CorDoJogador = PRETAS;
                    JogadorDOIS.CorDoJogador = BRANCAS;
                }

                if (ckbIniciaJogoEmbaixo.Checked)
                    JogadorUM.IsEmbaixo = true;

                if (ckbIsPrimeiroJogar.Checked)
                    JogadorUM.IsPrimeiroJogador = true;

                if (ckbIsJogadorDoisFuncional.Checked)
                    JogadorDOIS.IsFuncional = true;

                SetaTipoJogadores();

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                this.DialogResult = DialogResult.Cancel;
                throw ex;
            }
            finally
            {
                this.Close();
            }


        }

        private void ckbCorPecaJogadorUM_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbCorPecaJogadorUM.Checked)
            {
                lblCorPecaJogadorDOIS.Text = "Pretas";
            }
            else
                lblCorPecaJogadorDOIS.Text = "Brancas";

            if (ckbIniciaJogoEmbaixo.Checked)
            {
                lblPosicaoInicialPecasJogadorDOIS.Text = lblCorPecaJogadorDOIS.Text + " em cima";
            }
            else
                lblPosicaoInicialPecasJogadorDOIS.Text = lblCorPecaJogadorDOIS.Text + " embaixo";
        }

        private void ckbIniciaJogoEmbaixo_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbIniciaJogoEmbaixo.Checked)
            {
                lblPosicaoInicialPecasJogadorDOIS.Text = lblCorPecaJogadorDOIS.Text + " em cima";
            }
            else
                lblPosicaoInicialPecasJogadorDOIS.Text = lblCorPecaJogadorDOIS.Text + " embaixo";
        }

        private void ckbIsPrimeiroJogar_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbIsPrimeiroJogar.Checked)
            {
                lblComecaJogo.Text = "Segundo a jogar";
            }
            else
            {
                lblComecaJogo.Text = "Primeiro a jogar";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void SetaTipoJogadores()
        {
            if (JogadorUM.IsHumano)
                if (JogadorDOIS.IsHumano)
                    this.TipoDosJogadores = TipoDosJogadores.HumanoXHumano;
                else if (JogadorDOIS.IsFuncional)
                    this.TipoDosJogadores = TipoDosJogadores.HumanoXFComputador;
                else
                    this.TipoDosJogadores = TipoDosJogadores.HumanoXComputador;
            else
                if (JogadorDOIS.IsHumano)
                    this.TipoDosJogadores = TipoDosJogadores.ComputadorXHumano;
                else if (JogadorDOIS.IsFuncional)
                    this.TipoDosJogadores = TipoDosJogadores.ComputadorXFComputador;
                else
                    this.TipoDosJogadores = TipoDosJogadores.ComputadorXComputador;
        }

       
        private void ckbIsJogadorDoisHumano_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked == true)
            {
                ckbIsJogadorDoisFuncional.Visible = false;
                ckbIsJogadorDoisFuncional.Checked = false;
            }
            else
            {
                ckbIsJogadorDoisFuncional.Visible = true;
                ckbIsJogadorDoisFuncional.Checked = true;
            }

        }
    }
}
