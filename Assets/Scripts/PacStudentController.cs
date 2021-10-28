using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    public Animator animator;
    public AudioSource steps;
    public ParticleSystem dust;
    private Tween tween = null;
    private float duration = 1f;
    private string lastInput;
    private string currentInput;
    private int currX = 1, currY = 1;

    int[,] levelMap = 
    { 
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7,7,2,2,2,2,2,2,2,2,2,2,2,2,1}, 
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4,4,5,5,5,5,5,5,5,5,5,5,5,5,2}, 
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4,4,5,3,4,4,4,3,5,3,4,4,3,5,2},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4,4,5,4,0,0,0,4,5,4,0,0,4,6,2},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3,3,5,3,4,4,4,3,5,3,4,4,3,5,2},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,2},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4,4,4,4,3,5,3,3,5,3,4,4,3,5,2},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3,3,4,4,3,5,4,4,5,3,4,4,3,5,2},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,5,5,2},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4,4,0,3,4,4,3,4,5,1,2,2,2,2,1},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3,3,0,3,4,4,3,4,5,2,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0,0,0,0,0,0,4,4,5,2,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0,0,4,4,3,0,4,4,5,2,0,0,0,0,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0,0,0,0,4,0,3,3,5,1,2,2,2,2,2},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0,0,0,0,4,0,0,0,5,0,0,0,0,0,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0,0,0,0,4,0,3,3,5,1,2,2,2,2,2},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0,0,4,4,3,0,4,4,5,2,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0,0,0,0,0,0,4,4,5,2,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3,3,0,3,4,4,3,4,5,2,0,0,0,0,0},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4,4,0,3,4,4,3,4,5,1,2,2,2,2,1},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,5,5,2},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3,3,4,4,3,5,4,4,5,3,4,4,3,5,2},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4,4,4,4,3,5,3,3,5,3,4,4,3,5,2},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,2},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3,3,5,3,4,4,4,3,5,3,4,4,3,5,2},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4,4,5,4,0,0,0,4,5,4,0,0,4,6,2},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4,4,5,3,4,4,4,3,5,3,4,4,3,5,2},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4,4,5,5,5,5,5,5,5,5,5,5,5,5,2},
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7,7,2,2,2,2,2,2,2,2,2,2,2,2,1},
    };

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tween != null)
            CheckTween();
        GetMovementInput();
        if (tween == null)
            SetMove();
        MovePacStudent();
        WalkingAnimation();
    }

    void CheckTween()
    {
        if (new Vector2(transform.position.x, transform.position.y) == tween.EndPos)
        {
            tween = null;
        }
    }

        void GetMovementInput()
    {
        if(Input.GetKeyDown(KeyCode.W))
            lastInput = "W";
        else if (Input.GetKeyDown(KeyCode.A))
            lastInput = "A";
        else if (Input.GetKeyDown(KeyCode.S))
            lastInput = "S";
        else if (Input.GetKeyDown(KeyCode.D))
            lastInput = "D";
    }

    void SetMove()
    {
        if(CheckValid(lastInput))
            currentInput = lastInput;
        if (!CheckValid(currentInput))
            currentInput = null;
        if (!steps.isPlaying)
        {
            steps.Play();
            dust.Play();
        }
        switch (currentInput)
        {
            case "W":
                tween = new Tween(transform.position, new Vector2(transform.position.x, transform.position.y + 1), Time.time, duration);
                currX -= 1;
                break;
            case "A":
                tween = new Tween(transform.position, new Vector2(transform.position.x - 1, transform.position.y), Time.time, duration);
                currY -= 1;
                break;
            case "S":
                tween = new Tween(transform.position, new Vector2(transform.position.x, transform.position.y - 1), Time.time, duration);
                currX += 1;
                break;
            case "D":
                tween = new Tween(transform.position, new Vector2(transform.position.x + 1, transform.position.y), Time.time, duration);
                currY += 1;
                break;
            default:
                tween = null;
                break;
        }
    }

    void MovePacStudent()
    {
        if (tween != null)
        {
            float timeFraction = (Time.time - tween.StartTime) / tween.Duration;
            transform.position = Vector2.Lerp(tween.StartPos, tween.EndPos, timeFraction);
        }
    }

    void WalkingAnimation()
    {
        animator.ResetTrigger("Up");
        animator.ResetTrigger("Left");
        animator.ResetTrigger("Down");
        animator.ResetTrigger("Right");
        animator.SetFloat("Speed", 1.0f);
        switch (currentInput)
        {
            case "W":
                animator.SetTrigger("Up"); 
                break;
            case "A":
                animator.SetTrigger("Left"); 
                break;
            case "S":
                animator.SetTrigger("Down"); 
                break;
            case "D":
                animator.SetTrigger("Right"); 
                break;
            default:
                animator.SetFloat("Speed", 0.0f);
                steps.Stop();
                dust.Stop();
                break;
        }
    }

    bool CheckValid(string input)
    {
        switch (input)
        {
            case "W":
                return (levelMap[currX - 1, currY] == 0 || levelMap[currX - 1, currY] == 5 || levelMap[currX - 1, currY] == 6);
            case "A":
                return (levelMap[currX, currY - 1] == 0 || levelMap[currX, currY - 1] == 5 || levelMap[currX, currY - 1] == 6);
            case "S":
                return (levelMap[currX + 1, currY] == 0 || levelMap[currX + 1, currY] == 5 || levelMap[currX + 1, currY] == 6);
            case "D":
                return (levelMap[currX, currY + 1] == 0 || levelMap[currX, currY + 1] == 5 || levelMap[currX, currY + 1] == 6);
            default:
                return false;
        }
    }
}
