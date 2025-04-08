namespace EC_Control
{
    partial class Form2
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
            this.labelEC = new System.Windows.Forms.Label();
            this.labelAssunto = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.textBoxComentario = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_EC = new System.Windows.Forms.Label();
            this.label_Assunto = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelData = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelEC
            // 
            this.labelEC.AutoSize = true;
            this.labelEC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEC.Location = new System.Drawing.Point(121, 62);
            this.labelEC.Name = "labelEC";
            this.labelEC.Size = new System.Drawing.Size(115, 15);
            this.labelEC.TabIndex = 0;
            this.labelEC.Text = "Sem número de EC";
            // 
            // labelAssunto
            // 
            this.labelAssunto.AutoSize = true;
            this.labelAssunto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAssunto.Location = new System.Drawing.Point(121, 92);
            this.labelAssunto.Name = "labelAssunto";
            this.labelAssunto.Size = new System.Drawing.Size(115, 15);
            this.labelAssunto.TabIndex = 1;
            this.labelAssunto.Text = "Sem assunto da EC";
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(372, 459);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 6;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click_1);
            // 
            // textBoxComentario
            // 
            this.textBoxComentario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxComentario.Location = new System.Drawing.Point(12, 172);
            this.textBoxComentario.Multiline = true;
            this.textBoxComentario.Name = "textBoxComentario";
            this.textBoxComentario.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxComentario.Size = new System.Drawing.Size(776, 212);
            this.textBoxComentario.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(326, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "Criar relatório do dia";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Descrição:";
            // 
            // label_EC
            // 
            this.label_EC.AutoSize = true;
            this.label_EC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_EC.Location = new System.Drawing.Point(12, 62);
            this.label_EC.Name = "label_EC";
            this.label_EC.Size = new System.Drawing.Size(104, 15);
            this.label_EC.TabIndex = 9;
            this.label_EC.Text = "Número da EC:";
            // 
            // label_Assunto
            // 
            this.label_Assunto.AutoSize = true;
            this.label_Assunto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Assunto.Location = new System.Drawing.Point(12, 92);
            this.label_Assunto.Name = "label_Assunto";
            this.label_Assunto.Size = new System.Drawing.Size(103, 15);
            this.label_Assunto.TabIndex = 10;
            this.label_Assunto.Text = "Assunto da EC:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "Data:";
            // 
            // labelData
            // 
            this.labelData.AutoSize = true;
            this.labelData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelData.Location = new System.Drawing.Point(59, 123);
            this.labelData.Name = "labelData";
            this.labelData.Size = new System.Drawing.Size(115, 15);
            this.labelData.TabIndex = 12;
            this.labelData.Text = "Sem assunto da EC";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Location = new System.Drawing.Point(15, 424);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 13;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(15, 401);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(102, 17);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "Criar notificação";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 491);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.labelData);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label_Assunto);
            this.Controls.Add(this.label_EC);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxComentario);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.labelAssunto);
            this.Controls.Add(this.labelEC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelEC;
        private System.Windows.Forms.Label labelAssunto;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.TextBox textBoxComentario;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_EC;
        private System.Windows.Forms.Label label_Assunto;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelData;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}