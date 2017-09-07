namespace MasujimaRyohei
{
    using UnityEngine;
    using System.Collections;

    public class Math
    {
        #region Variables
        #endregion


        /// <summary>
        /// Check whether B is the almost same as A./// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="dec"> -1 is the same as "1.0f", </param>
        /// <returns></returns>
        public static bool NearyEqual(float a, float b, int dec = -1)
        {
            float decLine = 1;
            if (dec >= 0)
                for (int i = 1; i <= dec; i++)
                    decLine *= 10;
            else
                for (int i = 0; i < System.Math.Abs(dec); i++)
                    decLine /= 10;

            float fa = (float)System.Math.Floor(a * decLine) / decLine;
            float fb = (float)System.Math.Floor(b * decLine) / decLine;

            return (fa == fb) ? true : false;
        }
    }
}