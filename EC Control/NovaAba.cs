using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using OfficeOpenXml;

namespace EC_Control
{
    public partial class NovaAba : Form
    {

        private List<ECInfo> listaRecebida;
        private string codigoEC;
        private string assunto;
        private string comentario;
        private string dataReuniao;
        private string codEC;

        public NovaAba(List<ECInfo> lista, string EC)
        {
            InitializeComponent();
            listaRecebida = lista;
            codEC = EC;
        }

        private void NovaAba_Load(object sender, EventArgs e)
        {
            this.Text = codEC;
            dgvResultados.DataSource = listaRecebida; // Exibe os resultados no DataGridView
            dgvResultados.Columns[0].Width = 80;
            dgvResultados.Columns[1].Width = 80;
            dgvResultados.Columns[2].Width = 350;
            dgvResultados.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvResultados.Columns[4].Width = 100;

            dgvResultados.CurrentCell = dgvResultados.Rows[0].Cells[0]; // Define o foco na primeira célula
            dgvResultados.Focus(); // Garante que o DataGridView receba o foco

            dgvResultados.DataSource = ((List<ECInfo>)dgvResultados.DataSource)
                .OrderByDescending(ec => DateTime.TryParse(ec.Data, out DateTime data) ? data : DateTime.MaxValue)
                .ToList();

        }

        private void dgvResultados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            codigoEC = dgvResultados.Rows[e.RowIndex].Cells["CodigoEC"].Value.ToString();
            assunto = dgvResultados.Rows[e.RowIndex].Cells["Assunto"].Value.ToString();
            comentario = dgvResultados.Rows[e.RowIndex].Cells["Comentarios"].Value.ToString();
            dataReuniao = dgvResultados.Rows[e.RowIndex].Cells["Data"].Value.ToString();

            AtualizarRichTextBox(codigoEC, assunto, comentario, dataReuniao);
        }

        private void dgvResultados_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            codigoEC = dgvResultados.Rows[e.RowIndex].Cells["CodigoEC"].Value.ToString();
            assunto = dgvResultados.Rows[e.RowIndex].Cells["Assunto"].Value.ToString();
            comentario = dgvResultados.Rows[e.RowIndex].Cells["Comentarios"].Value.ToString();
            dataReuniao = dgvResultados.Rows[e.RowIndex].Cells["Data"].Value.ToString();

            AtualizarRichTextBox(codigoEC, assunto, comentario, dataReuniao);
        }

        private void AtualizarRichTextBox(string codigoEC, string assunto, string comentario, string dataReuniao)
        {
            // Exibir comentário detalhado em MessageBox
            //FontStyle.Bold, FontStyle.Regular, FontStyle.Italic
            richTextBox1.Clear();

            // 📌 EC em negrito
            richTextBox1.SelectionFont = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
            richTextBox1.AppendText($"📌 {codigoEC}\n\n");

            // 📖 Assunto em negrito
            richTextBox1.SelectionFont = new System.Drawing.Font("Arial", 9, FontStyle.Bold);
            richTextBox1.AppendText("📖 Assunto:\n\n");

            // Texto normal após o título
            richTextBox1.SelectionFont = new System.Drawing.Font("Arial", 9, FontStyle.Regular);
            richTextBox1.AppendText($"{assunto}\n\n");

            // 💬 Comentário em negrito  
            richTextBox1.SelectionFont = new System.Drawing.Font("Arial", 9, FontStyle.Bold);
            richTextBox1.AppendText("💬 Comentário:\n\n");

            // Texto normal após o título
            richTextBox1.SelectionFont = new System.Drawing.Font("Arial", 9, FontStyle.Regular);
            richTextBox1.AppendText($"{comentario}\n\n");

            // 📅 Data em negrito
            richTextBox1.SelectionFont = new System.Drawing.Font("Arial", 9, FontStyle.Bold);
            richTextBox1.AppendText("📅 Data:\n\n");

            // Texto normal após o título
            richTextBox1.SelectionFont = new System.Drawing.Font("Arial", 9, FontStyle.Regular);
            richTextBox1.AppendText($"{dataReuniao}\n\n");

            // Linha separadora
            richTextBox1.AppendText("\n");

            //labelEC.Text = codigoEC;
        }


    }
}
