using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimationStart : MonoBehaviour
{
    //door animation
    private Animation anim; 

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();   
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            anim.Play("DoorOpen");
    }
}
