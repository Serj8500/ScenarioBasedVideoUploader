using System.Windows.Forms;

namespace ScenarioBasedVideoUploader
{
    public partial class MultilineDialog : Form
    {
        public string Value 
        {
            get => GetValue();
            set => SetValue( value );
        }

        private string GetValue()
        {
            return textBoxValue.Text.Replace("\r\n", "\\n").Trim();
        }

        private void SetValue( string value )
        {
            textBoxValue.Text = value.Replace("\\n", "\r\n").Trim();
        }

        public MultilineDialog()
        {
            InitializeComponent();
        }
    }
}