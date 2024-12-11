using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float inputVertical;
    public float inputHorizontal;
    public float speed = 3f;
    Vector3 lastDirection = new Vector3 (1,0,0);   
    public int order = 1;
    Animator animator;

    void Movement(){
        
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

        Vector3 direction = new Vector3 (order* inputHorizontal, 0 , order* inputVertical);
        if(direction != new Vector3 (0,0,0)) lastDirection = direction;
        direction.Normalize();
        transform.position += direction*speed* Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(lastDirection);
        if(direction != new Vector3 (0,0,0)) animator.SetFloat("movement", 1);
        else animator.SetFloat("movement", 0);
    }

void Start(){
    animator = GetComponent<Animator>();
}
    
    void Update()
    {
        Movement();
    }
}
