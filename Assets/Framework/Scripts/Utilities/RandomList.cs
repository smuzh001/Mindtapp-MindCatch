using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace MindTAPP.Unity.Utilities
{
    public class RandomList<T>
    {
        private List<T> randomList;
        private int currentIndex;
        public int RemainingUnique { get { return randomList.Count - currentIndex; } }

        public RandomList(IEnumerable<T> list)
        {
            Assert.IsNotNull(list);
            randomList = new List<T>(list);
            ShuffleUtility.Shuffle(randomList);
            currentIndex = 0;
        }
        public int Size()
        {
            return randomList.Count;
        }
        public T GetRandomUnique(T defaultValue = default(T))
        {
            if (randomList.Count == currentIndex)
            {
                return defaultValue;
            }
            T uniqueRandomElement = randomList[currentIndex];
            currentIndex++;
            return uniqueRandomElement;
        }

        public void ResetUniqueSet()
        {
            ShuffleUtility.Shuffle(randomList);
            currentIndex = 0;
        }

        public T GetRandom()
        {
            return randomList[Random.Range(0, randomList.Count)];
        }
    }
}