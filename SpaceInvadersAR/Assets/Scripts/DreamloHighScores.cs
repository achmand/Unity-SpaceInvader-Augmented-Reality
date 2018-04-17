using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public struct Highscore
    {
        public string TeamName;
        public int Score;

        public Highscore(string teamName, int score)
        {
            TeamName = teamName;
            Score = score;
        }
    }

    public class DreamloHighScores : MonoBehaviour
    {
        private const string privateCode = "W3rw8WiMHUejFzJO1EJkfAeCO4STN4rEOYRFFIeJyfrQ";
        private const string publicCode = "5ad55e99d6024519e0ca4b0e";
        private const string webUrl = "http://dreamlo.com/lb/";

        private const string slash = "/";
        private const string add = slash + "add" + slash;
        private const string pipe = slash + "pipe" + slash;

        private const int topHighScoreCount = 100;

        public Highscore[] topHighscores;

        void Awake()
        {
            GetTopHighScores(topHighScoreCount);
        }

        public void AddHighScore(string teamName, int highScore)
        {
            StartCoroutine(UploadHighScore(teamName, highScore));
        }

        public void GetTopHighScores(int top)
        {
            StartCoroutine(GetHighScores(top));
        }

        private static IEnumerator UploadHighScore(string teamName, int highScore)
        {
            var request = new WWW(webUrl + privateCode + add + WWW.EscapeURL(teamName) + slash + highScore);
            yield return request;

            if (string.IsNullOrEmpty(request.error))
            {
                print("Highscore upload successful.");
            }
            else
            {
                print("Error uploading highscore: " + request.error);
            }
        }

        private IEnumerator GetHighScores(int top)
        {
            var request = new WWW(webUrl + publicCode + pipe + slash + "0" + slash + top);
            yield return request;

            if (string.IsNullOrEmpty(request.error))
            {
                ParseHighScores(request.text);
            }
            else
            {
                print("Error fetching highscore: " + request.error);
            }
        }

        private void ParseHighScores(string textStream)
        {
            var entries = textStream.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            topHighscores = new Highscore[entries.Length];

            for (int i = 0; i < entries.Length; i++)
            {
                var entryInfo = entries[i].Split('|');
                var teamName = entryInfo[0];
                var score = int.Parse(entryInfo[1]);

                topHighscores[i] = new Highscore(teamName, score);
                
                //print(topHighscores[i].TeamName + ": " + topHighscores[i].Score);
            }
        }

    }
}
