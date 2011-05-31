using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class SudokuSolverForm : Form
    {
        private int[,] map = new int[9, 9];
        private List<BigSquare> Squares { get; set; }

        /// <summary>
        /// Holds a list of the Verbose output
        /// </summary>
        public static List<String> Output { get; private set; }

        /// <summary>
        /// Default ctor.
        /// </summary>
        public SudokuSolverForm()
        {
            InitializeComponent();

            Squares = new List<BigSquare>();

            Squares.Add( bigSquare1 );
            Squares.Add( bigSquare2 );
            Squares.Add( bigSquare3 );
            Squares.Add( bigSquare4 );
            Squares.Add( bigSquare5 );
            Squares.Add( bigSquare6 );
            Squares.Add( bigSquare7 );
            Squares.Add( bigSquare8 );
            Squares.Add( bigSquare9 );

            Solve_Button.Enabled = false;
            Check_Button.Enabled = false;

            Output = new List<string>();

            Verbose_Output.Visible = false;
        }

        /// <summary>
        /// Transfers the output from Output to the visable verbose output
        /// </summary>
        private static void Verbose_Update()
        {
            Verbose_Output.Lines = Output.ToArray();
        }

        /// <summary>
        /// Method called when the solve button is pressed. Solves the puzzle, then sets the values on the grid
        /// </summary>
        /// <param name="sender">Button sender</param>
        /// <param name="e">Eventargs</param>
        private void Solve_Click( object sender, EventArgs e )
        {
            // Solve 
            for ( int i = 0; i < 9; i++ )
            {
                Squares[i].setValues( setup().getSquareValues( i ) );
            }

            Verbose_Update();
        }

        /// <summary>
        /// Used to setup the puzzle, returns the puzzle object. By default only reads the locked values
        /// </summary>
        /// <returns></returns>
        private SudokuPuzzle setup()
        {
            populateMap( true );
            Boolean verbose = Verbose_CheckBox.Checked;

            return new SudokuPuzzle( map, verbose );
        }

        /// <summary>
        /// Populates Map based on the values set in the grid.
        /// </summary>
        /// <param name="locked"></param>
        private void populateMap( Boolean locked )
        {
            int xOffSet = 0;
            int yOffSet = 0;

            // iterate the big squares
            for ( int x = 0; x < 9; x++ )
            {
                if ( x < 3 )
                {
                    xOffSet = 3 * x;
                }
                if ( x >= 3 && x < 6 )
                {
                    xOffSet = 3 * ( x - 3 );
                    yOffSet = 3;
                }
                if ( x >= 6 )
                {
                    xOffSet = 3 * ( x - 6 );
                    yOffSet = 6;
                }

                // iterate big square values
                // every 3 move the y position one more
                for ( int y = 0; y < 9; y++ )
                {
                    if ( y < 3 )
                    {
                        if ( !locked )
                            map[xOffSet + y, yOffSet] = Squares[x][y];
                        else
                            map[xOffSet + y, yOffSet] = Squares[x].lockedValues( y );
                    }
                    if ( y >= 3 && y < 6 )
                    {
                        if ( !locked )
                            map[xOffSet + y - 3, yOffSet + 1] = Squares[x][y];
                        else
                            map[xOffSet + y - 3, yOffSet + 1] = Squares[x].lockedValues( y );
                    }
                    if ( y >= 6 )
                    {
                        if ( !locked )
                            map[xOffSet + y - 6, yOffSet + 2] = Squares[x][y];
                        else
                            map[xOffSet + y - 6, yOffSet + 2] = Squares[x].lockedValues( y );
                    }
                }
            }
        }

        /// <summary>
        /// Method called when the clear button is pressed. Calls the clear method in each BigSquare
        /// </summary>
        /// <param name="sender">Button object sender</param>
        /// <param name="e">Eventargs</param>
        private void Clear_Click( object sender, EventArgs e )
        {
            // Clear button
            foreach ( BigSquare iter in Squares )
            {
                iter.clear();
            }

            Lock_CheckBox.Checked = false;

            Solve_Button.Enabled = false;
            Check_Button.Enabled = false;

            Output = new List<string>();
            Verbose_Update();

            Verbose_CheckBox.Checked = false;
        }

        /// <summary>
        /// Adds the given string as a new entry in the Output list to be displayed in the verbose output
        /// </summary>
        /// <param name="str">String to add</param>
        public static void Verbose_Add( String str )
        {
            Output.Add( str );
            //Verbose_Update();
        }

        /// <summary>
        /// Method called when the check button is pressed. Calls the checkValue method in each BigSquare
        /// </summary>
        /// <param name="sender">Button object sender</param>
        /// <param name="e">Eventargs</param>
        private void Check_Click( object sender, EventArgs e )
        {
            // Check button
            for ( int i = 0; i < 9; i++ )
            {
                Squares[i].checkValues( setup().getSquareValues( i ) );
            }

            Verbose_Update();
        }

        /// <summary>
        /// Called when the lock checkbox is toggled. Calls the lock/unlockValues in each BigSquare
        /// </summary>
        /// <param name="sender">Checkbox object sender</param>
        /// <param name="e">Eventargs</param>
        private void Lock_CheckedChanged( object sender, EventArgs e )
        {
            // Lock values
            foreach ( BigSquare iter in Squares )
            {
                if ( Lock_CheckBox.Checked )
                    iter.lockValues();
                else
                    iter.unlockValues();
            }

            // Toggle solve/check buttons
            if ( Solve_Button.Enabled )
            { 
                Solve_Button.Enabled = false;
                Check_Button.Enabled = false;
            }
            else
            {
                Solve_Button.Enabled = true;
                Check_Button.Enabled = true;
            }
        }

        /// <summary>
        /// Called when the verbose checkbox is toggled. Hides/shows the verbose output
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Verbose_CheckBox_CheckedChanged( object sender, EventArgs e )
        {
            if ( Verbose_CheckBox.Checked )
            {
                Verbose_Output.Visible = true;
            }
            else
            {
                Verbose_Output.Visible = false;
            }
        }

        /// <summary>
        /// Launches the aboutbox
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventargs</param>
        private void aboutToolStripMenuItem_Click( object sender, EventArgs e )
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        /// <summary>
        /// Exits the program
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventargs</param>
        private void exitToolStripMenuItem_Click( object sender, EventArgs e )
        {
            this.Close();
        }

    }
}
