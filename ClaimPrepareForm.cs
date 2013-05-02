using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using ActivityEvidence.EvidenceService;
using GlacialComponents.Controls;

namespace ActivityEvidence
{
	/// <summary>
	/// Summary description for ClaimForm.
	/// </summary>
	public class ClaimPrepareForm : System.Windows.Forms.Form
	{
		private const float ONCALL_STANDBY = 0.123F;
		private const float ONCALL_WORK = 1.915F;
		
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.Button applyChangesButton;
		private GlacialComponents.Controls.GlacialList logGlacialList;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button calculateClaimButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		ActivityEvidence.EvidenceService.ActivitiesEvidence _service;
		R.Helpers.Settings _settings;
		ClaimStruct[] _claimData;
		DateTime _startDay;
		private System.Windows.Forms.MonthCalendar claimWeekCalendar;
		static string _clientVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

		public ClaimPrepareForm(ActivitiesEvidence Service, R.Helpers.Settings Settings)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			_service = Service;
			_settings = Settings;
			claimWeekCalendar.Parent.Width = claimWeekCalendar.Width + 16;
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
			GlacialComponents.Controls.GLColumn glColumn1 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn2 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn3 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn4 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn5 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn6 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn7 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn8 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn9 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn10 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn11 = new GlacialComponents.Controls.GLColumn();
			this.panel2 = new System.Windows.Forms.Panel();
			this.claimWeekCalendar = new System.Windows.Forms.MonthCalendar();
			this.calculateClaimButton = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.closeButton = new System.Windows.Forms.Button();
			this.applyChangesButton = new System.Windows.Forms.Button();
			this.logGlacialList = new GlacialComponents.Controls.GlacialList();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.claimWeekCalendar);
			this.panel2.Controls.Add(this.calculateClaimButton);
			this.panel2.Controls.Add(this.button1);
			this.panel2.Controls.Add(this.closeButton);
			this.panel2.Controls.Add(this.applyChangesButton);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel2.Location = new System.Drawing.Point(1040, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(168, 597);
			this.panel2.TabIndex = 10;
			// 
			// claimWeekCalendar
			// 
			this.claimWeekCalendar.FirstDayOfWeek = System.Windows.Forms.Day.Saturday;
			this.claimWeekCalendar.Location = new System.Drawing.Point(8, 8);
			this.claimWeekCalendar.Name = "claimWeekCalendar";
			this.claimWeekCalendar.TabIndex = 5;
			this.claimWeekCalendar.SizeChanged += new System.EventHandler(this.claimWeekCalendar_SizeChanged);
			this.claimWeekCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.claimWeekCalendar_DateSelected);
			// 
			// calculateClaimButton
			// 
			this.calculateClaimButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.calculateClaimButton.Location = new System.Drawing.Point(8, 200);
			this.calculateClaimButton.Name = "calculateClaimButton";
			this.calculateClaimButton.Size = new System.Drawing.Size(152, 23);
			this.calculateClaimButton.TabIndex = 4;
			this.calculateClaimButton.Text = "Calculate";
			this.calculateClaimButton.Click += new System.EventHandler(this.calculateClaimButton_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(8, 315);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(88, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "Show ...";
			this.button1.Visible = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// closeButton
			// 
			this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.closeButton.Location = new System.Drawing.Point(8, 568);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(152, 23);
			this.closeButton.TabIndex = 2;
			this.closeButton.Text = "Close";
			this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
			// 
			// applyChangesButton
			// 
			this.applyChangesButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.applyChangesButton.Enabled = false;
			this.applyChangesButton.Location = new System.Drawing.Point(8, 168);
			this.applyChangesButton.Name = "applyChangesButton";
			this.applyChangesButton.Size = new System.Drawing.Size(152, 23);
			this.applyChangesButton.TabIndex = 0;
			this.applyChangesButton.Text = "Apply changes";
			this.applyChangesButton.Click += new System.EventHandler(this.applyChangesButton_Click);
			// 
			// logGlacialList
			// 
			this.logGlacialList.AllowColumnResize = true;
			this.logGlacialList.AllowMultiselect = false;
			this.logGlacialList.AlternateBackground = System.Drawing.Color.DarkGreen;
			this.logGlacialList.AlternatingColors = false;
			this.logGlacialList.AutoHeight = true;
			this.logGlacialList.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.logGlacialList.BackgroundStretchToFit = true;
			glColumn1.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn1.CheckBoxes = false;
			glColumn1.ImageIndex = -1;
			glColumn1.Name = "dateColumn";
			glColumn1.NumericSort = false;
			glColumn1.Text = "Date";
			glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn1.Width = 65;
			glColumn2.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn2.CheckBoxes = false;
			glColumn2.ImageIndex = -1;
			glColumn2.Name = "customerColumn";
			glColumn2.NumericSort = false;
			glColumn2.Text = "Customer";
			glColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn2.Width = 125;
			glColumn3.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn3.CheckBoxes = false;
			glColumn3.ImageIndex = -1;
			glColumn3.Name = "activityColumn";
			glColumn3.NumericSort = false;
			glColumn3.Text = "Activity";
			glColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn3.Width = 155;
			glColumn4.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn4.CheckBoxes = false;
			glColumn4.ImageIndex = -1;
			glColumn4.Name = "startTimeColumn";
			glColumn4.NumericSort = false;
			glColumn4.Text = "Start time";
			glColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn4.Width = 65;
			glColumn5.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn5.CheckBoxes = false;
			glColumn5.ImageIndex = -1;
			glColumn5.Name = "durationColumn";
			glColumn5.NumericSort = false;
			glColumn5.Text = "Duration";
			glColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn5.Width = 51;
			glColumn6.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn6.CheckBoxes = false;
			glColumn6.ImageIndex = -1;
			glColumn6.Name = "ticketColumn";
			glColumn6.NumericSort = true;
			glColumn6.Text = "Ticket number";
			glColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn6.Width = 80;
			glColumn7.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.CheckBox;
			glColumn7.CheckBoxes = false;
			glColumn7.ImageIndex = -1;
			glColumn7.Name = "standardClaimColumn";
			glColumn7.NumericSort = false;
			glColumn7.Text = "Standard Claim";
			glColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn7.Width = 86;
			glColumn8.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.CheckBox;
			glColumn8.CheckBoxes = false;
			glColumn8.ImageIndex = -1;
			glColumn8.Name = "plannedOvertimeColumn";
			glColumn8.NumericSort = false;
			glColumn8.Text = "Planned Overtime";
			glColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn8.Width = 99;
			glColumn9.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.TextBox;
			glColumn9.CheckBoxes = false;
			glColumn9.ImageIndex = -1;
			glColumn9.Name = "claimAccountColumn";
			glColumn9.NumericSort = false;
			glColumn9.Text = "Account";
			glColumn9.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn9.Width = 60;
			glColumn10.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.TextBox;
			glColumn10.CheckBoxes = false;
			glColumn10.ImageIndex = -1;
			glColumn10.Name = "claimWorkItemColumn";
			glColumn10.NumericSort = false;
			glColumn10.Text = "Work Item";
			glColumn10.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn10.Width = 70;
			glColumn11.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
			glColumn11.CheckBoxes = false;
			glColumn11.ImageIndex = -1;
			glColumn11.Name = "commentColumn";
			glColumn11.NumericSort = false;
			glColumn11.Text = "Comment";
			glColumn11.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			glColumn11.Width = 180;
			this.logGlacialList.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
																							   glColumn1,
																							   glColumn2,
																							   glColumn3,
																							   glColumn4,
																							   glColumn5,
																							   glColumn6,
																							   glColumn7,
																							   glColumn8,
																							   glColumn9,
																							   glColumn10,
																							   glColumn11});
			this.logGlacialList.ControlStyle = GlacialComponents.Controls.GLControlStyles.Normal;
			this.logGlacialList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logGlacialList.FullRowSelect = true;
			this.logGlacialList.GridColor = System.Drawing.Color.LightGray;
			this.logGlacialList.GridLines = GlacialComponents.Controls.GLGridLines.gridBoth;
			this.logGlacialList.GridLineStyle = GlacialComponents.Controls.GLGridLineStyles.gridSolid;
			this.logGlacialList.GridTypes = GlacialComponents.Controls.GLGridTypes.gridNormal;
			this.logGlacialList.HeaderHeight = 15;
			this.logGlacialList.HeaderVisible = true;
			this.logGlacialList.HeaderWordWrap = false;
			this.logGlacialList.HotColumnTracking = false;
			this.logGlacialList.HotItemTracking = false;
			this.logGlacialList.HotTrackingColor = System.Drawing.Color.LightGray;
			this.logGlacialList.HoverEvents = false;
			this.logGlacialList.HoverTime = 1;
			this.logGlacialList.ImageList = null;
			this.logGlacialList.ItemHeight = 17;
			this.logGlacialList.ItemWordWrap = false;
			this.logGlacialList.Location = new System.Drawing.Point(0, 0);
			this.logGlacialList.Name = "logGlacialList";
			this.logGlacialList.Selectable = true;
			this.logGlacialList.SelectedTextColor = System.Drawing.Color.White;
			this.logGlacialList.SelectionColor = System.Drawing.Color.DarkBlue;
			this.logGlacialList.ShowBorder = true;
			this.logGlacialList.ShowFocusRect = false;
			this.logGlacialList.Size = new System.Drawing.Size(1040, 597);
			this.logGlacialList.SortType = GlacialComponents.Controls.SortTypes.InsertionSort;
			this.logGlacialList.SuperFlatHeaderColor = System.Drawing.Color.White;
			this.logGlacialList.TabIndex = 9;
			this.logGlacialList.ItemChangedEvent += new GlacialComponents.Controls.ChangedEventHandler(this.logGlacialList_ItemChangedEvent);
			this.logGlacialList.EmbeddedActivating += new GlacialComponents.Controls.GlacialList.EmbeddedActivatingEventHandler(this.logGlacialList_EmbeddedActivating);
			// 
			// ClaimPrepareForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(1208, 597);
			this.Controls.Add(this.logGlacialList);
			this.Controls.Add(this.panel2);
			this.MinimumSize = new System.Drawing.Size(176, 288);
			this.Name = "ClaimPrepareForm";
			this.Text = "ClaimForm";
			this.Load += new System.EventHandler(this.ClaimForm_Load);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void ClaimForm_Load(object sender, System.EventArgs e)
		{
			_startDay = (DateTime.Today.DayOfWeek == DayOfWeek.Saturday ? DateTime.Today : DateTime.Today.AddDays(-(Convert.ToInt32(DateTime.Today.DayOfWeek) + 1)));
			claimWeekCalendar.SelectionStart = _startDay;
			claimWeekCalendar.SelectionEnd = _startDay.AddDays(6);
			GetData();
		}

