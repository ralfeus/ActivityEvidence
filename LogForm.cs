using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using ActivityEvidence.EvidenceService;
using R.Helpers;
using GlacialComponents.Controls;
using System.Web.Services.Protocols;

namespace ActivityEvidence
{
	/// <summary>
	/// Summary description for LogForm.
	/// </summary>
	public class LogForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DateTimePicker firstDateTimePicker;
		private System.Windows.Forms.DateTimePicker lastDateTimePicker;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox customersComboBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.Button btnClear;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private ActivitiesEvidence _service;
		private Settings _settings;
		private GlacialComponents.Controls.GlacialList logGlacialList;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel filterPanel;
		private System.Windows.Forms.Button filterButton;
		private System.Windows.Forms.Button applyChangesButton;
		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.ContextMenu logContextMenu;
		private System.Windows.Forms.MenuItem removeMenuItem;
		private System.Windows.Forms.Label workTimeLabel;
		private System.Windows.Forms.ComboBox workTimeComboBox;
		private System.Windows.Forms.MenuItem addMenuItem;
		private ActivityPresentation[] _activities;
		private static LogForm _instance = null;

		private LogForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		private LogForm(ActivitiesEvidence WebService, Settings AppSettings)
			:this()
		{
			_service = WebService;
			_settings = AppSettings;
			ComboBox customerColumnTemplate = logGlacialList.Columns["customerColumn"].ActivatedEmbeddedControlTemplate as ComboBox;
			ComboBox activityColumnTemplate = logGlacialList.Columns["activityColumn"].ActivatedEmbeddedControlTemplate as ComboBox;
			if (customerColumnTemplate == null)
				throw new Exception("Wrong customer column template");
			if (activityColumnTemplate == null)
				throw new Exception("Wrong activity column template");

			customerColumnTemplate.Items.Clear();
			customersComboBox.Items.Add("---- No filter ----");
			foreach (ObjectStruct customer in _service.GetCustomers(_settings["AdminID"]))
			{
				customersComboBox.Items.Add(customer);
				customerColumnTemplate.Items.Add(customer);
			}
			customerColumnTemplate.Sorted = true;

			activityColumnTemplate.Items.Clear();
			foreach (ObjectStruct activity in _service.GetActivities(_settings["AdminID"]))
			{
				activityColumnTemplate.Items.Add(activity);
			}
			activityColumnTemplate.Sorted = true;

			((DateTimePicker)logGlacialList.Columns["dateColumn"].ActivatedEmbeddedControlTemplate).Format = DateTimePickerFormat.Short;
			((DateTimePicker)logGlacialList.Columns["startTimeColumn"].ActivatedEmbeddedControlTemplate).Format = DateTimePickerFormat.Time;
			((DateTimePicker)logGlacialList.Columns["startTimeColumn"].ActivatedEmbeddedControlTemplate).ShowUpDown = true;
			//((TextBox)logGlacialList.Columns["ticketColumn"].ActivatedEmbeddedControlTemplate).ReadOnly = true;
			((NumericUpDown)logGlacialList.Columns["durationColumn"].ActivatedEmbeddedControlTemplate).Minimum = 0;
			((NumericUpDown)logGlacialList.Columns["durationColumn"].ActivatedEmbeddedControlTemplate).Maximum = 999;
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
			_instance = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogForm));
            this.firstDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.lastDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.filterPanel = new System.Windows.Forms.Panel();
            this.workTimeLabel = new System.Windows.Forms.Label();
            this.workTimeComboBox = new System.Windows.Forms.ComboBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.customersComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.logGlacialList = new GlacialComponents.Controls.GlacialList();
            this.logContextMenu = new System.Windows.Forms.ContextMenu();
            this.addMenuItem = new System.Windows.Forms.MenuItem();
            this.removeMenuItem = new System.Windows.Forms.MenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.closeButton = new System.Windows.Forms.Button();
            this.filterButton = new System.Windows.Forms.Button();
            this.applyChangesButton = new System.Windows.Forms.Button();
            this.filterPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // firstDateTimePicker
            // 
            this.firstDateTimePicker.Location = new System.Drawing.Point(64, 5);
            this.firstDateTimePicker.Name = "firstDateTimePicker";
            this.firstDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.firstDateTimePicker.TabIndex = 1;
            // 
            // lastDateTimePicker
            // 
            this.lastDateTimePicker.Location = new System.Drawing.Point(64, 32);
            this.lastDateTimePicker.Name = "lastDateTimePicker";
            this.lastDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.lastDateTimePicker.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "First day";
            // 
            // filterPanel
            // 
            this.filterPanel.Controls.Add(this.workTimeLabel);
            this.filterPanel.Controls.Add(this.workTimeComboBox);
            this.filterPanel.Controls.Add(this.btnClear);
            this.filterPanel.Controls.Add(this.btnApply);
            this.filterPanel.Controls.Add(this.label3);
            this.filterPanel.Controls.Add(this.customersComboBox);
            this.filterPanel.Controls.Add(this.label2);
            this.filterPanel.Controls.Add(this.firstDateTimePicker);
            this.filterPanel.Controls.Add(this.lastDateTimePicker);
            this.filterPanel.Controls.Add(this.label1);
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.filterPanel.Location = new System.Drawing.Point(0, 0);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(704, 64);
            this.filterPanel.TabIndex = 4;
            // 
            // workTimeLabel
            // 
            this.workTimeLabel.Location = new System.Drawing.Point(272, 32);
            this.workTimeLabel.Name = "workTimeLabel";
            this.workTimeLabel.Size = new System.Drawing.Size(96, 16);
            this.workTimeLabel.TabIndex = 6;
            this.workTimeLabel.Text = "Show work &period";
            // 
            // workTimeComboBox
            // 
            this.workTimeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.workTimeComboBox.Items.AddRange(new object[] {
            "Total",
            "Office hours",
            "Out of office hours"});
            this.workTimeComboBox.Location = new System.Drawing.Point(376, 32);
            this.workTimeComboBox.Name = "workTimeComboBox";
            this.workTimeComboBox.Size = new System.Drawing.Size(168, 21);
            this.workTimeComboBox.TabIndex = 7;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(624, 35);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(624, 5);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "Apply";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(272, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Customer";
            // 
            // customersComboBox
            // 
            this.customersComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.customersComboBox.Location = new System.Drawing.Point(376, 8);
            this.customersComboBox.Name = "customersComboBox";
            this.customersComboBox.Size = new System.Drawing.Size(168, 21);
            this.customersComboBox.Sorted = true;
            this.customersComboBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Last day";
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
            glColumn1.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.DateTimePicker;
            glColumn1.CheckBoxes = false;
            glColumn1.ImageIndex = -1;
            glColumn1.Name = "dateColumn";
            glColumn1.NumericSort = false;
            glColumn1.Text = "Date";
            glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn1.Width = 60;
            glColumn2.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.ComboBox;
            glColumn2.CheckBoxes = false;
            glColumn2.ImageIndex = -1;
            glColumn2.Name = "customerColumn";
            glColumn2.NumericSort = false;
            glColumn2.Text = "Customer";
            glColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn2.Width = 125;
            glColumn3.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.ComboBox;
            glColumn3.CheckBoxes = false;
            glColumn3.ImageIndex = -1;
            glColumn3.Name = "activityColumn";
            glColumn3.NumericSort = false;
            glColumn3.Text = "Activity";
            glColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn3.Width = 155;
            glColumn4.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.DateTimePicker;
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
            glColumn5.Name = "finishTimeColumn";
            glColumn5.NumericSort = false;
            glColumn5.Text = "Finish time";
            glColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn5.Width = 65;
            glColumn6.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.NumericUpDown;
            glColumn6.CheckBoxes = false;
            glColumn6.ImageIndex = -1;
            glColumn6.Name = "durationColumn";
            glColumn6.NumericSort = false;
            glColumn6.Text = "Duration";
            glColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn6.Width = 80;
            glColumn7.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.TextBox;
            glColumn7.CheckBoxes = false;
            glColumn7.ImageIndex = -1;
            glColumn7.Name = "ticketColumn";
            glColumn7.NumericSort = true;
            glColumn7.Text = "Ticket number";
            glColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn7.Width = 80;
            glColumn8.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.TextBox;
            glColumn8.CheckBoxes = false;
            glColumn8.ImageIndex = -1;
            glColumn8.Name = "commentColumn";
            glColumn8.NumericSort = false;
            glColumn8.Text = "Comment";
            glColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn8.Width = 65;
            this.logGlacialList.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn1,
            glColumn2,
            glColumn3,
            glColumn4,
            glColumn5,
            glColumn6,
            glColumn7,
            glColumn8});
            this.logGlacialList.ContextMenu = this.logContextMenu;
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
            this.logGlacialList.Location = new System.Drawing.Point(0, 64);
            this.logGlacialList.Name = "logGlacialList";
            this.logGlacialList.Selectable = true;
            this.logGlacialList.SelectedTextColor = System.Drawing.Color.White;
            this.logGlacialList.SelectionColor = System.Drawing.Color.DarkBlue;
            this.logGlacialList.ShowBorder = true;
            this.logGlacialList.ShowFocusRect = false;
            this.logGlacialList.Size = new System.Drawing.Size(704, 429);
            this.logGlacialList.SortType = GlacialComponents.Controls.SortTypes.InsertionSort;
            this.logGlacialList.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.logGlacialList.TabIndex = 6;
            this.logGlacialList.Text = "glacialList1";
            this.logGlacialList.ItemChangedEvent += new GlacialComponents.Controls.ChangedEventHandler(this.logGlacialList_ItemChangedEvent);
            this.logGlacialList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.logGlacialList_KeyUp);
            // 
            // logContextMenu
            // 
            this.logContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.addMenuItem,
            this.removeMenuItem});
            // 
            // addMenuItem
            // 
            this.addMenuItem.Index = 0;
            this.addMenuItem.Text = "&Add activity";
            this.addMenuItem.Click += new System.EventHandler(this.addMenuItem_Click);
            // 
            // removeMenuItem
            // 
            this.removeMenuItem.Index = 1;
            this.removeMenuItem.Text = "&Remove activity";
            this.removeMenuItem.Click += new System.EventHandler(this.removeMenuItem_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.closeButton);
            this.panel2.Controls.Add(this.filterButton);
            this.panel2.Controls.Add(this.applyChangesButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(704, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(104, 493);
            this.panel2.TabIndex = 7;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.closeButton.Location = new System.Drawing.Point(8, 464);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(88, 23);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "Close";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // filterButton
            // 
            this.filterButton.Location = new System.Drawing.Point(8, 5);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(88, 23);
            this.filterButton.TabIndex = 1;
            this.filterButton.Text = "Hide filter";
            this.filterButton.Click += new System.EventHandler(this.filterButton_Click);
            // 
            // applyChangesButton
            // 
            this.applyChangesButton.Enabled = false;
            this.applyChangesButton.Location = new System.Drawing.Point(8, 35);
            this.applyChangesButton.Name = "applyChangesButton";
            this.applyChangesButton.Size = new System.Drawing.Size(88, 23);
            this.applyChangesButton.TabIndex = 0;
            this.applyChangesButton.Text = "Apply changes";
            this.applyChangesButton.Click += new System.EventHandler(this.applyChangesButton_Click);
            // 
            // LogForm
            // 
            this.AcceptButton = this.btnApply;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(808, 493);
            this.Controls.Add(this.logGlacialList);
            this.Controls.Add(this.filterPanel);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(40, 40);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(590, 200);
            this.Name = "LogForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "LogForm";
            this.Load += new System.EventHandler(this.LogForm_Load);
            this.filterPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		public static LogForm GetInstance(ActivitiesEvidence WebService, Settings AppSettings)
		{
			if (_instance == null)
				_instance = new LogForm(WebService, AppSettings);
			return _instance;
		}

		private void RefreshData()
		{
			LoadActivities();
			ResetFilter();
		}

		private void LoadActivities()
		{
			ActivityReportStruct[] activityReport = _service.GetLastActivities(_settings["AdminID"].ToUpper());
			_activities = new ActivityPresentation[activityReport.Length];
			int i = 0;
			foreach (ActivityReportStruct activityEntry in activityReport)
				_activities[i++] = new ActivityPresentation(activityEntry);
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			ResetFilter();
		}

		private void ResetFilter()
		{
			firstDateTimePicker.Value = DateTime.Today;
			lastDateTimePicker.Value = DateTime.Today;
			customersComboBox.SelectedIndex = 0;
			workTimeComboBox.SelectedIndex = 0;
			btnApply_Click(null, null);
		}

		private void btnApply_Click(object sender, System.EventArgs e)
		{
			RefreshActivitiesList();
		}

		private void RefreshActivitiesList()
		{
			logGlacialList.Items.Clear();
			try
			{
				foreach (ActivityPresentation t in _activities)
				{
					if (t.Entry.StartTime.Date < firstDateTimePicker.Value.Date)
						break;
					if (t.Entry.StartTime.Date > lastDateTimePicker.Value.Date)
						continue;
					if ((customersComboBox.SelectedIndex != 0) && (customersComboBox.Text != t.Entry.Customer))
						continue;
					if (workTimeComboBox.SelectedIndex != 0)
					{
						if ((workTimeComboBox.SelectedIndex == 1) &&
							((t.Entry.StartTime.Hour < 7) || (t.Entry.StartTime.Hour >= 19) || 
							(t.Entry.StartTime.DayOfWeek == DayOfWeek.Saturday) || (t.Entry.StartTime.DayOfWeek == DayOfWeek.Sunday)))
							continue;
						else if ((workTimeComboBox.SelectedIndex == 2) && 
							((t.Entry.StartTime.Hour >= 7) && (t.Entry.StartTime.Hour < 19) && 
							(t.Entry.StartTime.DayOfWeek != DayOfWeek.Saturday) && (t.Entry.StartTime.DayOfWeek != DayOfWeek.Sunday)))
							continue;
					}
						
					GLItem glItem = new GLItem(logGlacialList);
					GLSubItem glSubItem;

					glSubItem = new GLSubItem();
					glSubItem.Text = t.Entry.StartTime.Date.ToShortDateString();
					glItem.SubItems.Add(glSubItem);
					glSubItem = new GLSubItem();
					glSubItem.Text = t.Entry.Customer;
					glItem.SubItems.Add(glSubItem);
					glSubItem = new GLSubItem();
					glSubItem.Text = t.Entry.Activity;
					glItem.SubItems.Add(glSubItem);
					glSubItem = new GLSubItem();
					glSubItem.Text = t.Entry.StartTime.TimeOfDay.ToString();
					glItem.SubItems.Add(glSubItem);
					glSubItem = new GLSubItem();
					glSubItem.Text = t.Entry.StartTime.AddMinutes(t.Entry.WorkDuration).TimeOfDay.ToString();
					glItem.SubItems.Add(glSubItem);
					glSubItem = new GLSubItem();
					glSubItem.Text = t.Entry.WorkDuration.ToString();
					glItem.SubItems.Add(glSubItem);
					glSubItem = new GLSubItem();
					glSubItem.Text = t.Entry.TicketNumber;
					glItem.SubItems.Add(glSubItem);
					glSubItem = new GLSubItem();
					glSubItem.Text = t.Entry.Comment;
					glItem.SubItems.Add(glSubItem);

					glItem.Tag = t;
					logGlacialList.Items.Add(glItem);
				}
				logGlacialList.Refresh();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}		
		}

		private void filterButton_Click(object sender, System.EventArgs e)
		{
			if (filterPanel.Visible)
			{
				filterButton.Text = "Show filter";
				filterPanel.Hide();
			}
			else
			{
				filterButton.Text = "Hide filter";
				filterPanel.Show();
			}
		}

		private void closeButton_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void logGlacialList_ItemChangedEvent(object source, GlacialComponents.Controls.ChangedEventArgs e)
		{
			if (e.ChangedType == ChangedTypes.SubItemChanged)
			{
				GLColumn changedColumn = null;
				ActivityPresentation activityItem = (ActivityPresentation)e.Item.Tag;

				changedColumn = e.SubItem.Column;
//				for(int i = 0; i < e.Item.SubItems.Count; i++)
//					if (e.Item.SubItems[i] == e.SubItem)
//					{
//						changedColumn = e.Item.ParentList.Columns[i];
//						break;
//					}
				if (changedColumn.Name == "dateColumn")
				{
					activityItem.Entry.StartTime = (((DateTime)e.SubItem.Value).Date).Add(activityItem.Entry.StartTime.TimeOfDay);
				}
				else if (changedColumn.Name == "customerColumn")
				{
					activityItem.Entry.Customer = e.SubItem.Text;
				}
				else if (changedColumn.Name == "activityColumn")
				{
					activityItem.Entry.Activity = e.SubItem.Text;
				}
				else if (changedColumn.Name == "startTimeColumn")
				{
					activityItem.Entry.StartTime = activityItem.Entry.StartTime.Date.Add(((DateTime)e.SubItem.Value).TimeOfDay);
				}
				else if (changedColumn.Name == "durationColumn")
				{
					activityItem.Entry.WorkDuration = Convert.ToInt32(e.SubItem.Text);
				}
				else if (changedColumn.Name == "ticketColumn")
				{
					activityItem.Entry.TicketNumber = e.SubItem.Text;
				}
				else if (changedColumn.Name == "commentColumn")
				{
					activityItem.Entry.Comment = e.SubItem.Text;
				}
				
				activityItem.Modified = ModificationStatus.Modified;
				applyChangesButton.Enabled = true;
				RefreshActivitiesList();
			}
		}

		private void applyChangesButton_Click(object sender, System.EventArgs e)
		{
			bool applySuccessful = true;
			ComboBox customerColumnTemplate = logGlacialList.Columns["customerColumn"].ActivatedEmbeddedControlTemplate as ComboBox;
			ComboBox activityColumnTemplate = logGlacialList.Columns["activityColumn"].ActivatedEmbeddedControlTemplate as ComboBox;
			foreach (ActivityPresentation item in _activities)
			{
				if (item.Modified == ModificationStatus.Modified)
				{
					try
					{
						foreach (object customer in customerColumnTemplate.Items)
							if (((ObjectStruct)customer).ObjectName == item.Entry.Customer)
							{
								item.Entry.Customer = ((ObjectStruct)customer).ObjectID.ToString();
								break;
							}
						foreach (object activity in activityColumnTemplate.Items)
							if (((ObjectStruct)activity).ObjectName == item.Entry.Activity)
							{
								item.Entry.Activity = ((ObjectStruct)activity).ObjectID.ToString();
								break;
							}
						bool temp;
						_service.UpdateActivity(item.Entry, out temp, out temp);
						item.Modified = ModificationStatus.None;
					}
					catch (SoapException exception)
					{
						MessageBox.Show(
							"Following error has occured in method " + exception.Actor + ":\n" + exception.Detail.InnerText, 
							exception.Message,
							MessageBoxButtons.OK,
							MessageBoxIcon.Error);
						applySuccessful = false;
					}
					catch (Exception exception)
					{
						System.Diagnostics.Debug.WriteLine(exception.Message);
						applySuccessful = false;
					}
				}
				else if (item.Modified == ModificationStatus.Deleted)
				{
					try
					{
						_service.DeleteActivity(item.Entry.ID);
					}
					catch (Exception exception)
					{
						System.Diagnostics.Debug.WriteLine(exception.Message);
						applySuccessful = false;
					}
				}
			}
			if (applySuccessful)
			{
				applyChangesButton.Enabled = false;
				this.LoadActivities();
				btnApply_Click(null, null);
			}
		}

		private void removeMenuItem_Click(object sender, System.EventArgs e)
		{
			//GLItem item;
			//GlacialList items = sender as GlacialList;
			//ArrayList selItems = items.SelectedItems;
			foreach (GLItem item in logGlacialList.SelectedItems)
				RemoveActivity(item);
		}

		private void RemoveActivity(GLItem item)
		{
			item.ForeColor = Color.Gray;
			((ActivityPresentation)item.Tag).Modified = ModificationStatus.Deleted;
			applyChangesButton.Enabled = true;
		}

		private void logGlacialList_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyData == Keys.Delete)
				foreach (GLItem item in ((GlacialList)sender).SelectedItems)
					RemoveActivity(item);
		}

		private void addMenuItem_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		public void ShowAndActivate()
		{
			this.RefreshData();
			this.Show();
			this.Activate();
		}

		private void LogForm_Load(object sender, System.EventArgs e)
		{
			//this.firstDateTimePicker.Value = DateTime.Today.AddDays(-7);
            this.RefreshActivitiesList();
		}
	}
}
