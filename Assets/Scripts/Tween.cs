using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween
{
    //public Transform Target { get; private set; }
    public Vector2 StartPos { get; private set; }
    public Vector2 EndPos { get; private set; }
    public float StartTime { get; private set; }
    public float Duration { get; private set; }

    public Tween(Vector2 startPos, Vector2 endPos, float startTime, float duration)
    {
        //Target = transform;
        StartPos = startPos;
        EndPos = endPos;
        StartTime = startTime;
        Duration = duration;
    }
}
