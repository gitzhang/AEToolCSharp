using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GDBProcessWin
{
    class Utils
    {

        public static ArrayList ConvertEnumerlatorToArrayList(IEnumerator enumerlator)
        {
            ArrayList array = new ArrayList();

            if (enumerlator != null)
            {
                while (enumerlator.MoveNext())
                {
                    array.Add(enumerlator.Current);
                }
            }
            return array;

        }

    }
}
