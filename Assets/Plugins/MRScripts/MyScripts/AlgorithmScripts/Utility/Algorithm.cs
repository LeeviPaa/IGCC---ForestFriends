namespace MasujimaRyohei
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    partial class Algorithm
    {
        public static void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            b = a;
            a = c;
        }

    }
}