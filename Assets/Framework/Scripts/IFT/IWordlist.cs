/*
 * Author(s): Joshua Beto
 * Company: MindTAPP
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace MindTAPP.Unity.IFT
{
    // Data Access Layer
    // Handles access of IFT words or other word lists
    public abstract class IWordlist : ScriptableObject
    {
        public abstract ReadOnlyCollection<string> GetWords();
        public abstract string GetJson();
    }
}