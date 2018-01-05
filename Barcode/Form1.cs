using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Text;
using System.IO;

namespace Barcode
{
	/// <summary>
	/// Formulario encargado de mostrar como cargar una fuente personalizada,
	/// en este caso un BarCode e imprimir facilmente en un documento.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		#region Controles 
		private System.ComponentModel.Container components = null;		
		private System.Windows.Forms.ComboBox cbListaFonts;
		private System.Windows.Forms.Label lbCode;
		private System.Windows.Forms.Label lbBarCode;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox txtBarcode;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Button btPrint;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
		private System.Windows.Forms.PrintDialog printDialog1;
		#endregion Controles 

		#region Variables 

		private Font _Font;
		private string PATH_FONTS;

		#endregion Variables 		

		#region Eventos 

		private void cbListaFonts_SelectedIndexChanged_1(object sender, System.EventArgs e)
		{
			try
			{
				//Cargamos la fuente selecionada.
				CargarFuente( cbListaFonts.SelectedItem.ToString());
			}
			catch( Exception ex)
			{
				MessageBox.Show( ex.Message);
			}
		}

		private void btPrint_Click(object sender, System.EventArgs e)
		{
			try
			{
				printDialog1.Document = printDocument1;

					printDocument1.Print();
			}
			catch( Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			try
			{
				if( txtBarcode.Text == string.Empty)
					lbBarCode.Text = "Tienes que introducir un Código";
				else
				{			
					if( _Font != null)
					{
						lbBarCode.Font= _Font;
						lbBarCode.Text = FormatBarCode( txtBarcode.Text );
						lbCode.Text = FormatBarCode( txtBarcode.Text );
					}
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show( ex.Message);
			}
		}

		#endregion Eventos 

		#region Métodos Privados 

		/// <summary>
		/// Carga el ComboBox con las fuentes que se encuentran en la carpeta Fonts. 
		/// </summary>
		private void CargarListaFuentes()
		{
			try
			{
				DirectoryInfo dir = new DirectoryInfo( PATH_FONTS );
				if(dir.Exists)
				{
					FileInfo[] file = dir.GetFiles();
					//Si la carpeta existe recorremos su contenido en busca de Fuentes. 
					foreach( FileInfo fonts in file )
					{
						if(fonts.Extension == ".TTF")
						{
							cbListaFonts.Items.Add( fonts.Name );
						}
					}
					//Seleccionamos por defecto una fuente.
					cbListaFonts.SelectedIndex = 0;
				}
			}
			catch( Exception ex)
			{
				MessageBox.Show( ex.Message);
			}
		}

		/// <summary>
		/// Carga del fichero la Fuente Pasada como parámetro. 
		/// </summary>
		/// <param name="fuente">Fuente a Cargar del Archivo</param>
		private void CargarFuente(string fuente)
		{	
			PrivateFontCollection pfc = new PrivateFontCollection();
			pfc.AddFontFile( PATH_FONTS  + @"\" + fuente);
			FontFamily fontFamily = pfc.Families[0];
			_Font = new Font( fontFamily,30);					
		}

		/// <summary>
		/// Incluye al Código los * para que el lector lea correctamente el Código. 
		/// </summary>
		/// <param name="code">Código a Formatear.</param>
		/// <returns>Código con los *</returns>
		private string FormatBarCode(string code)
		{
			string barcode = string.Empty;
			barcode = string.Format("*{0}*",code);
			return barcode;
		}

		#endregion Métodos Privados 
		
		#region Documento de Impresion 

		private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			//Cargamos de nuevo la fuente por si acaso.
			CargarFuente( cbListaFonts.SelectedItem.ToString());
			e.Graphics.DrawString( "Muestra de Impresión código de Barras", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 30, 125);
			e.Graphics.DrawString( FormatBarCode( txtBarcode.Text ), _Font, Brushes.Black, 30,60);
		
		}

		#endregion Documento de Impresion 

		#region Constructor 

		public Form1()
		{
			InitializeComponent();
			PATH_FONTS = @"C:\Users\Cristhiam\Desktop\Barcode\Fonts";

			//Inicializamos la Fuente. 
			CargarListaFuentes();
		}

		#endregion Constructor 

		#region Dispose 

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion Dispose 		

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cbListaFonts = new System.Windows.Forms.ComboBox();
            this.lbCode = new System.Windows.Forms.Label();
            this.lbBarCode = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btPrint = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.SuspendLayout();
            // 
            // cbListaFonts
            // 
            this.cbListaFonts.ItemHeight = 13;
            this.cbListaFonts.Location = new System.Drawing.Point(152, 40);
            this.cbListaFonts.Name = "cbListaFonts";
            this.cbListaFonts.Size = new System.Drawing.Size(121, 21);
            this.cbListaFonts.TabIndex = 4;
            this.cbListaFonts.SelectedIndexChanged += new System.EventHandler(this.cbListaFonts_SelectedIndexChanged_1);
            // 
            // lbCode
            // 
            this.lbCode.Location = new System.Drawing.Point(24, 160);
            this.lbCode.Name = "lbCode";
            this.lbCode.Size = new System.Drawing.Size(248, 24);
            this.lbCode.TabIndex = 3;
            this.lbCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbBarCode
            // 
            this.lbBarCode.Location = new System.Drawing.Point(24, 120);
            this.lbBarCode.Name = "lbBarCode";
            this.lbBarCode.Size = new System.Drawing.Size(248, 40);
            this.lbBarCode.TabIndex = 2;
            this.lbBarCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbBarCode.Click += new System.EventHandler(this.lbBarCode_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(120, 80);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Generar";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(24, 40);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(112, 20);
            this.txtBarcode.TabIndex = 1;
            this.txtBarcode.Text = "125649852";
            this.txtBarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox1
            // 
            this.comboBox1.Location = new System.Drawing.Point(184, 32);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // btPrint
            // 
            this.btPrint.Location = new System.Drawing.Point(200, 80);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(75, 23);
            this.btPrint.TabIndex = 5;
            this.btPrint.Text = "Imprimir";
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(292, 195);
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.lbCode);
            this.Controls.Add(this.lbBarCode);
            this.Controls.Add(this.cbListaFonts);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtBarcode);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BarCode Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region Método Main 
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

        #endregion Método Main 

        private void lbBarCode_Click(object sender, EventArgs e)
        {

        }
    }
}
