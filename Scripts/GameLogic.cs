using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random; //random
public class GameLogic : MonoBehaviour
{
    public int BoardSize;
    public GameObject Spot;
    public GameObject Block;
    public Text startLetter;
    bool Started = false;
    GameObject MazeArea;
    
    private void ShowStartLetter(Text Letter,bool Started){    //this method controls the visibility of the Start Letter
        if(!Started)Letter.gameObject.SetActive(true);
        else Letter.gameObject.SetActive(false);      
    }
    
    private int[] randomizeIntArray(int[] sortedIntArray){       //this method randomizes the order of the elements of an array of integers, used to alternate the movement of the position in the DFS algorythm
        int[] randomizedArray = new int[sortedIntArray.Length];
        List<int> newArray = sortedIntArray.ToList();
        for(int i = 0; i < randomizedArray.Length; i++){
            int randomIndex = Random.Range(0, newArray.Count);
            randomizedArray[i] = newArray[randomIndex];
            newArray.Remove(newArray[randomIndex]);
        }
        return randomizedArray;
    }
    private void GenerateBoard( GameObject Spot, GameObject Floor){      //this method generates the playable area, organized in cells 
    
        for(int i = 0; i < BoardSize; i++){
           for(int j = 0; j < BoardSize; j++){
                GameObject BoardSpot = GameObject.Instantiate(Spot);
                BoardSpot.GetComponent<Spot>().row = j;
                BoardSpot.GetComponent<Spot>().column = i;
                BoardSpot.transform.position = new UnityEngine.Vector3(i,0,-j);
                BoardSpot.transform.SetParent(Floor.transform, false);
            }
        }
    }

    private void MazeDFS(bool[,] mazeMask, int row, int column, int wallToDestroyY, int wallToDestroyX, int[] movesRow, int[] movesColumn, bool[,] visitedSpots){   //DFS algorythm that generates a maze
        if(row < 0 || row >= mazeMask.GetLength(0) || column < 0 || column >= mazeMask.GetLength(1))return;
        if(visitedSpots[row,column])return;
        if(!visitedSpots[row,column]){
            visitedSpots[row, column] = true;
            mazeMask[wallToDestroyY,wallToDestroyX] = false;
        }
        int[] indexArray = {0,1,2,3};
        int[] randomIndex = randomizeIntArray(indexArray);
        
        for(int i = 0; i < 4; i++){
            MazeDFS(mazeMask, row + movesRow[randomIndex[i]]*2, column + movesColumn[randomIndex[i]]*2, row + movesRow[randomIndex[i]], column + movesColumn[randomIndex[i]], movesRow, movesColumn, visitedSpots);
        }

    }
    private void GenerateMaze(GameObject MazeArea){                 //Maze Generator method, calls DFS algorythm
        bool[,] mazeMask = new bool[BoardSize, BoardSize];
        bool[,] visitedSpots = new bool[BoardSize, BoardSize];
        int[] movesRow = {-1,1,0,0};
        int[] movesColumn = {0,0,-1,1};
        for(int i = 0; i < BoardSize; i++){
            for (int j = 0; j < BoardSize; j++){
                if(i%2 == 0 || j%2 == 0)mazeMask[i,j] = true;
            }
        }

        MazeDFS(mazeMask, 1, 1, 1, 1, movesRow, movesColumn, visitedSpots);

        for(int i = 0; i < MazeArea.transform.childCount; i++){
            Transform spot = MazeArea.transform.GetChild(i);
            int spotRow = MazeArea.transform.GetChild(i).GetComponent<Spot>().row;
            int spotColumn = MazeArea.transform.GetChild(i).GetComponent<Spot>().column;

            if(mazeMask[spotRow,spotColumn]){
                GameObject Wall = GameObject.Instantiate(Block);
                Wall.transform.SetParent(spot, false);
                Wall.transform.localPosition = new UnityEngine.Vector3 (0,50,0);
                Wall.transform.localScale = new UnityEngine.Vector3 (1,100,1);
            }

        }         
       
    }
    void Awake()
    {
        
        MazeArea = GameObject.FindGameObjectWithTag("MazeArea");
        if(BoardSize%2 == 0)BoardSize++;
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
