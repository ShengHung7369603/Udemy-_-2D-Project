using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    [SerializeField] private float destroyTime;

    
    private void Update()
    {
        Destroy(gameObject, destroyTime);
    }
}
