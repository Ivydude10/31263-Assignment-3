using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int score;
    public static bool inputEnabled = false;
    private static bool isPowered = false;
    public Text countdown;
    public AudioSource bgMusic;
    public AudioClip[] musicTracks;
    private static float time;
    // Start is called before the first frame update

    void Awake()
    {
        Instance = this;
        StartCoroutine(Countdown());
    }
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPowered)
            PowerUp();
    }

    public void EatItem(string name)
    {
        if(name == "Pellet")
        {
            score += 10;
        }
        if(name == "Cherry")
        {
            score += 100;
        }
        if(name == "Power Pellet")
        {
            time = 10;
            PowerUp();
            isPowered = true;
        }

    }

    IEnumerator Countdown()
    {
        countdown.text = "3";
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "2";
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "1";
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "GO!";
        yield return new WaitForSecondsRealtime(1);
        countdown.enabled = false;
        Time.timeScale = 1.0f;
        inputEnabled = true;
        bgMusic.Play();
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        countdown.enabled = true;
        bgMusic.Stop();
        PacStudentController.Instance.GameOver();
        Time.timeScale = 0.0f;
        countdown.text = "Game Over";
        yield return new WaitForSecondsRealtime(3); 
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    void PowerUp()
    {
        if (time == 10)
        {
            UIManager.StartGhost();
            ScaredMusic();
        }
        UIManager.GhostTime(TimeFormat(time));
        time -= Time.deltaTime;
        if (time <= 0)
        {
            UIManager.EndGhost();
            isPowered = false;
            NormalMusic();
        }
    }

    static string TimeFormat(float timer)
    {
        int min = Mathf.FloorToInt(timer / 60f);
        int sec = Mathf.FloorToInt(timer % 60f);
        int millSec = Mathf.FloorToInt((timer * 100f) % 100f);
        return min.ToString("00") + ":" + sec.ToString("00") + ":" + millSec.ToString("00");
    }

    public void NormalMusic()
    {
        bgMusic.clip = musicTracks[0];
        bgMusic.Play();
    }

    public void ScaredMusic()
    {
        bgMusic.clip = musicTracks[1];
        bgMusic.Play();
    }

    public void DeadMusic()
    {
        bgMusic.clip = musicTracks[2];
        bgMusic.Play();
    }

    public void GameOver()
    {
        StartCoroutine(EndGame());
    }
}
