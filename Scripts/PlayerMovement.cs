using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    private void Movement(KeyCode up, KeyCode down, KeyCode left, KeyCode right){
        if(Input.GetKey(up))transform.position+= new Vector3(0,0,6) * Time.deltaTime;
        if(Input.GetKey(down))transform.position+= new Vector3(0,0,-6) * Time.deltaTime;
        if(Input.GetKey(left))transform.position+= new Vector3(-6,0,0) * Time.deltaTime;
        if(Input.GetKey(right))transform.position+= new Vector3(6,0,0) * Time.deltaTime;
    }
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement(up, down, left, right);
    }
}
