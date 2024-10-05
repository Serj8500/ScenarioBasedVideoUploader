using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ScenarioBasedVideoUploader
{
    public partial class CreateNewScenario : Form
    {
        public string ScenarioName => textBoxNewScenarioName.Text;
        
        public Settings                 Settings        { get; set; }
        public IReadOnlyList<string>    QueuedScenarios { get; set; }

        public CreateNewScenario()
        {
            InitializeComponent();
            
            buttonCreateNewScenario.Click += ButtonCreateNewScenarioOnClick;
            
            textBoxNewScenarioName.KeyDown += TextBoxNewScenarioNameOnKeyDown;
        }

        private void TextBoxNewScenarioNameOnKeyDown( object sender, KeyEventArgs e )
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonCreateNewScenario.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void ButtonCreateNewScenarioOnClick( object sender, EventArgs e )
        {
            if( string.IsNullOrEmpty( ScenarioName ) )
            {
                MessageBox.Show( "Scenario name cannot be empty", "Error" );
                return;
            }
            
            if( QueuedScenarios.Any( i => string.Compare( i, ScenarioName, StringComparison.InvariantCultureIgnoreCase ) == 0 ) )
            {
                MessageBox.Show( $"Scenario with the name: {ScenarioName} is already in the queue", "Error" );
                return;
            }
            
            if( !IsValidScenarioName( ScenarioName ) )
            {
                MessageBox.Show( $"Scenario: {ScenarioName} already exists", "Error" );
                return;
            }
            
            DialogResult = DialogResult.OK;
        }

        private bool IsValidScenarioName( string scenarioName )
        {
            var files = DebuggerEngine.GetFileNames( Settings.FolderForScenarios, Settings.ScenarioFileExtension );
            var existingScenarioNames = DebuggerEngine.GetScenarioNames( files, Settings.FolderForScenarios, Settings.ScenarioFileExtension );
            
            return existingScenarioNames.All( e => string.Compare( e, scenarioName, StringComparison.InvariantCultureIgnoreCase ) != 0 );
        }
    }
}