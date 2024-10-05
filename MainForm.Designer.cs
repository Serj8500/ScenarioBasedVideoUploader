namespace ScenarioBasedVideoUploader
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageDebugger = new System.Windows.Forms.TabPage();
            this.groupBoxCurrentScenario = new System.Windows.Forms.GroupBox();
            this.buttonSaveCurrentScenario = new System.Windows.Forms.Button();
            this.buttonDeleteScenarioCommand = new System.Windows.Forms.Button();
            this.buttonUpdateScenarioCommand = new System.Windows.Forms.Button();
            this.buttonInsertScenarioCommand = new System.Windows.Forms.Button();
            this.listBoxCurrentScenario = new System.Windows.Forms.ListBox();
            this.checkBoxMarkExecutedLines = new System.Windows.Forms.CheckBox();
            this.checkBoxAutorunNextScenario = new System.Windows.Forms.CheckBox();
            this.buttonExecuteSingleLine = new System.Windows.Forms.Button();
            this.buttonRunScenario = new System.Windows.Forms.Button();
            this.buttonAddScenarioCommand = new System.Windows.Forms.Button();
            this.textBoxCommandTemplateToAdd = new System.Windows.Forms.TextBox();
            this.comboBoxAvailableCommands = new System.Windows.Forms.ComboBox();
            this.groupBoxConfiguration = new System.Windows.Forms.GroupBox();
            this.groupBoxParameters = new System.Windows.Forms.GroupBox();
            this.textBoxSelectedOutputParameterValue = new System.Windows.Forms.TextBox();
            this.dataGridOutputParameters = new System.Windows.Forms.DataGridView();
            this.buttonOpenMultilineInputParameterDialog = new System.Windows.Forms.Button();
            this.buttonInputParametersSelectFile = new System.Windows.Forms.Button();
            this.dataGridInputParameters = new System.Windows.Forms.DataGridView();
            this.buttonInputParametersAddOrUpdate = new System.Windows.Forms.Button();
            this.textBoxParameterValue = new System.Windows.Forms.TextBox();
            this.textBoxParameterName = new System.Windows.Forms.TextBox();
            this.groupBoxQueuedScenarios = new System.Windows.Forms.GroupBox();
            this.buttonDeleteScenario = new System.Windows.Forms.Button();
            this.buttonAddScenario = new System.Windows.Forms.Button();
            this.buttonCreateScenario = new System.Windows.Forms.Button();
            this.listBoxQueuedScenarios = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSaveConfigFile = new System.Windows.Forms.Button();
            this.buttonCreateConfigFile = new System.Windows.Forms.Button();
            this.buttonLoadConfigFile = new System.Windows.Forms.Button();
            this.textBoxConfigurationFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.buttonSaveOnSettingsTab = new System.Windows.Forms.Button();
            this.buttonReloadOnSettingsTab = new System.Windows.Forms.Button();
            this.textBoxSettingsOnTabSettings = new System.Windows.Forms.TextBox();
            this.tabControl.SuspendLayout();
            this.tabPageDebugger.SuspendLayout();
            this.groupBoxCurrentScenario.SuspendLayout();
            this.groupBoxConfiguration.SuspendLayout();
            this.groupBoxParameters.SuspendLayout();
            ( (System.ComponentModel.ISupportInitialize)( this.dataGridOutputParameters ) ).BeginInit();
            ( (System.ComponentModel.ISupportInitialize)( this.dataGridInputParameters ) ).BeginInit();
            this.groupBoxQueuedScenarios.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add( this.tabPageDebugger );
            this.tabControl.Controls.Add( this.tabPageSettings );
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point( 0, 0 );
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size( 1913, 979 );
            this.tabControl.TabIndex = 0;
            // 
            // tabPageDebugger
            // 
            this.tabPageDebugger.BackColor = System.Drawing.Color.Transparent;
            this.tabPageDebugger.Controls.Add( this.groupBoxCurrentScenario );
            this.tabPageDebugger.Controls.Add( this.groupBoxConfiguration );
            this.tabPageDebugger.Location = new System.Drawing.Point( 4, 34 );
            this.tabPageDebugger.Name = "tabPageDebugger";
            this.tabPageDebugger.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPageDebugger.Size = new System.Drawing.Size( 1905, 941 );
            this.tabPageDebugger.TabIndex = 0;
            this.tabPageDebugger.Text = "Debugger";
            // 
            // groupBoxCurrentScenario
            // 
            this.groupBoxCurrentScenario.Controls.Add( this.buttonSaveCurrentScenario );
            this.groupBoxCurrentScenario.Controls.Add( this.buttonDeleteScenarioCommand );
            this.groupBoxCurrentScenario.Controls.Add( this.buttonUpdateScenarioCommand );
            this.groupBoxCurrentScenario.Controls.Add( this.buttonInsertScenarioCommand );
            this.groupBoxCurrentScenario.Controls.Add( this.listBoxCurrentScenario );
            this.groupBoxCurrentScenario.Controls.Add( this.checkBoxMarkExecutedLines );
            this.groupBoxCurrentScenario.Controls.Add( this.checkBoxAutorunNextScenario );
            this.groupBoxCurrentScenario.Controls.Add( this.buttonExecuteSingleLine );
            this.groupBoxCurrentScenario.Controls.Add( this.buttonRunScenario );
            this.groupBoxCurrentScenario.Controls.Add( this.buttonAddScenarioCommand );
            this.groupBoxCurrentScenario.Controls.Add( this.textBoxCommandTemplateToAdd );
            this.groupBoxCurrentScenario.Controls.Add( this.comboBoxAvailableCommands );
            this.groupBoxCurrentScenario.Location = new System.Drawing.Point( 8, 507 );
            this.groupBoxCurrentScenario.Name = "groupBoxCurrentScenario";
            this.groupBoxCurrentScenario.Size = new System.Drawing.Size( 1889, 426 );
            this.groupBoxCurrentScenario.TabIndex = 1;
            this.groupBoxCurrentScenario.TabStop = false;
            this.groupBoxCurrentScenario.Text = "Current scenario";
            // 
            // buttonSaveCurrentScenario
            // 
            this.buttonSaveCurrentScenario.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.buttonSaveCurrentScenario.Location = new System.Drawing.Point( 1614, 217 );
            this.buttonSaveCurrentScenario.Name = "buttonSaveCurrentScenario";
            this.buttonSaveCurrentScenario.Size = new System.Drawing.Size( 263, 36 );
            this.buttonSaveCurrentScenario.TabIndex = 30;
            this.buttonSaveCurrentScenario.Text = "Save scenario";
            this.buttonSaveCurrentScenario.UseVisualStyleBackColor = true;
            // 
            // buttonDeleteScenarioCommand
            // 
            this.buttonDeleteScenarioCommand.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.buttonDeleteScenarioCommand.Location = new System.Drawing.Point( 1760, 322 );
            this.buttonDeleteScenarioCommand.Name = "buttonDeleteScenarioCommand";
            this.buttonDeleteScenarioCommand.Size = new System.Drawing.Size( 117, 36 );
            this.buttonDeleteScenarioCommand.TabIndex = 29;
            this.buttonDeleteScenarioCommand.Text = "Delete";
            this.buttonDeleteScenarioCommand.UseVisualStyleBackColor = true;
            // 
            // buttonUpdateScenarioCommand
            // 
            this.buttonUpdateScenarioCommand.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.buttonUpdateScenarioCommand.Location = new System.Drawing.Point( 1760, 377 );
            this.buttonUpdateScenarioCommand.Name = "buttonUpdateScenarioCommand";
            this.buttonUpdateScenarioCommand.Size = new System.Drawing.Size( 117, 36 );
            this.buttonUpdateScenarioCommand.TabIndex = 28;
            this.buttonUpdateScenarioCommand.Text = "Update";
            this.buttonUpdateScenarioCommand.UseVisualStyleBackColor = true;
            // 
            // buttonInsertScenarioCommand
            // 
            this.buttonInsertScenarioCommand.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.buttonInsertScenarioCommand.Location = new System.Drawing.Point( 1614, 322 );
            this.buttonInsertScenarioCommand.Name = "buttonInsertScenarioCommand";
            this.buttonInsertScenarioCommand.Size = new System.Drawing.Size( 117, 36 );
            this.buttonInsertScenarioCommand.TabIndex = 27;
            this.buttonInsertScenarioCommand.Text = "Insert";
            this.buttonInsertScenarioCommand.UseVisualStyleBackColor = true;
            // 
            // listBoxCurrentScenario
            // 
            this.listBoxCurrentScenario.FormattingEnabled = true;
            this.listBoxCurrentScenario.ItemHeight = 25;
            this.listBoxCurrentScenario.Location = new System.Drawing.Point( 12, 29 );
            this.listBoxCurrentScenario.Name = "listBoxCurrentScenario";
            this.listBoxCurrentScenario.Size = new System.Drawing.Size( 1577, 329 );
            this.listBoxCurrentScenario.TabIndex = 26;
            // 
            // checkBoxMarkExecutedLines
            // 
            this.checkBoxMarkExecutedLines.Location = new System.Drawing.Point( 1627, 129 );
            this.checkBoxMarkExecutedLines.Name = "checkBoxMarkExecutedLines";
            this.checkBoxMarkExecutedLines.Size = new System.Drawing.Size( 250, 30 );
            this.checkBoxMarkExecutedLines.TabIndex = 25;
            this.checkBoxMarkExecutedLines.Text = "Mark executed lines";
            this.checkBoxMarkExecutedLines.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutorunNextScenario
            // 
            this.checkBoxAutorunNextScenario.Location = new System.Drawing.Point( 1627, 86 );
            this.checkBoxAutorunNextScenario.Name = "checkBoxAutorunNextScenario";
            this.checkBoxAutorunNextScenario.Size = new System.Drawing.Size( 250, 30 );
            this.checkBoxAutorunNextScenario.TabIndex = 24;
            this.checkBoxAutorunNextScenario.Text = "Autorun next scenario";
            this.checkBoxAutorunNextScenario.UseVisualStyleBackColor = true;
            // 
            // buttonExecuteSingleLine
            // 
            this.buttonExecuteSingleLine.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.buttonExecuteSingleLine.Location = new System.Drawing.Point( 1760, 29 );
            this.buttonExecuteSingleLine.Name = "buttonExecuteSingleLine";
            this.buttonExecuteSingleLine.Size = new System.Drawing.Size( 117, 36 );
            this.buttonExecuteSingleLine.TabIndex = 23;
            this.buttonExecuteSingleLine.Text = "By step";
            this.buttonExecuteSingleLine.UseVisualStyleBackColor = true;
            // 
            // buttonRunScenario
            // 
            this.buttonRunScenario.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.buttonRunScenario.Location = new System.Drawing.Point( 1614, 29 );
            this.buttonRunScenario.Name = "buttonRunScenario";
            this.buttonRunScenario.Size = new System.Drawing.Size( 117, 36 );
            this.buttonRunScenario.TabIndex = 22;
            this.buttonRunScenario.Text = "Run";
            this.buttonRunScenario.UseVisualStyleBackColor = true;
            // 
            // buttonAddScenarioCommand
            // 
            this.buttonAddScenarioCommand.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.buttonAddScenarioCommand.Location = new System.Drawing.Point( 1614, 377 );
            this.buttonAddScenarioCommand.Name = "buttonAddScenarioCommand";
            this.buttonAddScenarioCommand.Size = new System.Drawing.Size( 117, 36 );
            this.buttonAddScenarioCommand.TabIndex = 20;
            this.buttonAddScenarioCommand.Text = "Add";
            this.buttonAddScenarioCommand.UseVisualStyleBackColor = true;
            // 
            // textBoxCommandTemplateToAdd
            // 
            this.textBoxCommandTemplateToAdd.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.textBoxCommandTemplateToAdd.Location = new System.Drawing.Point( 306, 380 );
            this.textBoxCommandTemplateToAdd.Name = "textBoxCommandTemplateToAdd";
            this.textBoxCommandTemplateToAdd.Size = new System.Drawing.Size( 1283, 30 );
            this.textBoxCommandTemplateToAdd.TabIndex = 19;
            // 
            // comboBoxAvailableCommands
            // 
            this.comboBoxAvailableCommands.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.comboBoxAvailableCommands.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAvailableCommands.FormattingEnabled = true;
            this.comboBoxAvailableCommands.Location = new System.Drawing.Point( 15, 380 );
            this.comboBoxAvailableCommands.Name = "comboBoxAvailableCommands";
            this.comboBoxAvailableCommands.Size = new System.Drawing.Size( 274, 33 );
            this.comboBoxAvailableCommands.TabIndex = 18;
            // 
            // groupBoxConfiguration
            // 
            this.groupBoxConfiguration.Controls.Add( this.groupBoxParameters );
            this.groupBoxConfiguration.Controls.Add( this.groupBoxQueuedScenarios );
            this.groupBoxConfiguration.Controls.Add( this.buttonSaveConfigFile );
            this.groupBoxConfiguration.Controls.Add( this.buttonCreateConfigFile );
            this.groupBoxConfiguration.Controls.Add( this.buttonLoadConfigFile );
            this.groupBoxConfiguration.Controls.Add( this.textBoxConfigurationFileName );
            this.groupBoxConfiguration.Controls.Add( this.label2 );
            this.groupBoxConfiguration.Location = new System.Drawing.Point( 8, 6 );
            this.groupBoxConfiguration.Name = "groupBoxConfiguration";
            this.groupBoxConfiguration.Size = new System.Drawing.Size( 1889, 495 );
            this.groupBoxConfiguration.TabIndex = 0;
            this.groupBoxConfiguration.TabStop = false;
            this.groupBoxConfiguration.Text = "Configuration";
            // 
            // groupBoxParameters
            // 
            this.groupBoxParameters.Controls.Add( this.textBoxSelectedOutputParameterValue );
            this.groupBoxParameters.Controls.Add( this.dataGridOutputParameters );
            this.groupBoxParameters.Controls.Add( this.buttonOpenMultilineInputParameterDialog );
            this.groupBoxParameters.Controls.Add( this.buttonInputParametersSelectFile );
            this.groupBoxParameters.Controls.Add( this.dataGridInputParameters );
            this.groupBoxParameters.Controls.Add( this.buttonInputParametersAddOrUpdate );
            this.groupBoxParameters.Controls.Add( this.textBoxParameterValue );
            this.groupBoxParameters.Controls.Add( this.textBoxParameterName );
            this.groupBoxParameters.Location = new System.Drawing.Point( 325, 85 );
            this.groupBoxParameters.Name = "groupBoxParameters";
            this.groupBoxParameters.Size = new System.Drawing.Size( 1558, 404 );
            this.groupBoxParameters.TabIndex = 13;
            this.groupBoxParameters.TabStop = false;
            this.groupBoxParameters.Text = "Parameters";
            // 
            // textBoxSelectedOutputParameterValue
            // 
            this.textBoxSelectedOutputParameterValue.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.textBoxSelectedOutputParameterValue.Location = new System.Drawing.Point( 1185, 365 );
            this.textBoxSelectedOutputParameterValue.Name = "textBoxSelectedOutputParameterValue";
            this.textBoxSelectedOutputParameterValue.Size = new System.Drawing.Size( 367, 30 );
            this.textBoxSelectedOutputParameterValue.TabIndex = 16;
            this.textBoxSelectedOutputParameterValue.TabStop = false;
            // 
            // dataGridOutputParameters
            // 
            this.dataGridOutputParameters.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font( "Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 204 ) ) );
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridOutputParameters.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridOutputParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridOutputParameters.Location = new System.Drawing.Point( 1185, 35 );
            this.dataGridOutputParameters.MultiSelect = false;
            this.dataGridOutputParameters.Name = "dataGridOutputParameters";
            this.dataGridOutputParameters.ReadOnly = true;
            this.dataGridOutputParameters.RowHeadersVisible = false;
            this.dataGridOutputParameters.RowTemplate.Height = 28;
            this.dataGridOutputParameters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridOutputParameters.Size = new System.Drawing.Size( 367, 307 );
            this.dataGridOutputParameters.TabIndex = 15;
            // 
            // buttonOpenMultilineInputParameterDialog
            // 
            this.buttonOpenMultilineInputParameterDialog.Font = new System.Drawing.Font( "Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 204 ) ) );
            this.buttonOpenMultilineInputParameterDialog.Location = new System.Drawing.Point( 959, 365 );
            this.buttonOpenMultilineInputParameterDialog.Name = "buttonOpenMultilineInputParameterDialog";
            this.buttonOpenMultilineInputParameterDialog.Size = new System.Drawing.Size( 39, 30 );
            this.buttonOpenMultilineInputParameterDialog.TabIndex = 14;
            this.buttonOpenMultilineInputParameterDialog.Text = "≡";
            this.buttonOpenMultilineInputParameterDialog.UseVisualStyleBackColor = true;
            // 
            // buttonInputParametersSelectFile
            // 
            this.buttonInputParametersSelectFile.Location = new System.Drawing.Point( 996, 365 );
            this.buttonInputParametersSelectFile.Name = "buttonInputParametersSelectFile";
            this.buttonInputParametersSelectFile.Size = new System.Drawing.Size( 39, 30 );
            this.buttonInputParametersSelectFile.TabIndex = 13;
            this.buttonInputParametersSelectFile.Text = "...";
            this.buttonInputParametersSelectFile.UseVisualStyleBackColor = true;
            // 
            // dataGridInputParameters
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font( "Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 204 ) ) );
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridInputParameters.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridInputParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridInputParameters.Location = new System.Drawing.Point( 6, 35 );
            this.dataGridInputParameters.MultiSelect = false;
            this.dataGridInputParameters.Name = "dataGridInputParameters";
            this.dataGridInputParameters.ReadOnly = true;
            this.dataGridInputParameters.RowHeadersVisible = false;
            this.dataGridInputParameters.RowTemplate.Height = 28;
            this.dataGridInputParameters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridInputParameters.Size = new System.Drawing.Size( 1152, 307 );
            this.dataGridInputParameters.TabIndex = 12;
            // 
            // buttonInputParametersAddOrUpdate
            // 
            this.buttonInputParametersAddOrUpdate.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.buttonInputParametersAddOrUpdate.Location = new System.Drawing.Point( 1041, 362 );
            this.buttonInputParametersAddOrUpdate.Name = "buttonInputParametersAddOrUpdate";
            this.buttonInputParametersAddOrUpdate.Size = new System.Drawing.Size( 117, 36 );
            this.buttonInputParametersAddOrUpdate.TabIndex = 11;
            this.buttonInputParametersAddOrUpdate.Text = "Add";
            this.buttonInputParametersAddOrUpdate.UseVisualStyleBackColor = true;
            // 
            // textBoxParameterValue
            // 
            this.textBoxParameterValue.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left ) | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.textBoxParameterValue.Location = new System.Drawing.Point( 162, 365 );
            this.textBoxParameterValue.Name = "textBoxParameterValue";
            this.textBoxParameterValue.Size = new System.Drawing.Size( 798, 30 );
            this.textBoxParameterValue.TabIndex = 10;
            this.textBoxParameterValue.TabStop = false;
            // 
            // textBoxParameterName
            // 
            this.textBoxParameterName.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left ) | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.textBoxParameterName.Location = new System.Drawing.Point( 6, 365 );
            this.textBoxParameterName.Name = "textBoxParameterName";
            this.textBoxParameterName.Size = new System.Drawing.Size( 150, 30 );
            this.textBoxParameterName.TabIndex = 9;
            this.textBoxParameterName.TabStop = false;
            // 
            // groupBoxQueuedScenarios
            // 
            this.groupBoxQueuedScenarios.Controls.Add( this.buttonDeleteScenario );
            this.groupBoxQueuedScenarios.Controls.Add( this.buttonAddScenario );
            this.groupBoxQueuedScenarios.Controls.Add( this.buttonCreateScenario );
            this.groupBoxQueuedScenarios.Controls.Add( this.listBoxQueuedScenarios );
            this.groupBoxQueuedScenarios.Controls.Add( this.label1 );
            this.groupBoxQueuedScenarios.Location = new System.Drawing.Point( 6, 85 );
            this.groupBoxQueuedScenarios.Name = "groupBoxQueuedScenarios";
            this.groupBoxQueuedScenarios.Size = new System.Drawing.Size( 313, 404 );
            this.groupBoxQueuedScenarios.TabIndex = 12;
            this.groupBoxQueuedScenarios.TabStop = false;
            this.groupBoxQueuedScenarios.Text = "Scenarios";
            // 
            // buttonDeleteScenario
            // 
            this.buttonDeleteScenario.Location = new System.Drawing.Point( 190, 181 );
            this.buttonDeleteScenario.Name = "buttonDeleteScenario";
            this.buttonDeleteScenario.Size = new System.Drawing.Size( 117, 36 );
            this.buttonDeleteScenario.TabIndex = 14;
            this.buttonDeleteScenario.Text = "Delete";
            this.buttonDeleteScenario.UseVisualStyleBackColor = true;
            // 
            // buttonAddScenario
            // 
            this.buttonAddScenario.Location = new System.Drawing.Point( 190, 122 );
            this.buttonAddScenario.Name = "buttonAddScenario";
            this.buttonAddScenario.Size = new System.Drawing.Size( 117, 36 );
            this.buttonAddScenario.TabIndex = 13;
            this.buttonAddScenario.Text = "Add...";
            this.buttonAddScenario.UseVisualStyleBackColor = true;
            // 
            // buttonCreateScenario
            // 
            this.buttonCreateScenario.Location = new System.Drawing.Point( 190, 63 );
            this.buttonCreateScenario.Name = "buttonCreateScenario";
            this.buttonCreateScenario.Size = new System.Drawing.Size( 117, 36 );
            this.buttonCreateScenario.TabIndex = 12;
            this.buttonCreateScenario.Text = "Create...";
            this.buttonCreateScenario.UseVisualStyleBackColor = true;
            // 
            // listBoxQueuedScenarios
            // 
            this.listBoxQueuedScenarios.FormattingEnabled = true;
            this.listBoxQueuedScenarios.ItemHeight = 25;
            this.listBoxQueuedScenarios.Location = new System.Drawing.Point( 6, 63 );
            this.listBoxQueuedScenarios.Name = "listBoxQueuedScenarios";
            this.listBoxQueuedScenarios.Size = new System.Drawing.Size( 165, 329 );
            this.listBoxQueuedScenarios.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 47, 35 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 83, 25 );
            this.label1.TabIndex = 0;
            this.label1.Text = "Queued";
            // 
            // buttonSaveConfigFile
            // 
            this.buttonSaveConfigFile.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.buttonSaveConfigFile.Location = new System.Drawing.Point( 1760, 31 );
            this.buttonSaveConfigFile.Name = "buttonSaveConfigFile";
            this.buttonSaveConfigFile.Size = new System.Drawing.Size( 117, 36 );
            this.buttonSaveConfigFile.TabIndex = 11;
            this.buttonSaveConfigFile.Text = "Save...";
            this.buttonSaveConfigFile.UseVisualStyleBackColor = true;
            // 
            // buttonCreateConfigFile
            // 
            this.buttonCreateConfigFile.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.buttonCreateConfigFile.Location = new System.Drawing.Point( 1510, 31 );
            this.buttonCreateConfigFile.Name = "buttonCreateConfigFile";
            this.buttonCreateConfigFile.Size = new System.Drawing.Size( 117, 36 );
            this.buttonCreateConfigFile.TabIndex = 10;
            this.buttonCreateConfigFile.Text = "Create";
            this.buttonCreateConfigFile.UseVisualStyleBackColor = true;
            // 
            // buttonLoadConfigFile
            // 
            this.buttonLoadConfigFile.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.buttonLoadConfigFile.Location = new System.Drawing.Point( 1366, 31 );
            this.buttonLoadConfigFile.Name = "buttonLoadConfigFile";
            this.buttonLoadConfigFile.Size = new System.Drawing.Size( 117, 36 );
            this.buttonLoadConfigFile.TabIndex = 9;
            this.buttonLoadConfigFile.Text = "Load...";
            this.buttonLoadConfigFile.UseVisualStyleBackColor = true;
            // 
            // textBoxConfigurationFileName
            // 
            this.textBoxConfigurationFileName.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left ) | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.textBoxConfigurationFileName.Location = new System.Drawing.Point( 197, 34 );
            this.textBoxConfigurationFileName.Name = "textBoxConfigurationFileName";
            this.textBoxConfigurationFileName.ReadOnly = true;
            this.textBoxConfigurationFileName.Size = new System.Drawing.Size( 1134, 30 );
            this.textBoxConfigurationFileName.TabIndex = 8;
            this.textBoxConfigurationFileName.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 12, 37 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 165, 25 );
            this.label2.TabIndex = 7;
            this.label2.Text = "File name (*.json)";
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.BackColor = System.Drawing.Color.Transparent;
            this.tabPageSettings.Controls.Add( this.buttonSaveOnSettingsTab );
            this.tabPageSettings.Controls.Add( this.buttonReloadOnSettingsTab );
            this.tabPageSettings.Controls.Add( this.textBoxSettingsOnTabSettings );
            this.tabPageSettings.Location = new System.Drawing.Point( 4, 34 );
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPageSettings.Size = new System.Drawing.Size( 1905, 941 );
            this.tabPageSettings.TabIndex = 1;
            this.tabPageSettings.Text = "Settings";
            // 
            // buttonSaveOnSettingsTab
            // 
            this.buttonSaveOnSettingsTab.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.buttonSaveOnSettingsTab.Location = new System.Drawing.Point( 1780, 1251 );
            this.buttonSaveOnSettingsTab.Name = "buttonSaveOnSettingsTab";
            this.buttonSaveOnSettingsTab.Size = new System.Drawing.Size( 117, 36 );
            this.buttonSaveOnSettingsTab.TabIndex = 8;
            this.buttonSaveOnSettingsTab.Text = "Save";
            this.buttonSaveOnSettingsTab.UseVisualStyleBackColor = true;
            // 
            // buttonReloadOnSettingsTab
            // 
            this.buttonReloadOnSettingsTab.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.buttonReloadOnSettingsTab.Location = new System.Drawing.Point( 1627, 1251 );
            this.buttonReloadOnSettingsTab.Name = "buttonReloadOnSettingsTab";
            this.buttonReloadOnSettingsTab.Size = new System.Drawing.Size( 117, 36 );
            this.buttonReloadOnSettingsTab.TabIndex = 7;
            this.buttonReloadOnSettingsTab.Text = "Reload";
            this.buttonReloadOnSettingsTab.UseVisualStyleBackColor = true;
            // 
            // textBoxSettingsOnTabSettings
            // 
            this.textBoxSettingsOnTabSettings.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom ) | System.Windows.Forms.AnchorStyles.Left ) | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.textBoxSettingsOnTabSettings.Location = new System.Drawing.Point( 3, 3 );
            this.textBoxSettingsOnTabSettings.Multiline = true;
            this.textBoxSettingsOnTabSettings.Name = "textBoxSettingsOnTabSettings";
            this.textBoxSettingsOnTabSettings.Size = new System.Drawing.Size( 1899, 1207 );
            this.textBoxSettingsOnTabSettings.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 12F, 25F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size( 1913, 979 );
            this.Controls.Add( this.tabControl );
            this.Font = new System.Drawing.Font( "Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 204 ) ) );
            this.Location = new System.Drawing.Point( 15, 15 );
            this.Margin = new System.Windows.Forms.Padding( 4 );
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ScenarioBasedVideoUploader";
            this.tabControl.ResumeLayout( false );
            this.tabPageDebugger.ResumeLayout( false );
            this.groupBoxCurrentScenario.ResumeLayout( false );
            this.groupBoxCurrentScenario.PerformLayout();
            this.groupBoxConfiguration.ResumeLayout( false );
            this.groupBoxConfiguration.PerformLayout();
            this.groupBoxParameters.ResumeLayout( false );
            this.groupBoxParameters.PerformLayout();
            ( (System.ComponentModel.ISupportInitialize)( this.dataGridOutputParameters ) ).EndInit();
            ( (System.ComponentModel.ISupportInitialize)( this.dataGridInputParameters ) ).EndInit();
            this.groupBoxQueuedScenarios.ResumeLayout( false );
            this.groupBoxQueuedScenarios.PerformLayout();
            this.tabPageSettings.ResumeLayout( false );
            this.tabPageSettings.PerformLayout();
            this.ResumeLayout( false );
        }

        private System.Windows.Forms.TextBox textBoxSelectedOutputParameterValue;

        private System.Windows.Forms.DataGridView dataGridOutputParameters;

        private System.Windows.Forms.Button buttonOpenMultilineInputParameterDialog;

        private System.Windows.Forms.Button buttonInputParametersSelectFile;

        private System.Windows.Forms.Button buttonSaveCurrentScenario;

        private System.Windows.Forms.DataGridView dataGridInputParameters;

        private System.Windows.Forms.Button buttonInsertScenarioCommand;
        private System.Windows.Forms.Button buttonUpdateScenarioCommand;
        private System.Windows.Forms.Button buttonDeleteScenarioCommand;

        private System.Windows.Forms.ListBox listBoxCurrentScenario;

        private System.Windows.Forms.CheckBox checkBoxMarkExecutedLines;

        private System.Windows.Forms.Button buttonRunScenario;
        private System.Windows.Forms.Button buttonExecuteSingleLine;
        private System.Windows.Forms.CheckBox checkBoxAutorunNextScenario;

        private System.Windows.Forms.Button buttonAddScenarioCommand;
        private System.Windows.Forms.TextBox textBoxCommandTemplateToAdd;
        private System.Windows.Forms.ComboBox comboBoxAvailableCommands;

        private System.Windows.Forms.GroupBox groupBoxCurrentScenario;

        private System.Windows.Forms.TextBox textBoxParameterName;
        private System.Windows.Forms.TextBox textBoxParameterValue;
        private System.Windows.Forms.Button buttonInputParametersAddOrUpdate;

        private System.Windows.Forms.GroupBox groupBoxParameters;

        private System.Windows.Forms.Button buttonAddScenario;
        private System.Windows.Forms.Button buttonDeleteScenario;

        private System.Windows.Forms.Button buttonCreateScenario;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxQueuedScenarios;

        private System.Windows.Forms.GroupBox groupBoxQueuedScenarios;

        private System.Windows.Forms.Button buttonLoadConfigFile;
        private System.Windows.Forms.TextBox textBoxConfigurationFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonCreateConfigFile;
        private System.Windows.Forms.Button buttonSaveConfigFile;

        private System.Windows.Forms.GroupBox groupBoxConfiguration;

        private System.Windows.Forms.Button buttonReloadOnSettingsTab;
        private System.Windows.Forms.Button buttonSaveOnSettingsTab;

        private System.Windows.Forms.TextBox textBoxSettingsOnTabSettings;

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageDebugger;
        private System.Windows.Forms.TabPage tabPageSettings;

        #endregion
    }
}