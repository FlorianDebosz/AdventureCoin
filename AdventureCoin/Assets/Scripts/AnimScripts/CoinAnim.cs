using UnityEngine;

public class CoinAnim : MonoBehaviour
{
    public Vector3 rotationCoin;
    void Update()
    {
        transform.Rotate(rotationCoin * Time.deltaTime);
    }
}
