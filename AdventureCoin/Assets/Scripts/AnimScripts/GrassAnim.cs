using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassAnim : MonoBehaviour
{
    // Start is called before the first frame update
    public Color newColorGrass;
    public float timeColor;
    void Start()
    {
        Vector3 position = transform.position;
        float randomTimeColor = Random.Range(timeColor - 0.5f,timeColor + 0.5f);

        iTween.ColorTo(gameObject, iTween.Hash(
            "color", newColorGrass,
            "time", randomTimeColor,
            "looptype", iTween.LoopType.pingPong 
        ));

    }
}
