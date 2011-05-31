namespace SudokuSolver
{
    /// <summary>
    /// Form for the SudokuSolver
    /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( SudokuSolverForm ) );
            this.Solve_Button = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Verbose_CheckBox = new System.Windows.Forms.CheckBox();
            this.Check_Button = new System.Windows.Forms.Button();
            this.Lock_CheckBox = new System.Windows.Forms.CheckBox();
            SudokuSolverForm.Verbose_Output = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bigSquare9 = new SudokuSolver.BigSquare();
            this.bigSquare8 = new SudokuSolver.BigSquare();
            this.bigSquare7 = new SudokuSolver.BigSquare();
            this.bigSquare6 = new SudokuSolver.BigSquare();
            this.bigSquare5 = new SudokuSolver.BigSquare();
            this.bigSquare4 = new SudokuSolver.BigSquare();
            this.bigSquare3 = new SudokuSolver.BigSquare();
            this.bigSquare2 = new SudokuSolver.BigSquare();
            this.bigSquare1 = new SudokuSolver.BigSquare();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Solve_Button
            // 
            this.Solve_Button.Location = new System.Drawing.Point( 12, 257 );
            this.Solve_Button.Name = "Solve_Button";
            this.Solve_Button.Size = new System.Drawing.Size( 75, 23 );
            this.Solve_Button.TabIndex = 1;
            this.Solve_Button.Text = "Solve!";
            this.Solve_Button.UseVisualStyleBackColor = true;
            this.Solve_Button.Click += new System.EventHandler( this.Solve_Click );
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point( 12, 228 );
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size( 75, 23 );
            this.button2.TabIndex = 12;
            this.button2.Text = "Clear";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler( this.Clear_Click );
            // 
            // Verbose_CheckBox
            // 
            this.Verbose_CheckBox.AutoSize = true;
            this.Verbose_CheckBox.Location = new System.Drawing.Point( 12, 81 );
            this.Verbose_CheckBox.Name = "Verbose_CheckBox";
            this.Verbose_CheckBox.Size = new System.Drawing.Size( 65, 17 );
            this.Verbose_CheckBox.TabIndex = 13;
            this.Verbose_CheckBox.Text = "Verbose";
            this.Verbose_CheckBox.UseVisualStyleBackColor = true;
            this.Verbose_CheckBox.CheckedChanged += new System.EventHandler( this.Verbose_CheckBox_CheckedChanged );
            // 
            // Check_Button
            // 
            this.Check_Button.Location = new System.Drawing.Point( 12, 27 );
            this.Check_Button.Name = "Check_Button";
            this.Check_Button.Size = new System.Drawing.Size( 75, 23 );
            this.Check_Button.TabIndex = 14;
            this.Check_Button.Text = "Check";
            this.Check_Button.UseVisualStyleBackColor = true;
            this.Check_Button.Click += new System.EventHandler( this.Check_Click );
            // 
            // Lock_CheckBox
            // 
            this.Lock_CheckBox.AutoSize = true;
            this.Lock_CheckBox.Location = new System.Drawing.Point( 14, 57 );
            this.Lock_CheckBox.Name = "Lock_CheckBox";
            this.Lock_CheckBox.Size = new System.Drawing.Size( 81, 17 );
            this.Lock_CheckBox.TabIndex = 15;
            this.Lock_CheckBox.Text = "Lock Given";
            this.Lock_CheckBox.UseVisualStyleBackColor = true;
            this.Lock_CheckBox.CheckedChanged += new System.EventHandler( this.Lock_CheckedChanged );
            // 
            // Verbose_Output
            // 
            SudokuSolverForm.Verbose_Output.Font = new System.Drawing.Font( "Calibri", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
            SudokuSolverForm.Verbose_Output.Location = new System.Drawing.Point( 501, 30 );
            SudokuSolverForm.Verbose_Output.Multiline = true;
            SudokuSolverForm.Verbose_Output.Name = "Verbose_Output";
            SudokuSolverForm.Verbose_Output.ReadOnly = true;
            SudokuSolverForm.Verbose_Output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            SudokuSolverForm.Verbose_Output.Size = new System.Drawing.Size( 312, 250 );
            SudokuSolverForm.Verbose_Output.TabIndex = 16;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.exitToolStripMenuItem} );
            this.menuStrip1.Location = new System.Drawing.Point( 0, 0 );
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size( 825, 24 );
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem} );
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size( 44, 20 );
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size( 107, 22 );
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler( this.aboutToolStripMenuItem_Click );
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size( 37, 20 );
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler( this.exitToolStripMenuItem_Click );
            // 
            // bigSquare9
            // 
            this.bigSquare9.Location = new System.Drawing.Point( 368, 204 );
            this.bigSquare9.Name = "bigSquare9";
            this.bigSquare9.Size = new System.Drawing.Size( 128, 83 );
            this.bigSquare9.TabIndex = 11;
            // 
            // bigSquare8
            // 
            this.bigSquare8.Location = new System.Drawing.Point( 236, 204 );
            this.bigSquare8.Name = "bigSquare8";
            this.bigSquare8.Size = new System.Drawing.Size( 128, 83 );
            this.bigSquare8.TabIndex = 10;
            // 
            // bigSquare7
            // 
            this.bigSquare7.Location = new System.Drawing.Point( 102, 204 );
            this.bigSquare7.Name = "bigSquare7";
            this.bigSquare7.Size = new System.Drawing.Size( 128, 83 );
            this.bigSquare7.TabIndex = 9;
            // 
            // bigSquare6
            // 
            this.bigSquare6.Location = new System.Drawing.Point( 368, 114 );
            this.bigSquare6.Name = "bigSquare6";
            this.bigSquare6.Size = new System.Drawing.Size( 128, 83 );
            this.bigSquare6.TabIndex = 8;
            // 
            // bigSquare5
            // 
            this.bigSquare5.Location = new System.Drawing.Point( 236, 114 );
            this.bigSquare5.Name = "bigSquare5";
            this.bigSquare5.Size = new System.Drawing.Size( 128, 83 );
            this.bigSquare5.TabIndex = 7;
            // 
            // bigSquare4
            // 
            this.bigSquare4.Location = new System.Drawing.Point( 102, 114 );
            this.bigSquare4.Name = "bigSquare4";
            this.bigSquare4.Size = new System.Drawing.Size( 128, 83 );
            this.bigSquare4.TabIndex = 6;
            // 
            // bigSquare3
            // 
            this.bigSquare3.Location = new System.Drawing.Point( 368, 26 );
            this.bigSquare3.Name = "bigSquare3";
            this.bigSquare3.Size = new System.Drawing.Size( 128, 83 );
            this.bigSquare3.TabIndex = 5;
            // 
            // bigSquare2
            // 
            this.bigSquare2.Location = new System.Drawing.Point( 236, 26 );
            this.bigSquare2.Name = "bigSquare2";
            this.bigSquare2.Size = new System.Drawing.Size( 128, 83 );
            this.bigSquare2.TabIndex = 4;
            // 
            // bigSquare1
            // 
            this.bigSquare1.Location = new System.Drawing.Point( 102, 26 );
            this.bigSquare1.Name = "bigSquare1";
            this.bigSquare1.Size = new System.Drawing.Size( 128, 83 );
            this.bigSquare1.TabIndex = 3;
            // 
            // SudokuSolverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size( 825, 286 );
            this.Controls.Add( SudokuSolverForm.Verbose_Output );
            this.Controls.Add( this.Lock_CheckBox );
            this.Controls.Add( this.Check_Button );
            this.Controls.Add( this.Verbose_CheckBox );
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
            this.Controls.Add( this.Solve_Button );
            this.Controls.Add( this.menuStrip1 );
            this.Icon = ( (System.Drawing.Icon) ( resources.GetObject( "$this.Icon" ) ) );
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "SudokuSolverForm";
            this.Text = "Sudoku Solver";
            this.menuStrip1.ResumeLayout( false );
            this.menuStrip1.PerformLayout();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Solve_Button;
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
        private System.Windows.Forms.CheckBox Verbose_CheckBox;
        private System.Windows.Forms.Button Check_Button;
        private System.Windows.Forms.CheckBox Lock_CheckBox;
        private static System.Windows.Forms.TextBox Verbose_Output;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

