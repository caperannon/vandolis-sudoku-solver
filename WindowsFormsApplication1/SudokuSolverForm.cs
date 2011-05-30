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
        public static List<String> Output { get; private set; }

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

        private void Solve_Click( object sender, EventArgs e )
        {
            // Solve 
            for ( int i = 0; i < 9; i++ )
            {
                Squares[i].setValues( setup().getSquareValues( i ) );
            }

            Verbose_Output.Lines = Output.ToArray();
        }

        private SudokuPuzzle setup()
        {
            populateMap( true );
            Boolean verbose = Verbose_CheckBox.Checked;

            return new SudokuPuzzle( map, verbose );
        }

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

            /*
            Console.Write("The Matrix is:\n");
            String str = "";
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    str += map[x,y] + " ";
                }
                Console.Write(str+ "\n");
                str = "";
            }*/

        }

        private void testSolve()
        {
            map = new int[,]
            {
                {7,2,0,0,0,3,0,5,0},
                {4,5,8,9,0,0,6,0,0},
                {0,1,9,6,0,0,0,0,0},
                {0,0,0,0,3,4,1,8,0},
                {0,0,5,0,0,0,4,0,0},
                {0,4,2,1,9,0,0,0,0},
                {0,0,0,0,0,9,7,4,0},
                {0,0,4,0,0,1,9,2,8},
                {0,7,0,8,0,0,0,1,6}
            };

            /*
            map = new int[,]
            {
                {
                    0,5,0,0,0,0,0,0,0
                },
                {
                    2,0,0,0,3,0,5,7,4
                },
                {
                    6,0,0,0,0,4,9,0,0
                },
                {
                    8,0,7,0,0,1,2,5,0
                },
                {
                    4,0,0,0,0,0,0,0,9
                },
                {
                    0,1,6,3,0,0,4,0,7
                },
                {
                    0,0,2,6,0,0,0,0,3
                },
                {
                    3,4,8,0,2,0,0,0,5
                },
                {
                    0,0,0,0,0,0,0,2,0
                }
            };*/

            SudokuPuzzle puzzle = new SudokuPuzzle( map );
            //puzzle.Verbose = true;
            Console.Write( "The Puzzle it recieved is:\n" );
            Console.Write( puzzle.ToString() + "\n" );


            //Console.Write("The solution to the problem is:\n");
            Console.Write( puzzle.solveText() );
        }

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

            Verbose_Output.Lines = new String[1];
            Verbose_CheckBox.Checked = false;
        }

        private void Check_Click( object sender, EventArgs e )
        {
            // Check button
            for ( int i = 0; i < 9; i++ )
            {
                Squares[i].checkValues( setup().getSquareValues( i ) );
            }

            Verbose_Output.Lines = Output.ToArray();
        }

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

        private void aboutToolStripMenuItem_Click( object sender, EventArgs e )
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        private void exitToolStripMenuItem_Click( object sender, EventArgs e )
        {
            this.Close();
        }

    }
}
