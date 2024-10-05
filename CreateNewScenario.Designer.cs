using System.ComponentModel;

namespace ScenarioBasedVideoUploader
{
    partial class CreateNewScenario
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.buttonCreateNewScenario = new System.Windows.Forms.Button();
            this.textBoxNewScenarioName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonCreateNewScenario
            // 
            this.buttonCreateNewScenario.Location = new System.Drawing.Point( 121, 79 );
            this.buttonCreateNewScenario.Name = "buttonCreateNewScenario";
            this.buttonCreateNewScenario.Size = new System.Drawing.Size( 117, 36 );
            this.buttonCreateNewScenario.TabIndex = 12;
            this.buttonCreateNewScenario.Text = "Create";
            this.buttonCreateNewScenario.UseVisualStyleBackColor = true;
            // 
            // textBoxNewScenarioName
            // 
            this.textBoxNewScenarioName.Location = new System.Drawing.Point( 196, 21 );
            this.textBoxNewScenarioName.Name = "textBoxNewScenarioName";
            this.textBoxNewScenarioName.Size = new System.Drawing.Size( 286, 30 );
            this.textBoxNewScenarioName.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 31, 24 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 150, 25 );
            this.label2.TabIndex = 10;
            this.label2.Text = "Scenario name:";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point( 284, 79 );
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size( 117, 36 );
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // CreateNewScenario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 12F, 25F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 535, 146 );
            this.ControlBox = false;
            this.Controls.Add( this.buttonCancel );
            this.Controls.Add( this.buttonCreateNewScenario );
            this.Controls.Add( this.textBoxNewScenarioName );
            this.Controls.Add( this.label2 );
            this.Font = new System.Drawing.Font( "Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 204 ) ) );
            this.Margin = new System.Windows.Forms.Padding( 4, 5, 4, 5 );
            this.Name = "CreateNewScenario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Scenario";
            this.ResumeLayout( false );
            this.PerformLayout();
        }

        private System.Windows.Forms.Button buttonCancel;

        private System.Windows.Forms.Button buttonCreateNewScenario;
        private System.Windows.Forms.TextBox textBoxNewScenarioName;
        private System.Windows.Forms.Label label2;

        #endregion
    }
}