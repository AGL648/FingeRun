using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpScene : MonoBehaviour
{
    public float tiempopulsado = 0;
    public string SampleScene;

    // Update is called once per frame
    void Update()
    {
        if (SimpleInput.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SampleScene);

        }
    }
}
