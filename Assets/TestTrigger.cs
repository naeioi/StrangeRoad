using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("TestTrigger");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
