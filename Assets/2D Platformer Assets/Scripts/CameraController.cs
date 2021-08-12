using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject far;
    [SerializeField] private GameObject middle;
    [SerializeField] private float minHeight, maxHeight;
    
    private float lastPosX, lastPosY;
    
    // Start is called before the first frame update
    void Start()
    {
        //�����_�l�I
        lastPosX = transform.position.x;
        lastPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //�]�w�e���d��
        float clampedY = Mathf.Clamp(player.transform.position.y, minHeight, maxHeight);
        transform.position = new Vector3(player.transform.position.x, clampedY , transform.position.z);

        BackGroundMoving();
    }

    void BackGroundMoving()
    {
        float posXChange = transform.position.x - lastPosX;
        float posYChange = transform.position.y - lastPosY;
        far.transform.position += new Vector3(posXChange,0,0);
        middle.transform.position += new Vector3(posXChange / 2, posYChange/3,0);
        //��s���ʫe��X��m����
        lastPosX = transform.position.x;
        lastPosY = transform.position.y;
    }
}
