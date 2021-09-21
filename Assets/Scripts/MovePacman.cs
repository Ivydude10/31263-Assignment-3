using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePacman : MonoBehaviour
{

    private Tween tween;
    //private float duration = 1f;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        tween = new Tween(transform.position, new Vector2(-12, 13), Time.time, 2f);
        animator.SetTrigger("Left");
    }

    // Update is called once per frame
    void Update()
    {
        if(new Vector2(transform.position.x, transform.position.y) == tween.EndPos)
        {
            tween = null;
            detNewTween();
        }
        move();
    }

    private void move()
    {
        float timeFraction = (Time.time - tween.StartTime)/tween.Duration;
        transform.position = Vector2.Lerp(tween.StartPos, tween.EndPos, timeFraction);
    }

    private void detNewTween()
    {
        Vector2 currPos = new Vector2(transform.position.x, transform.position.y);
        if(currPos == new Vector2(-12, 13))
        {
            tween = new Tween(transform.position, new Vector2(-12, 9), Time.time, 4f);
            animator.SetTrigger("Down");
        }
        else if(currPos == new Vector2(-12, 9))
        {
            tween = new Tween(transform.position, new Vector2(-7, 9), Time.time, 5f);
            animator.SetTrigger("Right");
        }
        else if(currPos == new Vector2(-7, 9))
        {
            tween = new Tween(transform.position, new Vector2(-7, 13), Time.time, 4f);
            animator.SetTrigger("Up");
        }
        else if(currPos == new Vector2(-7, 13))
        {
            tween = new Tween(transform.position, new Vector2(-12, 13), Time.time, 5f);
            animator.SetTrigger("Left");
        }
    }
}
