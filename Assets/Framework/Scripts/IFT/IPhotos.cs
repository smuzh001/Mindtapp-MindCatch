/*
* Author(s): Joshua Beto
* Company: MindTAPP
*/

using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace MindTAPP.Unity.IFT
{
    public abstract class IPhotos : ScriptableObject
    {
        public abstract ReadOnlyCollection<Sprite> GetPhotos();
        public abstract void AddPhoto(Sprite photo, string fileName);
        public abstract string DeletePhoto(Sprite photo);
    }
}