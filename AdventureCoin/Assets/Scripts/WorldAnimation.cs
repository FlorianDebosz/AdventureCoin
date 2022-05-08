using UnityEngine;

public class WorldAnimation : MonoBehaviour
{
    public Vector3 rotationCoin;
    void Update()
    {
        transform.Rotate(rotationCoin * Time.deltaTime);
    }
}
