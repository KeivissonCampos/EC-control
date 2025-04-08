namespace EC_Control
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtCodigoEC = new System.Windows.Forms.TextBox();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.dgvResultados = new System.Windows.Forms.DataGridView();
            this.txtAssunto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDataReuniaoInicio = new System.Windows.Forms.DateTimePicker();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.configurarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPastas = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpDataReuniaoFim = new System.Windows.Forms.DateTimePicker();
            this.checkBox_relatorio = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.groupBox_Aguardando = new System.Windows.Forms.GroupBox();
            this.labelLocalEC = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labeldir = new System.Windows.Forms.Label();
            this.labelEC = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkBox_Fila = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.checkBox_Finalizado = new System.Windows.Forms.CheckBox();
            this.checkBox_excel = new System.Windows.Forms.CheckBox();
            this.checkBox_AguardandoAvaliacao = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox_ComPendencia = new System.Windows.Forms.CheckBox();
            this.checkBox_NovaEC = new System.Windows.Forms.CheckBox();
            this.checkBox_Andamento = new System.Windows.Forms.CheckBox();
            this.btn_notificacao = new System.Windows.Forms.Button();
            this.checkBox_Word = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel9.SuspendLayout();
            this.groupBox_Aguardando.SuspendLayout();
            this.panel8.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCodigoEC
            // 
            this.txtCodigoEC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoEC.Location = new System.Drawing.Point(104, 24);
            this.txtCodigoEC.Name = "txtCodigoEC";
            this.txtCodigoEC.Size = new System.Drawing.Size(175, 21);
            this.txtCodigoEC.TabIndex = 0;
            this.txtCodigoEC.TextChanged += new System.EventHandler(this.txtCodigoEC_TextChanged);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Location = new System.Drawing.Point(130, 151);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisar.TabIndex = 4;
            this.btnPesquisar.Text = "Pesquisar";
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click_1);
            // 
            // dgvResultados
            // 
            this.dgvResultados.AllowUserToOrderColumns = true;
            this.dgvResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvResultados.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvResultados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResultados.Location = new System.Drawing.Point(3, 16);
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.ReadOnly = true;
            this.dgvResultados.RowHeadersVisible = false;
            this.dgvResultados.Size = new System.Drawing.Size(693, 323);
            this.dgvResultados.TabIndex = 3;
            this.dgvResultados.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResultados_CellEnter);
            this.dgvResultados.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResultados_CellDoubleClick);
            this.dgvResultados.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResultados_CellEnter);
            this.dgvResultados.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvResultados_ColumnHeaderMouseClick);
            // 
            // txtAssunto
            // 
            this.txtAssunto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAssunto.Location = new System.Drawing.Point(105, 49);
            this.txtAssunto.Name = "txtAssunto";
            this.txtAssunto.Size = new System.Drawing.Size(175, 21);
            this.txtAssunto.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Número da EC:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Assunto da EC:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Data da EC:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // dtpDataReuniaoInicio
            // 
            this.dtpDataReuniaoInicio.Enabled = false;
            this.dtpDataReuniaoInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataReuniaoInicio.Location = new System.Drawing.Point(103, 117);
            this.dtpDataReuniaoInicio.Name = "dtpDataReuniaoInicio";
            this.dtpDataReuniaoInicio.Size = new System.Drawing.Size(97, 20);
            this.dtpDataReuniaoInicio.TabIndex = 6;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(10, 23);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(342, 309);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurarToolStripMenuItem,
            this.menuPastas});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1084, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // configurarToolStripMenuItem
            // 
            this.configurarToolStripMenuItem.Name = "configurarToolStripMenuItem";
            this.configurarToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.configurarToolStripMenuItem.Text = "Configurar";
            this.configurarToolStripMenuItem.Click += new System.EventHandler(this.configurarToolStripMenuItem_Click);
            // 
            // menuPastas
            // 
            this.menuPastas.Name = "menuPastas";
            this.menuPastas.Size = new System.Drawing.Size(52, 20);
            this.menuPastas.Text = "Pastas";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(21, 85);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(114, 17);
            this.checkBox1.TabIndex = 12;
            this.checkBox1.Text = "Pesquisar por data";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 10);
            this.panel1.Size = new System.Drawing.Size(365, 537);
            this.panel1.TabIndex = 13;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(10, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(355, 527);
            this.panel3.TabIndex = 13;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Control;
            this.panel5.Controls.Add(this.groupBox2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 185);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(355, 342);
            this.panel5.TabIndex = 15;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.richTextBox1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(10, 10, 3, 10);
            this.groupBox2.Size = new System.Drawing.Size(355, 342);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Descrição da EC";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(355, 185);
            this.panel4.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.dtpDataReuniaoFim);
            this.groupBox1.Controls.Add(this.txtCodigoEC);
            this.groupBox1.Controls.Add(this.dtpDataReuniaoInicio);
            this.groupBox1.Controls.Add(this.txtAssunto);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.btnPesquisar);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 185);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pesquisar por:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(79, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "de";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(207, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "até";
            // 
            // dtpDataReuniaoFim
            // 
            this.dtpDataReuniaoFim.Checked = false;
            this.dtpDataReuniaoFim.Enabled = false;
            this.dtpDataReuniaoFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataReuniaoFim.Location = new System.Drawing.Point(236, 117);
            this.dtpDataReuniaoFim.Name = "dtpDataReuniaoFim";
            this.dtpDataReuniaoFim.Size = new System.Drawing.Size(95, 20);
            this.dtpDataReuniaoFim.TabIndex = 13;
            // 
            // checkBox_relatorio
            // 
            this.checkBox_relatorio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_relatorio.AutoSize = true;
            this.checkBox_relatorio.Checked = true;
            this.checkBox_relatorio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_relatorio.Location = new System.Drawing.Point(6, 66);
            this.checkBox_relatorio.Name = "checkBox_relatorio";
            this.checkBox_relatorio.Size = new System.Drawing.Size(126, 17);
            this.checkBox_relatorio.TabIndex = 15;
            this.checkBox_relatorio.Text = "Incluir relatório do dia";
            this.checkBox_relatorio.UseVisualStyleBackColor = true;
            this.checkBox_relatorio.Click += new System.EventHandler(this.checkBox_excel_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(209, 62);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Criar relatório do dia";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(365, 24);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.panel2.Size = new System.Drawing.Size(719, 537);
            this.panel2.TabIndex = 14;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.groupBox3);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(10, 185);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(699, 342);
            this.panel7.TabIndex = 5;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvResultados);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(699, 342);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Lista de ECs";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel9);
            this.panel6.Controls.Add(this.panel8);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(10, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(699, 185);
            this.panel6.TabIndex = 4;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.groupBox_Aguardando);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(306, 185);
            this.panel9.TabIndex = 21;
            // 
            // groupBox_Aguardando
            // 
            this.groupBox_Aguardando.Controls.Add(this.labelLocalEC);
            this.groupBox_Aguardando.Controls.Add(this.label9);
            this.groupBox_Aguardando.Controls.Add(this.label4);
            this.groupBox_Aguardando.Controls.Add(this.labeldir);
            this.groupBox_Aguardando.Controls.Add(this.labelEC);
            this.groupBox_Aguardando.Controls.Add(this.label7);
            this.groupBox_Aguardando.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_Aguardando.Location = new System.Drawing.Point(0, 0);
            this.groupBox_Aguardando.Name = "groupBox_Aguardando";
            this.groupBox_Aguardando.Size = new System.Drawing.Size(306, 185);
            this.groupBox_Aguardando.TabIndex = 19;
            this.groupBox_Aguardando.TabStop = false;
            this.groupBox_Aguardando.Text = "Informações da EC";
            this.groupBox_Aguardando.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // labelLocalEC
            // 
            this.labelLocalEC.AutoSize = true;
            this.labelLocalEC.Location = new System.Drawing.Point(74, 47);
            this.labelLocalEC.Name = "labelLocalEC";
            this.labelLocalEC.Size = new System.Drawing.Size(10, 13);
            this.labelLocalEC.TabIndex = 26;
            this.labelLocalEC.Text = "-";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(11, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Local EC:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Status:";
            // 
            // labeldir
            // 
            this.labeldir.AutoSize = true;
            this.labeldir.Location = new System.Drawing.Point(58, 72);
            this.labeldir.Name = "labeldir";
            this.labeldir.Size = new System.Drawing.Size(10, 13);
            this.labeldir.TabIndex = 23;
            this.labeldir.Text = "-";
            // 
            // labelEC
            // 
            this.labelEC.AutoSize = true;
            this.labelEC.Location = new System.Drawing.Point(82, 24);
            this.labelEC.Name = "labelEC";
            this.labelEC.Size = new System.Drawing.Size(130, 13);
            this.labelEC.TabIndex = 18;
            this.labelEC.Text = "Nenhuma EC selecionada";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(11, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Número EC:";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.groupBox5);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel8.Location = new System.Drawing.Point(306, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(393, 185);
            this.panel8.TabIndex = 20;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.checkBox_Fila);
            this.groupBox5.Controls.Add(this.button4);
            this.groupBox5.Controls.Add(this.checkBox_Finalizado);
            this.groupBox5.Controls.Add(this.checkBox_excel);
            this.groupBox5.Controls.Add(this.checkBox_AguardandoAvaliacao);
            this.groupBox5.Controls.Add(this.button2);
            this.groupBox5.Controls.Add(this.checkBox_ComPendencia);
            this.groupBox5.Controls.Add(this.button1);
            this.groupBox5.Controls.Add(this.checkBox_NovaEC);
            this.groupBox5.Controls.Add(this.checkBox_Andamento);
            this.groupBox5.Controls.Add(this.btn_notificacao);
            this.groupBox5.Controls.Add(this.checkBox_relatorio);
            this.groupBox5.Controls.Add(this.checkBox_Word);
            this.groupBox5.Controls.Add(this.button3);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(393, 185);
            this.groupBox5.TabIndex = 23;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Controle de relátorio";
            // 
            // checkBox_Fila
            // 
            this.checkBox_Fila.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_Fila.AutoSize = true;
            this.checkBox_Fila.Location = new System.Drawing.Point(6, 163);
            this.checkBox_Fila.Name = "checkBox_Fila";
            this.checkBox_Fila.Size = new System.Drawing.Size(64, 17);
            this.checkBox_Fila.TabIndex = 32;
            this.checkBox_Fila.Text = "QUEUE";
            this.checkBox_Fila.UseVisualStyleBackColor = true;
            this.checkBox_Fila.Click += new System.EventHandler(this.checkBox_Fila_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(209, 90);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(177, 23);
            this.button4.TabIndex = 23;
            this.button4.Text = "Excluir relatório do dia";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // checkBox_Finalizado
            // 
            this.checkBox_Finalizado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_Finalizado.AutoSize = true;
            this.checkBox_Finalizado.Location = new System.Drawing.Point(77, 163);
            this.checkBox_Finalizado.Name = "checkBox_Finalizado";
            this.checkBox_Finalizado.Size = new System.Drawing.Size(69, 17);
            this.checkBox_Finalizado.TabIndex = 31;
            this.checkBox_Finalizado.Text = "CLOSED";
            this.checkBox_Finalizado.UseVisualStyleBackColor = true;
            this.checkBox_Finalizado.Click += new System.EventHandler(this.checkBox_Finalizado_Click);
            // 
            // checkBox_excel
            // 
            this.checkBox_excel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_excel.AutoSize = true;
            this.checkBox_excel.Checked = true;
            this.checkBox_excel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_excel.Location = new System.Drawing.Point(6, 43);
            this.checkBox_excel.Name = "checkBox_excel";
            this.checkBox_excel.Size = new System.Drawing.Size(162, 17);
            this.checkBox_excel.TabIndex = 21;
            this.checkBox_excel.Text = "Incluir relatórios das reuniões";
            this.checkBox_excel.UseVisualStyleBackColor = true;
            this.checkBox_excel.Click += new System.EventHandler(this.checkBox_excel_Click);
            // 
            // checkBox_AguardandoAvaliacao
            // 
            this.checkBox_AguardandoAvaliacao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_AguardandoAvaliacao.AutoSize = true;
            this.checkBox_AguardandoAvaliacao.Location = new System.Drawing.Point(77, 140);
            this.checkBox_AguardandoAvaliacao.Name = "checkBox_AguardandoAvaliacao";
            this.checkBox_AguardandoAvaliacao.Size = new System.Drawing.Size(82, 17);
            this.checkBox_AguardandoAvaliacao.TabIndex = 30;
            this.checkBox_AguardandoAvaliacao.Text = "FEEDBACK";
            this.checkBox_AguardandoAvaliacao.UseVisualStyleBackColor = true;
            this.checkBox_AguardandoAvaliacao.Click += new System.EventHandler(this.checkBox_AguardandoAvaliacao_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(209, 156);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(177, 23);
            this.button2.TabIndex = 22;
            this.button2.Text = "Salvar em XML";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox_ComPendencia
            // 
            this.checkBox_ComPendencia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_ComPendencia.AutoSize = true;
            this.checkBox_ComPendencia.Location = new System.Drawing.Point(77, 117);
            this.checkBox_ComPendencia.Name = "checkBox_ComPendencia";
            this.checkBox_ComPendencia.Size = new System.Drawing.Size(112, 17);
            this.checkBox_ComPendencia.TabIndex = 29;
            this.checkBox_ComPendencia.Text = "WIP - FEEDBACK";
            this.checkBox_ComPendencia.UseVisualStyleBackColor = true;
            this.checkBox_ComPendencia.Click += new System.EventHandler(this.checkBox_ComPendencia_Click);
            // 
            // checkBox_NovaEC
            // 
            this.checkBox_NovaEC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_NovaEC.AutoSize = true;
            this.checkBox_NovaEC.Location = new System.Drawing.Point(6, 117);
            this.checkBox_NovaEC.Name = "checkBox_NovaEC";
            this.checkBox_NovaEC.Size = new System.Drawing.Size(52, 17);
            this.checkBox_NovaEC.TabIndex = 28;
            this.checkBox_NovaEC.Text = "NEW";
            this.checkBox_NovaEC.UseVisualStyleBackColor = true;
            this.checkBox_NovaEC.Click += new System.EventHandler(this.checkBox_NovaEC_Click);
            // 
            // checkBox_Andamento
            // 
            this.checkBox_Andamento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_Andamento.AutoSize = true;
            this.checkBox_Andamento.Location = new System.Drawing.Point(6, 140);
            this.checkBox_Andamento.Name = "checkBox_Andamento";
            this.checkBox_Andamento.Size = new System.Drawing.Size(47, 17);
            this.checkBox_Andamento.TabIndex = 27;
            this.checkBox_Andamento.Text = "WIP";
            this.checkBox_Andamento.UseVisualStyleBackColor = true;
            this.checkBox_Andamento.CheckedChanged += new System.EventHandler(this.label3_Click);
            this.checkBox_Andamento.Click += new System.EventHandler(this.checkBox_Andamento_Click);
            // 
            // btn_notificacao
            // 
            this.btn_notificacao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_notificacao.BackColor = System.Drawing.Color.White;
            this.btn_notificacao.Location = new System.Drawing.Point(209, 14);
            this.btn_notificacao.Name = "btn_notificacao";
            this.btn_notificacao.Size = new System.Drawing.Size(177, 23);
            this.btn_notificacao.TabIndex = 17;
            this.btn_notificacao.Text = "Notificações do dia";
            this.btn_notificacao.UseVisualStyleBackColor = false;
            this.btn_notificacao.Click += new System.EventHandler(this.btn_notificacao_Click);
            // 
            // checkBox_Word
            // 
            this.checkBox_Word.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_Word.AutoSize = true;
            this.checkBox_Word.Checked = true;
            this.checkBox_Word.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Word.Location = new System.Drawing.Point(6, 89);
            this.checkBox_Word.Name = "checkBox_Word";
            this.checkBox_Word.Size = new System.Drawing.Size(123, 17);
            this.checkBox_Word.TabIndex = 20;
            this.checkBox_Word.Text = "Incluir dados da ECs";
            this.checkBox_Word.UseVisualStyleBackColor = true;
            this.checkBox_Word.CheckedChanged += new System.EventHandler(this.checkBox_Word_CheckedChanged);
            this.checkBox_Word.Click += new System.EventHandler(this.checkBox_Word_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(209, 127);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(177, 23);
            this.button3.TabIndex = 19;
            this.button3.Text = "Abrir documento da EC";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Lista de ECs:";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 561);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Engineering change control";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.groupBox_Aguardando.ResumeLayout(false);
            this.groupBox_Aguardando.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCodigoEC;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.DataGridView dgvResultados;
        private System.Windows.Forms.TextBox txtAssunto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDataReuniaoInicio;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem configurarToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox_relatorio;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_notificacao;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelEC;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox_Aguardando;
        private System.Windows.Forms.ToolStripMenuItem menuPastas;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox checkBox_excel;
        private System.Windows.Forms.CheckBox checkBox_Word;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labeldir;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpDataReuniaoFim;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelLocalEC;
        private System.Windows.Forms.CheckBox checkBox_ComPendencia;
        private System.Windows.Forms.CheckBox checkBox_NovaEC;
        private System.Windows.Forms.CheckBox checkBox_Andamento;
        private System.Windows.Forms.CheckBox checkBox_Finalizado;
        private System.Windows.Forms.CheckBox checkBox_AguardandoAvaliacao;
        private System.Windows.Forms.CheckBox checkBox_Fila;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

