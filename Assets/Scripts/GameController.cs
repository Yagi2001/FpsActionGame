using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public static Action firstMeetingWithGod;
    private float timeElapsed = 0;
    [SerializeField]
    private float secondsToMeetGod;

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= secondsToMeetGod)
            firstMeetingWithGod?.Invoke();
    }
}
