using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;

namespace MindTAPP.Unity.Utilities
{
    public class GoUtility : MonoBehaviour
    {
        public static T GetSafeComponent<T>(GameObject go)
        {
            T component = go.GetComponent<T>();
            if (component == null)
            {
                Debug.Log("Component does not exist.");
            }
            return component;
        }
    }
}