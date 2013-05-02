using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using R.Helpers;
using R.Utilities;
using ActivityEvidence.EvidenceService;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml;
using System.Diagnostics;
using System.Reflection;
using ActivityEvidence.Exceptions;

namespace ActivityEvidence
{
    //delegate void NotifierDelegate();
		
	/// <summary>
	/// Represents activity creation window
	/// </summary>
    public class MainForm : System.Windows.Forms.Form
    {
        #region Designer generated declarations
        private System.Windows.Forms.ComboBox customersComboBox;
        private System.Windows.Forms.ComboBox activitiesComboBox;
        private System.Windows.Forms.NumericUpDown durationNumericUpDown;
        private System.Windows.Forms.CheckBox isTicketCheckBox;
        private System.Windows.Forms.TextBox ticketNumberTextBox;
        private System.Windows.Forms.TextBox commentTextBox;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.DateTimePicker startTimePicker;
        private System.Windows.Forms.DateTimePicker finishTimePicker;
        private System.Windows.Forms.DateTimePicker activityDatePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MenuItem newMenuItem;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem exitMenuItem;
        private System.Windows.Forms.MenuItem showLastActivitiesMenuItem;
        private System.Windows.Forms.ContextMenu mainContextMenu;
        private System.Windows.Forms.MenuItem startWorkingMenuItem;
        private System.Windows.Forms.MenuItem stopWorkingMenuItem;
        private System.Windows.Forms.CheckBox defaultCustomerCheckBox;
        private System.Windows.Forms.CheckBox customerDefaultActivityCheckBox;
        private System.Windows.Forms.CheckBox defaultActivityCheckBox;
        private System.Windows.Forms.MenuItem cancelWorkingMenuItem;
        private System.Windows.Forms.Timer idleReminderTimer;
        private System.Windows.Forms.MenuItem aboutMenuItem;
        private System.Windows.Forms.MenuItem updateMenuItem;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem claimMenuItem;
        private System.Windows.Forms.Button defaultActionButton;
        private System.Windows.Forms.Button saveAndButton;
        private System.Windows.Forms.ContextMenu saveContextMenu;
        private System.Windows.Forms.MenuItem saveAndCloseMenuItem;
        private System.Windows.Forms.MenuItem saveAndClearMenuItem;
        #endregion

        private Settings _settings;
        //private LogFacility _logger;
        private ActivitiesEvidence _service = new ActivitiesEvidence();
        private ObjectStruct[] _customers;
        private ObjectStruct[] _activities;
        private string _notifyIconStatusText = "Activity Evidence";

        private const int DEFAULT_DURATION = 30;
        private const int DEFAULT_OVERTIME_TYPE = 0;
        private int _todayWorkDuration = 0;
        private DateTime _timeOfStartWorking = DateTime.MinValue;
        private IconNotifier _notifier = new IconNotifier();
        private System.Threading.Timer _reminderTimer;
        private int _reminderPeriod;
        //private NotifierDelegate _notifierDelegate;
        private ObjectStruct _defaultCustomer;
        private ObjectStruct _defaultActivity;
        private bool _exit = false;
        private bool _defaultCustomerCheckboxChanged = false;
        private bool _defaultActivityCheckboxChanged = false;
        private Hashtable _saveHandlers = new Hashtable(3);
        private System.Windows.Forms.MenuItem saveAndCloneMenuItem;
        private MenuItem menuItem1;
        private MenuItem saveAndStartWorkingMenuItem;
        private ComboBox overtimeTypesComboBox;
        private Label label8;
        private CheckBox activityDefaultOvertimeTypeCheckBox;
        private Label requiredDescriptionLabel;
        private Hashtable _saveMenuItems = new Hashtable(3);
        private ErrorProvider _errorProvider;
        private BackgroundWorker _connectionInitializer = new BackgroundWorker();

