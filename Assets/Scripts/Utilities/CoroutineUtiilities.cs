using System.Collections;
using UnityEngine;

public class CoroutineUtiilities
{
    bool runningCoroutine;

    public CoroutineUtiilities(){ runningCoroutine = false;}
    
    // Define Coroutine
    public IEnumerator moveObjectOverTime(Transform objTransform, 
                                                    Vector3 initPos, 
                                                    Vector3 finalPos,
                                                    float durationSec)
    {
        runningCoroutine = true;

        float initialTime = Time.time;
        float progress = (Time.time - initialTime) / durationSec;

        // progress = 1 when duration is complete
        while (progress < 1)
        {
            progress = (Time.time - initialTime) / durationSec;
            // use Vector3.Lerp
            objTransform.position = Vector3.Lerp(initPos, finalPos, progress); 
            yield return null; // continue to next frame
        }

        // finish coroutine
        objTransform.position = finalPos;
        runningCoroutine = false;
    }

    public bool isRunningCoroutine(){return runningCoroutine;}
}
