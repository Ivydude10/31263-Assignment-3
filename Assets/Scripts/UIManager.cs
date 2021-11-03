using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private AudioSource source;
    private Text score;
    private Text time;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        source = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(score!=null)
        {
            timer += Time.deltaTime;
            score.text = GameManager.score.ToString();
            time.text = CalcTimer();
        }
    }

    public void LoadLevelOne()
    {
        DontDestroyOnLoad(this);
        source.enabled = false;
        SceneManager.LoadScene(1);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            Button exit = GameObject.FindWithTag("ExitButton").GetComponent<Button>();
            score = GameObject.FindWithTag("Score").GetComponent<Text>();
            time = GameObject.FindWithTag("Time").GetComponent<Text>();
            exit.onClick.AddListener(ExitGame);
        }
    }

    public string CalcTimer()
    {
        int min = Mathf.FloorToInt(timer / 60f);
        int sec = Mathf.FloorToInt(timer % 60f);
        int millSec = Mathf.FloorToInt((timer * 100f) % 100f);
        return min.ToString("00") + ":" + sec.ToString("00") + ":" + millSec.ToString("00"); ;
    }
}
