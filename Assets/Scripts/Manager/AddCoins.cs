using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AddCoins : MonoBehaviour
{
    public int gemCollected;
    

    public UnityEngine.UI.Text CoinText;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        gemCollected = 20;
        UpdateCoinCount();
    }

    public void UpdateCoinCount()
    {
        //Actualizar el número de gemas recogidas
        CoinText.text = "" + gemCollected;//Cast -> convertimos el número entero en texto para que pueda ser representado en la UI
    }
}
