using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locationeffect : Effect
{

    public override void ActivateEffect(GameObject target){
        GameObject MazeArea = GameObject.FindWithTag("MazeArea");
        List<Transform> availableSpots = new();
        for(int i = 0; i < MazeArea.transform.childCount; i++){
            if(MazeArea.transform.GetChild(i).childCount == 0){
                availableSpots.Add(MazeArea.transform.GetChild(i));
            }
        } 
        int randomIndex = Random.Range(0,availableSpots.Count);
        target.transform.position = availableSpots[randomIndex].position/* + new Vector3(0,1,0)*/;
        StartCoroutine(RevertAfterTime(15f));
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
