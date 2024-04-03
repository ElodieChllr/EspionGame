using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    

    public void MyLoadScene(int idScene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(idScene);
    }
}
