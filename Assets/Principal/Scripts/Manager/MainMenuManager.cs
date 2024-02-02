using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MyLoadScene(int idScene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(idScene);
    }

    public void button_exit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

}
