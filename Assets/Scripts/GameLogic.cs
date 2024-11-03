using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public int BoardSize; 
    public GameObject Spot;
    public GameObject Block;
    public Text startLetter;
    bool Started = false;
    GameObject MazeArea;
    
    void ShowStartLetter(Text Letter,bool Started){
        if(!Started)Letter.gameObject.SetActive(true);
        else Letter.gameObject.SetActive(false);        
    }
    void GenerateBoard( GameObject Spot, GameObject Floor){
    
        for(int i = 0; i < BoardSize; i++){
           for(int j = 0; j < BoardSize; j++){
            GameObject BoardSpot = GameObject.Instantiate(Spot);
            BoardSpot.transform.position = new UnityEngine.Vector3(i,0,j);
            BoardSpot.transform.SetParent(Floor.transform, false);
            if(i==0 || i == BoardSize-1|| j == 0 || j== BoardSize-1){
                GameObject Wall = GameObject.Instantiate(Block);
                Wall.transform.SetParent(BoardSpot.transform, false);
                Wall.transform.localScale = new UnityEngine.Vector3(1, 100, 1);
                Wall.transform.localPosition = new UnityEngine.Vector3 (0,50,0);
                
            }
           }
        }
    }

    void GenerateMaze(GameObject MazeArea){
        
        List<Transform> MazeChildrenList = new();
        for (int i = 0; i < MazeArea.transform.childCount ; i++){
            if(MazeArea.transform.GetChild(i).childCount == 0){
                MazeChildrenList.Add(MazeArea.transform.GetChild(i));
            }
        }
        int availableSpots = MazeChildrenList.Count;
        for(int i = 0; i < availableSpots/3; i++){
            int randomIndex = Random.Range(0,MazeChildrenList.Count - 1);
            GameObject Wall = GameObject.Instantiate(Block);
            Wall.transform.SetParent(MazeChildrenList[randomIndex].transform, false);
            Wall.transform.localPosition = new UnityEngine.Vector3 (0,50,0);
            Wall.transform.localScale = new UnityEngine.Vector3(1, 100, 1);
            MazeChildrenList.Remove(MazeChildrenList[randomIndex]);
        }

    }
    void Awake()
    {
        
        MazeArea = GameObject.FindGameObjectWithTag("MazeArea");
        
        GenerateBoard(Spot, MazeArea);
    }

    // Update is called once per frame
    void Update()
    {
        ShowStartLetter(startLetter, Started);
        if(Input.GetKeyDown(KeyCode.Space) && !Started){
            Started = true;
            GenerateMaze(MazeArea);
       }   
    
    //   metodo para verificar la condicion de victoria
    //   
    }

}