        public MainForm()
        {
            //EventLog.WriteEntry("ActivityEvidence", "Creating MainForm instance", EventLogEntryType.Information);
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            try
            {
                EventLog.CreateEventSource("ActivityEvidence", "Application");
            }
            catch (ArgumentException) { }
            catch (Exception)
            {
                this.Restart(true);
            }               
            
            _saveHandlers.Add("Close", new EventHandler(this.SaveAndClose_Action));
            _saveHandlers.Add("Clone", new EventHandler(this.SaveAndClone_Action));
            _saveHandlers.Add("Clear", new EventHandler(this.SaveAndClear_Action));
            _saveHandlers.Add("StartWorking", new EventHandler(this.SaveAndStartWorking_Action));
            _saveMenuItems.Add("Close", this.saveAndCloseMenuItem);
            _saveMenuItems.Add("Clone", this.saveAndCloneMenuItem);
            _saveMenuItems.Add("Clear", this.saveAndClearMenuItem);
            _saveMenuItems.Add("StartWorking", this.saveAndStartWorkingMenuItem);

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.customersComboBox = new System.Windows.Forms.ComboBox();
            this.activitiesComboBox = new System.Windows.Forms.ComboBox();
            this.mainContextMenu = new System.Windows.Forms.ContextMenu();
            this.newMenuItem = new System.Windows.Forms.MenuItem();
            this.startWorkingMenuItem = new System.Windows.Forms.MenuItem();
            this.stopWorkingMenuItem = new System.Windows.Forms.MenuItem();
            this.cancelWorkingMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.showLastActivitiesMenuItem = new System.Windows.Forms.MenuItem();
            this.claimMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.updateMenuItem = new System.Windows.Forms.MenuItem();
            this.aboutMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.activityDatePicker = new System.Windows.Forms.DateTimePicker();
            this.durationNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.isTicketCheckBox = new System.Windows.Forms.CheckBox();
            this.ticketNumberTextBox = new System.Windows.Forms.TextBox();
            this.commentTextBox = new System.Windows.Forms.TextBox();
            this.defaultActionButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.startTimePicker = new System.Windows.Forms.DateTimePicker();
            this.finishTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.defaultCustomerCheckBox = new System.Windows.Forms.CheckBox();
            this.customerDefaultActivityCheckBox = new System.Windows.Forms.CheckBox();
            this.defaultActivityCheckBox = new System.Windows.Forms.CheckBox();
            this.idleReminderTimer = new System.Windows.Forms.Timer(this.components);
            this.saveAndButton = new System.Windows.Forms.Button();
            this.saveContextMenu = new System.Windows.Forms.ContextMenu();
            this.saveAndCloseMenuItem = new System.Windows.Forms.MenuItem();
            this.saveAndCloneMenuItem = new System.Windows.Forms.MenuItem();
            this.saveAndClearMenuItem = new System.Windows.Forms.MenuItem();
            this.saveAndStartWorkingMenuItem = new System.Windows.Forms.MenuItem();
            this.overtimeTypesComboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.activityDefaultOvertimeTypeCheckBox = new System.Windows.Forms.CheckBox();
            this.requiredDescriptionLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.durationNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // customersComboBox
            // 
            this.customersComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.customersComboBox.Location = new System.Drawing.Point(56, 8);
            this.customersComboBox.Name = "customersComboBox";
            this.customersComboBox.Size = new System.Drawing.Size(168, 21);
            this.customersComboBox.Sorted = true;
            this.customersComboBox.TabIndex = 1;
            this.customersComboBox.SelectedValueChanged += new System.EventHandler(this.customersComboBox_SelectedValueChanged);
            // 
            // activitiesComboBox
            // 
            this.activitiesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.activitiesComboBox.Location = new System.Drawing.Point(56, 48);
            this.activitiesComboBox.Name = "activitiesComboBox";
            this.activitiesComboBox.Size = new System.Drawing.Size(168, 21);
            this.activitiesComboBox.Sorted = true;
            this.activitiesComboBox.TabIndex = 3;
            this.activitiesComboBox.SelectedValueChanged += new System.EventHandler(this.activitiesComboBox_SelectedValueChanged);
            // 
            // mainContextMenu
            // 
            this.mainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.newMenuItem,
            this.startWorkingMenuItem,
            this.stopWorkingMenuItem,
            this.cancelWorkingMenuItem,
            this.menuItem3,
            this.showLastActivitiesMenuItem,
            this.claimMenuItem,
            this.menuItem1,
            this.updateMenuItem,
            this.aboutMenuItem,
            this.menuItem2,
            this.exitMenuItem});
            // 
            // newMenuItem
            // 
            this.newMenuItem.Enabled = false;
            this.newMenuItem.Index = 0;
            this.newMenuItem.Text = "&New Record";
            this.newMenuItem.Click += new System.EventHandler(this.newMenuItem_Click);
            // 
            // startWorkingMenuItem
            // 
            this.startWorkingMenuItem.Enabled = false;
            this.startWorkingMenuItem.Index = 1;
            this.startWorkingMenuItem.Text = "&Start Working";
            this.startWorkingMenuItem.Click += new System.EventHandler(this.startWorkingMenuItem_Click);
            // 
            // stopWorkingMenuItem
            // 
            this.stopWorkingMenuItem.Enabled = false;
            this.stopWorkingMenuItem.Index = 2;
            this.stopWorkingMenuItem.Text = "Sto&p Working";
            this.stopWorkingMenuItem.Click += new System.EventHandler(this.stopWorkingMenuItem_Click);
            // 
            // cancelWorkingMenuItem
            // 
            this.cancelWorkingMenuItem.Enabled = false;
            this.cancelWorkingMenuItem.Index = 3;
            this.cancelWorkingMenuItem.Text = "&Cancel Working";
            this.cancelWorkingMenuItem.Click += new System.EventHandler(this.cancelWorkingMenuItem_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 4;
            this.menuItem3.Text = "-";
            // 
            // showLastActivitiesMenuItem
            // 
            this.showLastActivitiesMenuItem.Enabled = false;
            this.showLastActivitiesMenuItem.Index = 5;
            this.showLastActivitiesMenuItem.Text = "S&how last activity entries";
            this.showLastActivitiesMenuItem.Click += new System.EventHandler(this.showLastActivitiesMenuItem_Click);
            // 
            // claimMenuItem
            // 
            this.claimMenuItem.Enabled = false;
            this.claimMenuItem.Index = 6;
            this.claimMenuItem.Text = "Show data to c&laim";
            this.claimMenuItem.Click += new System.EventHandler(this.claimMenuItem_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 7;
            this.menuItem1.Text = "-";
            // 
            // updateMenuItem
            // 
            this.updateMenuItem.Enabled = false;
            this.updateMenuItem.Index = 8;
            this.updateMenuItem.Text = "Check &Updates";
            this.updateMenuItem.Click += new System.EventHandler(this.updateMenuItem_Click);
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Index = 9;
            this.aboutMenuItem.Text = "About";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 10;
            this.menuItem2.Text = "-";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Index = 11;
            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenu = this.mainContextMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.newMenuItem_Click);
            // 
            // activityDatePicker
            // 
            this.activityDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.activityDatePicker.Location = new System.Drawing.Point(296, 8);
            this.activityDatePicker.Name = "activityDatePicker";
            this.activityDatePicker.Size = new System.Drawing.Size(120, 20);
            this.activityDatePicker.TabIndex = 5;
            // 
            // durationNumericUpDown
            // 
            this.durationNumericUpDown.Location = new System.Drawing.Point(360, 56);
            this.durationNumericUpDown.Maximum = new decimal(new int[] {
            6000,
            0,
            0,
            0});
            this.durationNumericUpDown.Name = "durationNumericUpDown";
            this.durationNumericUpDown.ReadOnly = true;
            this.durationNumericUpDown.Size = new System.Drawing.Size(40, 20);
            this.durationNumericUpDown.TabIndex = 4;
            this.durationNumericUpDown.ValueChanged += new System.EventHandler(this.durationNumericUpDown_ValueChanged);
            // 
            // isTicketCheckBox
            // 
            this.isTicketCheckBox.Location = new System.Drawing.Point(0, 107);
            this.isTicketCheckBox.Name = "isTicketCheckBox";
            this.isTicketCheckBox.Size = new System.Drawing.Size(176, 24);
            this.isTicketCheckBox.TabIndex = 8;
            this.isTicketCheckBox.Text = "Has &ticket/change";
            this.isTicketCheckBox.CheckedChanged += new System.EventHandler(this.isTicketCheckBox_CheckedChanged);
            // 
            // ticketNumberTextBox
            // 
            this.ticketNumberTextBox.Location = new System.Drawing.Point(160, 107);
            this.ticketNumberTextBox.MaxLength = 20;
            this.ticketNumberTextBox.Name = "ticketNumberTextBox";
            this.ticketNumberTextBox.Size = new System.Drawing.Size(100, 20);
            this.ticketNumberTextBox.TabIndex = 9;
            this.ticketNumberTextBox.Visible = false;
            this.ticketNumberTextBox.TextChanged += new System.EventHandler(this.ticketNumberTextBox_TextChanged);
            // 
            // commentTextBox
            // 
            this.commentTextBox.Location = new System.Drawing.Point(0, 155);
            this.commentTextBox.Multiline = true;
            this.commentTextBox.Name = "commentTextBox";
            this.commentTextBox.Size = new System.Drawing.Size(432, 56);
            this.commentTextBox.TabIndex = 11;
            this.commentTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.commentTextBox_Validating);
            // 
            // defaultActionButton
            // 
            this.defaultActionButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.defaultActionButton.Location = new System.Drawing.Point(0, 219);
            this.defaultActionButton.Name = "defaultActionButton";
            this.defaultActionButton.Size = new System.Drawing.Size(136, 23);
            this.defaultActionButton.TabIndex = 12;
            this.defaultActionButton.Text = "&Save and ... ";
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(352, 219);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(80, 23);
            this.closeButton.TabIndex = 15;
            this.closeButton.Text = "Close";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(160, 219);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(80, 23);
            this.clearButton.TabIndex = 14;
            this.clearButton.Text = "C&lear form";
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // startTimePicker
            // 
            this.startTimePicker.CustomFormat = "HH:mm";
            this.startTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startTimePicker.Location = new System.Drawing.Point(296, 32);
            this.startTimePicker.Name = "startTimePicker";
            this.startTimePicker.ShowUpDown = true;
            this.startTimePicker.Size = new System.Drawing.Size(56, 20);
            this.startTimePicker.TabIndex = 5;
            this.startTimePicker.ValueChanged += new System.EventHandler(this.startTimePicker_ValueChanged);
            // 
            // finishTimePicker
            // 
            this.finishTimePicker.CustomFormat = "HH:mm";
            this.finishTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.finishTimePicker.Location = new System.Drawing.Point(296, 56);
            this.finishTimePicker.Name = "finishTimePicker";
            this.finishTimePicker.ShowUpDown = true;
            this.finishTimePicker.Size = new System.Drawing.Size(56, 20);
            this.finishTimePicker.TabIndex = 7;
            this.finishTimePicker.ValueChanged += new System.EventHandler(this.finishTimePicker_ValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Customer:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Act&ivity:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(232, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Date:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(232, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Sta&rt time:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(232, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "&Finish time:";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(360, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 20;
            this.label6.Text = "Work duration:";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(0, 139);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 16);
            this.label7.TabIndex = 10;
            this.label7.Text = "Co&mment:";
            // 
            // defaultCustomerCheckBox
            // 
            this.defaultCustomerCheckBox.Location = new System.Drawing.Point(56, 32);
            this.defaultCustomerCheckBox.Name = "defaultCustomerCheckBox";
            this.defaultCustomerCheckBox.Size = new System.Drawing.Size(112, 16);
            this.defaultCustomerCheckBox.TabIndex = 22;
            this.defaultCustomerCheckBox.Text = "Default";
            this.defaultCustomerCheckBox.CheckedChanged += new System.EventHandler(this.defaultCustomerCheckBox_CheckedChanged);
            // 
            // customerDefaultActivityCheckBox
            // 
            this.customerDefaultActivityCheckBox.Location = new System.Drawing.Point(120, 72);
            this.customerDefaultActivityCheckBox.Name = "customerDefaultActivityCheckBox";
            this.customerDefaultActivityCheckBox.Size = new System.Drawing.Size(128, 16);
            this.customerDefaultActivityCheckBox.TabIndex = 23;
            this.customerDefaultActivityCheckBox.Text = "Default for customer";
            this.customerDefaultActivityCheckBox.CheckedChanged += new System.EventHandler(this.customerDefaultActivityCheckBox_CheckedChanged);
            // 
            // defaultActivityCheckBox
            // 
            this.defaultActivityCheckBox.Location = new System.Drawing.Point(56, 72);
            this.defaultActivityCheckBox.Name = "defaultActivityCheckBox";
            this.defaultActivityCheckBox.Size = new System.Drawing.Size(64, 16);
            this.defaultActivityCheckBox.TabIndex = 24;
            this.defaultActivityCheckBox.Text = "Default";
            this.defaultActivityCheckBox.CheckedChanged += new System.EventHandler(this.defaultActivityCheckBox_CheckedChanged);
            // 
            // idleReminderTimer
            // 
            this.idleReminderTimer.Enabled = true;
            this.idleReminderTimer.Interval = 600000;
            this.idleReminderTimer.Tick += new System.EventHandler(this.idleReminderTimer_Tick);
            // 
            // saveAndButton
            // 
            this.saveAndButton.Location = new System.Drawing.Point(136, 219);
            this.saveAndButton.Name = "saveAndButton";
            this.saveAndButton.Size = new System.Drawing.Size(16, 23);
            this.saveAndButton.TabIndex = 25;
            this.saveAndButton.Text = ">";
            this.saveAndButton.Click += new System.EventHandler(this.saveAndButton_Click);
            // 
            // saveContextMenu
            // 
            this.saveContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.saveAndCloseMenuItem,
            this.saveAndCloneMenuItem,
            this.saveAndClearMenuItem,
            this.saveAndStartWorkingMenuItem});
            // 
            // saveAndCloseMenuItem
            // 
            this.saveAndCloseMenuItem.Index = 0;
            this.saveAndCloseMenuItem.Text = "Save and &close";
            this.saveAndCloseMenuItem.Click += new System.EventHandler(this.SaveAndClose_Action);
            // 
            // saveAndCloneMenuItem
            // 
            this.saveAndCloneMenuItem.Index = 1;
            this.saveAndCloneMenuItem.Text = "Save and &duplicate";
            this.saveAndCloneMenuItem.Click += new System.EventHandler(this.SaveAndClone_Action);
            // 
            // saveAndClearMenuItem
            // 
            this.saveAndClearMenuItem.Index = 2;
            this.saveAndClearMenuItem.Text = "Save and c&lear";
            this.saveAndClearMenuItem.Click += new System.EventHandler(this.SaveAndClear_Action);
            // 
            // saveAndStartWorkingMenuItem
            // 
            this.saveAndStartWorkingMenuItem.Index = 3;
            this.saveAndStartWorkingMenuItem.Text = "Save and &start working";
            this.saveAndStartWorkingMenuItem.Click += new System.EventHandler(this.SaveAndStartWorking_Action);
            // 
            // overtimeTypesComboBox
            // 
            this.overtimeTypesComboBox.DisplayMember = "ObjectName";
            this.overtimeTypesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.overtimeTypesComboBox.Location = new System.Drawing.Point(285, 106);
            this.overtimeTypesComboBox.Name = "overtimeTypesComboBox";
            this.overtimeTypesComboBox.Size = new System.Drawing.Size(147, 21);
            this.overtimeTypesComboBox.TabIndex = 26;
            this.overtimeTypesComboBox.ValueMember = "ObjectID";
            this.overtimeTypesComboBox.SelectedValueChanged += new System.EventHandler(this.overtimeTypesComboBox_SelectedValueChanged);
            this.overtimeTypesComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.overtimeTypesComboBox_Validating);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(282, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(150, 19);
            this.label8.TabIndex = 27;
            this.label8.Text = "Overtime type (if applicable)";
            // 
            // activityDefaultOvertimeTypeCheckBox
            // 
            this.activityDefaultOvertimeTypeCheckBox.Location = new System.Drawing.Point(285, 133);
            this.activityDefaultOvertimeTypeCheckBox.Name = "activityDefaultOvertimeTypeCheckBox";
            this.activityDefaultOvertimeTypeCheckBox.Size = new System.Drawing.Size(128, 16);
            this.activityDefaultOvertimeTypeCheckBox.TabIndex = 28;
            this.activityDefaultOvertimeTypeCheckBox.Text = "Default for activity";
            this.activityDefaultOvertimeTypeCheckBox.Visible = false;
            this.activityDefaultOvertimeTypeCheckBox.CheckedChanged += new System.EventHandler(this.activityDefaultOvertimeTypeCheckBox_CheckedChanged);
            // 
            // requiredDescriptionLabel
            // 
            this.requiredDescriptionLabel.AutoSize = true;
            this.requiredDescriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.requiredDescriptionLabel.ForeColor = System.Drawing.Color.Red;
            this.requiredDescriptionLabel.Location = new System.Drawing.Point(49, 137);
            this.requiredDescriptionLabel.Name = "requiredDescriptionLabel";
            this.requiredDescriptionLabel.Size = new System.Drawing.Size(13, 15);
            this.requiredDescriptionLabel.TabIndex = 29;
            this.requiredDescriptionLabel.Text = "*";
            this.requiredDescriptionLabel.Visible = false;
            // 
            // MainForm
            // 
            this.AcceptButton = this.defaultActionButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(440, 247);
            this.ContextMenu = this.mainContextMenu;
            this.ControlBox = false;
            this.Controls.Add(this.requiredDescriptionLabel);
            this.Controls.Add(this.activityDefaultOvertimeTypeCheckBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.overtimeTypesComboBox);
            this.Controls.Add(this.saveAndButton);
            this.Controls.Add(this.defaultActivityCheckBox);
            this.Controls.Add(this.customerDefaultActivityCheckBox);
            this.Controls.Add(this.defaultCustomerCheckBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.finishTimePicker);
            this.Controls.Add(this.startTimePicker);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.defaultActionButton);
            this.Controls.Add(this.commentTextBox);
            this.Controls.Add(this.ticketNumberTextBox);
            this.Controls.Add(this.isTicketCheckBox);
            this.Controls.Add(this.durationNumericUpDown);
            this.Controls.Add(this.activityDatePicker);
            this.Controls.Add(this.activitiesComboBox);
            this.Controls.Add(this.customersComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(20, 20);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Activity Evidence recorder";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.durationNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            Mutex m = new Mutex(true, "Form loading");
            try
            {
                _settings = new Settings("settings.xml", SettingsOpenModes.Exists);
                if (_settings["Update"].ToLower() != "false")
                {
                    CheckUpdates();
                }

                _service.Url = _settings["URL"];
                this._connectionInitializer.DoWork += new DoWorkEventHandler((object o, DoWorkEventArgs dwea) => 
                {
                    m.WaitOne();
                    m.ReleaseMutex();
                    this.Invoke((Action)delegate { this.Hide(); });
                    while (true)
                    {
                        try
                        {
                            this.InitConnection();
                            this.Invoke((Action)delegate { this.ClearForm(); });
                            this._todayWorkDuration = _service.GetTodayWorkDuration(_settings["AdminID"].ToUpper());
                            return;
                        }
                        catch (Exception) { Thread.Sleep(30000); }
                    }
                });
                this._connectionInitializer.RunWorkerAsync();
                
                //this.overtimeTypesComboBox.DisplayMember = "ObjectName";
                //this.overtimeTypesComboBox.ValueMember = "ObjectID";

                this.SetDefaultSaveAction();
                this.KeyPreview = true;

                try
                {
                    _reminderPeriod = Convert.ToInt32(_settings["ReminderTimer"]);
                    _reminderTimer = new System.Threading.Timer(new TimerCallback(RemindTimer_Tick), null, Timeout.Infinite, Timeout.Infinite);
                    //_notifierDelegate = new NotifierDelegate(Notify);
                }
                catch (Exception)
                {
                }

                SetTodayWorkDurationNotifyIconText();
                InitErrorProviders();
            }
            catch (System.IO.IOException exception)
            {
                MessageBox.Show(exception.Message, "File operation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Application will be terminated", "File operation fatal error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                MessageBox.Show(exception.StackTrace);
            }
            m.ReleaseMutex();
        }

        private void InitConnection()
        {
            try
            {
                this._service.SendInformation(_settings["AdminID"], System.IO.Directory.GetDirectories("c:\\windows\\microsoft.net\\framework"));
                #region Adding customers
                this._customers = _service.GetCustomers(_settings["AdminID"].ToUpper());
                this.Invoke((Action)delegate { customersComboBox.Items.Clear(); });
                foreach (ObjectStruct customer in _customers)
                {
                    //MessageBox.Show(customer.ObjectID + "\n" + customer.ObjectName);
                    this.Invoke((Action)delegate { customersComboBox.Items.Add(customer); });
                }
                _defaultCustomer = new ObjectStruct();
                bool hasDefaultCustomer = false;
                foreach (ObjectStruct customer in customersComboBox.Items)
                    if (customer.ObjectName == _settings["DefaultCustomer"])
                    {
                        hasDefaultCustomer = true;
                        _defaultCustomer = customer;
                        break;
                    }
                if (!hasDefaultCustomer)
                    _defaultCustomer = customersComboBox.Items[0] as ObjectStruct;
                #endregion
                #region Adding activities
                this._activities = _service.GetActivities(_settings["AdminID"].ToUpper());
                this.Invoke((Action)delegate { activitiesComboBox.Items.Clear(); });
                foreach (ObjectStruct activity in _activities)
                    this.Invoke((Action)delegate { activitiesComboBox.Items.Add(activity); });
                _defaultActivity = new ObjectStruct();
                bool hasDefaultActivity = false;
                foreach (ObjectStruct activity in activitiesComboBox.Items)
                    if (activity.ObjectName == _settings["DefaultActivity"])
                    {
                        hasDefaultActivity = true;
                        _defaultActivity = activity;
                        break;
                    }
                if (!hasDefaultActivity)
                    _defaultActivity = activitiesComboBox.Items[0] as ObjectStruct;
                #endregion
                #region Adding overtime types
                var _overtimeTypes = _service.GetOvertimeTypes(_settings["AdminID"]);
                this.Invoke((Action)delegate { this.overtimeTypesComboBox.DataSource = _overtimeTypes; });
                #endregion
                this.EnableNetworkRelatedFunctions();
                this.ShowTrayNotification("Activity Evidence recorder", "Connected to server and ready to work", 2000);
            }
            catch (System.Web.Services.Protocols.SoapException exception)
            {
                if (exception.Message == "No customers")
                {
                    this.ShowTrayNotification(
                        "Activity Evidence recorder", "No customers have been found for you. Possible actions:\n" +
                        "1. Check your AdminID in the settings.xml (you provided '" + _settings["AdminID"] + "')\n" +
                        "2. Ask Activity Evidence DB administrator whether your ID is assigned to some team\n" +
                        exception.Message,
                        2000);
                }
                else if (exception.Message == "No activities")
                {
                    this.ShowTrayNotification(
                        "Activity Evidence recorder",
                        "No activities have been found for you. Possible actions:\n" +
                        "1. Check your AdminID in the settings.xml (you provided '" + _settings["AdminID"] + "')\n" +
                        "2. Ask Activity Evidence DB administrator whether your ID is assigned to some team\n" +
                        exception.Message,
                        2000);
                }
                else
                {
                    this.ShowTrayNotification(
                        "Activity Evidence recorder Unknown SOAP error",
                        "Unknown error has occured:" +
                        "Code: " + exception.Code + "\n" +
                        "Actor: " + exception.Actor + "\n" +
                        "Message: " + exception.Message + "\n" +
                        "Details: " + exception.Detail + "\n",
                        2000);
                }
                throw new ConnectionException("Can't get required data from the server");
            }
            catch (System.Net.WebException exception)
            {
                this.ShowTrayNotification(
                    "Activity Evidence recorder Web Service error",
                    exception.Message + "\n" +
                    "Check <URL> tag in the settings.xml (you provided '" + _service.Url + "')",
                    2000);
                throw new ConnectionException("Can't connect to the web service");
            }
            catch (System.InvalidOperationException exception)
            {
                this.ShowTrayNotification(
                    "Activity Evidence recorder Web Service error",
                    exception.Message + "\n" +
                    "Check <URL> tag in the settings.xml (you provided '" + _service.Url + "')",
                    2000);
                ErrorHandler.Log(exception);
                throw new ConnectionException("Something wrong has happened");
            }
            catch (Exception exception)
            {
                ErrorHandler.Log(exception);
                throw new ConnectionException("Unknown error has occurred while trying to communicate with the server");
            }
        }

        private void EnableNetworkRelatedFunctions()
        {
            this.newMenuItem.Enabled = true;
            this.startWorkingMenuItem.Enabled = true;
            this.showLastActivitiesMenuItem.Enabled = true;
            this.claimMenuItem.Enabled = true;
            this.updateMenuItem.Enabled = true;
        }

        private void InitErrorProviders()
        {
            this._errorProvider = new System.Windows.Forms.ErrorProvider();
            this._errorProvider.SetIconAlignment(this.commentTextBox, ErrorIconAlignment.MiddleRight);
            this._errorProvider.SetIconPadding(this.commentTextBox, -10);
            //this._commentFieldErrorProvider.BlinkRate = 1000;
            //this._commentFieldErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
        }

        private void RemindTimer_Tick(object stateObject)
        {
            this.Notify();
        }

        private void idleReminderTimer_Tick(object sender, System.EventArgs e)
        {
            this.ShowTrayNotification("Activity Evidence recorder", "Looks like you are not working... Think about it :-)", 5000);
            try
            {
                _todayWorkDuration = _service.GetTodayWorkDuration(_settings["AdminID"].ToUpper());
                SetTodayWorkDurationNotifyIconText();
            }
            catch (System.Net.WebException)
            {
                notifyIcon.Text = _notifyIconStatusText + "\nCan't get work duration. Connection failure";
            }
        }

        private void Notify()
        {
            this.ShowTrayNotification("Activity Evidence recorder", "Are you still working on the problem?", 5000);
        }

        private void durationNumericUpDown_ValueChanged(object sender, System.EventArgs e)
        {
            finishTimePicker.ValueChanged -= new EventHandler(finishTimePicker_ValueChanged);
            finishTimePicker.Value = startTimePicker.Value.AddMinutes((double)durationNumericUpDown.Value);
            finishTimePicker.ValueChanged += new EventHandler(finishTimePicker_ValueChanged);
        }

        private void SendData()
        {
            try
            {
                DateTime start = activityDatePicker.Value.Date.Add(new TimeSpan(startTimePicker.Value.Hour, startTimePicker.Value.Minute, 0));
                DateTime finish = activityDatePicker.Value.Date.Add(new TimeSpan(finishTimePicker.Value.Hour, finishTimePicker.Value.Minute, 0));
                _service.SendActivity(
                    _settings["AdminID"].ToUpper(),
                    ((ObjectStruct)customersComboBox.SelectedItem).ObjectID,
                    ((ObjectStruct)activitiesComboBox.SelectedItem).ObjectID,
                    start,
                    (int)durationNumericUpDown.Value,
                    isTicketCheckBox.Checked,
                    ticketNumberTextBox.Text,
                    commentTextBox.Text,
                    Convert.ToInt32(((ObjectStruct)this.overtimeTypesComboBox.SelectedItem).ObjectID));
            }
            catch (System.Net.WebException exception)
            {
                throw new ActivityEvidence.Exceptions.ConnectionException(exception.Message);
            }
        }

        private void ClearForm()
        {
            activityDatePicker.Value = DateTime.Today;
            commentTextBox.Text = "";
            customersComboBox.SelectedItem = _defaultCustomer;
            customersComboBox_SelectedValueChanged(null, null);
            durationNumericUpDown.Value = DEFAULT_DURATION;
            isTicketCheckBox.Checked = false;
            this.overtimeTypesComboBox.SelectedValue = DEFAULT_OVERTIME_TYPE;
            startTimePicker.Value = DateTime.Now;
            ticketNumberTextBox.Text = "";
        }

        private bool SaveRecord()
        {
            DialogResult result = DialogResult.Retry;
            while (result == DialogResult.Retry)
            {
                try
                {
                    ValidateData();
                    SendData();
                    _todayWorkDuration = _service.GetTodayWorkDuration(_settings["AdminID"].ToUpper());
                    startWorkingMenuItem.Enabled = true;
                    stopWorkingMenuItem.Enabled = false;
                    cancelWorkingMenuItem.Enabled = false;
                    this.SetIdleTrayIcon();
                    SetTodayWorkDurationNotifyIconText();
                    idleReminderTimer.Start();
                    return true;
                }
                catch (ActivityEvidence.Exceptions.DataValidationException exception)
                {
                    result = MessageBox.Show(exception.Message, "Data validation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (ActivityEvidence.Exceptions.ConnectionException exception)
                {
                    result =
                        MessageBox.Show("Can't send data to the server. Connection failed. Check your connection to the IBM network and try again.\r\nInternal error is:" + exception.Message,
                        "Connection error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
            return false;
        }

        private void SetIdleTrayIcon()
        {
            this._notifyIconStatusText = "Activity Evidence";
            this.notifyIcon.Icon = Properties.Resources.IconIdle;
        }

        private void saveClearButton_Click(object sender, System.EventArgs e)
        {
            if (SaveRecord())
            {
                ClearForm();
            }
        }

        private void startTimePicker_ValueChanged(object sender, System.EventArgs e)
        {
            finishTimePicker.Value = startTimePicker.Value.AddMinutes((double)durationNumericUpDown.Value);
        }

        private void finishTimePicker_ValueChanged(object sender, System.EventArgs e)
        {
            TimeSpan difference = finishTimePicker.Value - startTimePicker.Value;
            durationNumericUpDown.ValueChanged -= new EventHandler(durationNumericUpDown_ValueChanged);
            if (difference.Ticks >= 0)
            {
                durationNumericUpDown.Value = (decimal)Math.Round(difference.Hours * 60 + difference.Minutes + (double)difference.Seconds / 60);
            }
            else
            {
                durationNumericUpDown.Value = 0;
                finishTimePicker.Value = startTimePicker.Value;
            }
            durationNumericUpDown.ValueChanged += new EventHandler(durationNumericUpDown_ValueChanged);
        }

        private void isTicketCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            ticketNumberTextBox.Visible = isTicketCheckBox.Checked;
            if (isTicketCheckBox.Checked)
            {
                isTicketCheckBox.Text = "Has &ticket/change - number:";
                ticketNumberTextBox.Focus();
            }
            else
                isTicketCheckBox.Text = "Has &ticket/change";
        }

        private void clearButton_Click(object sender, System.EventArgs e)
        {
            ClearForm();
        }

        private void closeButton_Click(object sender, System.EventArgs e)
        {
            //this.ClearForm();
            this.Hide();
            if (startWorkingMenuItem.Enabled)
                idleReminderTimer.Start();
        }

        private void newMenuItem_Click(object sender, System.EventArgs e)
        {
            if (this.newMenuItem.Enabled)
            {
                this.ClearForm();
                this.Show();
                this.Activate();
            }
        }

        private void exitMenuItem_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                _exit = true;
                this.Close();
            }
        }

        private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_exit)
            {
                this.ClearForm();
                this.Hide();
                e.Cancel = true;
            }
            else
            {
                notifyIcon.Dispose();
            }
        }

        private void startWorkingMenuItem_Click(object sender, System.EventArgs e)
        {
            _timeOfStartWorking = DateTime.Now;
            startWorkingMenuItem.Enabled = false;
            stopWorkingMenuItem.Enabled = true;
            cancelWorkingMenuItem.Enabled = true;
            idleReminderTimer.Stop();
            _notifyIconStatusText = "Activities Evidence\n(Working...)";
            this.notifyIcon.Icon = Properties.Resources.IconWorking;
            SetTodayWorkDurationNotifyIconText();
            if (_reminderTimer != null)
                _reminderTimer.Change(_reminderPeriod * 60000, _reminderPeriod * 60000);
        }

        private void stopWorkingMenuItem_Click(object sender, System.EventArgs e)
        {
            if (_reminderTimer != null)
                _reminderTimer.Change(Timeout.Infinite, Timeout.Infinite);
            this.ClearForm();
            startTimePicker.Value = _timeOfStartWorking;
            finishTimePicker.Value = DateTime.Now;
            this.Show();
            this.Activate();
        }

        private void customersComboBox_SelectedValueChanged(object sender, System.EventArgs e)
        {
            _defaultCustomerCheckboxChanged = true;
            defaultCustomerCheckBox.Checked = customersComboBox.Text == _settings["DefaultCustomer"];
            bool hasDefaultActivity = false;
            System.Xml.XmlDocument settingsDoc = new System.Xml.XmlDocument();
            string customerDefaultActivity = "";
            settingsDoc.Load("settings.xml");
            foreach (System.Xml.XmlNode setting in settingsDoc.DocumentElement.ChildNodes)
                if ((setting.Name == "CustomerDefaultActivity") && (setting.Attributes["Customer"] != null) && (setting.Attributes["Customer"].Value == customersComboBox.Text))
                {
                    customerDefaultActivity = setting.FirstChild.Value;
                    break;
                }
            if (customerDefaultActivity != "")
                foreach (ObjectStruct activity in activitiesComboBox.Items)
                    if (activity.ObjectName == customerDefaultActivity)
                    {
                        hasDefaultActivity = true;
                        activitiesComboBox.SelectedItem = activity;
                        break;
                    }
            if (!hasDefaultActivity)
                activitiesComboBox.SelectedItem = _defaultActivity;
            _defaultCustomerCheckboxChanged = false;
        }

        private void showLastActivitiesMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                //LogForm logForm = new LogForm(_service, _settings);
                //if (logForm.ShowDialog() == DialogResult.OK)
                //	newMenuItem_Click(null, null);
                LogForm.GetInstance(_service, _settings).ShowAndActivate();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private const int WM_QUIT = 0x0012;

        protected override void WndProc(ref Message m)
        {
            if (m.WParam.ToInt32() == WM_QUIT)
            {
                _exit = true;
            }
            base.WndProc(ref m);
        }

        private void activitiesComboBox_SelectedValueChanged(object sender, System.EventArgs e)
        {
            _defaultActivityCheckboxChanged = true;
            defaultActivityCheckBox.Checked = _settings["DefaultActivity"] == activitiesComboBox.Text;
            System.Xml.XmlDocument settingsDoc = new System.Xml.XmlDocument();
            settingsDoc.Load("settings.xml");
            customerDefaultActivityCheckBox.Checked = false;
            foreach (System.Xml.XmlNode setting in settingsDoc.DocumentElement.ChildNodes)
                if ((setting.Name == "CustomerDefaultActivity") && (setting.Attributes["Customer"] != null) &&
                    (setting.Attributes["Customer"].Value == customersComboBox.Text) &&
                    (setting.FirstChild.Value == activitiesComboBox.Text))
                {
                    customerDefaultActivityCheckBox.Checked = true;
                    break;
                }

            _defaultActivityCheckboxChanged = false;
        }

        private void activityDefaultOvertimeTypeCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            if (!_defaultActivityCheckboxChanged)
            {
                XmlDocument settingsDoc = new System.Xml.XmlDocument();
                settingsDoc.Load("settings.xml");
                bool defaultActivityOvertimeTypeExists = false;
                if (activityDefaultOvertimeTypeCheckBox.Checked)
                {
                    foreach (XmlNode setting in settingsDoc.DocumentElement.ChildNodes)
                        if ((setting.Name == "ActivityDefaultOvertimeType") && (setting.Attributes["Activity"] != null) &&
                            (setting.Attributes["Activity"].Value == this.activitiesComboBox.Text))
                        {
                            setting.FirstChild.Value = this.overtimeTypesComboBox.Text;
                            defaultActivityOvertimeTypeExists = true;
                            break;
                        }
                    if (!defaultActivityOvertimeTypeExists)
                    {
                        XmlNode tmpNode = settingsDoc.DocumentElement.AppendChild(settingsDoc.CreateElement("ActivityDefaultOvertimeType"));
                        tmpNode.Attributes.Append(settingsDoc.CreateAttribute("Activity"));
                        tmpNode.Attributes["Activity"].Value = this.activitiesComboBox.Text;
                        tmpNode.AppendChild(settingsDoc.CreateTextNode(this.overtimeTypesComboBox.Text));
                    }
                }
                else
                {
                    foreach (XmlNode setting in settingsDoc.DocumentElement.ChildNodes)
                        if ((setting.Name == "ActivityDefaultOvertimeType") && (setting.Attributes["Activity"] != null) &&
                            (setting.Attributes["Activity"].Value == this.activitiesComboBox.Text))
                        {
                            settingsDoc.DocumentElement.RemoveChild(setting);
                            break;
                        }
                }
                settingsDoc.Save("settings.xml");
                _settings.Reload();
            }
        }

        private void defaultCustomerCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            if (!_defaultCustomerCheckboxChanged)
            {
                if (defaultCustomerCheckBox.Checked)
                {
                    _settings["DefaultCustomer"] = customersComboBox.Text;
                    _defaultCustomer = (ObjectStruct)customersComboBox.SelectedValue;
                }
                else
                {
                    _settings["DefaultCustomer"] = "";
                    _defaultCustomer = null;
                }
                _settings.Save();
            }
        }

