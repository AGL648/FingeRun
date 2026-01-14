using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadSceneAfterTime : MonoBehaviour
{
    public string LoadScene;
    public float timer;
    private Text timerSeconds;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay(timer));
    }

    // Update is called once per frame
    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(LoadScene);

    }
}
