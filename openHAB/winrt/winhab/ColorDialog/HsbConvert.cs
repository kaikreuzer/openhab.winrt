using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openhab.winrt.winhab.ColorDialog
{
    /// <summary>
    /// A Class to convert RGB Values to HSB Values
    /// </summary>
    class HsbConverter
    {
        private Int32 r { get; set; }
        private Int32 g { get; set; }
        private Int32 b { get; set; }
        private Int32 m { get; set; }
        private Int32 M { get; set; }
        private Int32 H { get; set; }
        private Int32 B { get; set; }
        private Int32 S { get; set; }
        private Int32[] result = new Int32[3];
        /// <summary>
        /// Converts RGB to HSB
        /// </summary>
        /// <param name="r">R-Value</param>
        /// <param name="g">G-Value</param>
        /// <param name="b">B-Value</param>
        /// <returns>Returns an Arry with [0]=H [1]=S [2]=B</returns>
        public Int32[] convertToHsb(Int32 r, Int32 g, Int32 b)
        {
            M = max(r, g, b);
            m = min(r, g, b);
            if (M - m == 0)
            {
                throw new ArgumentException("Wrong Values in HsbConverter", "R: " + Convert.ToString(r) + " G: " + Convert.ToString(g) + " B: " + Convert.ToString(b));
            }
            if (M == r)
            {
                H = (60 * ((g - b) / (M - m))) % 360;
            }
            if (M == g)
            {
                H = 60 * (b - r) / (M - m) + 120;
            }
            if (M == b)
            {
                H = 60 * ((r - g) / (M - m)) + 240;
            }
            B = M;
            S = (M - m) / b;
            result[0] = H;
            result[1] = S;
            result[2] = B;
            return result;
        }
        /// <summary>
        /// Returns the minimum of three Values
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns>The min Value in Int32</returns>
        private Int32 min(Int32 r, Int32 g, Int32 b)
        {
            Int32 min = r;
            if (min > g)
            {
                min = g;
            }
            if (min > b)
            {
                min = b;
            }
            return min;
        }
        /// <summary>
        /// Returns the Maximum of three Values
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns>The max Value in Int32</returns>
        private Int32 max(Int32 r, Int32 g, Int32 b)
        {
            int max = r;
            if (max < g)
            {
                max = g;
            }
            if (max < b)
            {
                max = b;
            }
            return max;
        }
    }
}
