namespace WindowsFormsApplication1
{
    partial class SudokuSolverForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.bigSquare9 = new WindowsFormsApplication1.BigSquare();
            this.bigSquare8 = new WindowsFormsApplication1.BigSquare();
            this.bigSquare7 = new WindowsFormsApplication1.BigSquare();
            this.bigSquare6 = new WindowsFormsApplication1.BigSquare();
            this.bigSquare5 = new WindowsFormsApplication1.BigSquare();
            this.bigSquare4 = new WindowsFormsApplication1.BigSquare();
            this.bigSquare3 = new WindowsFormsApplication1.BigSquare();
            this.bigSquare2 = new WindowsFormsApplication1.BigSquare();
            this.bigSquare1 = new WindowsFormsApplication1.BigSquare();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point( 12, 244 );
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size( 75, 23 );
            this.button1.TabIndex = 1;
            this.button1.Text = "Solve!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler( this.button1_Click );
            // 
            // bigSquare9
            // 
            this.bigSquare9.Location = new System.Drawing.Point( 368, 191 );
            this.bigSquare9.Name = "bigSquare9";
            this.bigSquare9.Size = new System.Drawing.Size( 127, 83 );
            this.bigSquare9.TabIndex = 11;
            // 
            // bigSquare8
            // 
            this.bigSquare8.Location = new System.Drawing.Point( 235, 191 );
            this.bigSquare8.Name = "bigSquare8";
            this.bigSquare8.Size = new System.Drawing.Size( 127, 83 );
            this.bigSquare8.TabIndex = 10;
            // 
            // bigSquare7
            // 
            this.bigSquare7.Location = new System.Drawing.Point( 102, 191 );
            this.bigSquare7.Name = "bigSquare7";
            this.bigSquare7.Size = new System.Drawing.Size( 127, 83 );
            this.bigSquare7.TabIndex = 9;
            // 
            // bigSquare6
            // 
            this.bigSquare6.Location = new System.Drawing.Point( 368, 102 );
            this.bigSquare6.Name = "bigSquare6";
            this.bigSquare6.Size = new System.Drawing.Size( 127, 83 );
            this.bigSquare6.TabIndex = 8;
            // 
            // bigSquare5
            // 
            this.bigSquare5.Location = new System.Drawing.Point( 235, 102 );
            this.bigSquare5.Name = "bigSquare5";
            this.bigSquare5.Size = new System.Drawing.Size( 127, 83 );
            this.bigSquare5.TabIndex = 7;
            // 
            // bigSquare4
            // 
            this.bigSquare4.Location = new System.Drawing.Point( 102, 102 );
            this.bigSquare4.Name = "bigSquare4";
            this.bigSquare4.Size = new System.Drawing.Size( 127, 83 );
            this.bigSquare4.TabIndex = 6;
            // 
            // bigSquare3
            // 
            this.bigSquare3.Location = new System.Drawing.Point( 368, 13 );
            this.bigSquare3.Name = "bigSquare3";
            this.bigSquare3.Size = new System.Drawing.Size( 127, 83 );
            this.bigSquare3.TabIndex = 5;
            // 
            // bigSquare2
            // 
            this.bigSquare2.Location = new System.Drawing.Point( 235, 13 );
            this.bigSquare2.Name = "bigSquare2";
            this.bigSquare2.Size = new System.Drawing.Size( 127, 83 );
            this.bigSquare2.TabIndex = 4;
            // 
            // bigSquare1
            // 
            this.bigSquare1.Location = new System.Drawing.Point( 102, 13 );
            this.bigSquare1.Name = "bigSquare1";
            this.bigSquare1.Size = new System.Drawing.Size( 127, 83 );
            this.bigSquare1.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point( 12, 13 );
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size( 75, 23 );
            this.button2.TabIndex = 12;
            this.button2.Text = "Clear";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler( this.button2_Click );
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point( 13, 43 );
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size( 65, 17 );
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "Verbose";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler( this.checkBox1_CheckedChanged );
            // 
            // SudokuSolverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 497, 279 );
            this.Controls.Add( this.checkBox1 );
            this.Controls.Add( this.button2 );
            this.Controls.Add( this.bigSquare9 );
            this.Controls.Add( this.bigSquare8 );
            this.Controls.Add( this.bigSquare7 );
            this.Controls.Add( this.bigSquare6 );
            this.Controls.Add( this.bigSquare5 );
            this.Controls.Add( this.bigSquare4 );
            this.Controls.Add( this.bigSquare3 );
            this.Controls.Add( this.bigSquare2 );
            this.Controls.Add( this.bigSquare1 );
            this.Controls.Add( this.button1 );
            this.Name = "SudokuSolverForm";
            this.Text = "Sudoku Solver";
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private BigSquare bigSquare1;
        private BigSquare bigSquare2;
        private BigSquare bigSquare3;
        private BigSquare bigSquare4;
        private BigSquare bigSquare5;
        private BigSquare bigSquare6;
        private BigSquare bigSquare7;
        private BigSquare bigSquare8;
        private BigSquare bigSquare9;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

