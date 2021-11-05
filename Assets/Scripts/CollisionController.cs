using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{

    private const int totalPellets = 218;
    private int currPellets = 0;

    void OnTriggerEnter(Collider other)
    {
        switch(other.name)
        {
            case "Pellet":
                currPellets += 1;
                PacStudentController.Instance.EatSound();
                EatItem(other);
                if (currPellets == totalPellets)
                    GameOver();
                break;
            case "Power Pellet":
            case "Cherry":
                EatItem(other);
                break;

        }
    }

    void EatItem(Collider other)
    {
        GameManager.Instance.EatItem(other.name);
        Destroy(other.gameObject);
    }

    void GameOver()
    {
        SaveManager.SaveScore(UIManager.Instance.score.text, UIManager.Instance.time.text);
        GameManager.Instance.GameOver();
    }
}
