using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MindTAPP.Unity.Framework
{
    public class Dissapear : MonoBehaviour
    {
        private void OnEnable()
        {
            StartCoroutine(DisableAfterSeconds(3f));
        }

        private IEnumerator DisableAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            gameObject.SetActive(false);
        }
    }
}