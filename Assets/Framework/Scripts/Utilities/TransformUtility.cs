using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MindTAPP.Unity.Utilities
{
    public static class TransformUtility
    {
        public static void DestroyChildren(Transform parent)
        {
            foreach (Transform child in parent)
            {
                MonoBehaviour.Destroy(child.gameObject);
            }
        }

        public static IEnumerable<Transform> PushChildren(Transform from, Transform to)
        {
            if (from != to)
            {
                while (from.childCount > 0)
                {
                    Transform child = from.GetChild(from.childCount - 1);
                    child.SetParent(to);
                    yield return child;
                }
            }
        }
    }
}