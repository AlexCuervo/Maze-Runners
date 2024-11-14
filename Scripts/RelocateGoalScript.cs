using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelocateGoalScript : Effect
{
    public override void ActivateEffect(GameObject target){
        GameObject goal = GameObject.FindWithTag("Goal");
        GameObject MazeArea = GameObject.FindWithTag("MazeArea");
        List<Transform> availableSpots = new();
        for(int i = 0; i < MazeArea.transform.childCount; i++){
            if(MazeArea.transform.GetChild(i).childCount == 0){
                availableSpots.Add(MazeArea.transform.GetChild(i));
                isActivated = true;
            }
        } 
        int randomIndex = Random.Range(0,availableSpots.Count);
        goal.transform.position = availableSpots[randomIndex].position + new Vector3(0,2,0);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
