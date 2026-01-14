
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTimer2 : MonoBehaviour
{

    
    [SerializeField] private float timer = 20f;
    public string startScene;

    [SerializeField] private Slider slider;
    private float timeElapsed;

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
