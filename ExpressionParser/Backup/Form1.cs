using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ExpressionParser
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtbx_Response;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ComboBox txtbxInput;
		private System.Windows.Forms.CheckBox ckbxFormula;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Summary of Form1.
		/// </summary>
		/// 		
		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <summary>
		/// Summary of Dispose.
		/// </summary>
		/// <param name=disposing></param>
		/// 		
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		/// <summary>
		/// Summary of InitializeComponent.
		/// </summary>
		/// 		
		private void InitializeComponent()
		{
			this.txtbx_Response = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.txtbxInput = new System.Windows.Forms.ComboBox();
			this.ckbxFormula = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// txtbx_Response
			// 
			this.txtbx_Response.Location = new System.Drawing.Point(8, 56);
			this.txtbx_Response.Multiline = true;
			this.txtbx_Response.Name = "txtbx_Response";
			this.txtbx_Response.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtbx_Response.Size = new System.Drawing.Size(280, 208);
			this.txtbx_Response.TabIndex = 3;
			this.txtbx_Response.Text = "";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(240, 8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(48, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Parse";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// txtbxInput
			// 
			this.txtbxInput.Location = new System.Drawing.Point(8, 8);
			this.txtbxInput.Name = "txtbxInput";
			this.txtbxInput.Size = new System.Drawing.Size(224, 21);
			this.txtbxInput.TabIndex = 0;
			// 
			// ckbxFormula
			// 
			this.ckbxFormula.Location = new System.Drawing.Point(8, 32);
			this.ckbxFormula.Name = "ckbxFormula";
			this.ckbxFormula.Size = new System.Drawing.Size(80, 16);
			this.ckbxFormula.TabIndex = 2;
			this.ckbxFormula.Text = "&Formula";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(296, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.ckbxFormula,
																		  this.txtbxInput,
																		  this.button1,
																		  this.txtbx_Response});
			this.Name = "Form1";
			this.Text = "Expression Parser";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// <summary>
		/// Summary of Main.
		/// </summary>
		/// 		
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
		/// <summary>
		/// Summary of ClearResult.
		/// </summary>
		/// <param name=szResult></param>
		/// 		
		private void ClearResult( string szResult )
		{
			this.txtbx_Response.Text = "";
		}
		/// <summary>
		/// Summary of WriteResult.
		/// </summary>
		/// <param name=szResult></param>
		/// 		
		private void WriteResult( string szResult )
		{
			this.txtbx_Response.Text += szResult + "\r\n";
		}
		/// <summary>
		/// Summary of button1_Click.
		/// </summary>
		/// <param name=sender></param>
		/// <param name=e></param>
		/// 		
		private void button1_Click(object sender, System.EventArgs e)
		{
			try
			{
				RPNParser parser = new RPNParser();
				ArrayList arrExpr = parser.GetPostFixNotation( this.txtbxInput.Text, 
									Type.GetType("System.Int64" ), this.ckbxFormula.Checked );
				string szResult = parser.Convert2String(arrExpr);
				WriteResult( szResult );
				WriteResult( parser.EvaluateRPN(arrExpr, Type.GetType("System.Int64"), null ).ToString() );
			}
			catch( Exception ex )
			{
				WriteResult( "Exception - " + ex.Message );
			}
			this.txtbxInput.Items.Remove(this.txtbxInput.Text );
			this.txtbxInput.Items.Add(this.txtbxInput.Text );
		}
	}
}
