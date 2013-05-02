using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ActivityEvidence
{
	/// <summary>
	/// Summary description for ClaimDataForm.
	/// </summary>
	public class ClaimDataForm : System.Windows.Forms.Form
	{
		internal System.Windows.Forms.ListView ClaimListView;
		internal System.Windows.Forms.ColumnHeader columnClaimCode;
		private System.Windows.Forms.ColumnHeader columnSunday;
		private System.Windows.Forms.ColumnHeader columnSaturday;
		private System.Windows.Forms.ColumnHeader columnMonday;
		private System.Windows.Forms.ColumnHeader columnTuesday;
		private System.Windows.Forms.ColumnHeader columnWednesday;
		private System.Windows.Forms.ColumnHeader columnThursday;
		private System.Windows.Forms.ColumnHeader columnFriday;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ClaimDataForm()
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
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
		private void InitializeComponent()
		{
			this.ClaimListView = new System.Windows.Forms.ListView();
			this.columnClaimCode = new System.Windows.Forms.ColumnHeader();
			this.columnSaturday = new System.Windows.Forms.ColumnHeader();
			this.columnSunday = new System.Windows.Forms.ColumnHeader();
			this.columnMonday = new System.Windows.Forms.ColumnHeader();
			this.columnTuesday = new System.Windows.Forms.ColumnHeader();
			this.columnWednesday = new System.Windows.Forms.ColumnHeader();
			this.columnThursday = new System.Windows.Forms.ColumnHeader();
			this.columnFriday = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// ClaimListView
			// 
			this.ClaimListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							this.columnClaimCode,
																							this.columnSaturday,
																							this.columnSunday,
																							this.columnMonday,
																							this.columnTuesday,
																							this.columnWednesday,
																							this.columnThursday,
																							this.columnFriday});
			this.ClaimListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ClaimListView.FullRowSelect = true;
			this.ClaimListView.GridLines = true;
			this.ClaimListView.LabelWrap = false;
			this.ClaimListView.Location = new System.Drawing.Point(0, 0);
			this.ClaimListView.MultiSelect = false;
			this.ClaimListView.Name = "ClaimListView";
			this.ClaimListView.Size = new System.Drawing.Size(544, 273);
			this.ClaimListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.ClaimListView.TabIndex = 0;
			this.ClaimListView.View = System.Windows.Forms.View.Details;
			// 
			// columnClaimCode
			// 
			this.columnClaimCode.Text = "Claim Code";
			this.columnClaimCode.Width = 100;
			// 
			// columnSaturday
			// 
			this.columnSaturday.Text = "Saturday";
			this.columnSaturday.Width = 55;
			// 
			// columnSunday
			// 
			this.columnSunday.Text = "Sunday";
			this.columnSunday.Width = 48;
			// 
			// columnMonday
			// 
			this.columnMonday.Text = "Monday";
			this.columnMonday.Width = 51;
			// 
			// columnTuesday
			// 
			this.columnTuesday.Text = "Tuesday";
			this.columnTuesday.Width = 54;
			// 
			// columnWednesday
			// 
			this.columnWednesday.Text = "Wednesday";
			this.columnWednesday.Width = 69;
			// 
			// columnThursday
			// 
			this.columnThursday.Text = "Thursday";
			this.columnThursday.Width = 57;
			// 
			// columnFriday
			// 
			this.columnFriday.Text = "Friday";
			this.columnFriday.Width = 40;
			// 
			// ClaimDataForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(544, 273);
			this.Controls.Add(this.ClaimListView);
			this.Name = "ClaimDataForm";
			this.Text = "ClaimDataForm";
			this.Activated += new System.EventHandler(this.ClaimDataForm_Activated);
			this.ResumeLayout(false);

		}
		#endregion

		private void ClaimDataForm_Activated(object sender, System.EventArgs e)
		{
			int width = 12;
			foreach (ColumnHeader column in ClaimListView.Columns)
				width += column.Width;
			this.Width = width;
		}
	}
}
