using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager1 : MonoBehaviour
{
    public string scene;


    public void changeScene()
    {
        SceneManager.LoadScene(scene);
    }
}
