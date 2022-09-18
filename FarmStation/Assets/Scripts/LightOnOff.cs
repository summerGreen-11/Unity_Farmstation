using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnOff : MonoBehaviour
{
    public GameObject light;

    // Start is called before the first frame update
    void Start()
    {
       
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {          
            light.SetActive(true);
            Debug.Log("light start");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {           
            light.SetActive(false);
            Debug.Log("light down");
        }
    }
}
