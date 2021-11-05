using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private AudioSource source;
    public Text score;
    public Text time;
    private static Text scared;
    private static Text scaredTime;
    private float timer;
    private int index;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        index = SceneManager.GetActiveScene().buildIndex;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(score!=null && index == 1)
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
        Time.timeScale = 0.0f;
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
            scared = GameObject.FindWithTag("Scared Ghost").GetComponent<Text>();
            scaredTime = GameObject.FindWithTag("Scared Timer").GetComponent<Text>();
            scared.enabled = false;
            scaredTime.enabled = false;
            index = 1;
            exit.onClick.AddListener(ExitGame);
        }
    }

    public string CalcTimer()
    {
        int min = Mathf.FloorToInt(timer / 60f);
        int sec = Mathf.FloorToInt(timer % 60f);
        int millSec = Mathf.FloorToInt((timer * 100f) % 100f);
        return min.ToString("00") + ":" + sec.ToString("00") + ":" + millSec.ToString("00");
    }

    public static void StartGhost()
    {
        scared.enabled = true;
        scaredTime.enabled = true;
    } 
    
    public static void EndGhost()
    {
        scared.enabled = false;
        scaredTime.enabled = false;
    }

    public static void GhostTime(string time)
    {
        scaredTime.text = time;
    }
}
