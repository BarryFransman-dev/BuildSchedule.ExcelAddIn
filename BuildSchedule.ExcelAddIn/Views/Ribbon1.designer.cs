namespace BuildSchedule.ExcelAddIn
{
    partial class SysproBuildSchedule : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public SysproBuildSchedule()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SysproBuildSchedule));
            this.AddIn2 = this.Factory.CreateRibbonTab();
            this.group3 = this.Factory.CreateRibbonGroup();
            this.button2 = this.Factory.CreateRibbonButton();
            this.separator2 = this.Factory.CreateRibbonSeparator();
            this.button4 = this.Factory.CreateRibbonButton();
            this.lblUser = this.Factory.CreateRibbonLabel();
            this.lblCompany = this.Factory.CreateRibbonLabel();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.btnRefresh = this.Factory.CreateRibbonButton();
            this.ddWarehouse = this.Factory.CreateRibbonDropDown();
            this.lblVersion = this.Factory.CreateRibbonLabel();
            this.ddCheckQty = this.Factory.CreateRibbonCheckBox();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.button1 = this.Factory.CreateRibbonButton();
            this.AddIn2.SuspendLayout();
            this.group3.SuspendLayout();
            this.group2.SuspendLayout();
            this.group1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddIn2
            // 
            this.AddIn2.Groups.Add(this.group3);
            this.AddIn2.Groups.Add(this.group2);
            this.AddIn2.Groups.Add(this.group1);
            this.AddIn2.Label = "Build Schedule";
            this.AddIn2.Name = "AddIn2";
            // 
            // group3
            // 
            this.group3.Items.Add(this.button2);
            this.group3.Items.Add(this.separator2);
            this.group3.Items.Add(this.button4);
            this.group3.Items.Add(this.lblUser);
            this.group3.Items.Add(this.lblCompany);
            this.group3.Label = "Login";
            this.group3.Name = "group3";
            // 
            // button2
            // 
            this.button2.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Label = "Syspro Log In";
            this.button2.Name = "button2";
            this.button2.ScreenTip = "Log in to Syspro";
            this.button2.ShowImage = true;
            this.button2.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnLogin_Click);
            // 
            // separator2
            // 
            this.separator2.Name = "separator2";
            // 
            // button4
            // 
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Label = " ";
            this.button4.Name = "button4";
            this.button4.ShowImage = true;
            // 
            // lblUser
            // 
            this.lblUser.Label = "User: ";
            this.lblUser.Name = "lblUser";
            // 
            // lblCompany
            // 
            this.lblCompany.Label = "Company: ";
            this.lblCompany.Name = "lblCompany";
            // 
            // group2
            // 
            this.group2.Items.Add(this.btnRefresh);
            this.group2.Items.Add(this.ddWarehouse);
            this.group2.Items.Add(this.lblVersion);
            this.group2.Items.Add(this.ddCheckQty);
            this.group2.Label = "Selection";
            this.group2.Name = "group2";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Label = "Refresh Data";
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.ShowImage = true;
            this.btnRefresh.Visible = false;
            // 
            // ddWarehouse
            // 
            this.ddWarehouse.Label = "Warehouse";
            this.ddWarehouse.Name = "ddWarehouse";
            this.ddWarehouse.SizeString = "NNN";
            this.ddWarehouse.Visible = false;
            // 
            // lblVersion
            // 
            this.lblVersion.Label = "Ver 1.001";
            this.lblVersion.Name = "lblVersion";
            // 
            // ddCheckQty
            // 
            this.ddCheckQty.Label = "Qty  On Hand 0";
            this.ddCheckQty.Name = "ddCheckQty";
            this.ddCheckQty.Visible = false;
            // 
            // group1
            // 
            this.group1.Items.Add(this.button1);
            this.group1.Label = "Syspro Transactions";
            this.group1.Name = "group1";
            // 
            // button1
            // 
            this.button1.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Label = "Post";
            this.button1.Name = "button1";
            this.button1.ShowImage = true;
            this.button1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnPost_Click);
            // 
            // SysproBuildSchedule
            // 
            this.Name = "SysproBuildSchedule";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.AddIn2);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
            this.AddIn2.ResumeLayout(false);
            this.AddIn2.PerformLayout();
            this.group3.ResumeLayout(false);
            this.group3.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Office.Tools.Ribbon.RibbonTab AddIn2;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        private Microsoft.Office.Tools.Ribbon.RibbonButton button1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown ddWarehouse;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel lblVersion;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group3;
        private Microsoft.Office.Tools.Ribbon.RibbonButton button2;
        private Microsoft.Office.Tools.Ribbon.RibbonSeparator separator2;
        private Microsoft.Office.Tools.Ribbon.RibbonButton button4;
        private Microsoft.Office.Tools.Ribbon.RibbonLabel lblUser;
        private Microsoft.Office.Tools.Ribbon.RibbonLabel lblCompany;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox ddCheckQty;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnRefresh;
    }

    partial class ThisRibbonCollection
    {
        internal SysproBuildSchedule Ribbon1
        {
            get { return this.GetRibbon<SysproBuildSchedule>(); }
        }
    }
}
