using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int score;
    public static bool inputEnabled = false;
    public Text countdown;
    // Start is called before the first frame update
        StartCoroutine(Countdown());
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void EatItem(string name)
    {
        if(name == "Pellet")
        {
            score += 10;
        }
        if(name == "Cherry")
        {
            score += 100;
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

}