		private void GetData()
		{
			logGlacialList.Items.Clear();
			try
			{
				_claimData = _service.GetActivitiesForClaim(_settings["AdminID"], _clientVersion, _startDay);
				foreach (ClaimStruct claimItem in _claimData)
				{
					GLItem glItem = new GLItem(logGlacialList);
					glItem.Tag = new ActivityPresentation(claimItem);
					GLSubItem glSubItem;
				
					glSubItem = new GLSubItem();
					glSubItem.Text = claimItem.Activity.StartTime.Date.ToShortDateString();
					glItem.SubItems.Add(glSubItem);
					glSubItem = new GLSubItem();
					glSubItem.Text = claimItem.Activity.Customer;
					glItem.SubItems.Add(glSubItem);
					glSubItem = new GLSubItem();
					glSubItem.Text = claimItem.Activity.Activity;
					glItem.SubItems.Add(glSubItem);
					glSubItem = new GLSubItem();
					glSubItem.Text = claimItem.Activity.StartTime.TimeOfDay.ToString();
					glItem.SubItems.Add(glSubItem);
					glSubItem = new GLSubItem();
					glSubItem.Text = claimItem.Activity.WorkDuration.ToString();
					glItem.SubItems.Add(glSubItem);
					glSubItem = new GLSubItem();
					glSubItem.Text = claimItem.Activity.TicketNumber;
					glItem.SubItems.Add(glSubItem);
					glSubItem = new GLSubItem();
					glSubItem.Text = claimItem.StandardClaim.ToString();
					glItem.SubItems.Add(glSubItem);
					glSubItem = new GLSubItem();
					glSubItem.Text = claimItem.PlannedOvertime.ToString();
					glItem.SubItems.Add(glSubItem);
					glSubItem = new GLSubItem();
					glSubItem.Text = claimItem.ClaimCode.Account;
					if (claimItem.StandardClaim)
						glSubItem.ForeColor = Color.Gray;
					glItem.SubItems.Add(glSubItem);
					glSubItem = new GLSubItem();
					glSubItem.Text = claimItem.ClaimCode.WorkItem;
					if (claimItem.StandardClaim)
						glSubItem.ForeColor = Color.Gray;
					glItem.SubItems.Add(glSubItem);
					glSubItem = new GLSubItem();
					glSubItem.Text = claimItem.Activity.Comment;
					glItem.SubItems.Add(glSubItem);

					logGlacialList.Items.Add(glItem);
				}
			}
			catch (System.Web.Services.Protocols.SoapException exception)
			{
				if (exception.Message == "No activity entries")
					this._claimData = new ClaimStruct[0];
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "Getting claim data error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			logGlacialList.Refresh();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			string tmp = "";
			foreach (GLColumn column in logGlacialList.Columns)
				tmp += column.Name + " " + column.Width.ToString() + "\n";
			MessageBox.Show(tmp);
		}

		private void calculateClaimButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				ClaimMatrixDictionary claimMatrix = CalculateClaim();
				//claimMatrix.Keys.Sort();
				WeekDayHoursDictionary weekDayTotal = new WeekDayHoursDictionary();
				// getting result
				ClaimDataForm resultForm = new ClaimDataForm();
				string maxString = "";
				foreach (ClaimCode claimCode in claimMatrix.Keys)
				{
					ListViewItem currItem = resultForm.ClaimListView.Items.Add(claimCode.ToString());
					currItem.SubItems.AddRange(new string[] {"", "", "", "", "", "", ""});
					if (claimCode.ToString().Length > maxString.Length)
						maxString = claimCode.ToString();
					foreach (DayOfWeek weekDay in claimMatrix[claimCode].Keys)
					{
						currItem.SubItems[(Convert.ToInt32(weekDay) + 1 - (int)((Convert.ToInt32(weekDay) + 1) / 7) * 7) + 1].Text = claimMatrix[claimCode][weekDay].ToString(); //Math.Round(claimMatrix[claimCode][weekDay], 1).ToString();
						weekDayTotal[weekDay] += claimMatrix[claimCode][weekDay];
					}
				}
				ListViewItem totalItem = resultForm.ClaimListView.Items.Add("Total");
				totalItem.Font = new Font(totalItem.Font, FontStyle.Bold);
				totalItem.SubItems.AddRange(new string[]
				{
					weekDayTotal[DayOfWeek.Saturday].ToString(),
					weekDayTotal[DayOfWeek.Sunday].ToString(),
					weekDayTotal[DayOfWeek.Monday].ToString(),
					weekDayTotal[DayOfWeek.Tuesday].ToString(),
					weekDayTotal[DayOfWeek.Wednesday].ToString(),
					weekDayTotal[DayOfWeek.Thursday].ToString(),
					weekDayTotal[DayOfWeek.Friday].ToString()
				});
				resultForm.columnClaimCode.Width = (int)Graphics.FromImage(new Bitmap(1, 1)).MeasureString(maxString, resultForm.ClaimListView.Items[0].Font).Width;
				resultForm.Text = String.Format(Properties.Resources.ClaimResultWindowCaption, _startDay.ToShortDateString(), _startDay.AddDays(6).ToShortDateString());
				resultForm.Show();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "Claim calculation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void claimWeekCalendar_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
		{
			_startDay = (e.Start.DayOfWeek == DayOfWeek.Saturday ? e.Start : e.Start.AddDays(-(Convert.ToInt32(e.Start.DayOfWeek) + 1)));
			claimWeekCalendar.SelectionStart = _startDay;
			claimWeekCalendar.SelectionEnd = _startDay.AddDays(6);
			GetData();
		}

		private void logGlacialList_EmbeddedActivating(object source, GlacialComponents.Controls.EmbeddedActivatingEventArgs e)
		{
			if (e.SubItem.Column.Name == "standardClaimColumn")
			{
				if (e.SubItem.Value != null)
					e.Cancel = ((ActivityPresentation)e.SubItem.ParentItem.Tag).Claim.StandardClaim;
			}
			else if (e.SubItem.Column.Name == "plannedOvertimeColumn")
			{
				if (e.SubItem.Value != null)
					e.Cancel = !((ActivityPresentation)e.SubItem.ParentItem.Tag).Claim.ClaimCode.Overtime;
			}
		}

		private void logGlacialList_ItemChangedEvent(object source, GlacialComponents.Controls.ChangedEventArgs e)
		{
			if (e.ChangedType == ChangedTypes.SubItemChanged)
			{
				if (e.Column.Name == "standardClaimColumn")
				{
					((ActivityPresentation)e.Item.Tag).Claim.StandardClaim = true;
					e.Item.SubItems[8].ForeColor = Color.Gray;
					e.Item.SubItems[9].ForeColor = Color.Gray;
					e.Item.SubItems[10].ForeColor = Color.Gray;
					((ActivityPresentation)e.Item.Tag).Modified = ModificationStatus.Modified;
				}
				else if (e.Column.Name == "claimAccountColumn")
				{
					if (!((ActivityPresentation)e.Item.Tag).OldValues.Contains("ClaimAccount"))
						((ActivityPresentation)e.Item.Tag).OldValues.Add("ClaimAccount", null);
					((ActivityPresentation)e.Item.Tag).OldValues["ClaimAccount"] = ((ActivityPresentation)e.Item.Tag).Claim.ClaimCode.Account;
					((ActivityPresentation)e.Item.Tag).Claim.ClaimCode.Account = e.SubItem.Text;
					((ActivityPresentation)e.Item.Tag).Claim.StandardClaim = false;
					e.Item.SubItems[6].Value = false;
					e.Item.SubItems[8].ForeColor = Color.Black;
					e.Item.SubItems[9].ForeColor = Color.Black;
					e.Item.SubItems[10].ForeColor = Color.Black;
					((ActivityPresentation)e.Item.Tag).Modified = ModificationStatus.Modified;
				}
				else if (e.Column.Name == "claimWorkItemColumn")
				{
					if (!((ActivityPresentation)e.Item.Tag).OldValues.Contains("ClaimWorkItem"))
						((ActivityPresentation)e.Item.Tag).OldValues.Add("ClaimWorkItem", null);
					((ActivityPresentation)e.Item.Tag).OldValues["ClaimWorkItem"] = ((ActivityPresentation)e.Item.Tag).Claim.ClaimCode.WorkItem;
					((ActivityPresentation)e.Item.Tag).Claim.ClaimCode.WorkItem = e.SubItem.Text;
					((ActivityPresentation)e.Item.Tag).Claim.StandardClaim = false;
					e.Item.SubItems[6].Value = false;
					e.Item.SubItems[8].ForeColor = Color.Black;
					e.Item.SubItems[9].ForeColor = Color.Black;
					e.Item.SubItems[10].ForeColor = Color.Black;
					((ActivityPresentation)e.Item.Tag).Modified = ModificationStatus.Modified;
				}
				else if (e.Column.Name == "plannedOvertimeColumn")
				{
					((ActivityPresentation)e.Item.Tag).Claim.PlannedOvertime = (bool)e.SubItem.Value;
					//e.Item.SubItems[7].Value = true;
					((ActivityPresentation)e.Item.Tag).Modified = ModificationStatus.Modified;
				}
				applyChangesButton.Enabled = true;
			}
		}

		private void applyChangesButton_Click(object sender, System.EventArgs e)
		{
			bool successfully_applied = true;
			foreach (GLItem item in logGlacialList.Items)
			{
				ActivityPresentation activityItem = (ActivityPresentation)item.Tag;
				if (activityItem.Modified == ModificationStatus.Modified)
				{
					try
					{
						_service.UpdateActivityForClaim(_settings["AdminID"], _clientVersion, activityItem.Claim);
						activityItem.Modified = ModificationStatus.None;
					}
					catch (Exception)
					{
						successfully_applied = false;
					}
				}
			}
			applyChangesButton.Enabled = !successfully_applied;
		}

		private void closeButton_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private ClaimMatrixDictionary CalculateClaim()
		{
			double tmp;
			WeekDayHoursDictionary officeWorkHours = new WeekDayHoursDictionary();
			ClaimMatrixDictionary claimHours = new ClaimMatrixDictionary();
			ClaimCode defaultClaimCode = _service.GetDefaultClaimCode(_settings["AdminID"], _clientVersion);
			OncallDateStruct[] oncallDays = _service.GetOncallSchedule(_settings["AdminID"], _clientVersion, _startDay, _startDay.AddDays(6));
			ClaimCode[] oncallStandByGroups = PrepareStandByOncallGroups(oncallDays != null);
			WeekDayHoursDictionary standByHours = PrepareStandByHours(oncallDays);
			DateTime[] publicHolidays = _service.GetPublicHolidays();

			if (_claimData != null)
			{
				foreach (ClaimStruct claimItem in _claimData)
				{
					DayOfWeek weekDay = claimItem.Activity.StartTime.DayOfWeek;
					double workDuration = claimItem.Activity.WorkDuration; //(float)Math.Round(claimItem.Activity.WorkDuration / 60F, 2);
					ClaimCode claimCode = claimItem.ClaimCode;

					if (claimCode.Overtime)
					{
						if (claimItem.PlannedOvertime)
						{
							if (!claimHours.Contains(claimCode))
								claimHours.Add(claimCode);
							claimHours[claimCode][weekDay] += workDuration;
						}
						else
						{
							if (workDuration > standByHours[weekDay])
								throw new Exception("Oncall work duration is more oncall hours for this day (" + claimItem.Activity.StartTime.ToShortDateString() + ")");
							standByHours[weekDay] -= workDuration;
							claimCode.CalledIn = true;
							if (!claimHours.Contains(claimCode))
								claimHours.Add(claimCode);
							tmp = claimHours[claimCode][weekDay];
							claimHours[claimCode][weekDay] += workDuration; // * ONCALL_WORK; //(float)Math.Round(workDuration * ONCALL_WORK, 2);
							tmp = claimHours[claimCode][weekDay];
						}
					}
					else
					{
						officeWorkHours[weekDay] += workDuration;
#if DEBUG
						System.Diagnostics.Debug.WriteLine(weekDay + "\t" + officeWorkHours[weekDay]);
#endif
											
						if (!claimHours.Contains(claimCode))
							claimHours.Add(claimCode);
						claimHours[claimCode][weekDay] += workDuration;
#if DEBUG
						tmp = claimHours[claimCode][weekDay];
						System.Diagnostics.Debug.WriteLine(claimCode + "\t" + weekDay + "\t" + claimHours[claimCode][weekDay]);
#endif
					}
				}
			}

			// rounding to 8 hours per day
			foreach (DayOfWeek weekDay in officeWorkHours.Keys)
			{
				if ((weekDay == DayOfWeek.Saturday) || (weekDay == DayOfWeek.Sunday) ||
					IsPublicHoliday(weekDay, publicHolidays))
					continue;
#if DEBUG
				tmp = claimHours[defaultClaimCode] != null ? claimHours[defaultClaimCode][weekDay] : 0;
				tmp = officeWorkHours[weekDay];
#endif
				double dayWorkDurationDifference = 480 - officeWorkHours[weekDay]; //(float)Math.Round(8 - officeWorkHours[weekDay], 2);
				if (dayWorkDurationDifference != 0)
				{
					if (!claimHours.Contains(defaultClaimCode))
						claimHours.Add(defaultClaimCode);
					claimHours[defaultClaimCode][weekDay] = claimHours[defaultClaimCode][weekDay] + dayWorkDurationDifference; //(float)Math.Round(claimHours[defaultClaimCode][weekDay] + dayWorkDurationDifference, 1);
				}
#if DEBUG
				tmp = claimHours[defaultClaimCode][weekDay];
#endif
			}

			// adding standby oncall hours
			foreach (DayOfWeek weekDay in standByHours.Keys)
			{
				if (standByHours[weekDay] == 0)
					continue;
				tmp = standByHours[weekDay];
				double standByHoursPerGroup = standByHours[weekDay] / oncallStandByGroups.Length; //Math.Round(standByHours[weekDay] / oncallStandByGroups.Length, 1);
				ClaimCode firstStandByClaimCode = null;
				//				float tmp = standByHours[weekDay];
				//				tmp = tmp * 0.123F;
				//				tmp = (float)Math.Round(tmp, 1);
				double standByHoursRemain = standByHours[weekDay]; // Math.Round(standByHours[weekDay] /60 * ONCALL_STANDBY, 1) * 60; //Math.Round(standByHours[weekDay] * ONCALL_STANDBY, 1);
				foreach (ClaimCode standByClaimCode in oncallStandByGroups)
				{
					if (!claimHours.Contains(standByClaimCode))
						claimHours.Add(standByClaimCode);
					if (firstStandByClaimCode == null)
					{
						firstStandByClaimCode = standByClaimCode;
					}
					else
					{
						double hoursToClaim = standByHoursPerGroup; // * ONCALL_STANDBY; //Math.Round(standByHoursPerGroup * ONCALL_STANDBY, 1);
						standByHoursRemain -= Math.Round(hoursToClaim / 60, 1) * 60;
						tmp = claimHours[standByClaimCode][weekDay];
						claimHours[standByClaimCode][weekDay] += hoursToClaim;
						tmp = claimHours[standByClaimCode][weekDay];
					}
				}
#if DEBUG
				tmp = claimHours[firstStandByClaimCode][weekDay];
#endif
				claimHours[firstStandByClaimCode][weekDay] += standByHoursRemain;
#if DEBUG
				tmp = claimHours[firstStandByClaimCode][weekDay];
#endif
			}

			//string testResult = "";
			foreach (ClaimCode code in claimHours.Keys)
			{
				//testResult += code.ToString();
				//System.Diagnostics.Debug.Write(code.ToString());
				foreach (DayOfWeek weekDay in claimHours[code].Keys)
				{
					//testResult += "\t" + claimHours[code][weekDay].ToString();
					//System.Diagnostics.Debug.Write("\t" + Math.Round(claimHours[code][weekDay] / 60, 1).ToString());
					claimHours[code][weekDay] = Math.Round(claimHours[code][weekDay] / 60, 1);
				}
				//testResult += "\n";
				//System.Diagnostics.Debug.WriteLine("");
			}
			return claimHours;
		}

		private void claimWeekCalendar_SizeChanged(object sender, System.EventArgs e)
		{
			((Control)sender).Parent.Width = ((Control)sender).Width + 16;
		}

		private ClaimCode[] PrepareStandByOncallGroups(bool HasOncall)
		{
			if (HasOncall)
			{
				ClaimCode[] tmpGroups = _service.GetOncallStandbyGroups(_settings["AdminID"], _clientVersion);
				foreach (ClaimCode group in tmpGroups)
					group.OnCallStandby = true;
				return tmpGroups;
			}
			else
				return null;
		}

		private WeekDayHoursDictionary PrepareStandByHours(OncallDateStruct[] OncallDays)
		{
			WeekDayHoursDictionary tmp = new WeekDayHoursDictionary();
			if (OncallDays != null)
			{
				foreach (OncallDateStruct oncallDay in OncallDays)
				{
					tmp[oncallDay.Date.DayOfWeek] = oncallDay.OncallHours * 60;
				}
			}
			return tmp;
		}

		private DateTime GetDateByWeekDay(DayOfWeek WeekDay)
		{
			int tmp;
			if (WeekDay == DayOfWeek.Saturday)
				tmp = 0;
			else if (WeekDay == DayOfWeek.Sunday)
				tmp = 1;
			else
				tmp = (int)WeekDay + 1;

			return _startDay.AddDays(tmp);
		}

		private bool IsPublicHoliday(DayOfWeek WeekDay, DateTime[] PublicHolidays)
		{
			foreach (DateTime publicHoliday in PublicHolidays)
			{
				DateTime tmp = GetDateByWeekDay(WeekDay);
				if (GetDateByWeekDay(WeekDay) == publicHoliday)
				{
					return true;
				}
			}
			return false;
		}
	}
}
