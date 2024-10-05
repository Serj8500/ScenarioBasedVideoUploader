using System;
using System.Windows.Forms;

namespace ScenarioBasedVideoUploader
{
    public partial class AddExistingScenarioDialog : Form
    {
        public string SelectedScenarioName => comboBoxScenarios.Text;

        public AddExistingScenarioDialog()
        {
            InitializeComponent();
        }
        
        public void SetAvailableUnusedScenarios( string[] scenarios )
        {
            if( scenarios.Length == 0 )
                throw new Exception("The scenario list must not be empty");
            
            comboBoxScenarios.Items.AddRange( scenarios );
            comboBoxScenarios.Text = scenarios[ 0 ];
        }        
    }
}