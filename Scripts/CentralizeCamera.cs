using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CentralizeCamera : MonoBehaviour
{
    UnityEngine.Vector3 offset;

    GameObject Maze;
    int BoardSize;
    Transform target;
    void Start()
    {
        BoardSize = GameObject.FindWithTag("GameManager").transform.GetComponent<GameLogic>().BoardSize;
        Maze = GameObject.FindWithTag("MazeArea");
        Transform[] MazeChildren = Maze.GetComponentsInChildren<Transform>();
        UnityEngine.Vector3 offset = new UnityEngine.Vector3 (0,BoardSize + 3,-5);
    
        
        if(BoardSize%2==0)target = MazeChildren[MazeChildren.Length/2 - BoardSize/2];
        else target = MazeChildren[(MazeChildren.Length+1)/2];
        
        UnityEngine.Vector3 desiredPosition = target.position + offset;
        transform.position = desiredPosition;        
        transform.LookAt(target.transform);
    }
    void Update()
    {

    }
}
