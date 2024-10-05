using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ScenarioBasedVideoUploader
{
    public partial class MainForm : Form
    {
        private TabPage         _currentTabPage;
        private int             _selectedIndexInScenarioListBox = -1;
        private int             _selectedRowInInputParameters   = -1;
        
        private DebuggerEngine  _debuggerEngine;

        public MainForm()
        {
            InitializeComponent();

            _debuggerEngine = new DebuggerEngine();

            SubscribeToDebuggerEngineEvents();
            
            textBoxSettingsOnTabSettings.Text   = _debuggerEngine.GetSettingsText();

            InitializeDataGridInputParameters( dataGridInputParameters );
            InitializeDataGridInputParameters( dataGridOutputParameters );
            
            ApplyLayoutFixes();
            
            _debuggerEngine.InitializeUI();
            
            buttonLoadConfigFile.Click                      += ButtonLoadConfigFileOnClick;
            buttonSaveConfigFile.Click                      += ButtonSaveConfigFileOnClick;
            buttonCreateConfigFile.Click                    += ButtonCreateConfigFileOnClick;
            buttonCreateScenario.Click                      += ButtonCreateScenarioOnClick;
            
            comboBoxAvailableCommands.SelectedIndexChanged  += ComboBoxAvailableCommandsSelectedIndexChanged;
            comboBoxAvailableCommands.DataSource            = _debuggerEngine.GetListOfAvailableCommands();
            
            buttonAddScenarioCommand.Click                  += ButtonAddScenarioCommandOnClick;
            buttonDeleteScenarioCommand.Click               += ButtonDeleteScenarioCommandOnClick;
            buttonInsertScenarioCommand.Click               += ButtonInsertScenarioCommandOnClick;
            buttonUpdateScenarioCommand.Click               += ButtonUpdateScenarioCommandOnClick;
            
            listBoxCurrentScenario.SelectedIndexChanged     += ListBoxScenarioOnSelectedIndexChanged;

            textBoxCommandTemplateToAdd.KeyDown             += TextBoxCommandTemplateToAddOnKeyDown;

            dataGridInputParameters.CellClick               += DataGridInputParametersOnCellClick;
            dataGridInputParameters.CellDoubleClick         += DataGridInputParametersOnCellClick;
            dataGridInputParameters.SelectionChanged        += DataGridInputParametersOnSelectionChanged;
            
            dataGridOutputParameters.SelectionChanged       += DataGridOutputParametersOnSelectionChanged;
            dataGridOutputParameters.CellClick               += DataGridOutputParametersOnCellClick;
            dataGridOutputParameters.CellDoubleClick         += DataGridOutputParametersOnCellClick;
            
            textBoxParameterName.TextChanged                += TextBoxParameterNameOnTextChanged;
            textBoxParameterValue.KeyDown                   += TextBoxParameterValueOnKeyDown;
            buttonInputParametersAddOrUpdate.Click          += ButtonInputParametersAddOrUpdateOnClick;
            buttonInputParametersSelectFile.Click           += ButtonInputParametersSelectFileOnClick;
            
            buttonRunScenario.Click                         += ButtonRunScenarioOnClick;
            buttonExecuteSingleLine.Click                   += ButtonExecuteSingleLineOnClick;
            checkBoxAutorunNextScenario.CheckedChanged      += CheckBoxAutorunNextScenarioOnCheckedChanged;
            
            tabControl.Selecting                            += TabControlOnSelecting;

            buttonReloadOnSettingsTab.Click                 += ButtonReloadOnSettingsTabOnClick;
            buttonSaveOnSettingsTab.Click                   += ButtonSaveOnSettingsTabOnClick;
            
            checkBoxMarkExecutedLines.CheckedChanged        += CheckBoxMarkExecutedLinesOnCheckedChanged;
            checkBoxMarkExecutedLines.Checked               = true;
            
            buttonSaveCurrentScenario.Click                 += ButtonSaveCurrentScenarioOnClick;
            
            listBoxQueuedScenarios.SelectedIndexChanged     += ListBoxQueuedScenariosOnSelectedIndexChanged;
            
            buttonAddScenario.Click                         += ButtonAddScenarioOnClick;
            buttonDeleteScenario.Click                      += ButtonDeleteScenarioOnClick;
            
            buttonOpenMultilineInputParameterDialog.Click   += ButtonOpenMultilineInputParameterDialogOnClick; 
            
            _currentTabPage                                 = tabPageDebugger;
        }

        private void SubscribeToDebuggerEngineEvents()
        {
            _debuggerEngine.OnQueuedScenariosSectionEnabledStatusChanged    += OnQueuedScenariosSectionEnabledStatusChanged;
            _debuggerEngine.OnParametersSectionEnabledStatusChanged         += OnParametersSectionEnabledStatusChanged;
            _debuggerEngine.OnCurrentScenarioSectionEnabledStatusChanged    += OnCurrentScenarioSectionEnabledStatusChanged;
            
            _debuggerEngine.OnCreateNewQueuedScenario                       += OnCreateNewQueuedScenario;
            _debuggerEngine.OnSetQueuedScenarioIndex                        += OnSetQueuedScenarioIndex;
            
            _debuggerEngine.OnSetInputParametersDataGrid                    += OnSetInputParametersDataGrid;
            _debuggerEngine.OnSetOutputParametersDataGrid                   += OnSetOutputParametersDataGrid;
            
            _debuggerEngine.OnSetCurrentScenarioLines                       += OnSetCurrentScenarioLines;
            _debuggerEngine.OnAddCurrentScenarioLine                        += OnAddCurrentScenarioLine;
            _debuggerEngine.OnDeleteCurrentScenarioLine                     += OnDeleteCurrentScenarioLine;
            _debuggerEngine.OnInsertCurrentScenarioLine                     += OnInsertCurrentScenarioLine;
            _debuggerEngine.OnUpdateCurrentScenarioLine                     += OnUpdateCurrentScenarioLine;
            
            _debuggerEngine.OnCurrentScenarioLineExecuted                   += OnCurrentScenarioLineExecuted;
            _debuggerEngine.OnSetCurrentScenarioLineIndex                   += OnSetCurrentScenarioLineIndex;
            
            _debuggerEngine.OnSetAutorunNextScenario                        += OnSetAutorunNextScenario;
            
            _debuggerEngine.OnSetQueuedScenarios                            += OnSetQueuedScenarios;
            _debuggerEngine.OnSelectQueuedScenarioLineIndex                 += OnSelectQueuedScenarioLineIndex;
            
            _debuggerEngine.OnSetConfigFileName                             += OnSetConfigFileName;
        }

        private void CheckBoxAutorunNextScenarioOnCheckedChanged( object sender, EventArgs e )
        {
            _debuggerEngine.AutorunNextScenario = checkBoxAutorunNextScenario.Checked;
        }

        private void OnSetConfigFileName( string fileName )
        {
            textBoxConfigurationFileName.Text = fileName;
        }
        
        private void ButtonSaveConfigFileOnClick( object sender, EventArgs e )
        {
            var dialog = new SaveFileDialog { Filter = @"Json files (*.json)|*.json|All files (*.*)|*.*" };
            
            if( dialog.ShowDialog() == DialogResult.OK )
                _debuggerEngine.SaveConfig( dialog.FileName );
        }

        private void ButtonLoadConfigFileOnClick( object sender, EventArgs e )
        {
            var dialog = new OpenFileDialog { Filter = @"Json files (*.json)|*.json|All files (*.*)|*.*" };
            
            if( dialog.ShowDialog() == DialogResult.OK )
                _debuggerEngine.LoadConfig( dialog.FileName );
        }
        
        private void TextBoxParameterNameOnTextChanged( object sender, EventArgs e )
        {
            if( string.IsNullOrEmpty( textBoxParameterName.Text.Trim() ) && _selectedRowInInputParameters != -1 )
                buttonInputParametersAddOrUpdate.Text = "Delete";
            else
                buttonInputParametersAddOrUpdate.Text = _selectedRowInInputParameters == -1 ? "Add" : "Update";
        }
        
        private void ButtonAddScenarioOnClick( object sender, EventArgs e )
        {
            var availableUnusedScenarios = _debuggerEngine.GetAvailableUnusedScenarios();
            
            if( availableUnusedScenarios.Length == 0 )
            {
                MessageBox.Show(
                    $"All scenarios from the {_debuggerEngine.Settings.FolderForScenarios} folder are already in the queue" );
                return;
            }
            
            var dialog = new AddExistingScenarioDialog();
            dialog.SetAvailableUnusedScenarios( availableUnusedScenarios );
            
            if( dialog.ShowDialog() == DialogResult.OK )
                _debuggerEngine.AddScenario( dialog.SelectedScenarioName );
        }

        
        private void ButtonOpenMultilineInputParameterDialogOnClick( object sender, EventArgs e )
        {
            var dialog = new MultilineDialog();
            dialog.Value = textBoxParameterValue.Text;
            
            if( dialog.ShowDialog() == DialogResult.OK )
                textBoxParameterValue.Text = dialog.Value;
        }

        
        private void ButtonDeleteScenarioOnClick( object sender, EventArgs e )
        {
            _debuggerEngine.DeleteQueuedScenario( listBoxQueuedScenarios.SelectedIndex ); 
        }
        
        private void ListBoxQueuedScenariosOnSelectedIndexChanged( object sender, EventArgs e )
        {
            _debuggerEngine.SelectScenario( listBoxQueuedScenarios.SelectedIndex );
        }

        
        private void OnSelectQueuedScenarioLineIndex( int index )
        {
            if( listBoxQueuedScenarios.Items.Count > index )
                listBoxQueuedScenarios.SelectedIndex = index;
        }

        private void OnSetQueuedScenarios( List< string > lines )
        {
            listBoxQueuedScenarios.Items.Clear();
            listBoxQueuedScenarios.Items.AddRange( lines );
        }

        private void TextBoxParameterValueOnKeyDown( object sender, KeyEventArgs e )
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonInputParametersAddOrUpdate.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        
        private void ButtonSaveCurrentScenarioOnClick( object sender, EventArgs e )
        {
            _debuggerEngine.SaveCurrentScenario();
        }

        private void CheckBoxMarkExecutedLinesOnCheckedChanged( object sender, EventArgs e )
        {
            var isChecked = checkBoxMarkExecutedLines.Checked;
            if( isChecked )
            {
                listBoxCurrentScenario.DrawMode = DrawMode.OwnerDrawFixed;
                listBoxCurrentScenario.DrawItem += ListBoxScenarioOnDrawItem;
            }
            else
            {
                listBoxCurrentScenario.DrawMode = DrawMode.Normal;
                listBoxCurrentScenario.DrawItem -= ListBoxScenarioOnDrawItem;
            }
            
            listBoxCurrentScenario.Invalidate();
        }
        
        private void OnSetAutorunNextScenario( bool isEnabled )
        {
            checkBoxAutorunNextScenario.Checked = isEnabled;
        }

        private void OnSetCurrentScenarioLineIndex( int index )
        {
            listBoxCurrentScenario.SelectedIndex = index;            
        }

        private void ButtonExecuteSingleLineOnClick( object sender, EventArgs e )
        {
            _debuggerEngine.ExecuteScenarioLine( listBoxCurrentScenario.SelectedIndex );
        }

        private void ButtonRunScenarioOnClick( object sender, EventArgs e )
        {
            _debuggerEngine.RunScenario( listBoxCurrentScenario.SelectedIndex );
        }

        
        private void OnCurrentScenarioLineExecuted( int index )
        {
            listBoxCurrentScenario.Invalidate( listBoxCurrentScenario.GetItemRectangle( index ) );
        }

        private void ButtonInputParametersSelectFileOnClick( object sender, EventArgs e )
        {
            var dialog = new OpenFileDialog();
            
            if( dialog.ShowDialog() == DialogResult.OK )
                textBoxParameterValue.Text = dialog.FileName;
        }

        private void OnSetInputParametersDataGrid( List< Parameter > records, int selectedRowIndex )
        {
            dataGridInputParameters.SelectionChanged -= DataGridInputParametersOnSelectionChanged;
            
            dataGridInputParameters.DataSource = null;
            dataGridInputParameters.DataSource = new BindingList<Parameter>( records );

            dataGridInputParameters.Columns[ 0 ].AutoSizeMode   = DataGridViewAutoSizeColumnMode.None;
            dataGridInputParameters.Columns[ 0 ].Width          = 150;
            dataGridInputParameters.Columns[ 1 ].AutoSizeMode   = DataGridViewAutoSizeColumnMode.Fill;
            
            dataGridInputParameters.SelectionChanged += DataGridInputParametersOnSelectionChanged;
            
            _selectedRowInInputParameters       = selectedRowIndex;
            if( selectedRowIndex == -1 )
                dataGridInputParameters.ClearSelection();
            else if( selectedRowIndex < dataGridInputParameters.Rows.Count )
                dataGridInputParameters.CurrentCell = dataGridInputParameters.Rows[ selectedRowIndex ].Cells[ 0 ];
            
            SetInputParametersControlTexts();
        }

        private void DataGridInputParametersOnSelectionChanged( object sender, EventArgs e )
        {
            SetInputParametersControlTexts();
        }
        
        private void OnSetOutputParametersDataGrid( List< Parameter > records, int selectedrowindex )
        {
            dataGridOutputParameters.SelectionChanged -= DataGridOutputParametersOnSelectionChanged;
            dataGridOutputParameters.DataSource = null;
            dataGridOutputParameters.DataSource = new BindingList<Parameter>( records );
            
            dataGridOutputParameters.Columns[ 0 ].AutoSizeMode   = DataGridViewAutoSizeColumnMode.None;
            dataGridOutputParameters.Columns[ 0 ].Width          = 150;
            dataGridOutputParameters.Columns[ 1 ].AutoSizeMode   = DataGridViewAutoSizeColumnMode.Fill;
            
            dataGridOutputParameters.SelectionChanged += DataGridOutputParametersOnSelectionChanged;
            
            DataGridOutputParametersOnSelectionChanged( dataGridOutputParameters, EventArgs.Empty );
        }

        private void DataGridOutputParametersOnSelectionChanged( object sender, EventArgs e )
        {
            if( dataGridOutputParameters.SelectedRows.Count > 0 )
                textBoxSelectedOutputParameterValue.Text = dataGridOutputParameters.SelectedRows[ 0 ].Cells[ 1 ].Value.ToString();
        }

        private void DataGridOutputParametersOnCellClick( object sender, DataGridViewCellEventArgs e )
        {
            if( e.RowIndex != -1 )
                textBoxSelectedOutputParameterValue.Text = dataGridOutputParameters.Rows[ e.RowIndex ].Cells[ 1 ].Value.ToString();
        }
        
        
        private void SetInputParametersControlTexts()
        {
            buttonInputParametersAddOrUpdate.Text = _selectedRowInInputParameters == -1 
                ? "Add" 
                : string.IsNullOrEmpty( textBoxParameterName.Text.Trim() ) 
                    ? "Delete" 
                    : "Update";
            
            if( _selectedRowInInputParameters != -1 && dataGridInputParameters.SelectedRows.Count > 0 && dataGridInputParameters?.SelectedRows[ 0 ]?.Cells[ 0 ]?.Value is not null )
            {
                textBoxParameterName.Text  = dataGridInputParameters.SelectedRows[ 0 ].Cells[ 0 ].Value.ToString();
                textBoxParameterValue.Text = dataGridInputParameters.SelectedRows[ 0 ].Cells[ 1 ].Value.ToString();
            }
            else
            {
                textBoxParameterName.Text  = string.Empty;
                textBoxParameterValue.Text = string.Empty;
            }
        }

        private void ButtonInputParametersAddOrUpdateOnClick( object sender, EventArgs e )
        {
            _debuggerEngine.ProcessInputParameter( textBoxParameterName.Text, textBoxParameterValue.Text, _selectedRowInInputParameters );
            
            if( _selectedRowInInputParameters != -1 )
                dataGridInputParameters.InvalidateRow( _selectedRowInInputParameters );
        }

        private void DataGridInputParametersOnCellClick( object sender, DataGridViewCellEventArgs e )
        {
            if( _selectedRowInInputParameters == e.RowIndex )
            {
                _selectedRowInInputParameters = -1;
                dataGridInputParameters.ClearSelection();
            }
            else            
                _selectedRowInInputParameters = e.RowIndex;
            
            SetInputParametersControlTexts();
        }
        
        private void OnUpdateCurrentScenarioLine( int index, string str )
        {
            listBoxCurrentScenario.Items[ index ] = str;
        }

        private void ButtonUpdateScenarioCommandOnClick( object sender, EventArgs e )
        {
            var commandText = textBoxCommandTemplateToAdd.Text;
            
            if( listBoxCurrentScenario.SelectedIndex == -1 )
            {
                MessageBox.Show( "There is no any row selected to update", "Error" );
                return;
            }

            _debuggerEngine.UpdateCommandInCurrentScenario( commandText, listBoxCurrentScenario.SelectedIndex );
        }
        
        private void OnInsertCurrentScenarioLine( int index, string str )
        {
            listBoxCurrentScenario.Items.Insert( index, str );
        }

        private void ButtonInsertScenarioCommandOnClick( object sender, EventArgs e )
        {
            var commandText = textBoxCommandTemplateToAdd.Text;
            
            if( listBoxCurrentScenario.SelectedIndex == -1 )
            {
                if( MessageBox.Show( "There is no any row selected, add to the end?", "Warning", MessageBoxButtons.OKCancel ) == DialogResult.OK )
                    _debuggerEngine.AddCommandToCurrentScenario( commandText );

                return;
            }

            _debuggerEngine.InsertCommandToCurrentScenario( commandText, listBoxCurrentScenario.SelectedIndex );
        }

        private void ButtonDeleteScenarioCommandOnClick( object sender, EventArgs e )
        {
            var selectedIndex = listBoxCurrentScenario.SelectedIndex;
            
            if( selectedIndex == -1 )
                return;
            
            _debuggerEngine.DeleteCurrentScenarioLine( selectedIndex );
        }

        private void OnDeleteCurrentScenarioLine( int index )
        {
            listBoxCurrentScenario.Items.RemoveAt( index );
        }

        private void TextBoxCommandTemplateToAddOnKeyDown( object sender, KeyEventArgs e )
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonAddScenarioCommand.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        
        private void ListBoxScenarioOnSelectedIndexChanged( object sender, EventArgs e )
        {
            //для старого выделения
            if( _selectedIndexInScenarioListBox != -1 && listBoxCurrentScenario.Items.Count > _selectedIndexInScenarioListBox )
                listBoxCurrentScenario.Invalidate(listBoxCurrentScenario.GetItemRectangle( _selectedIndexInScenarioListBox ));
            
            if( listBoxCurrentScenario.SelectedIndex != _selectedIndexInScenarioListBox )
                _selectedIndexInScenarioListBox = listBoxCurrentScenario.SelectedIndex;
            else
            {
                _selectedIndexInScenarioListBox = -1;
                listBoxCurrentScenario.SelectedIndex   = _selectedIndexInScenarioListBox;
            }
            
            //для нового выделения
            if( _selectedIndexInScenarioListBox != -1 )
                listBoxCurrentScenario.Invalidate(listBoxCurrentScenario.GetItemRectangle( _selectedIndexInScenarioListBox ));
            
            if( _selectedIndexInScenarioListBox != -1 )
                textBoxCommandTemplateToAdd.Text = listBoxCurrentScenario.Items[ _selectedIndexInScenarioListBox ].ToString();
        }
        
        
        private void ButtonAddScenarioCommandOnClick( object sender, EventArgs e )
        {
            _debuggerEngine.AddCommandToCurrentScenario( textBoxCommandTemplateToAdd.Text );
        }

        private void ComboBoxAvailableCommandsSelectedIndexChanged( object sender, EventArgs e )
        {
            textBoxCommandTemplateToAdd.Text = _debuggerEngine.GetCommandTemplate( comboBoxAvailableCommands.Text );  
        }
        
        private void ButtonCreateConfigFileOnClick( object sender, EventArgs e )
        {
            try
            {
                _debuggerEngine.CreateNewConfig();
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.Message, "Error in the new config file" );
            }
        }
        
        private void ButtonCreateScenarioOnClick( object sender, EventArgs e )
        {
            var dialog              = new CreateNewScenario();
            dialog.Settings         = _debuggerEngine.Settings;
            dialog.QueuedScenarios  = listBoxQueuedScenarios.Items.Select( i => i.ToString() ).ToArray(); 
            
            if( dialog.ShowDialog() == DialogResult.OK )
                _debuggerEngine.CreateNewScenario( dialog.ScenarioName );
        }

        private void OnAddCurrentScenarioLine( string line )
        {
            listBoxCurrentScenario.Items.Add( line );
        }

        private void OnSetCurrentScenarioLines( IReadOnlyList< string > lines )
        {
            listBoxCurrentScenario.Items.Clear();
            listBoxCurrentScenario.Items.AddRange( lines );
            
            _selectedIndexInScenarioListBox = -1;
            listBoxCurrentScenario.SelectedIndex = -1;
        }

        private void OnSetQueuedScenarioIndex( int index )
        {
            listBoxQueuedScenarios.SelectedIndex = index;
        }

        private void OnCreateNewQueuedScenario( string scenarioName )
        {
            listBoxQueuedScenarios.Items.Add( scenarioName );
        }

        private void OnCurrentScenarioSectionEnabledStatusChanged( bool isEnabled )
        {
            groupBoxCurrentScenario.Enabled = isEnabled;
        }

        private void OnParametersSectionEnabledStatusChanged( bool isEnabled )
        {
            var cellStyle = isEnabled ? new DataGridViewCellStyle() : new DataGridViewCellStyle { ForeColor = Color.Gray };
            
            dataGridInputParameters.Columns[ 0 ].HeaderCell.Style = cellStyle;
            dataGridInputParameters.Columns[ 1 ].HeaderCell.Style = cellStyle;

            dataGridOutputParameters.Columns[ 0 ].HeaderCell.Style = cellStyle;
            dataGridOutputParameters.Columns[ 1 ].HeaderCell.Style = cellStyle;
            
            groupBoxParameters.Enabled      = isEnabled;
        }

        private void OnQueuedScenariosSectionEnabledStatusChanged( bool isEnabled )
        {
            groupBoxQueuedScenarios.Enabled = isEnabled;

            buttonSaveConfigFile.Enabled    = isEnabled;
        }

        private void InitializeDataGridInputParameters( DataGridView dataGridView )
        {
            dataGridView.Columns.Add( "Name", "Name" );
            dataGridView.Columns.Add( "Value", "Value" );

            dataGridView.Columns[ 0 ].AutoSizeMode   = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns[ 0 ].Width          = 150;
            dataGridView.Columns[ 1 ].AutoSizeMode   = DataGridViewAutoSizeColumnMode.Fill;
            
            dataGridView.Columns[ 0 ].DataPropertyName = "Name";
            dataGridView.Columns[ 1 ].DataPropertyName = "Value";
            
            dataGridView.SelectionMode               = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.EnableHeadersVisualStyles   = false;
            dataGridView.AllowUserToAddRows          = false;
        }

        private void ListBoxScenarioOnDrawItem( object sender, DrawItemEventArgs e )
        {
            var rowIndex    = e.Index;

            if( rowIndex >= _debuggerEngine.GetCurrentScenarioLineCount() )
            {
                e.Graphics.DrawString(
                    string.Empty, 
                    e.Font, 
                    new SolidBrush( Color.Black ), 
                    e.Bounds);

                return;
            }
            
            var rowStatus   = _debuggerEngine.GetCurrentScenarioRowStatus( rowIndex );
            
            var isSelected  = rowIndex == _selectedIndexInScenarioListBox;
            var isExecuted  = rowStatus.IsExecuted;
            var color       = Color.FromArgb(153, 180, 209);
            
            if( isSelected && isExecuted )
                e.Graphics.FillRectangle( new SolidBrush( color ), e.Bounds );
            else if( isSelected )                
                e.Graphics.FillRectangle( new SolidBrush( Color.FromArgb(0,120,215) ), e.Bounds );
            else if( isExecuted )
                e.Graphics.FillRectangle( new SolidBrush(Color.Chartreuse), e.Bounds );
            
            e.Graphics.DrawString(
                rowStatus.Text, 
                e.Font,
                isSelected 
                    ? new SolidBrush( Color.White )
                    :new SolidBrush( Color.Black ), 
                e.Bounds);
        }

        private void ButtonSaveOnSettingsTabOnClick( object sender, EventArgs e )
        {
            try
            {
                _debuggerEngine.SetSettings(textBoxSettingsOnTabSettings.Text);
                _debuggerEngine.SaveSettings();
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.Message, "Cannot save settings" );
            }
        }

        private void TabControlOnSelecting( object sender, TabControlCancelEventArgs e )
        {
            try
            {
                if( _currentTabPage == tabPageSettings )
                    _debuggerEngine.SetSettings( textBoxSettingsOnTabSettings.Text );
                
                _currentTabPage = e.TabPage;
            }
            catch( Exception exception )
            {
                MessageBox.Show( exception.Message, "Invalid JSON data" );
                e.Cancel = true;
            }
        }

        private void ButtonReloadOnSettingsTabOnClick( object sender, EventArgs e )
        {
            _debuggerEngine.ReloadSettings();
            textBoxSettingsOnTabSettings.Text = _debuggerEngine.GetSettingsText();
        }

        private void ApplyLayoutFixes()
        {
            textBoxSettingsOnTabSettings.SetHeightRelativeToParent( 
                tabPageSettings, LayoutFix.GetHeight( buttonSaveOnSettingsTab, LayoutFix.HeightGap1, LayoutFix.HeightGap1 ) );
            
            buttonReloadOnSettingsTab.PutBellowTarget( textBoxSettingsOnTabSettings, LayoutFix.HeightGap1 );
            buttonSaveOnSettingsTab.PutBellowTarget( textBoxSettingsOnTabSettings, LayoutFix.HeightGap1 );
        }
    }
}