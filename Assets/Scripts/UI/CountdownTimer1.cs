
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTimer1 : MonoBehaviour
{

    [SerializeField] public string sceneToLoad;
    [SerializeField] private float timer = 10f;
    private float timeElapsed;
    public string startScene;

    void Start(){
        
    }

    private void Update(){
        timeElapsed += Time.deltaTime;
        
        if (timeElapsed > timer){
            SceneManager.LoadScene(startScene);
        }

         else if(Input.GetKeyDown(KeyCode.KeypadEnter)){
            SceneManager.LoadScene(startScene);
        }
    }
   
}
