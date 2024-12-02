using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClick(){
        if(name == "PlayButton"){
            SceneManager.LoadScene("PlayScene");
        }
        if(name == "SettingsButton"){
            SceneManager.LoadScene("Settings");
        }
        if(name == "ExitButton"){}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