        private void defaultActivityCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            if (!_defaultActivityCheckboxChanged)
            {
                if (defaultActivityCheckBox.Checked)
                {
                    _settings["DefaultActivity"] = activitiesComboBox.Text;
                    _defaultActivity = (ObjectStruct)activitiesComboBox.SelectedValue;
                }
                else
                {
                    _settings["DefaultActivity"] = "";
                    _defaultActivity = null;
                }
                _settings.Save();
            }
        }

        private void customerDefaultActivityCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            if (!_defaultActivityCheckboxChanged)
            {
                XmlDocument settingsDoc = new System.Xml.XmlDocument();
                settingsDoc.Load("settings.xml");
                bool defaultCustomerActivityExists = false;
                if (customerDefaultActivityCheckBox.Checked)
                {
                    foreach (XmlNode setting in settingsDoc.DocumentElement.ChildNodes)
                        if ((setting.Name == "CustomerDefaultActivity") && (setting.Attributes["Customer"] != null) &&
                            (setting.Attributes["Customer"].Value == customersComboBox.Text))
                        {
                            setting.FirstChild.Value = activitiesComboBox.Text;
                            defaultCustomerActivityExists = true;
                            break;
                        }
                    if (!defaultCustomerActivityExists)
                    {
                        XmlNode tmpNode = settingsDoc.DocumentElement.AppendChild(settingsDoc.CreateElement("CustomerDefaultActivity"));
                        tmpNode.Attributes.Append(settingsDoc.CreateAttribute("Customer"));
                        tmpNode.Attributes["Customer"].Value = customersComboBox.Text;
                        tmpNode.AppendChild(settingsDoc.CreateTextNode(activitiesComboBox.Text));
                    }
                }
                else
                {
                    foreach (XmlNode setting in settingsDoc.DocumentElement.ChildNodes)
                        if ((setting.Name == "CustomerDefaultActivity") && (setting.Attributes["Customer"] != null) &&
                            (setting.Attributes["Customer"].Value == customersComboBox.Text))
                        {
                            settingsDoc.DocumentElement.RemoveChild(setting);
                            break;
                        }
                }
                settingsDoc.Save("settings.xml");
                _settings.Reload();
            }
        }

        private void cancelWorkingMenuItem_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel a work?", "Cancel work confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                startWorkingMenuItem.Enabled = true;
                stopWorkingMenuItem.Enabled = false;
                cancelWorkingMenuItem.Enabled = false;
                idleReminderTimer.Start();
                if (_reminderTimer != null)
                    _reminderTimer.Change(Timeout.Infinite, Timeout.Infinite);
                this.SetIdleTrayIcon();
                SetTodayWorkDurationNotifyIconText();
            }
        }

        private void SetTodayWorkDurationNotifyIconText()
        {
            if (_todayWorkDuration == 0)
                try
                {
                    notifyIcon.Text = _notifyIconStatusText + "\nYou didn't work today yet";
                }
                catch (Exception)
                {
                }
            else
            {
                int hours = (int)(_todayWorkDuration / 60);
                int minutes = _todayWorkDuration - (int)(_todayWorkDuration / 60) * 60;
                notifyIcon.Text = _notifyIconStatusText + "\nToday's time of work: " + String.Format("{0}:{1:0#}", hours, minutes);
            }
        }

        private void aboutMenuItem_Click(object sender, System.EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private UpdateResult CheckUpdates()
        {
            try
            {
                UpdateResult result = Updater.Update(_settings["URL"].Substring(0, _settings["URL"].LastIndexOf("/")) + "/client/");
                if (result == UpdateResult.UpToDate)
                {
#if DEBUG
                    EventLog.WriteEntry("ActivityEvidence", "The application is up to date", EventLogEntryType.Information);
#endif
                }
                else if (result == UpdateResult.Restart)
                {
                    try
                    {
                        EventLog.WriteEntry("ActivityEvidence", "The application is updated. Restart is needed", EventLogEntryType.Information);
                    }
                    catch (Exception)
                    { }
                    MessageBox.Show("The application is updated. Restart is needed", "Activity Evidence recorder");
                    this.Restart();
                }
                else if (result == UpdateResult.Success)
                {
                    try
                    {
                        EventLog.WriteEntry("ActivityEvidence", "The application is updated successfully. Current version is " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version, EventLogEntryType.Information);
                    }
                    catch (Exception)
                    { }
                    MessageBox.Show("The application is updated successfully. Current version is " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version, "Activity Evidence recorder");
                }
                else if (result == UpdateResult.Fail)
                {
                    try
                    {
                        EventLog.WriteEntry("ActivityEvidence", "Application failed to update.", EventLogEntryType.Error);
                    }
                    catch (Exception)
                    { }
                }
                return result;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return UpdateResult.Fail;
            }
        }

        private void Restart(bool asAdmin = false)
        {
            Program.ProgramMutex.ReleaseMutex();
            ProcessStartInfo si = new ProcessStartInfo(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            if (asAdmin)
                si.Verb = "runas";
            Process.Start(si);
            Application.Exit();
        }

        private void updateMenuItem_Click(object sender, System.EventArgs e)
        {
            if (CheckUpdates() == UpdateResult.UpToDate)
                MessageBox.Show("The application is up to date", "Activity Evidence recorder");
        }

        private void ValidateData()
        {
            this.commentTextBox_Validating(commentTextBox, null);
            if (this._errorProvider.GetError(this.commentTextBox) != "")
                throw new ActivityEvidence.Exceptions.DataValidationException(this._errorProvider.GetError(this.commentTextBox));
        }

        private void ticketNumberTextBox_TextChanged(object sender, System.EventArgs e)
        {
            ticketNumberTextBox.Text = ticketNumberTextBox.Text.Trim();
        }

        private void claimMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                new ClaimPrepareForm(_service, _settings).ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void saveAndButton_Click(object sender, System.EventArgs e)
        {
            saveContextMenu.Show((Control)sender, new Point(((Control)sender).Width, 0));
        }

        private void SetDefaultSaveAction()
        {
            this.defaultActionButton.Tag = this._saveMenuItems[this._settings["DefaultSaveAction"]];
            if (this.defaultActionButton.Tag != null)
            {
                this.defaultActionButton.Click += (EventHandler)this._saveHandlers[this._settings["DefaultSaveAction"]];
                this.defaultActionButton.Text = ((MenuItem)this.defaultActionButton.Tag).Text;
                ((MenuItem)this.defaultActionButton.Tag).Visible = false;
            }
            else
            {
                this.defaultActionButton.Click += new EventHandler(saveAndButton_Click);
            }
        }


        private void SaveAndClose_Action(object sender, System.EventArgs e)
        {
            if (SaveRecord())
            {
                this.Hide();
            }
        }

        private void SaveAndClone_Action(object sender, System.EventArgs e)
        {
            if (this.SaveRecord())
                this.ShowTrayNotification("Activity Evidence recorder", "Data is saved", 2000);
        }

        private void SaveAndClear_Action(object sender, System.EventArgs e)
        {
            if (this.SaveRecord())
                this.ClearForm();
        }

        private void SaveAndStartWorking_Action(object sender, EventArgs e)
        {
            if (this.SaveRecord())
            {
                startWorkingMenuItem_Click(sender, e);
                this.Hide();
            }
        }

        private void overtimeTypesComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.SetOvertimeFieldsRequired(((ObjectStruct)overtimeTypesComboBox.SelectedItem).ObjectID != 0);
            }
            catch (NullReferenceException) { }
        }

        private void SetOvertimeFieldsRequired(bool isRequired)
        {
            this.SetDescriptionFieldRequired(isRequired);
        }

        private void SetDescriptionFieldRequired(bool isRequired)
        {
            this.requiredDescriptionLabel.Visible = isRequired;
        }

        private void commentTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (this.requiredDescriptionLabel.Visible && 
                (this.commentTextBox.Text == ""))
                this._errorProvider.SetError(this.commentTextBox, "The description field is required for overtime entries");
            else
                this._errorProvider.SetError(this.commentTextBox, null);
        }

        private void overtimeTypesComboBox_Validating(object sender, CancelEventArgs e)
        {
            this.commentTextBox_Validating(sender, e);
        }

        private void ShowTrayNotification(string title, string message, uint duration)
        {
            if (this.InvokeRequired)
                this.Invoke((Action)delegate
                {
                    this._notifier.ShowBalloon(1, title, message, duration);
                });
            else
                this._notifier.ShowBalloon(1, title, message, duration);
        }
    }
}
