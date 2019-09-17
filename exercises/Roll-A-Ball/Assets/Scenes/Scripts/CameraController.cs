using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //GameObject for all entities in Unity Scenes
    public GameObject player;
    private Vector3 offset; 

    //Use this for initialization, start is called before the first frame update
    void Start()
    {
        //tranform - position, rotation, and scale of the object 
        offset = transform.position - player.transform.position; 
    }

    //Runs every frame, guaranteed to run after all items have been processed in Update()
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
