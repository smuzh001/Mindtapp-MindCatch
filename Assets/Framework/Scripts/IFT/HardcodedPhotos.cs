using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace MindTAPP.Unity.IFT
{
    [CreateAssetMenu(fileName = "HardcodedFollowers", menuName = "IFT/HardcodedFollowers")]
    public class HardcodedPhotos : IPhotos
    {
        [SerializeField]
        private List<Sprite> followerPhotos;

        public override void AddPhoto(Sprite photo, string fileName) { }

        public override ReadOnlyCollection<Sprite> GetPhotos()
        {
            return followerPhotos.AsReadOnly();
        }

        public override string DeletePhoto(Sprite photo)
        {
            return null;
        }
    }
}