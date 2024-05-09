using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] float maxTime;

    void Update()
    {
        Destroy(gameObject, maxTime);
    }
}
