using SolutionGenerator.Models;

namespace SolutionGenerator.Frontend
{
    partial class GeneratorForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label projectAssemblyNameLabel;
            System.Windows.Forms.Label projectNameLabel;
            System.Windows.Forms.Label projectRootNameSpaceLabel;
            System.Windows.Forms.Label rootPathLabel;
            System.Windows.Forms.Label solutionNameLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label includeGitAttributeLabel;
            System.Windows.Forms.Label includeGitIgnoreLabel;
            System.Windows.Forms.Label includeReadmeLabel;
            System.Windows.Forms.Label includeResharperLabel;
            System.Windows.Forms.Label includeStylecopLabel;
            System.Windows.Forms.Label includeTestProjectLabel;
            System.Windows.Forms.Label targetFrameworkLabel1;
            System.Windows.Forms.Label initiliazeGitLabel;
            System.Windows.Forms.Label projectOutputTypeLabel;
            System.Windows.Forms.Label licenseNameLabel;
            this.projectAssemblyNameTextBox = new System.Windows.Forms.TextBox();
            this.solutionModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.projectNameTextBox = new System.Windows.Forms.TextBox();
            this.projectRootNameSpaceTextBox = new System.Windows.Forms.TextBox();
            this.rootPathTextBox = new System.Windows.Forms.TextBox();
            this.solutionNameTextBox = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.readmeRichTextBox = new System.Windows.Forms.RichTextBox();
            this.chkStartVisualStudio = new System.Windows.Forms.CheckBox();
            this.includeGitAttributeCheckBox = new System.Windows.Forms.CheckBox();
            this.includeGitIgnoreCheckBox = new System.Windows.Forms.CheckBox();
            this.includeReadmeCheckBox = new System.Windows.Forms.CheckBox();
            this.includeResharperCheckBox = new System.Windows.Forms.CheckBox();
            this.includeStylecopCheckBox = new System.Windows.Forms.CheckBox();
            this.includeTestProjectCheckBox = new System.Windows.Forms.CheckBox();
            this.targetFrameworkComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.initiliazeGitCheckBox = new System.Windows.Forms.CheckBox();
            this.projectOutputTypeComboBox = new System.Windows.Forms.ComboBox();
            this.licenseNameComboBox = new System.Windows.Forms.ComboBox();
            this.availableLicensesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.openFolderOnCreateCheckBox = new System.Windows.Forms.CheckBox();
            projectAssemblyNameLabel = new System.Windows.Forms.Label();
            projectNameLabel = new System.Windows.Forms.Label();
            projectRootNameSpaceLabel = new System.Windows.Forms.Label();
            rootPathLabel = new System.Windows.Forms.Label();
            solutionNameLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            includeGitAttributeLabel = new System.Windows.Forms.Label();
            includeGitIgnoreLabel = new System.Windows.Forms.Label();
            includeReadmeLabel = new System.Windows.Forms.Label();
            includeResharperLabel = new System.Windows.Forms.Label();
            includeStylecopLabel = new System.Windows.Forms.Label();
            includeTestProjectLabel = new System.Windows.Forms.Label();
            targetFrameworkLabel1 = new System.Windows.Forms.Label();
            initiliazeGitLabel = new System.Windows.Forms.Label();
            projectOutputTypeLabel = new System.Windows.Forms.Label();
            licenseNameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.solutionModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.availableLicensesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // projectAssemblyNameLabel
            // 
            projectAssemblyNameLabel.AutoSize = true;
            projectAssemblyNameLabel.Location = new System.Drawing.Point(12, 146);
            projectAssemblyNameLabel.Name = "projectAssemblyNameLabel";
            projectAssemblyNameLabel.Size = new System.Drawing.Size(121, 13);
            projectAssemblyNameLabel.TabIndex = 0;
            projectAssemblyNameLabel.Text = "Project Assembly Name:";
            this.toolTip1.SetToolTip(projectAssemblyNameLabel, "Output dll name (without extension) of the generated project");
            // 
            // projectNameLabel
            // 
            projectNameLabel.AutoSize = true;
            projectNameLabel.Location = new System.Drawing.Point(12, 67);
            projectNameLabel.Name = "projectNameLabel";
            projectNameLabel.Size = new System.Drawing.Size(74, 13);
            projectNameLabel.TabIndex = 4;
            projectNameLabel.Text = "Project Name:";
            this.toolTip1.SetToolTip(projectNameLabel, "Project name can be different to Solution name.");
            // 
            // projectRootNameSpaceLabel
            // 
            projectRootNameSpaceLabel.AutoSize = true;
            projectRootNameSpaceLabel.Location = new System.Drawing.Point(12, 120);
            projectRootNameSpaceLabel.Name = "projectRootNameSpaceLabel";
            projectRootNameSpaceLabel.Size = new System.Drawing.Size(134, 13);
            projectRootNameSpaceLabel.TabIndex = 6;
            projectRootNameSpaceLabel.Text = "Project Root Name Space:";
            this.toolTip1.SetToolTip(projectRootNameSpaceLabel, "This will be the namespace used for classes added to the root of the generated pr" +
        "oject.");
            // 
            // rootPathLabel
            // 
            rootPathLabel.AutoSize = true;
            rootPathLabel.Location = new System.Drawing.Point(12, 15);
            rootPathLabel.Name = "rootPathLabel";
            rootPathLabel.Size = new System.Drawing.Size(58, 13);
            rootPathLabel.TabIndex = 8;
            rootPathLabel.Text = "Root Path:";
            this.toolTip1.SetToolTip(rootPathLabel, "Root folder the solution and project structure will be created in");
            // 
            // solutionNameLabel
            // 
            solutionNameLabel.AutoSize = true;
            solutionNameLabel.Location = new System.Drawing.Point(12, 41);
            solutionNameLabel.Name = "solutionNameLabel";
            solutionNameLabel.Size = new System.Drawing.Size(79, 13);
            solutionNameLabel.TabIndex = 12;
            solutionNameLabel.Text = "Solution Name:";
            this.toolTip1.SetToolTip(solutionNameLabel, "Name for the main solution");
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 226);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(91, 13);
            label1.TabIndex = 14;
            label1.Text = "ReadMe Content:";
            // 
            // includeGitAttributeLabel
            // 
            includeGitAttributeLabel.AutoSize = true;
            includeGitAttributeLabel.Location = new System.Drawing.Point(20, 24);
            includeGitAttributeLabel.Name = "includeGitAttributeLabel";
            includeGitAttributeLabel.Size = new System.Drawing.Size(63, 13);
            includeGitAttributeLabel.TabIndex = 0;
            includeGitAttributeLabel.Text = ".gitAttribute:";
            // 
            // includeGitIgnoreLabel
            // 
            includeGitIgnoreLabel.AutoSize = true;
            includeGitIgnoreLabel.Location = new System.Drawing.Point(20, 54);
            includeGitIgnoreLabel.Name = "includeGitIgnoreLabel";
            includeGitIgnoreLabel.Size = new System.Drawing.Size(54, 13);
            includeGitIgnoreLabel.TabIndex = 23;
            includeGitIgnoreLabel.Text = ".gitIgnore:";
            // 
            // includeReadmeLabel
            // 
            includeReadmeLabel.AutoSize = true;
            includeReadmeLabel.Location = new System.Drawing.Point(20, 84);
            includeReadmeLabel.Name = "includeReadmeLabel";
            includeReadmeLabel.Size = new System.Drawing.Size(67, 13);
            includeReadmeLabel.TabIndex = 27;
            includeReadmeLabel.Text = "Readme.md:";
            // 
            // includeResharperLabel
            // 
            includeResharperLabel.AutoSize = true;
            includeResharperLabel.Location = new System.Drawing.Point(20, 114);
            includeResharperLabel.Name = "includeResharperLabel";
            includeResharperLabel.Size = new System.Drawing.Size(59, 13);
            includeResharperLabel.TabIndex = 29;
            includeResharperLabel.Text = "Resharper:";
            this.toolTip1.SetToolTip(includeResharperLabel, "Resharper settings file.");
            // 
            // includeStylecopLabel
            // 
            includeStylecopLabel.AutoSize = true;
            includeStylecopLabel.Location = new System.Drawing.Point(20, 144);
            includeStylecopLabel.Name = "includeStylecopLabel";
            includeStylecopLabel.Size = new System.Drawing.Size(51, 13);
            includeStylecopLabel.TabIndex = 31;
            includeStylecopLabel.Text = "Stylecop:";
            this.toolTip1.SetToolTip(includeStylecopLabel, "Stylecop settings file");
            // 
            // includeTestProjectLabel
            // 
            includeTestProjectLabel.AutoSize = true;
            includeTestProjectLabel.Location = new System.Drawing.Point(20, 174);
            includeTestProjectLabel.Name = "includeTestProjectLabel";
            includeTestProjectLabel.Size = new System.Drawing.Size(67, 13);
            includeTestProjectLabel.TabIndex = 33;
            includeTestProjectLabel.Text = "Test Project:";
            // 
            // targetFrameworkLabel1
            // 
            targetFrameworkLabel1.AutoSize = true;
            targetFrameworkLabel1.Location = new System.Drawing.Point(12, 172);
            targetFrameworkLabel1.Name = "targetFrameworkLabel1";
            targetFrameworkLabel1.Size = new System.Drawing.Size(96, 13);
            targetFrameworkLabel1.TabIndex = 35;
            targetFrameworkLabel1.Text = "Target Framework:";
            // 
            // initiliazeGitLabel
            // 
            initiliazeGitLabel.AutoSize = true;
            initiliazeGitLabel.Location = new System.Drawing.Point(583, 229);
            initiliazeGitLabel.Name = "initiliazeGitLabel";
            initiliazeGitLabel.Size = new System.Drawing.Size(63, 13);
            initiliazeGitLabel.TabIndex = 37;
            initiliazeGitLabel.Text = "Initiliaze Git:";
            // 
            // projectOutputTypeLabel
            // 
            projectOutputTypeLabel.AutoSize = true;
            projectOutputTypeLabel.Location = new System.Drawing.Point(12, 93);
            projectOutputTypeLabel.Name = "projectOutputTypeLabel";
            projectOutputTypeLabel.Size = new System.Drawing.Size(105, 13);
            projectOutputTypeLabel.TabIndex = 38;
            projectOutputTypeLabel.Text = "Project Output Type:";
            // 
            // licenseNameLabel
            // 
            licenseNameLabel.AutoSize = true;
            licenseNameLabel.Location = new System.Drawing.Point(12, 199);
            licenseNameLabel.Name = "licenseNameLabel";
            licenseNameLabel.Size = new System.Drawing.Size(78, 13);
            licenseNameLabel.TabIndex = 38;
            licenseNameLabel.Text = "License Name:";
            // 
            // projectAssemblyNameTextBox
            // 
            this.projectAssemblyNameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.solutionModelBindingSource, "ProjectAssemblyName", true));
            this.projectAssemblyNameTextBox.Location = new System.Drawing.Point(152, 143);
            this.projectAssemblyNameTextBox.Name = "projectAssemblyNameTextBox";
            this.projectAssemblyNameTextBox.Size = new System.Drawing.Size(381, 20);
            this.projectAssemblyNameTextBox.TabIndex = 6;
            // 
            // solutionModelBindingSource
            // 
            this.solutionModelBindingSource.DataSource = typeof(SolutionGenerator.Models.SolutionModel);
            // 
            // projectNameTextBox
            // 
            this.projectNameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.solutionModelBindingSource, "ProjectName", true));
            this.projectNameTextBox.Location = new System.Drawing.Point(152, 64);
            this.projectNameTextBox.Name = "projectNameTextBox";
            this.projectNameTextBox.Size = new System.Drawing.Size(381, 20);
            this.projectNameTextBox.TabIndex = 3;
            // 
            // projectRootNameSpaceTextBox
            // 
            this.projectRootNameSpaceTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.solutionModelBindingSource, "ProjectRootNameSpace", true));
            this.projectRootNameSpaceTextBox.Location = new System.Drawing.Point(152, 117);
            this.projectRootNameSpaceTextBox.Name = "projectRootNameSpaceTextBox";
            this.projectRootNameSpaceTextBox.Size = new System.Drawing.Size(381, 20);
            this.projectRootNameSpaceTextBox.TabIndex = 5;
            // 
            // rootPathTextBox
            // 
            this.rootPathTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.solutionModelBindingSource, "RootPath", true));
            this.rootPathTextBox.Location = new System.Drawing.Point(152, 12);
            this.rootPathTextBox.Name = "rootPathTextBox";
            this.rootPathTextBox.Size = new System.Drawing.Size(293, 20);
            this.rootPathTextBox.TabIndex = 0;
            // 
            // solutionNameTextBox
            // 
            this.solutionNameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.solutionModelBindingSource, "SolutionName", true));
            this.solutionNameTextBox.Location = new System.Drawing.Point(152, 38);
            this.solutionNameTextBox.Name = "solutionNameTextBox";
            this.solutionNameTextBox.Size = new System.Drawing.Size(381, 20);
            this.solutionNameTextBox.TabIndex = 2;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(152, 450);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(381, 23);
            this.btnGenerate.TabIndex = 12;
            this.btnGenerate.Text = "Give me back 15 mins of my lilfe.";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.BtnGenerateClick);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(464, 12);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(69, 20);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowseClick);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Select a root folder";
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataSource = this.solutionModelBindingSource;
            // 
            // readmeRichTextBox
            // 
            this.readmeRichTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.solutionModelBindingSource, "SolutionReadme", true));
            this.readmeRichTextBox.Location = new System.Drawing.Point(152, 223);
            this.readmeRichTextBox.Name = "readmeRichTextBox";
            this.readmeRichTextBox.Size = new System.Drawing.Size(381, 191);
            this.readmeRichTextBox.TabIndex = 9;
            this.readmeRichTextBox.Text = "";
            // 
            // chkStartVisualStudio
            // 
            this.chkStartVisualStudio.AutoSize = true;
            this.chkStartVisualStudio.Location = new System.Drawing.Point(343, 424);
            this.chkStartVisualStudio.Name = "chkStartVisualStudio";
            this.chkStartVisualStudio.Size = new System.Drawing.Size(112, 17);
            this.chkStartVisualStudio.TabIndex = 11;
            this.chkStartVisualStudio.Text = "Start Visual Studio";
            this.chkStartVisualStudio.UseVisualStyleBackColor = true;
            // 
            // includeGitAttributeCheckBox
            // 
            this.includeGitAttributeCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.solutionModelBindingSource, "IncludeGitAttribute", true));
            this.includeGitAttributeCheckBox.Location = new System.Drawing.Point(131, 19);
            this.includeGitAttributeCheckBox.Name = "includeGitAttributeCheckBox";
            this.includeGitAttributeCheckBox.Size = new System.Drawing.Size(21, 24);
            this.includeGitAttributeCheckBox.TabIndex = 1;
            this.includeGitAttributeCheckBox.UseVisualStyleBackColor = true;
            // 
            // includeGitIgnoreCheckBox
            // 
            this.includeGitIgnoreCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.solutionModelBindingSource, "IncludeGitIgnore", true));
            this.includeGitIgnoreCheckBox.Location = new System.Drawing.Point(131, 49);
            this.includeGitIgnoreCheckBox.Name = "includeGitIgnoreCheckBox";
            this.includeGitIgnoreCheckBox.Size = new System.Drawing.Size(21, 24);
            this.includeGitIgnoreCheckBox.TabIndex = 2;
            this.includeGitIgnoreCheckBox.UseVisualStyleBackColor = true;
            // 
            // includeReadmeCheckBox
            // 
            this.includeReadmeCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.solutionModelBindingSource, "IncludeReadme", true));
            this.includeReadmeCheckBox.Location = new System.Drawing.Point(131, 79);
            this.includeReadmeCheckBox.Name = "includeReadmeCheckBox";
            this.includeReadmeCheckBox.Size = new System.Drawing.Size(21, 24);
            this.includeReadmeCheckBox.TabIndex = 4;
            this.includeReadmeCheckBox.UseVisualStyleBackColor = true;
            // 
            // includeResharperCheckBox
            // 
            this.includeResharperCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.solutionModelBindingSource, "IncludeResharper", true));
            this.includeResharperCheckBox.Location = new System.Drawing.Point(131, 109);
            this.includeResharperCheckBox.Name = "includeResharperCheckBox";
            this.includeResharperCheckBox.Size = new System.Drawing.Size(21, 24);
            this.includeResharperCheckBox.TabIndex = 5;
            this.includeResharperCheckBox.UseVisualStyleBackColor = true;
            // 
            // includeStylecopCheckBox
            // 
            this.includeStylecopCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.solutionModelBindingSource, "IncludeStylecop", true));
            this.includeStylecopCheckBox.Location = new System.Drawing.Point(131, 139);
            this.includeStylecopCheckBox.Name = "includeStylecopCheckBox";
            this.includeStylecopCheckBox.Size = new System.Drawing.Size(21, 24);
            this.includeStylecopCheckBox.TabIndex = 6;
            this.includeStylecopCheckBox.UseVisualStyleBackColor = true;
            // 
            // includeTestProjectCheckBox
            // 
            this.includeTestProjectCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.solutionModelBindingSource, "IncludeTestProject", true));
            this.includeTestProjectCheckBox.Location = new System.Drawing.Point(131, 169);
            this.includeTestProjectCheckBox.Name = "includeTestProjectCheckBox";
            this.includeTestProjectCheckBox.Size = new System.Drawing.Size(21, 24);
            this.includeTestProjectCheckBox.TabIndex = 7;
            this.includeTestProjectCheckBox.UseVisualStyleBackColor = true;
            // 
            // targetFrameworkComboBox
            // 
            this.targetFrameworkComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.solutionModelBindingSource, "TargetFramework", true));
            this.targetFrameworkComboBox.FormattingEnabled = true;
            this.targetFrameworkComboBox.Items.AddRange(new object[] {
            "v2.0",
            "v3.0",
            "v3.5",
            "v4.0",
            "v4.5"});
            this.targetFrameworkComboBox.Location = new System.Drawing.Point(152, 169);
            this.targetFrameworkComboBox.Name = "targetFrameworkComboBox";
            this.targetFrameworkComboBox.Size = new System.Drawing.Size(381, 21);
            this.targetFrameworkComboBox.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.includeGitIgnoreCheckBox);
            this.groupBox1.Controls.Add(this.includeReadmeCheckBox);
            this.groupBox1.Controls.Add(includeReadmeLabel);
            this.groupBox1.Controls.Add(includeResharperLabel);
            this.groupBox1.Controls.Add(this.includeResharperCheckBox);
            this.groupBox1.Controls.Add(includeGitAttributeLabel);
            this.groupBox1.Controls.Add(includeStylecopLabel);
            this.groupBox1.Controls.Add(this.includeStylecopCheckBox);
            this.groupBox1.Controls.Add(this.includeGitAttributeCheckBox);
            this.groupBox1.Controls.Add(includeTestProjectLabel);
            this.groupBox1.Controls.Add(this.includeTestProjectCheckBox);
            this.groupBox1.Controls.Add(includeGitIgnoreLabel);
            this.groupBox1.Location = new System.Drawing.Point(563, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(157, 202);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Include";
            // 
            // initiliazeGitCheckBox
            // 
            this.initiliazeGitCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.solutionModelBindingSource, "InitiliazeGit", true));
            this.initiliazeGitCheckBox.Location = new System.Drawing.Point(694, 223);
            this.initiliazeGitCheckBox.Name = "initiliazeGitCheckBox";
            this.initiliazeGitCheckBox.Size = new System.Drawing.Size(104, 24);
            this.initiliazeGitCheckBox.TabIndex = 14;
            this.initiliazeGitCheckBox.UseVisualStyleBackColor = true;
            // 
            // projectOutputTypeComboBox
            // 
            this.projectOutputTypeComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.solutionModelBindingSource, "ProjectType", true));
            this.projectOutputTypeComboBox.FormattingEnabled = true;
            this.projectOutputTypeComboBox.Items.AddRange(new object[] {
            "Library",
            "WPF",
            "WinForms",
            "Console"});
            this.projectOutputTypeComboBox.Location = new System.Drawing.Point(152, 90);
            this.projectOutputTypeComboBox.Name = "projectOutputTypeComboBox";
            this.projectOutputTypeComboBox.Size = new System.Drawing.Size(381, 21);
            this.projectOutputTypeComboBox.TabIndex = 4;
            // 
            // licenseNameComboBox
            // 
            this.licenseNameComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.solutionModelBindingSource, "LicenseName", true));
            this.licenseNameComboBox.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.solutionModelBindingSource, "LicenseName", true));
            this.licenseNameComboBox.DataSource = this.availableLicensesBindingSource;
            this.licenseNameComboBox.FormattingEnabled = true;
            this.licenseNameComboBox.Location = new System.Drawing.Point(152, 196);
            this.licenseNameComboBox.Name = "licenseNameComboBox";
            this.licenseNameComboBox.Size = new System.Drawing.Size(381, 21);
            this.licenseNameComboBox.TabIndex = 8;
            // 
            // availableLicensesBindingSource
            // 
            this.availableLicensesBindingSource.DataMember = "AvailableLicenses";
            this.availableLicensesBindingSource.DataSource = this.solutionModelBindingSource;
            // 
            // openFolderOnCreateCheckBox
            // 
            this.openFolderOnCreateCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.solutionModelBindingSource, "OpenFolderOnCreate", true));
            this.openFolderOnCreateCheckBox.Location = new System.Drawing.Point(152, 420);
            this.openFolderOnCreateCheckBox.Name = "openFolderOnCreateCheckBox";
            this.openFolderOnCreateCheckBox.Size = new System.Drawing.Size(185, 24);
            this.openFolderOnCreateCheckBox.TabIndex = 10;
            this.openFolderOnCreateCheckBox.Text = "Open Folder After Create";
            this.openFolderOnCreateCheckBox.UseVisualStyleBackColor = true;
            // 
            // GeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 482);
            this.Controls.Add(this.openFolderOnCreateCheckBox);
            this.Controls.Add(licenseNameLabel);
            this.Controls.Add(this.licenseNameComboBox);
            this.Controls.Add(projectOutputTypeLabel);
            this.Controls.Add(this.projectOutputTypeComboBox);
            this.Controls.Add(initiliazeGitLabel);
            this.Controls.Add(this.initiliazeGitCheckBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(targetFrameworkLabel1);
            this.Controls.Add(this.targetFrameworkComboBox);
            this.Controls.Add(this.chkStartVisualStudio);
            this.Controls.Add(this.readmeRichTextBox);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(projectAssemblyNameLabel);
            this.Controls.Add(this.projectAssemblyNameTextBox);
            this.Controls.Add(projectNameLabel);
            this.Controls.Add(this.projectNameTextBox);
            this.Controls.Add(projectRootNameSpaceLabel);
            this.Controls.Add(this.projectRootNameSpaceTextBox);
            this.Controls.Add(rootPathLabel);
            this.Controls.Add(this.rootPathTextBox);
            this.Controls.Add(solutionNameLabel);
            this.Controls.Add(this.solutionNameTextBox);
            this.Controls.Add(label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GeneratorForm";
            this.Text = "Solution Generator";
            ((System.ComponentModel.ISupportInitialize)(this.solutionModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.availableLicensesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource solutionModelBindingSource;
        private System.Windows.Forms.TextBox projectAssemblyNameTextBox;
        private System.Windows.Forms.TextBox projectNameTextBox;
        private System.Windows.Forms.TextBox projectRootNameSpaceTextBox;
        private System.Windows.Forms.TextBox rootPathTextBox;
        private System.Windows.Forms.TextBox solutionNameTextBox;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.RichTextBox readmeRichTextBox;
        private System.Windows.Forms.CheckBox chkStartVisualStudio;
        private System.Windows.Forms.CheckBox includeGitAttributeCheckBox;
        private System.Windows.Forms.CheckBox includeGitIgnoreCheckBox;
        private System.Windows.Forms.CheckBox includeReadmeCheckBox;
        private System.Windows.Forms.CheckBox includeResharperCheckBox;
        private System.Windows.Forms.CheckBox includeStylecopCheckBox;
        private System.Windows.Forms.CheckBox includeTestProjectCheckBox;
        private System.Windows.Forms.ComboBox targetFrameworkComboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox initiliazeGitCheckBox;
        private System.Windows.Forms.ComboBox projectOutputTypeComboBox;
        private System.Windows.Forms.ComboBox licenseNameComboBox;
        private System.Windows.Forms.BindingSource availableLicensesBindingSource;
        private System.Windows.Forms.CheckBox openFolderOnCreateCheckBox;
    }
}
