using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnim : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeScale;
    public Vector3 amount;
    void Start()
    {
        float randomTimeScale = Random.Range(timeScale - 1, timeScale + 1);
        iTween.PunchScale(gameObject, iTween.Hash(
            "amount", amount,
            "time", randomTimeScale,
            "looptype", iTween.LoopType.loop
        ));        
    }

}
