namespace Eukreukla
{
    partial class FrmConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckbIsPrimeiroJogar = new System.Windows.Forms.CheckBox();
            this.ckbIniciaJogoEmbaixo = new System.Windows.Forms.CheckBox();
            this.ckbCorPecaJogadorUM = new System.Windows.Forms.CheckBox();
            this.ckbIsJogadorUmHumano = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblComecaJogo = new System.Windows.Forms.Label();
            this.lblPosicaoInicialPecasJogadorDOIS = new System.Windows.Forms.Label();
            this.lblCorPecaJogadorDOIS = new System.Windows.Forms.Label();
            this.ckbIsJogadorDoisHumano = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ckbIsJogadorDoisFuncional = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckbIsPrimeiroJogar);
            this.groupBox1.Controls.Add(this.ckbIniciaJogoEmbaixo);
            this.groupBox1.Controls.Add(this.ckbCorPecaJogadorUM);
            this.groupBox1.Controls.Add(this.ckbIsJogadorUmHumano);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 134);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Jogador Um";
            // 
            // ckbIsPrimeiroJogar
            // 
            this.ckbIsPrimeiroJogar.AutoSize = true;
            this.ckbIsPrimeiroJogar.Location = new System.Drawing.Point(6, 89);
            this.ckbIsPrimeiroJogar.Name = "ckbIsPrimeiroJogar";
            this.ckbIsPrimeiroJogar.Size = new System.Drawing.Size(106, 17);
            this.ckbIsPrimeiroJogar.TabIndex = 4;
            this.ckbIsPrimeiroJogar.Text = "Começa jogando";
            this.ckbIsPrimeiroJogar.UseVisualStyleBackColor = true;
            this.ckbIsPrimeiroJogar.CheckedChanged += new System.EventHandler(this.ckbIsPrimeiroJogar_CheckedChanged);
            // 
            // ckbIniciaJogoEmbaixo
            // 
            this.ckbIniciaJogoEmbaixo.AutoSize = true;
            this.ckbIniciaJogoEmbaixo.Location = new System.Drawing.Point(6, 66);
            this.ckbIniciaJogoEmbaixo.Name = "ckbIniciaJogoEmbaixo";
            this.ckbIniciaJogoEmbaixo.Size = new System.Drawing.Size(127, 17);
            this.ckbIniciaJogoEmbaixo.TabIndex = 3;
            this.ckbIniciaJogoEmbaixo.Text = "Inicia na parte inferior";
            this.ckbIniciaJogoEmbaixo.UseVisualStyleBackColor = true;
            this.ckbIniciaJogoEmbaixo.CheckedChanged += new System.EventHandler(this.ckbIniciaJogoEmbaixo_CheckedChanged);
            // 
            // ckbCorPecaJogadorUM
            // 
            this.ckbCorPecaJogadorUM.AutoSize = true;
            this.ckbCorPecaJogadorUM.Location = new System.Drawing.Point(6, 43);
            this.ckbCorPecaJogadorUM.Name = "ckbCorPecaJogadorUM";
            this.ckbCorPecaJogadorUM.Size = new System.Drawing.Size(65, 17);
            this.ckbCorPecaJogadorUM.TabIndex = 2;
            this.ckbCorPecaJogadorUM.Text = "Brancas";
            this.ckbCorPecaJogadorUM.UseVisualStyleBackColor = true;
            this.ckbCorPecaJogadorUM.CheckedChanged += new System.EventHandler(this.ckbCorPecaJogadorUM_CheckedChanged);
            // 
            // ckbIsJogadorUmHumano
            // 
            this.ckbIsJogadorUmHumano.AutoSize = true;
            this.ckbIsJogadorUmHumano.Location = new System.Drawing.Point(7, 20);
            this.ckbIsJogadorUmHumano.Name = "ckbIsJogadorUmHumano";
            this.ckbIsJogadorUmHumano.Size = new System.Drawing.Size(66, 17);
            this.ckbIsJogadorUmHumano.TabIndex = 1;
            this.ckbIsJogadorUmHumano.Text = "Humano";
            this.ckbIsJogadorUmHumano.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ckbIsJogadorDoisFuncional);
            this.groupBox2.Controls.Add(this.lblComecaJogo);
            this.groupBox2.Controls.Add(this.lblPosicaoInicialPecasJogadorDOIS);
            this.groupBox2.Controls.Add(this.lblCorPecaJogadorDOIS);
            this.groupBox2.Controls.Add(this.ckbIsJogadorDoisHumano);
            this.groupBox2.Location = new System.Drawing.Point(218, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(164, 104);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Jogador Dois";
            // 
            // lblComecaJogo
            // 
            this.lblComecaJogo.AutoSize = true;
            this.lblComecaJogo.Location = new System.Drawing.Point(6, 88);
            this.lblComecaJogo.Name = "lblComecaJogo";
            this.lblComecaJogo.Size = new System.Drawing.Size(0, 13);
            this.lblComecaJogo.TabIndex = 4;
            // 
            // lblPosicaoInicialPecasJogadorDOIS
            // 
            this.lblPosicaoInicialPecasJogadorDOIS.AutoSize = true;
            this.lblPosicaoInicialPecasJogadorDOIS.Location = new System.Drawing.Point(6, 67);
            this.lblPosicaoInicialPecasJogadorDOIS.Name = "lblPosicaoInicialPecasJogadorDOIS";
            this.lblPosicaoInicialPecasJogadorDOIS.Size = new System.Drawing.Size(0, 13);
            this.lblPosicaoInicialPecasJogadorDOIS.TabIndex = 3;
            // 
            // lblCorPecaJogadorDOIS
            // 
            this.lblCorPecaJogadorDOIS.AutoSize = true;
            this.lblCorPecaJogadorDOIS.Location = new System.Drawing.Point(6, 44);
            this.lblCorPecaJogadorDOIS.Name = "lblCorPecaJogadorDOIS";
            this.lblCorPecaJogadorDOIS.Size = new System.Drawing.Size(0, 13);
            this.lblCorPecaJogadorDOIS.TabIndex = 2;
            // 
            // ckbIsJogadorDoisHumano
            // 
            this.ckbIsJogadorDoisHumano.AutoSize = true;
            this.ckbIsJogadorDoisHumano.Location = new System.Drawing.Point(7, 20);
            this.ckbIsJogadorDoisHumano.Name = "ckbIsJogadorDoisHumano";
            this.ckbIsJogadorDoisHumano.Size = new System.Drawing.Size(66, 17);
            this.ckbIsJogadorDoisHumano.TabIndex = 1;
            this.ckbIsJogadorDoisHumano.Text = "Humano";
            this.ckbIsJogadorDoisHumano.UseVisualStyleBackColor = true;
            this.ckbIsJogadorDoisHumano.CheckedChanged += new System.EventHandler(this.ckbIsJogadorDoisHumano_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(218, 122);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(299, 123);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ckbIsJogadorDoisFuncional
            // 
            this.ckbIsJogadorDoisFuncional.AutoSize = true;
            this.ckbIsJogadorDoisFuncional.Location = new System.Drawing.Point(71, 20);
            this.ckbIsJogadorDoisFuncional.Name = "ckbIsJogadorDoisFuncional";
            this.ckbIsJogadorDoisFuncional.Size = new System.Drawing.Size(85, 17);
            this.ckbIsJogadorDoisFuncional.TabIndex = 5;
            this.ckbIsJogadorDoisFuncional.Text = "IA Funcional";
            this.ckbIsJogadorDoisFuncional.UseVisualStyleBackColor = true;
            // 
            // FrmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 157);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmConfig";
            this.Text = "Configurações";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ckbIsJogadorUmHumano;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox ckbIsJogadorDoisHumano;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox ckbIsPrimeiroJogar;
        private System.Windows.Forms.CheckBox ckbIniciaJogoEmbaixo;
        private System.Windows.Forms.CheckBox ckbCorPecaJogadorUM;
        private System.Windows.Forms.Label lblComecaJogo;
        private System.Windows.Forms.Label lblPosicaoInicialPecasJogadorDOIS;
        private System.Windows.Forms.Label lblCorPecaJogadorDOIS;
        private System.Windows.Forms.CheckBox ckbIsJogadorDoisFuncional;
    }
}