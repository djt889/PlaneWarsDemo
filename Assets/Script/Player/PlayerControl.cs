using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    
    private float speed = 5f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            // 飞机移动
            transform.Translate(new Vector3(h, 0, v) * Time.deltaTime * speed, Space.World);
            
            // 飞机摇摆
            this.transform.eulerAngles = new Vector3(v * 15, 0, h * -30);
            
        }
        
    }
}
