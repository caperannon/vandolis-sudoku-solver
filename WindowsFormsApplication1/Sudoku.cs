using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSolver
{
    class SudokuPuzzle
    {
        public SudokuCell[,] Puzzle { get; private set; } // Actual array of cells
        public Boolean Verbose { private get; set; } // Var that controls the level of console output
        public Boolean Solved { get; private set; } // Var to tell if the puzzle was solved or not
        private Boolean Guessing { get; set; }
        private enum Status { Value_Set, Populate, Evaluate, Guess, Solved, Stuck }
        private static int GuessedPuzzles { get; set; }

        /// <summary>
        /// Ctor which takes an already assembled array of SudokuCells and tries to solve it. Deep copies
        /// </summary>
        /// <param name="puzzle">Puzzle to take</param>
        public SudokuPuzzle( SudokuCell[,] puzzle )
        {
            Puzzle = new SudokuCell[9, 9];

            // Perform a deep copy
            for ( int x = 0; x < 9; x++ )
            {
                for ( int y = 0; y < 9; y++ )
                {
                    Puzzle[x, y] = new SudokuCell( puzzle[x, y].Value );
                    Puzzle[x, y].setPossibly( puzzle[x, y].getPossibly() );
                }
            }

            solve();
        }

        /// <summary>
        /// Default ctor which makes a puzzle based on the given 9x9 array of integers, which it then solves
        /// </summary>
        /// <param name="puzzle">A 9x9 array of integers ranging from 0-9</param>
        public SudokuPuzzle( int[,] puzzle )
        {
            Puzzle = transform( puzzle );
            Verbose = false;
            solve();
        }

        /// <summary>
        /// Default ctor which makes a puzzle based on the given 9x9 array of integers, which it then solves being verbose in the console
        /// </summary>
        /// <param name="puzzle">A 9x9 array of integers ranging from 0-9</param>
        /// <param name="verbose">True to display full text output, false to not</param>
        public SudokuPuzzle( int[,] puzzle, Boolean verbose )
        {
            Puzzle = transform( puzzle );
            Verbose = verbose;

            GuessedPuzzles = 0;

            solve();
        }

        /// <summary>
        /// Solves the puzzle. No output
        /// </summary>
        public void solve()
        {
            // P-SQU -> P-COL -> P-ROW -> E-SQU -> E-COL -> E-ROW -> Guess -> Done/Stuck
            Status status = Status.Value_Set;

            do
            {
                switch ( status )
                {
                    case Status.Value_Set:
                        Solved = isValid( Puzzle );
                        if ( !Solved )
                            status = Status.Populate;
                        else
                            status = Status.Solved;
                        break;
                    case Status.Populate:
                        if ( ( status = populatePossibilitiesSquare() ) == Status.Populate )
                            if ( ( status = populatePossibilitiesColumn() ) == Status.Populate )
                                if ( ( status = populatePossibilitiesRow() ) == Status.Populate )
                                    status = Status.Evaluate;
                        break;
                    case Status.Evaluate:
                        if ( ( status = evaluatePossibilitiesSquare() ) == Status.Evaluate )
                            if ( ( status = evaluatePossibilitiesColumn() ) == Status.Evaluate )
                                if ( ( status = evaluatePossibilitiesRow() ) == Status.Evaluate )
                                    status = Status.Guess;
                        break;
                    case Status.Guess:
                        status = guess( Puzzle );
                        break;
                    case Status.Solved:
                        break;
                    case Status.Stuck:
                        break;
                    default:
                        break;
                }
            } while ( status != Status.Solved && status != Status.Stuck );

            if ( Verbose )
            {
                SudokuSolverForm.Verbose_Add( solveText() );
            }
        }

        /// <summary>
        /// Used to output the solved puzzle to the console
        /// </summary>
        /// <returns>String including whether it solved it or not, as well as the ToString output</returns>
        public String solveText()
        {
            if ( Solved )
                return "The solution to the puzzle is:\n" + this.ToString();
            else
                return "I couldn't finish the problem, this is what I have so far:\n" + this.ToString();
        }

        /// <summary>
        /// Transforms the given 9x9 array of integers into a puzzle form
        /// </summary>
        /// <param name="puzzle">int[9,9] to transform</param>
        /// <returns>SudokuCell[9,9] of the arr</returns>
        public SudokuCell[,] transform( int[,] puzzle )
        {
            SudokuCell[,] p = new SudokuCell[9, 9];

            for ( int x = 0; x < 9; x++ )
            {
                for ( int y = 0; y < 9; y++ )
                {
                    p[x, y] = new SudokuCell( puzzle[x, y] );
                }
            }

            return p;
        }

        /// <summary>
        /// Makes a normal 9x9 array of integers from the puzzle
        /// </summary>
        /// <param name="puzzle">SudokuCell[9,9] to transform</param>
        /// <returns>int[9,9] of the puzzle</returns>
        public int[,] transform( SudokuCell[,] puzzle )
        {
            int[,] arr = new int[9, 9];

            for ( int x = 0; x < 9; x++ )
            {
                for ( int y = 0; y < 9; y++ )
                {
                    arr[x, y] = Puzzle[x, y].Value;
                }
            }

            return arr;
        }

        /// <summary>
        /// Used to get a list, in order of left to right top to bottom, of the values in a given 3x3 square in the puzzle.
        /// </summary>
        /// <param name="square">The square to get the list from. TL is 0, BR is 8</param>
        /// <returns>List of the cells values</returns>
        public List<int> getSquareValues( int square )
        {
            List<int> list = new List<int>();

            // These values hold offsets that allow you to act as if the 9x9 array is made of multiple 3x3 arrays
            int xOffset = 0, yOffset = 0;

            switch ( square )
            {
                case 0:
                    xOffset = 0;
                    yOffset = 0;
                    break;
                case 1:
                    xOffset = 3;
                    yOffset = 0;
                    break;
                case 2:
                    xOffset = 6;
                    yOffset = 0;
                    break;
                case 3:
                    xOffset = 0;
                    yOffset = 3;
                    break;
                case 4:
                    xOffset = 3;
                    yOffset = 3;
                    break;
                case 5:
                    xOffset = 6;
                    yOffset = 3;
                    break;
                case 6:
                    xOffset = 0;
                    yOffset = 6;
                    break;
                case 7:
                    xOffset = 3;
                    yOffset = 6;
                    break;
                case 8:
                    xOffset = 6;
                    yOffset = 6;
                    break;
                default:
                    break;
            }

            // Iterate through the 3x3
            for ( int y = 0; y < 3; y++ )
            {
                for ( int x = 0; x < 3; x++ )
                {
                    // The actual positions of each cell is the x + offset and the y + offset
                    list.Add( Puzzle[x + xOffset, y + yOffset].Value );
                }
            }

            return list;
        }

        /// <summary>
        /// Begins the population by filling each cell based on its surrounding square. Inserts the maximum amount of possibilities.
        /// </summary>
        /// <returns>Status. Populate if nothing is set, Value_Set if a value is set</returns>
        private Status populatePossibilitiesSquare()
        {
            // These values hold offsets that allow you to act as if the 9x9 array is made of multiple 3x3 arrays
            int xOffset = 0, yOffset = 0;

            // Iterate through each of the 9 smaller 3x3 arrays
            for ( int square = 0; square < 9; square++ )
            {
                // Based on wich smaller array it is evaluating, set the offsets to their respective values
                switch ( square )
                {
                    case 0:
                        xOffset = 0;
                        yOffset = 0;
                        break;
                    case 1:
                        xOffset = 3;
                        yOffset = 0;
                        break;
                    case 2:
                        xOffset = 6;
                        yOffset = 0;
                        break;
                    case 3:
                        xOffset = 0;
                        yOffset = 3;
                        break;
                    case 4:
                        xOffset = 3;
                        yOffset = 3;
                        break;
                    case 5:
                        xOffset = 6;
                        yOffset = 3;
                        break;
                    case 6:
                        xOffset = 0;
                        yOffset = 6;
                        break;
                    case 7:
                        xOffset = 3;
                        yOffset = 6;
                        break;
                    case 8:
                        xOffset = 6;
                        yOffset = 6;
                        break;
                    default:
                        break;
                }

                // Holds each 3x3 arrays values that still need to be found
                List<int> notFound = new List<int>
                {
                    1,2,3,4,5,6,7,8,9 
                };

                // Iterate through the 3x3
                for ( int x = 0; x < 3; x++ )
                {
                    for ( int y = 0; y < 3; y++ )
                    {
                        // The actual positions of each cell is the x + offset and the y + offset
                        int value = Puzzle[x + xOffset, y + yOffset].Value;

                        // If the value is non-zero then remove it from the notFound list
                        if ( value != 0 ) notFound.Remove( value );
                    }
                }

                // Handle debug output
                if ( Verbose )
                {
                    String str = "[P-SQU]The numbers not found for square #" + square + " are: ";
                    notFound.Sort();
                    foreach ( int iter in notFound )
                    {
                        str += iter + ", ";
                    }
                    SudokuSolverForm.Verbose_Add( str + "\n" );
                }

                // Iterate through the 3x3 array again
                for ( int x = 0; x < 3; x++ )
                {
                    for ( int y = 0; y < 3; y++ )
                    {
                        // If the cells value is zero then set its possibilitites to the numbers that are not found
                        if ( Puzzle[x + xOffset, y + yOffset].Value == 0 )
                        {
                            if ( notFound.Count == 1 )
                            {
                                // Set the value
                                Puzzle[x + xOffset, y + yOffset].Value = notFound[0];

                                if ( Verbose )
                                    SudokuSolverForm.Verbose_Add( "[SET][P-SQU] Setting a value at X: " + ( x + xOffset ) + " Y: " + ( y + yOffset ) + " with value: " + notFound[0] + "\n" );

                                return Status.Value_Set;
                            }
                            else
                            {
                                // Set the cells possibilities
                                Puzzle[x + xOffset, y + yOffset].setPossibly( notFound );

                                if ( Verbose )
                                    SudokuSolverForm.Verbose_Add( "[P-SQU] Cell at X: " + ( x + xOffset ) + " Y: " + ( y + yOffset ) + " can be: " + Puzzle[x + xOffset, y + yOffset].getPossiblyText() + "\n" );
                            }
                        }
                    }
                }
            }

            return Status.Populate;
        }

        /// <summary>
        /// Adjusts each cells possibilities based on its column. Only removes possibilities so it must be ran after populatePossibilitiesSquare
        /// </summary>
        /// <returns>Status. Populate if nothing is set, Value_Set if a value is set</returns>
        private Status populatePossibilitiesColumn()
        {
            // Columns
            for ( int x = 0; x < 9; x++ )
            {
                // Holds the values that are found in each column, these numbers are then removed from each of the columns cells possiblities
                List<int> found = new List<int>();

                for ( int y = 0; y < 9; y++ )
                {
                    if ( Puzzle[x, y].Value != 0 )
                    {
                        // Non-zero value is found, add it to the list of found numbers
                        found.Add( Puzzle[x, y].Value );
                    }
                }

                // Handle debug output
                if ( Verbose )
                {
                    String str = "[P-COL] The numbers found in column #" + x + " are: ";
                    found.Sort();
                    foreach ( int iter in found )
                    {
                        str += iter + ", ";
                    }
                    SudokuSolverForm.Verbose_Add( str + "\n" );
                }

                // Iterate through the entire column again
                for ( int y = 0; y < 9; y++ )
                {
                    if ( Puzzle[x, y].Value == 0 )
                    {
                        // If the cells value is still 0 loop through the found values and remove them from each cells possibilities
                        foreach ( int iter in found )
                        {
                            Puzzle[x, y].getPossibly().Remove( iter );
                        }

                        if ( Verbose )
                            SudokuSolverForm.Verbose_Add( "[P-COL] Cell (after) at X: " + x + " Y: " + y + " can be: " + Puzzle[x, y].getPossiblyText() + "\n" );

                        // If the cell only has one possibility then set its value to it
                        if ( Puzzle[x, y].getPossibly().Count == 1 )
                        {
                            // Debug output
                            if ( Verbose )
                                SudokuSolverForm.Verbose_Add( "[SET][P-COL] Setting a value at X: " + x + " Y: " + y + " with value: " + Puzzle[x, y].getPossibly()[0] + "\n" );

                            // Set value
                            Puzzle[x, y].Value = Puzzle[x, y].getPossibly()[0];

                            return Status.Value_Set;
                        }
                    }
                }
            }

            return Status.Populate;
        }

        /// <summary>
        /// Adjusts each cells possibilities based on its row. Only removes possibilities so it must be ran after populatePossibilitiesSquare
        /// </summary>
        /// <returns>Status. Populate if nothing is set, Value_Set if a value is set</returns>
        private Status populatePossibilitiesRow()
        {
            // Rows
            for ( int y = 0; y < 9; y++ )
            {
                // Holds the values that are found in each row, these numbers are then removed from each of the rows cells possiblities
                List<int> found = new List<int>();

                // Iterate through the rows cells
                for ( int x = 0; x < 9; x++ )
                {
                    if ( Puzzle[x, y].Value != 0 )
                    {
                        // If the cells value is non-zero then add it to the found list
                        found.Add( Puzzle[x, y].Value );
                    }
                }

                // Handle debug output
                if ( Verbose )
                {
                    String str = "[P-ROW] The numbers found in row #" + y + " are: ";
                    found.Sort();
                    foreach ( int iter in found )
                    {
                        str += iter + ", ";
                    }
                    SudokuSolverForm.Verbose_Add( str + "\n" );
                }

                // Iterate through the row again
                for ( int x = 0; x < 9; x++ )
                {
                    if ( Puzzle[x, y].Value == 0 )
                    {
                        // If the cells value is still zero then remove all of the found values from its possibilities
                        foreach ( int iter in found )
                        {
                            Puzzle[x, y].getPossibly().Remove( iter );
                        }

                        if ( Verbose )
                            SudokuSolverForm.Verbose_Add( "[P-ROW] Cell (after) at X: " + x + " Y: " + y + " can be: " + Puzzle[x, y].getPossiblyText() + "\n" );

                        // If the cell only has one possiblitiy then go ahead and set its value to it
                        if ( Puzzle[x, y].getPossibly().Count == 1 )
                        {
                            // Debug output
                            if ( Verbose )
                                SudokuSolverForm.Verbose_Add( "[SET][P-ROW] Setting a value at X: " + x + " Y: " + y + " with value: " + Puzzle[x, y].getPossibly()[0] + "\n" );

                            // Set value
                            Puzzle[x, y].Value = Puzzle[x, y].getPossibly()[0];

                            return Status.Value_Set;
                        }
                    }
                }
            }

            return Status.Populate;
        }

        /// <summary>
        /// ToString method for a puzzle. Outputs a string with formatted columns/rows of the values.
        /// </summary>
        /// <returns>string containing only numbers and new lines</returns>
        public override String ToString()
        {
            String str = "";

            for ( int y = 0; y < 9; y++ )
            {
                for ( int x = 0; x < 9; x++ )
                {
                    str += Puzzle[x, y].Value + " ";
                }
                str += "\n";
            }

            return str;
        }

        /// <summary>
        /// Looks through each squares possibilities to see if there is a only one cell with a certain possibility
        /// </summary>
        /// <returns>Status. Evaluate if nothing is set, Value_Set if a value is set</returns>
        private Status evaluatePossibilitiesSquare()
        {
            // These values hold offsets that allow you to act as if the 9x9 array is made of multiple 3x3 arrays
            int xOffset = 0, yOffset = 0;

            // Iterate through each of the 9 smaller 3x3 arrays
            for ( int square = 0; square < 9; square++ )
            {
                // Based on wich smaller array it is evaluating, set the offsets to their respective values
                switch ( square )
                {
                    case 0:
                        xOffset = 0;
                        yOffset = 0;
                        break;
                    case 1:
                        xOffset = 3;
                        yOffset = 0;
                        break;
                    case 2:
                        xOffset = 6;
                        yOffset = 0;
                        break;
                    case 3:
                        xOffset = 0;
                        yOffset = 3;
                        break;
                    case 4:
                        xOffset = 3;
                        yOffset = 3;
                        break;
                    case 5:
                        xOffset = 6;
                        yOffset = 3;
                        break;
                    case 6:
                        xOffset = 0;
                        yOffset = 6;
                        break;
                    case 7:
                        xOffset = 3;
                        yOffset = 6;
                        break;
                    case 8:
                        xOffset = 6;
                        yOffset = 6;
                        break;
                    default:
                        break;
                }

                // Set up the counts for the square
                Dictionary<int, int> counts = new Dictionary<int, int>();
                for ( int i = 1; i < 10; i++ )
                {
                    counts.Add( i, 0 );
                }

                // Iterate through the 3x3
                for ( int x = 0; x < 3; x++ )
                {
                    for ( int y = 0; y < 3; y++ )
                    {
                        // Look through each cells possible value and add it to the total count
                        foreach ( int iter in Puzzle[x + xOffset, y + yOffset].getPossibly() )
                        {
                            counts[iter]++;
                        }
                    }
                }

                // Debug output
                if ( Verbose )
                {
                    SudokuSolverForm.Verbose_Add( "[E-SQU] Counts for square #" + square + " are: [1, " + counts[1] + "] [2, " + counts[2] + "] [3, " + counts[3] + "] [4, " + counts[4] +
                        "] [5, " + counts[5] + "] [6, " + counts[6] + "] [7, " + counts[7] + "] [8, " + counts[8] + "] [9, " + counts[9] + "]\n" );
                }

                // Look through the counts and see if there are any with only 1
                if ( counts.ContainsValue( 1 ) )
                {
                    int key = 0; // The value (key) that the cell should be set to

                    // iterate through the dictionary to find the correct key
                    for ( int i = 1; i < 10; i++ )
                    {
                        if ( counts[i] == 1 ) key = i;
                    }

                    // There is a key with only one possible place, iterate through the 3x3 again to find it
                    for ( int x = 0; x < 3; x++ )
                    {
                        for ( int y = 0; y < 3; y++ )
                        {
                            // Check the possible values for the key, if it is found then set the cells value to the key
                            if ( Puzzle[x + xOffset, y + yOffset].getPossibly().Contains( key ) )
                            {
                                // Debug output
                                if ( Verbose )
                                    SudokuSolverForm.Verbose_Add( "[SET][E-SQU] Setting a value at X: " + ( x + xOffset ) + " Y: " + ( y + yOffset ) + " with value: " + key + "\n" );

                                // Set the value
                                Puzzle[x + xOffset, y + yOffset].Value = key;

                                return Status.Value_Set;
                            }
                        }
                    }
                }
            }

            return Status.Evaluate;
        }

        /// <summary>
        /// Looks through each columns possibilities to see if there is a only one cell with a certain possibility
        /// </summary>
        /// <returns>Status. Evaluate if nothing is set, Value_Set if a value is set</returns>
        private Status evaluatePossibilitiesColumn()
        {
            for ( int x = 0; x < 9; x++ )
            {
                // Set up the counts for the square
                Dictionary<int, int> counts = new Dictionary<int, int>();
                for ( int i = 1; i < 10; i++ )
                {
                    counts.Add( i, 0 );
                }

                // Loop down through the column
                for ( int y = 0; y < 9; y++ )
                {
                    // Look through each cells possible value and add it to the total count
                    foreach ( int iter in Puzzle[x, y].getPossibly() )
                    {
                        counts[iter]++;
                    }
                }

                // Debug output
                if ( Verbose )
                {
                    SudokuSolverForm.Verbose_Add( "[E-COL] Counts for column #" + x + " are: [1, " + counts[1] + "] [2, " + counts[2] + "] [3, " + counts[3] + "] [4, " + counts[4] +
                        "] [5, " + counts[5] + "] [6, " + counts[6] + "] [7, " + counts[7] + "] [8, " + counts[8] + "] [9, " + counts[9] + "]\n" );
                }

                // Check to see if there is any key with a value of 1
                if ( counts.ContainsValue( 1 ) )
                {
                    int key = 0; // The value (key) that the cell should be set to

                    // iterate through the dictionary to find the correct key
                    for ( int i = 1; i < 10; i++ )
                    {
                        if ( counts[i] == 1 ) key = i;
                    }

                    // Loop down through the column
                    for ( int y = 0; y < 9; y++ )
                    {
                        // Check to see if the cell has the key as a possibility
                        if ( Puzzle[x, y].getPossibly().Contains( key ) )
                        {
                            // Debug output
                            if ( Verbose )
                                SudokuSolverForm.Verbose_Add( "[SET][E-COL] Setting a value at X: " + x + " Y: " + y + " with value: " + key + "\n" );

                            // Set the cells value to the key
                            Puzzle[x, y].Value = key;

                            return Status.Value_Set;
                        }
                    }
                }
            }

            return Status.Evaluate;
        }

        /// <summary>
        /// Looks through each rows possibilities to see if there is a only one cell with a certain possibility
        /// </summary>
        /// <returns>Status. Evaluate if nothing is set, Value_Set if a value is set</returns>
        private Status evaluatePossibilitiesRow()
        {
            // Loop down
            for ( int y = 0; y < 9; y++ )
            {
                // Set up the counts
                Dictionary<int, int> counts = new Dictionary<int, int>();
                for ( int i = 1; i < 10; i++ )
                {
                    counts.Add( i, 0 );
                }

                // Loop through the row
                for ( int x = 0; x < 9; x++ )
                {
                    // Look through each cells possible value and add it to the total count
                    foreach ( int iter in Puzzle[x, y].getPossibly() )
                    {
                        counts[iter]++;
                    }
                }

                // Debug output
                if ( Verbose )
                {
                    SudokuSolverForm.Verbose_Add( "[E-ROW] Counts for row #" + y + " are: [1, " + counts[1] + "] [2, " + counts[2] + "] [3, " + counts[3] + "] [4, " + counts[4] +
                        "] [5, " + counts[5] + "] [6, " + counts[6] + "] [7, " + counts[7] + "] [8, " + counts[8] + "] [9, " + counts[9] + "]\n" );
                }

                // Check to see if there is any key with a value of 1
                if ( counts.ContainsValue( 1 ) )
                {
                    int key = 0; // The value (key) that the cell should be set to

                    // iterate through the dictionary to find the correct key
                    for ( int i = 1; i < 10; i++ )
                    {
                        if ( counts[i] == 1 ) key = i;
                    }

                    // Loop down through the column
                    for ( int x = 0; x < 9; x++ )
                    {
                        // Check to see if the cell has the key as a possibility
                        if ( Puzzle[x, y].getPossibly().Contains( key ) )
                        {
                            // Debug output
                            if ( Verbose )
                                SudokuSolverForm.Verbose_Add( "[SET][E-ROW] Setting a value at X: " + x + " Y: " + y + " with value: " + key + "\n" );

                            Puzzle[x, y].Value = key; // Set the cells value to the key

                            return Status.Value_Set;
                        }
                    }
                }
            }

            return Status.Evaluate;
        }


        /// <summary>
        /// Finds the lowest amount of repeated possibilities (2) and picks one, attempting to solve it. If the answer comes out valid it must be true.
        /// </summary>
        /// <param name="puzzle">Puzzle to test</param>
        /// <returns>Status. Solved or Stuck</returns>
        private Status guess( SudokuCell[,] puzzle )
        {
            // These hold the position of the guessed cell and the value guessed
            int xPos = 0;
            int yPos = 0;
            List<int> possibilities = new List<int>();

            Boolean breakAll = false;

            // This is the threshold for allowed guess combos, gets bigger until it finds a suitable choice
            for ( int threshold = 2; threshold < 9; threshold++ )
            {
                for ( int x = 0; x < 9; x++ )
                {
                    for ( int y = 0; y < 9; y++ )
                    {
                        if ( puzzle[x, y].getPossibly().Count == threshold )
                        {
                            // Found a canidate, commence the guess
                            xPos = x;
                            yPos = y;
                            possibilities = puzzle[x, y].getPossibly();

                            if ( Verbose )
                            {
                                SudokuSolverForm.Verbose_Add( "[GUESS] Candidate found at X: " + xPos + " Y: " + yPos + " at a threshold of " + threshold +
                                    ". The possibilities are: " + puzzle[x, y].getPossiblyText() + "\n" );
                            }

                            // Break out to start guessing
                            breakAll = true;
                            break;
                        }
                    }

                    if ( breakAll ) break;
                }

                if ( breakAll ) break;
            }

            foreach ( int iter in possibilities )
            {
                SudokuCell[,] guessed = new SudokuCell[9, 9];

                // Perform a deep copy
                for ( int x = 0; x < 9; x++ )
                {
                    for ( int y = 0; y < 9; y++ )
                    {
                        guessed[x, y] = new SudokuCell( puzzle[x, y].Value );
                        guessed[x, y].setPossibly( puzzle[x, y].getPossibly() );
                    }
                }

                // Insert the guess
                guessed[xPos, yPos].Value = iter;
                String str = "[GUESS] Making a guess with X: " + xPos + " Y: " + yPos + " = " + iter + "..... ";

                GuessedPuzzles++;
                SudokuPuzzle guessPuzzle = new SudokuPuzzle( guessed );

                if ( guessPuzzle.Solved )
                {
                    if ( Verbose )
                        SudokuSolverForm.Verbose_Add( str + "Guess Succeeded, took " + GuessedPuzzles + " puzzles! Setting values...\n" );
                    Puzzle = guessPuzzle.Puzzle;
                    Solved = true;
                    return Status.Solved;
                }
                else if ( Verbose )
                {
                    SudokuSolverForm.Verbose_Add( "Guess Failed, moving on to next guess.\n" );
                }
            }

            SudokuSolverForm.Verbose_Add( "[GUESS] Failed to find a solution\n" );
            return Status.Stuck;
        }

        /// <summary>
        /// Checks a given puzzle for validity (No repeated numbers or zeros)
        /// </summary>
        /// <param name="puzzle">Puzzle to check</param>
        /// <returns>True if valid</returns>
        public static Boolean isValid( SudokuCell[,] puzzle )
        {
            // Check squares, columns, and rows
            return ( checkSquares( puzzle ) && checkColumns( puzzle ) && checkRows( puzzle ) );
        }

        /// <summary>
        /// Checks the given puzzles squares for validity (no repeated values or zeros)
        /// </summary>
        /// <param name="puzzle">Puzzle to check</param>
        /// <returns>True if valid</returns>
        private static Boolean checkSquares( SudokuCell[,] puzzle )
        {
            Boolean valid = true;

            // These values hold offsets that allow you to act as if the 9x9 array is made of multiple 3x3 arrays
            int xOffset = 0, yOffset = 0;

            // Iterate through each of the 9 smaller 3x3 arrays
            for ( int square = 0; square < 9; square++ )
            {
                // Based on wich smaller array it is evaluating, set the offsets to their respective values
                switch ( square )
                {
                    case 0:
                        xOffset = 0;
                        yOffset = 0;
                        break;
                    case 1:
                        xOffset = 3;
                        yOffset = 0;
                        break;
                    case 2:
                        xOffset = 6;
                        yOffset = 0;
                        break;
                    case 3:
                        xOffset = 0;
                        yOffset = 3;
                        break;
                    case 4:
                        xOffset = 3;
                        yOffset = 3;
                        break;
                    case 5:
                        xOffset = 6;
                        yOffset = 3;
                        break;
                    case 6:
                        xOffset = 0;
                        yOffset = 6;
                        break;
                    case 7:
                        xOffset = 3;
                        yOffset = 6;
                        break;
                    case 8:
                        xOffset = 6;
                        yOffset = 6;
                        break;
                    default:
                        break;
                }

                List<int> notFound = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                for ( int x = 0; x < 3; x++ )
                {
                    for ( int y = 0; y < 3; y++ )
                    {
                        notFound.Remove( puzzle[x + xOffset, y + yOffset].Value );
                    }
                }

                if ( notFound.Count != 0 ) valid = false;
            }

            return valid;
        }

        /// <summary>
        /// Checks the given puzzles columns for validity (no repeated values or zeros)
        /// </summary>
        /// <param name="puzzle">Puzzle to check</param>
        /// <returns>True if valid</returns>
        private static Boolean checkColumns( SudokuCell[,] puzzle )
        {
            Boolean valid = true;

            for ( int x = 0; x < 9; x++ )
            {
                List<int> notFound = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                for ( int y = 0; y < 9; y++ )
                {
                    notFound.Remove( puzzle[x, y].Value );
                }

                if ( notFound.Count != 0 ) valid = false;
            }

            return valid;
        }

        /// <summary>
        /// Checks the given puzzles rows for validity (no repeated values or zeros)
        /// </summary>
        /// <param name="puzzle">Puzzle to check</param>
        /// <returns>True if valid</returns>
        private static Boolean checkRows( SudokuCell[,] puzzle )
        {
            Boolean valid = true;

            for ( int y = 0; y < 9; y++ )
            {
                List<int> notFound = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                for ( int x = 0; x < 9; x++ )
                {
                    notFound.Remove( puzzle[x, y].Value );
                }

                if ( notFound.Count != 0 ) valid = false;
            }

            return valid;
        }
    }

    /// <summary>
    /// A cell for a sudoku puzzle. Keeps a list of possibilities and its current value. A value of 0 is considered to be empty.
    /// </summary>
    class SudokuCell
    {
        private List<int> Possibly = new List<int>();
        private int m_value = 0;
        public int Value { get { return m_value; } set { Possibly = new List<int>(); m_value = value; } } // Set the possibly to a new list so as to not cause errors later

        /// <summary>
        /// Default ctor that takes the cells current value
        /// </summary>
        /// <param name="value">The cells beginning value</param>
        public SudokuCell( int value )
        {
            Value = value;
        }

        /// <summary>
        /// Forms a String of the numbers in Possibly
        /// </summary>
        /// <returns>One line of text containing the list of possible values</returns>
        public String getPossiblyText()
        {
            String str = "";

            foreach ( int iter in Possibly )
            {
                str += iter + ", ";
            }

            return str;
        }

        /// <summary>
        /// Getter for the list of possibilities
        /// </summary>
        /// <returns>List of possible numbers</returns>
        public List<int> getPossibly()
        {
            return Possibly;
        }

        /// <summary>
        /// Setter for the list of possibilities
        /// </summary>
        /// <param name="list">List of possible numbers</param>
        public void setPossibly( List<int> list )
        {
            Possibly = new List<int>( list );

            // Automagically set its value (just in case)
            if ( Possibly.Count == 1 ) Value = Possibly[0];
        }
    }
}
