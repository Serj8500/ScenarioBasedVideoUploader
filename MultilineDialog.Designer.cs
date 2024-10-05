using System.ComponentModel;

namespace ScenarioBasedVideoUploader
{
    partial class MultilineDialog
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
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxValue
            // 
            this.textBoxValue.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom ) | System.Windows.Forms.AnchorStyles.Left ) | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.textBoxValue.Location = new System.Drawing.Point( 12, 12 );
            this.textBoxValue.Multiline = true;
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size( 1176, 626 );
            this.textBoxValue.TabIndex = 10;
            this.textBoxValue.TabStop = false;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point( 406, 655 );
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size( 117, 36 );
            this.buttonOk.TabIndex = 12;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point( 678, 655 );
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size( 117, 36 );
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // MultilineDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 12F, 25F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 1200, 703 );
            this.Controls.Add( this.buttonCancel );
            this.Controls.Add( this.buttonOk );
            this.Controls.Add( this.textBoxValue );
            this.Font = new System.Drawing.Font( "Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 204 ) ) );
            this.Margin = new System.Windows.Forms.Padding( 4, 5, 4, 5 );
            this.Name = "MultilineDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MultilineDialog";
            this.ResumeLayout( false );
            this.PerformLayout();
        }

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;

        private System.Windows.Forms.TextBox textBoxValue;

        #endregion
    }
}