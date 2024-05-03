using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public Animator player_Anim;
    public GameObject pnl_Transition;
    public Animator transition;
    void Start()
    {
        player_Anim.SetTrigger("MainMenuAnim");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MyLoadScene()
    {
        StartCoroutine(transitionScene());
    }

    public void button_exit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    IEnumerator transitionScene()
    {
        transition.SetTrigger("Transition");
        yield return new WaitForSeconds(2);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

}
