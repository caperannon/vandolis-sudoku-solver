using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SudokuSolver
{
    /// <summary>
    /// A big 3x3 square used for Sudoku
    /// </summary>
    public partial class BigSquare : UserControl
    {
        private readonly List<NumericUpDown> UpDownList = new List<NumericUpDown>();
        private List<NumericUpDown> CoreValues { get; set; }

        /// <summary>
        /// Default ctor
        /// </summary>
        public BigSquare()
        {
            InitializeComponent();

            this.UpDownList.Add( this.numericUpDown1 );
            this.UpDownList.Add( this.numericUpDown2 );
            this.UpDownList.Add( this.numericUpDown3 );
            this.UpDownList.Add( this.numericUpDown4 );
            this.UpDownList.Add( this.numericUpDown5 );
            this.UpDownList.Add( this.numericUpDown6 );
            this.UpDownList.Add( this.numericUpDown7 );
            this.UpDownList.Add( this.numericUpDown8 );
            this.UpDownList.Add( this.numericUpDown9 );

            CoreValues = new List<NumericUpDown>();
        }

        /// <summary>
        /// Index of the values
        /// </summary>
        /// <param name="index">Index of the NumericUpDown value to get. (0-8)</param>
        /// <returns>Value of the index</returns>
        public int this[int index]
        {
            get { return (int) this.UpDownList[index].Value; }
        }

        /// <summary>
        /// Returns only the locked values. If a UpDown is not locked it returns 0
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int lockedValues( int index )
        {
            if ( UpDownList[index].Enabled )
                return 0;
            else
                return (int) UpDownList[index].Value;
        }

        /// <summary>
        /// Clears all of the data, returning it to a new state
        /// </summary>
        public void clear()
        {
            // Reset all of the cells
            UpDownList.ForEach( reset );

            CoreValues = new List<NumericUpDown>();
        }

        /// <summary>
        /// Resets the NumericUpDowns value, background, and enabled
        /// </summary>
        /// <param name="num">NumericUpDown to reset</param>
        private static void reset( NumericUpDown num )
        {
            num.Value = 0;
            num.BackColor = Color.White;
            num.Enabled = true;
        }

        /// <summary>
        /// Sets all of the enabled NumericUpDowns to the given value in the list
        /// </summary>
        /// <param name="values">List of values to set</param>
        public void setValues( List<int> values )
        {
            for ( int i = 0; i < 9; i++ )
            {
                UpDownList[i].Value = values[i];
            }
        }

        /// <summary>
        /// Checks each of the NumericUpDowns values against the given list of values and colors their backgrounds around that
        /// </summary>
        /// <param name="values">Values expected</param>
        public void checkValues( List<int> values )
        {
            for ( int i = 0; i < 9; i++ )
            {
                if ( UpDownList[i].Value == values[i] && UpDownList[i].Enabled )
                {
                    UpDownList[i].BackColor = Color.LightGreen;
                }
                else if ( UpDownList[i].Value != values[i] )
                {
                    UpDownList[i].BackColor = Color.FromArgb(255, 128, 128);
                }
            }
        }

        /// <summary>
        /// If a NumericUpDowns value is changed then reset its background color to white
        /// </summary>
        /// <param name="sender">NumericUpDown performing the event</param>
        /// <param name="e">EventArgs</param>
        private void numericUpDown_ValueChanged( object sender, EventArgs e )
        {
            foreach ( NumericUpDown iter in UpDownList )
            {
                if ( iter.Equals( (NumericUpDown) sender ) )
                {
                    ( (NumericUpDown) sender ).BackColor = Color.White;
                }
            }
        }

        /// <summary>
        /// Sets the core values with the non-zero values, saves them to CoreValues, and then locks them
        /// </summary>
        internal void lockValues()
        {
            if ( CoreValues.Count == 0 )
            {
                foreach ( NumericUpDown iter in UpDownList )
                {
                    if ( iter.Value != 0 )
                    {
                        iter.BackColor = Color.LightYellow;
                        iter.Enabled = false;
                        CoreValues.Add( iter );
                    }
                }
            }
            else
            {
                foreach ( NumericUpDown iter in CoreValues )
                {
                    if ( iter.Enabled )
                    {
                        iter.BackColor = Color.LightYellow;
                        iter.Enabled = false;
                    }
                }
            }


        }

        /// <summary>
        /// Iterates through the core values and unlocks each of them
        /// </summary>
        internal void unlockValues()
        {
            foreach ( NumericUpDown iter in CoreValues )
            {
                iter.BackColor = Color.White;
                iter.Enabled = true;
            }
        }

        private void numericUpDown_Enter( object sender, EventArgs e )
        {
            ( (NumericUpDown) sender ).Select( 0, 1 );
        }
    }
}
