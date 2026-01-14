using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu1 : MonoBehaviour
{

    public float gemCollected;

    public string startScene;
    // Start is called before the first frame update
    void Start()
    {

        gemCollected = 20f;
    }

    // Update is called once per frame
    void Update()
    {

    }

     
    public void StartGame()
    {
        SceneManager.LoadScene(startScene);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }
}
