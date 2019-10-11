using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureScript : MonoBehaviour
{
    public int x = -1;
    public int y = -1;

    // Update is called once per frame
    void Update()
    {
        //Time - the interface to get time info from Unity 
        //Time.deltaTime - the completion time in seconds since the last frame
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}

