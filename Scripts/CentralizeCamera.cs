using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CentralizeCamera : MonoBehaviour
{
    Vector3 offset;
    GameObject Maze;
    int BoardSize;
    Transform target;
    Vector3 rotationPoint;
    private void Rotate(bool Left){
        float rotationSpeed;
        
        if(Left) rotationSpeed = 90*Time.deltaTime;
        else rotationSpeed = -90* Time.deltaTime;

        transform.RotateAround(rotationPoint, new Vector3(0,1,0) , rotationSpeed);
    }
    
    private void Centralize(){
        BoardSize = GameObject.FindWithTag("GameManager").transform.GetComponent<GameLogic>().BoardSize;
        Maze = GameObject.FindWithTag("MazeArea");
        Transform[] MazeChildren = Maze.GetComponentsInChildren<Transform>();
        offset = new(0,BoardSize + 3,-5);
        target = MazeChildren[(MazeChildren.Length+1)/2];
        Vector3 desiredPosition = target.position + offset;
        transform.position = desiredPosition;        
        transform.LookAt(target.transform);
    }
    void Start()
    {
        Centralize();
        rotationPoint = transform.position + new Vector3(0,0,5);
    }
    void Update()
    {
        // if(Input.GetKey(KeyCode.Q)) {
        //     Rotate(true);
        // }
        // else if(Input.GetKey(KeyCode.E)){
        //     Rotate(false);
        // } 
    }
}
