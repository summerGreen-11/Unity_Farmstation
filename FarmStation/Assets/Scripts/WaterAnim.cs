using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAnim : MonoBehaviour
{
    public GameObject obj; //bailer

    private Animation anim;
    

    // Start is called before the first frame update
    void Start()
    {
        
        anim = obj.GetComponent<Animation>();
      
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            obj.SetActive(true);
           
            anim.Play("Water");
          
        }
            
    }
}
