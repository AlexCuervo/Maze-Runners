using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;
using System.Numerics;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameLogic : MonoBehaviour
{
    public int BoardSize;
    public GameObject Spot;
    public GameObject Block;
    public Text startLetter;
    public Text player1ScoreLetter;
    public Text player2ScoreLetter;
    public Image winRoundLetter;
    public Image selectPLayersMenu;
    public GameObject locationTrap;
    public GameObject stunTrap;
    public GameObject slowTrap;
    public GameObject relocateGoalTrap;
    public GameObject speedBoost;
    public GameObject stunEnemyBoost;
    public GameObject invertEnemyControlsBoost;
    public GameObject goal;
    public bool winRound;
    public GameObject player1;
    public GameObject player2;
    bool started = false;
    bool isShowedWinRoundLetter = false;
    GameObject MazeArea;
    private int _scorePlayer1 = 0;
    private int _scorePlayer2 = 0;
    
    private void ShowStartLetter(){    //this method controls the visibility of the Start Letter
        if(!started)startLetter.gameObject.SetActive(true);
        else startLetter.gameObject.SetActive(false);
    }
    private IEnumerator Exit(float delay){
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Menu");
    }
    private void ShowWinRoundLetter(){
        if(_scorePlayer1 < 5 && _scorePlayer2 < 5){
            winRoundLetter.gameObject.SetActive(true);
            GameObject.FindWithTag("player1").GetComponent<PlayerMovement>().order = 0;      
            GameObject.FindWithTag("player2").GetComponent<PlayerMovement>().order = 0;
            winRoundLetter.transform.GetChild(0).GetComponent<Text>().text = $"{GameObject.FindWithTag("Goal").GetComponent<Goal>().winner.name} wins the round";
            isShowedWinRoundLetter = true;
            StartCoroutine(NextRound(1f)); 
        }
        else{
            winRoundLetter.transform.GetChild(0).GetComponent<Text>().text = $"{GameObject.FindWithTag("Goal").GetComponent<Goal>().winner.name} wins the game";
            winRoundLetter.gameObject.SetActive(true);
            winRoundLetter.transform.GetChild(1).gameObject.SetActive(false);
            winRoundLetter.transform.GetChild(2).gameObject.SetActive(false);
            isShowedWinRoundLetter = true;
            GameObject.FindWithTag("player1").GetComponent<PlayerMovement>().order = 0;      
            GameObject.FindWithTag("player2").GetComponent<PlayerMovement>().order = 0;
            StartCoroutine(Exit(5f));
        }
    }
    private IEnumerator NextRound(float preparation){
        for(int i = 0; i<5; i++){
            yield return new WaitForSeconds(preparation);
            winRoundLetter.transform.GetChild(2).GetComponent<Text>().text = $"{4 - i}";
            if(winRoundLetter.transform.GetChild(2).GetComponent<Text>().text == "0")winRoundLetter.transform.GetChild(2).GetComponent<Text>().text = "5";
        }
        ClearBoard();
        GenerateMaze(MazeArea);
        PlacePlayers();
        PlaceObjects();
        winRoundLetter.gameObject.SetActive(false);
        GameObject.FindWithTag("player1").GetComponent<PlayerMovement>().order = 1;      
        GameObject.FindWithTag("player2").GetComponent<PlayerMovement>().order = 1;
        isShowedWinRoundLetter = false;
    }
    private void InstantiateAtPosition(GameObject original, Transform parent, Vector3 localPosition, Vector3 localScale, string tag){
        GameObject item = Instantiate(original);
        item.transform.SetParent(parent, false);
        item.transform.localPosition = localPosition;
        item.transform.localScale = localScale;
        item.tag = tag;
        item.SetActive(true);
    }
    private void PlacePlayers(){
        player1 = GameObject.FindWithTag("player1");
        player2 = GameObject.FindWithTag("player2");
        int[] startPositions ={0,1,2,3};
        int[] startPositionsArray = RandomizeIntArray(startPositions);
        GameObject[] players = {player1,player2};
        for(int i = 0; i < startPositions.Length; i++){
            if(i > 1)break;
            if(startPositionsArray[i]==0)players[i].transform.position = new Vector3(1,0,-1);
            if(startPositionsArray[i]==1)players[i].transform.position = new Vector3(1,0,-(BoardSize - 2));
            if(startPositionsArray[i]==2)players[i].transform.position = new Vector3(BoardSize-2,0,-1);
            if(startPositionsArray[i]==3)players[i].transform.position = new Vector3(BoardSize-2,0,-(BoardSize-2));
        }
    }
    private void PlaceObjects(){
        List<GameObject> availableSpots = new();
        List<GameObject> objectsList = new(){locationTrap, stunTrap, slowTrap, relocateGoalTrap, speedBoost, stunEnemyBoost, invertEnemyControlsBoost};
        for(int i = 0; i < MazeArea.transform.childCount; i++){
            Transform cell = MazeArea.transform.GetChild(i);
            int row = cell.GetComponent<Spot>().row;
            int column = cell.GetComponent<Spot>().column;
            if(cell.childCount == 0 && ((row != 1 && row != BoardSize-1) || (column != 1 && column != BoardSize-1)) && row != (BoardSize+1)/2 && column != (BoardSize+1)/2){
                availableSpots.Add(cell.gameObject);
                i++;
            }
        
        }
        for(int i = 0; i < availableSpots.Count/15; i++){
            int randomIndex = Random.Range(0,objectsList.Count);
            if(objectsList[randomIndex] == stunEnemyBoost)InstantiateAtPosition(objectsList[randomIndex],availableSpots[Random.Range(0,availableSpots.Count)].transform, new Vector3 (0,1,0), new Vector3 (1,1,1), "StunBoost");
            else InstantiateAtPosition(objectsList[randomIndex],availableSpots[Random.Range(0,availableSpots.Count)].transform, new Vector3 (0,1,0), new Vector3 (1,1,1), "Untagged");
            if(randomIndex==3)objectsList.Remove(objectsList[3]);
        }        
    }
    private int[] RandomizeIntArray(int[] sortedIntArray){       //this method randomizes the order of the elements of an array of integers, used to alternate the movement of the position in the DFS algorythm
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
                BoardSpot.transform.position = new(i,0,-j);
                BoardSpot.transform.SetParent(Floor.transform, false);
            }
        }
    }
    private void ClearBoard(){
    for( int i = 0; i < MazeArea.transform.childCount; i++){
        for(int j = 0; j < MazeArea.transform.GetChild(i).childCount; j++){
            MazeArea.transform.GetChild(i).GetChild(j).gameObject.SetActive(false);
        }
        MazeArea.transform.GetChild(i).DetachChildren();
    }
}
    private void MazeDFS(bool[,] mazeMask, int row, int column, int wallToDestroyY, int wallToDestroyX, int[] movesRow, int[] movesColumn, bool[,] visitedSpots){   //DFS algorythm that generates a maze over a boolean matrix
        if(row < 0 || row >= mazeMask.GetLength(0) || column < 0 || column >= mazeMask.GetLength(1))return;
        if(visitedSpots[row,column])return;
        if(!visitedSpots[row,column]){
            visitedSpots[row, column] = true;
            mazeMask[wallToDestroyY,wallToDestroyX] = false;
        }
        int[] indexArray = {0,1,2,3};
        int[] randomIndex = RandomizeIntArray(indexArray);   
        
        for(int i = 0; i < 4; i++){
            MazeDFS(mazeMask, row + movesRow[randomIndex[i]]*2, column + movesColumn[randomIndex[i]]*2, row + movesRow[randomIndex[i]], column + movesColumn[randomIndex[i]], movesRow, movesColumn, visitedSpots);
            int openRandom = Random.Range(0,4);
            if(Random.Range(0,40) == 0){
                if(row + movesRow[indexArray[openRandom]] > 0 && row + movesRow[openRandom] < mazeMask.GetLength(0) - 1 && column + movesColumn[indexArray[openRandom]] > 0 && column + movesColumn[indexArray[openRandom]] < mazeMask.GetLength(1) - 1){
                    mazeMask[row + movesRow[indexArray[openRandom]], column + movesColumn[indexArray[openRandom]]] = false;
                }
            }
        }

    }
    private void GenerateMaze(GameObject MazeArea){                 //Maze Generator method, calls DFS algorythm & generates blocks over the True values of the boolean matrix
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

        for(int i = 0; i<3;i++){                        //this loop eliminates the blocks in the center of the maze
            for(int j = 0; j < 3; j++){
                int row = BoardSize/2 - 1 + i;
                int column = BoardSize/2 - 1 + j;
                mazeMask[row,column] = false;
            }
        }

        for(int i = 0; i < MazeArea.transform.childCount; i++){
            Transform spot = MazeArea.transform.GetChild(i);
            int spotRow = MazeArea.transform.GetChild(i).GetComponent<Spot>().row;
            int spotColumn = MazeArea.transform.GetChild(i).GetComponent<Spot>().column;

            if(mazeMask[spotRow,spotColumn]){
                InstantiateAtPosition(Block, spot, new Vector3 (0,70,0), new Vector3(1,140,1), "Untagged");
            }
            if(spotRow==BoardSize/2 && spotColumn==BoardSize/2){
                InstantiateAtPosition(goal, spot, new Vector3(0,200,0), new Vector3(1,400,1), "Goal");
            }
        }
       
    }
    void Awake()
    {
        MazeArea = GameObject.FindGameObjectWithTag("MazeArea");
        if(BoardSize%2 == 0)BoardSize++;
        GenerateBoard(Spot, MazeArea);
        player1ScoreLetter.gameObject.SetActive(false);
        player2ScoreLetter.gameObject.SetActive(false);
    }
    void Update()
    {
        ShowStartLetter();
        if(!started){
            if(Input.GetKeyDown(KeyCode.Space)){
                started = true;
                player1ScoreLetter.gameObject.SetActive(true);
                player2ScoreLetter.gameObject.SetActive(true);
                GenerateMaze(MazeArea);
                PlacePlayers();
                PlaceObjects();
                goal = GameObject.FindWithTag("Goal");
                winRoundLetter.gameObject.SetActive(false);
                GameObject.FindWithTag("player1").GetComponent<PlayerMovement>().order = 1;
                GameObject.FindWithTag("player2").GetComponent<PlayerMovement>().order = 1;
            }
        }
        else if(GameObject.FindWithTag("Goal")!= null){
                if(GameObject.FindWithTag("Goal").GetComponent<Goal>().contactWithPlayer && !isShowedWinRoundLetter){
                    if(GameObject.FindWithTag("Goal").GetComponent<Goal>().winner == player1) _scorePlayer1++;
                    else _scorePlayer2++;
                    ShowWinRoundLetter();
                }
        }

    }
}