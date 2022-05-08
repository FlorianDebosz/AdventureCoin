using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerAnim : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeRotation;
    public Vector3 rotationAmountZ;

    void Start()
    {
        float randomTimeScaleZ = Random.Range(timeRotation - 1, timeRotation + 1);
        float timeScaleX = randomTimeScaleZ;
        iTween.PunchRotation(gameObject, iTween.Hash(
            "amount", rotationAmountZ,
            "time", randomTimeScaleZ,
            "looptype", iTween.LoopType.loop
        ));
    }
}
