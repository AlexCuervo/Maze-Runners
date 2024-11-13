using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    float inputVertical;
    float inputHorizontal;
    public float speed = 5f;
    Vector3 lastDirection = new Vector3 (1,0,0);    

    private void Movement(){
        
        if(CompareTag("player1"))
        {
            inputHorizontal = Input.GetAxis("Horizontal");
            inputVertical = Input.GetAxis("Vertical");
        }
        if(CompareTag("player2"))
        {
            inputHorizontal = Input.GetAxis("Horizontal_P2");
            inputVertical = Input.GetAxis("Vertical_P2");
        }    

        Vector3 direction = new Vector3 (inputHorizontal, 0 , inputVertical);
        if(direction != new Vector3 (0,0,0)) lastDirection = direction;
        direction.Normalize();
        transform.position += direction*speed* Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(lastDirection);
    }

    void Update()
    {
        Movement();
    }
}
