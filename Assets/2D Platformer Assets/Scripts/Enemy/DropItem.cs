using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public static DropItem instance;

    [SerializeField] private GameObject healing, gem;
    [SerializeField] 
    

    

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update

   
}
