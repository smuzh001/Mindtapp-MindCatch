using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using GameAnalyticsSDK;
namespace MindTAPP.Unity.Framework
{
    [CreateAssetMenu(fileName = "Highscores", menuName = "Framework/Highscores")]
    public class Scoreboard : ScriptableObject
    {
        [SerializeField] private int numberOfScores;
        private List<int> scores;

        private void OnEnable()
        {
            string path = Path.Combine(Application.persistentDataPath, "Scores");
            if (File.Exists(path))
            {
                scores = File.ReadAllLines(path).Select(s => int.Parse(s)).ToList();
            }
            else
            {
                File.Create(path);
                scores = new List<int>(numberOfScores);
            }
        }

        private void OnDisable()
        {
            scores.Sort(delegate (int a, int b) { return b - a; });
            File.WriteAllLines(Path.Combine(Application.persistentDataPath, "Scores"), scores.Select(x => x.ToString()).ToArray());
        }

        public ReadOnlyCollection<int> GetScores()
        {
            scores.Sort(delegate (int a, int b) { return b - a; });
            return scores.AsReadOnly();
        }

        public void RecordScore(int score)
        {
            GameAnalytics.NewDesignEvent("Score", score);

            if (scores.Count < numberOfScores)
            {
                scores.Add(score);
            }
            else
            {
                int worstScoreIndex = numberOfScores - 1;
                if (scores[worstScoreIndex] < score)
                {
                    scores[worstScoreIndex] = score;
                    scores.Sort(delegate (int a, int b) { return b - a; });
                }
            }
        }

        public void DeleteScore(int index)
        {
            scores.RemoveAt(index);
            scores.Sort(delegate (int a, int b) { return b - a; });
        }

        public void ResetScores()
        {
            scores.Clear();
        }
    }
}