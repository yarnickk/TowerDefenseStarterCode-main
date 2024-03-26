using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;

public class HighScoreManager : MonoBehaviour
{
    // Singleton instance
    private static HighScoreManager _instance;
    public static HighScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<HighScoreManager>();

                if (_instance == null)
                {
                    GameObject managerObject = new GameObject("HighScoreManager");
                    _instance = managerObject.AddComponent<HighScoreManager>();
                }
            }
            return _instance;
        }
    }

    // Properties
    private string _playerName;
    public string PlayerName
    {
        get { return _playerName; }
        set { _playerName = value; }
    }

    private bool _gameIsWon;
    public bool GameIsWon
    {
        get { return _gameIsWon; }
        set { _gameIsWon = value; }
    }

    // HighScore class
    public class HighScore
    {
        public string Name { get; set; }
        public int Score { get; set; }
    }

    // List of high scores
    public List<HighScore> HighScores = new List<HighScore>();

    // File path for saving high scores
    private string filePath;

    private void Awake()
    {
        // Ensure singleton instance
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Set file path
        filePath = Application.persistentDataPath + "/highscores.json";

        // Load high scores from file if it exists
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            HighScores = JsonUtility.FromJson<List<HighScore>>(json);
        }
    }

    public void AddHighScore(int score)
    {
        // Check if the score is higher than at least 1 score in the list
        if (HighScores.Any(highScore => score > highScore.Score))
        {
            // Add new high score
            HighScores.Add(new HighScore { Name = PlayerName, Score = score });

            // Sort the list by score (descending)
            HighScores = HighScores.OrderByDescending(highScore => highScore.Score).ToList();

            // Keep only top 5 scores
            if (HighScores.Count > 5)
            {
                HighScores.RemoveAt(5);
            }

            // Serialize list to JSON and save to file
            string json = JsonUtility.ToJson(HighScores);
            File.WriteAllText(filePath, json);
        }
    }
}
