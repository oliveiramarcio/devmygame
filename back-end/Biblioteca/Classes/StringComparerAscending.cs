using System;
using System.Collections.Generic;

namespace Biblioteca.Classes
{
    public class StringComparerAscending : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return String.Compare(x, y);
        }
    }
}