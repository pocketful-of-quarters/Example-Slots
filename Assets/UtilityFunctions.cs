using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class Functions : MonoBehaviour
    {

        public int ToInt(long? value)
        {
            if (value.HasValue)
                return int.Parse(string.Format("{0}", value));

            return 0; //the input value was null
        }
    }
}