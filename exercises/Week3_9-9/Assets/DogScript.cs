using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogScript : MonoBehaviour
{
    GameObject playerObj;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.Find("Player");   
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerObj);
        transform.position += transform.forward * 2 * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
    }

}
