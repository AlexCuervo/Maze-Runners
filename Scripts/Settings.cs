using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClick(){
        if(name == "BackButton"){
            SceneManager.LoadScene("Menu");
        }
        // if(name == "VolumeSlider")
        if(name == "Plus"){
            GetComponentInParent<Text>().text = (int.Parse(GetComponentInParent<Text>().text) + 1).ToString();
        }
        if(name == "Minus"){
            if(GetComponentInParent<Text>().text != "10")GetComponentInParent<Text>().text = (int.Parse(GetComponentInParent<Text>().text) - 1).ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
