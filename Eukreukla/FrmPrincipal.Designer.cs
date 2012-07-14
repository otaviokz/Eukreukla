namespace Eukreukla
{
    partial class FrmPrincipal
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            this.Tempo = new System.Windows.Forms.Timer(this.components);
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.pnlTabuleiro = new System.Windows.Forms.Panel();
            this.lblMovePeca = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.lblCasa = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnIniciarJogo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPecasRestantesPretas = new System.Windows.Forms.Label();
            this.lblPecasRestantesBrancas = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnNovoJogo = new System.Windows.Forms.Button();
            this.lblTempo = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblNumeroMovimentos = new System.Windows.Forms.Label();
            this.lblNumeroPecasComidas = new System.Windows.Forms.Label();
            this.lblMediaComidasPorMovimentos = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.menuNovoJogo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuIniciarJogo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuSalvarJogo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCarregarJogo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuSair = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConfiguracoes = new System.Windows.Forms.ToolStripButton();
            this.menuDificuldade = new System.Windows.Forms.ToolStripDropDownButton();
            this.menuFacil = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFacilMedio = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMedio = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMedioDificil = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDificil = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAjuda = new System.Windows.Forms.ToolStripButton();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblEstadoDoJogo = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNumeroMovimentosRecursivos = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblRecursao = new System.Windows.Forms.Label();
            this.ckbSom = new System.Windows.Forms.CheckBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuColetaDados = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tempo
            // 
            this.Tempo.Interval = 1000;
            this.Tempo.Tick += new System.EventHandler(this.Tempo_Tick);
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.AutoScroll = true;
            this.ContentPanel.Size = new System.Drawing.Size(675, 463);
            // 
            // pnlTabuleiro
            // 
            this.pnlTabuleiro.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTabuleiro.Location = new System.Drawing.Point(9, 28);
            this.pnlTabuleiro.Name = "pnlTabuleiro";
            this.pnlTabuleiro.Size = new System.Drawing.Size(498, 411);
            this.pnlTabuleiro.TabIndex = 1;
            this.pnlTabuleiro.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTabuleiro_Paint);
            this.pnlTabuleiro.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlTabuleiro_MouseClick);
            // 
            // lblMovePeca
            // 
            this.lblMovePeca.AutoSize = true;
            this.lblMovePeca.Location = new System.Drawing.Point(466, 93);
            this.lblMovePeca.Name = "lblMovePeca";
            this.lblMovePeca.Size = new System.Drawing.Size(0, 13);
            this.lblMovePeca.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Y:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Casa:";
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(47, 39);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(0, 13);
            this.lblY.TabIndex = 3;
            // 
            // lblCasa
            // 
            this.lblCasa.AutoSize = true;
            this.lblCasa.Location = new System.Drawing.Point(47, 62);
            this.lblCasa.Name = "lblCasa";
            this.lblCasa.Size = new System.Drawing.Size(0, 13);
            this.lblCasa.TabIndex = 7;
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(47, 16);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(0, 13);
            this.lblX.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "X: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblX);
            this.groupBox1.Controls.Add(this.lblCasa);
            this.groupBox1.Controls.Add(this.lblY);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(513, 329);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(155, 85);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DEBUG";
            // 
            // btnIniciarJogo
            // 
            this.btnIniciarJogo.Location = new System.Drawing.Point(513, 31);
            this.btnIniciarJogo.Name = "btnIniciarJogo";
            this.btnIniciarJogo.Size = new System.Drawing.Size(155, 23);
            this.btnIniciarJogo.TabIndex = 10;
            this.btnIniciarJogo.Text = "Iniciar";
            this.btnIniciarJogo.UseVisualStyleBackColor = true;
            this.btnIniciarJogo.Click += new System.EventHandler(this.btnIniciarJogo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Brancas:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Pretas:";
            // 
            // lblPecasRestantesPretas
            // 
            this.lblPecasRestantesPretas.AutoSize = true;
            this.lblPecasRestantesPretas.Location = new System.Drawing.Point(61, 39);
            this.lblPecasRestantesPretas.Name = "lblPecasRestantesPretas";
            this.lblPecasRestantesPretas.Size = new System.Drawing.Size(0, 13);
            this.lblPecasRestantesPretas.TabIndex = 2;
            // 
            // lblPecasRestantesBrancas
            // 
            this.lblPecasRestantesBrancas.AutoSize = true;
            this.lblPecasRestantesBrancas.Location = new System.Drawing.Point(61, 16);
            this.lblPecasRestantesBrancas.Name = "lblPecasRestantesBrancas";
            this.lblPecasRestantesBrancas.Size = new System.Drawing.Size(0, 13);
            this.lblPecasRestantesBrancas.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblPecasRestantesBrancas);
            this.groupBox2.Controls.Add(this.lblPecasRestantesPretas);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(513, 264);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(155, 59);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Peças Restantes";
            // 
            // btnNovoJogo
            // 
            this.btnNovoJogo.Location = new System.Drawing.Point(513, 60);
            this.btnNovoJogo.Name = "btnNovoJogo";
            this.btnNovoJogo.Size = new System.Drawing.Size(155, 23);
            this.btnNovoJogo.TabIndex = 14;
            this.btnNovoJogo.Text = "Novo Jogo";
            this.btnNovoJogo.UseVisualStyleBackColor = true;
            this.btnNovoJogo.Click += new System.EventHandler(this.btnNovoJogo_Click);
            // 
            // lblTempo
            // 
            this.lblTempo.AutoSize = true;
            this.lblTempo.Location = new System.Drawing.Point(56, 16);
            this.lblTempo.Name = "lblTempo";
            this.lblTempo.Size = new System.Drawing.Size(0, 13);
            this.lblTempo.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblTempo);
            this.groupBox4.Location = new System.Drawing.Point(513, 89);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(155, 38);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Tempo";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Número de movimentos:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Número de peças comidas:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(141, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Peças comidas/Movimentos";
            // 
            // lblNumeroMovimentos
            // 
            this.lblNumeroMovimentos.AutoSize = true;
            this.lblNumeroMovimentos.Location = new System.Drawing.Point(6, 29);
            this.lblNumeroMovimentos.Name = "lblNumeroMovimentos";
            this.lblNumeroMovimentos.Size = new System.Drawing.Size(0, 13);
            this.lblNumeroMovimentos.TabIndex = 4;
            // 
            // lblNumeroPecasComidas
            // 
            this.lblNumeroPecasComidas.AutoSize = true;
            this.lblNumeroPecasComidas.Location = new System.Drawing.Point(6, 68);
            this.lblNumeroPecasComidas.Name = "lblNumeroPecasComidas";
            this.lblNumeroPecasComidas.Size = new System.Drawing.Size(0, 13);
            this.lblNumeroPecasComidas.TabIndex = 5;
            // 
            // lblMediaComidasPorMovimentos
            // 
            this.lblMediaComidasPorMovimentos.AutoSize = true;
            this.lblMediaComidasPorMovimentos.Location = new System.Drawing.Point(6, 103);
            this.lblMediaComidasPorMovimentos.Name = "lblMediaComidasPorMovimentos";
            this.lblMediaComidasPorMovimentos.Size = new System.Drawing.Size(0, 13);
            this.lblMediaComidasPorMovimentos.TabIndex = 6;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblMediaComidasPorMovimentos);
            this.groupBox3.Controls.Add(this.lblNumeroPecasComidas);
            this.groupBox3.Controls.Add(this.lblNumeroMovimentos);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(513, 133);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(155, 125);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Estatísticas";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNovoJogo,
            this.menuIniciarJogo,
            this.toolStripSeparator1,
            this.menuSalvarJogo,
            this.menuCarregarJogo,
            this.toolStripSeparator3,
            this.menuColetaDados,
            this.toolStripSeparator2,
            this.menuSair});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(62, 22);
            this.toolStripDropDownButton1.Text = "Arquivo";
            // 
            // menuNovoJogo
            // 
            this.menuNovoJogo.Name = "menuNovoJogo";
            this.menuNovoJogo.Size = new System.Drawing.Size(191, 22);
            this.menuNovoJogo.Text = "Novo Jogo";
            this.menuNovoJogo.Click += new System.EventHandler(this.btnNovoJogo_Click);
            // 
            // menuIniciarJogo
            // 
            this.menuIniciarJogo.Name = "menuIniciarJogo";
            this.menuIniciarJogo.Size = new System.Drawing.Size(191, 22);
            this.menuIniciarJogo.Text = "Iniciar Jogo";
            this.menuIniciarJogo.Click += new System.EventHandler(this.btnIniciarJogo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(188, 6);
            // 
            // menuSalvarJogo
            // 
            this.menuSalvarJogo.Name = "menuSalvarJogo";
            this.menuSalvarJogo.Size = new System.Drawing.Size(191, 22);
            this.menuSalvarJogo.Text = "Salvar Jogo";
            this.menuSalvarJogo.Click += new System.EventHandler(this.menuSalvarJogo_Click);
            // 
            // menuCarregarJogo
            // 
            this.menuCarregarJogo.Name = "menuCarregarJogo";
            this.menuCarregarJogo.Size = new System.Drawing.Size(191, 22);
            this.menuCarregarJogo.Text = "CarregarJogo";
            this.menuCarregarJogo.Click += new System.EventHandler(this.menuCarregarJogo_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(188, 6);
            // 
            // menuSair
            // 
            this.menuSair.Name = "menuSair";
            this.menuSair.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuSair.Size = new System.Drawing.Size(191, 22);
            this.menuSair.Text = "Sair";
            this.menuSair.Click += new System.EventHandler(this.menuSair_Click);
            // 
            // menuConfiguracoes
            // 
            this.menuConfiguracoes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuConfiguracoes.Image = ((System.Drawing.Image)(resources.GetObject("menuConfiguracoes.Image")));
            this.menuConfiguracoes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuConfiguracoes.Name = "menuConfiguracoes";
            this.menuConfiguracoes.Size = new System.Drawing.Size(88, 22);
            this.menuConfiguracoes.Text = "Configurações";
            this.menuConfiguracoes.Click += new System.EventHandler(this.configuraçõesToolStripMenuItem_Click);
            // 
            // menuDificuldade
            // 
            this.menuDificuldade.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuDificuldade.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFacil,
            this.menuFacilMedio,
            this.menuMedio,
            this.menuMedioDificil,
            this.menuDificil});
            this.menuDificuldade.Image = ((System.Drawing.Image)(resources.GetObject("menuDificuldade.Image")));
            this.menuDificuldade.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuDificuldade.Name = "menuDificuldade";
            this.menuDificuldade.Size = new System.Drawing.Size(80, 22);
            this.menuDificuldade.Text = "Dificuldade";
            // 
            // menuFacil
            // 
            this.menuFacil.Name = "menuFacil";
            this.menuFacil.Size = new System.Drawing.Size(123, 22);
            this.menuFacil.Tag = "3";
            this.menuFacil.Text = "Baby";
            this.menuFacil.Click += new System.EventHandler(this.menuDificuldade_Click);
            // 
            // menuFacilMedio
            // 
            this.menuFacilMedio.Name = "menuFacilMedio";
            this.menuFacilMedio.Size = new System.Drawing.Size(123, 22);
            this.menuFacilMedio.Tag = "4";
            this.menuFacilMedio.Text = "Kid";
            this.menuFacilMedio.Click += new System.EventHandler(this.menuDificuldade_Click);
            // 
            // menuMedio
            // 
            this.menuMedio.Name = "menuMedio";
            this.menuMedio.Size = new System.Drawing.Size(123, 22);
            this.menuMedio.Tag = "5";
            this.menuMedio.Text = "Teenager";
            this.menuMedio.Click += new System.EventHandler(this.menuDificuldade_Click);
            // 
            // menuMedioDificil
            // 
            this.menuMedioDificil.Name = "menuMedioDificil";
            this.menuMedioDificil.Size = new System.Drawing.Size(123, 22);
            this.menuMedioDificil.Tag = "6";
            this.menuMedioDificil.Text = "Mature";
            this.menuMedioDificil.Click += new System.EventHandler(this.menuDificuldade_Click);
            // 
            // menuDificil
            // 
            this.menuDificil.Name = "menuDificil";
            this.menuDificil.Size = new System.Drawing.Size(123, 22);
            this.menuDificil.Tag = "7";
            this.menuDificil.Text = "Old";
            this.menuDificil.Click += new System.EventHandler(this.menuDificuldade_Click);
            // 
            // menuAjuda
            // 
            this.menuAjuda.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuAjuda.Image = ((System.Drawing.Image)(resources.GetObject("menuAjuda.Image")));
            this.menuAjuda.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuAjuda.Name = "menuAjuda";
            this.menuAjuda.Size = new System.Drawing.Size(42, 22);
            this.menuAjuda.Text = "Ajuda";
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.menuConfiguracoes,
            this.menuDificuldade,
            this.menuAjuda,
            this.toolStripSeparator4,
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator,
            this.helpToolStripButton});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(677, 25);
            this.menu.TabIndex = 16;
            this.menu.Text = "menu";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "&New";
            this.newToolStripButton.Click += new System.EventHandler(this.btnNovoJogo_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Open";
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Save";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.helpToolStripButton.Text = "He&lp";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblEstadoDoJogo,
            this.lblNumeroMovimentosRecursivos});
            this.statusStrip1.Location = new System.Drawing.Point(0, 451);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(677, 22);
            this.statusStrip1.TabIndex = 17;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblEstadoDoJogo
            // 
            this.lblEstadoDoJogo.Name = "lblEstadoDoJogo";
            this.lblEstadoDoJogo.Size = new System.Drawing.Size(0, 17);
            // 
            // lblNumeroMovimentosRecursivos
            // 
            this.lblNumeroMovimentosRecursivos.Name = "lblNumeroMovimentosRecursivos";
            this.lblNumeroMovimentosRecursivos.Size = new System.Drawing.Size(0, 17);
            // 
            // lblRecursao
            // 
            this.lblRecursao.AutoSize = true;
            this.lblRecursao.Location = new System.Drawing.Point(525, 417);
            this.lblRecursao.Name = "lblRecursao";
            this.lblRecursao.Size = new System.Drawing.Size(0, 13);
            this.lblRecursao.TabIndex = 18;
            // 
            // ckbSom
            // 
            this.ckbSom.AutoSize = true;
            this.ckbSom.Checked = true;
            this.ckbSom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbSom.Location = new System.Drawing.Point(514, 421);
            this.ckbSom.Name = "ckbSom";
            this.ckbSom.Size = new System.Drawing.Size(53, 17);
            this.ckbSom.TabIndex = 19;
            this.ckbSom.Text = "Som?";
            this.ckbSom.UseVisualStyleBackColor = true;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.CheckFileExists = true;
            this.saveFileDialog.DefaultExt = "euk";
            this.saveFileDialog.Filter = "Eukreukla files |*.euk|All files|*.*";
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Eukreukla files |*.euk";
            this.openFileDialog.RestoreDirectory = true;
            // 
            // menuColetaDados
            // 
            this.menuColetaDados.Name = "menuColetaDados";
            this.menuColetaDados.Size = new System.Drawing.Size(191, 22);
            this.menuColetaDados.Text = "Salvar coleta de dados";
            this.menuColetaDados.Click += new System.EventHandler(this.menuColetaDados_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(188, 6);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 473);
            this.Controls.Add(this.ckbSom);
            this.Controls.Add(this.lblRecursao);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.btnIniciarJogo);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pnlTabuleiro);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.lblMovePeca);
            this.Controls.Add(this.btnNovoJogo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmPrincipal";
            this.Text = "Alquerque";
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer Tempo;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.Panel pnlTabuleiro;
        private System.Windows.Forms.Label lblMovePeca;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblCasa;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnIniciarJogo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPecasRestantesPretas;
        private System.Windows.Forms.Label lblPecasRestantesBrancas;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnNovoJogo;
        private System.Windows.Forms.Label lblTempo;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblNumeroMovimentos;
        private System.Windows.Forms.Label lblNumeroPecasComidas;
        private System.Windows.Forms.Label lblMediaComidasPorMovimentos;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripButton menuConfiguracoes;
        private System.Windows.Forms.ToolStripDropDownButton menuDificuldade;
        private System.Windows.Forms.ToolStripMenuItem menuFacil;
        private System.Windows.Forms.ToolStripMenuItem menuFacilMedio;
        private System.Windows.Forms.ToolStripMenuItem menuMedio;
        private System.Windows.Forms.ToolStripMenuItem menuMedioDificil;
        private System.Windows.Forms.ToolStripMenuItem menuDificil;
        private System.Windows.Forms.ToolStripButton menuAjuda;
        private System.Windows.Forms.ToolStrip menu;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblEstadoDoJogo;
        private System.Windows.Forms.Label lblRecursao;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem menuNovoJogo;
        private System.Windows.Forms.ToolStripMenuItem menuIniciarJogo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuSalvarJogo;
        private System.Windows.Forms.ToolStripMenuItem menuCarregarJogo;
        private System.Windows.Forms.ToolStripMenuItem menuSair;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripStatusLabel lblNumeroMovimentosRecursivos;
        private System.Windows.Forms.CheckBox ckbSom;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuColetaDados;
    }
}