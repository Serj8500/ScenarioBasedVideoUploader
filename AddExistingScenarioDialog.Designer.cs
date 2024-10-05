using System.ComponentModel;

namespace ScenarioBasedVideoUploader
{
    partial class AddExistingScenarioDialog
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.comboBoxScenarios = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point( 335, 97 );
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size( 117, 36 );
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOk.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point( 110, 97 );
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size( 117, 36 );
            this.buttonOk.TabIndex = 14;
            this.buttonOk.Text = "Add";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // comboBoxScenarios
            // 
            this.comboBoxScenarios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScenarios.FormattingEnabled = true;
            this.comboBoxScenarios.Location = new System.Drawing.Point( 292, 38 );
            this.comboBoxScenarios.Name = "comboBoxScenarios";
            this.comboBoxScenarios.Size = new System.Drawing.Size( 224, 33 );
            this.comboBoxScenarios.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 29, 41 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 257, 25 );
            this.label1.TabIndex = 17;
            this.label1.Text = "Available unused scenarios:";
            // 
            // AddExistingScenarioDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 12F, 25F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 552, 155 );
            this.Controls.Add( this.label1 );
            this.Controls.Add( this.comboBoxScenarios );
            this.Controls.Add( this.buttonCancel );
            this.Controls.Add( this.buttonOk );
            this.Font = new System.Drawing.Font( "Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 204 ) ) );
            this.Margin = new System.Windows.Forms.Padding( 4, 5, 4, 5 );
            this.Name = "AddExistingScenarioDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add an existing scenario";
            this.ResumeLayout( false );
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.ComboBox comboBoxScenarios;

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;

        #endregion
    }
}