using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Helpers
{
    public class DonutChartPiece
    {
        public string Title { get; set; }
        // In %
        public int Value { get; set; }

        public DonutChartPiece(string title, int value)
        {
            Title = title;
            Value = value; 
        }
    }
}
