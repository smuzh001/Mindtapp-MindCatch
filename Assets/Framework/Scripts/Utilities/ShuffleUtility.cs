using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MindTAPP.Unity.Utilities
{
    public static class ShuffleUtility
    {
        public static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                int x = Random.Range(0, n);
                n--;
                T temp = list[x];
                list[x] = list[n];
                list[n] = temp;
            }
        }

        public static void ShuffleChildren(Transform parent)
        {
            int n = parent.childCount;
            while (n > 1)
            {
                int x = Random.Range(0, n);
                parent.GetChild(x).SetAsLastSibling();
                n--;
            }
        }
    }
}