using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class BigSquare : UserControl
    {
        private readonly List<NumericUpDown> UpDownList = new List<NumericUpDown>();

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
        }

        public int this[int index]
        {
            get { return (int) this.UpDownList[index].Value; }
        }

        public void clear()
        {
            UpDownList.ForEach( reset );
        }

        private static void reset( NumericUpDown num )
        {
            num.Value = 0;
        }

        public void setValues(List<int> values)
        {
            for ( int i = 0; i < 9; i++ )
            {
                UpDownList[i].Value = values[i];
            }
        }
    }
}
