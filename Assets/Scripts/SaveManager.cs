using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    const string scoreKey = "High Score";
    const string timeKey = "Best Time";
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(scoreKey))
            LoadScore();
    }

    public static void SaveScore(string score, string time)
    {
        if (PlayerPrefs.HasKey(scoreKey))
        {
            string currScore = PlayerPrefs.GetString(scoreKey);
            if(int.Parse(currScore) < int.Parse(score))
            {
                PlayerPrefs.SetString(scoreKey, score);
                PlayerPrefs.SetString(timeKey, time);
                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetString(scoreKey, score);
            PlayerPrefs.SetString(timeKey, time);
            PlayerPrefs.Save();
        }
    }

    public void LoadScore()
    {
        UIManager.Instance.score.text = PlayerPrefs.GetString(scoreKey);
        UIManager.Instance.time.text = PlayerPrefs.GetString(timeKey);
    }
}
