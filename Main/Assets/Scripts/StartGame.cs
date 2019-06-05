using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{

    public Button startGame;
    // Start is called before the first frame update
    void Start()
    {
        startGame.onClick.AddListener(start_Game);
    }
    

    public void start_Game()
    {
        SceneManager.LoadScene("ShopScene");
    }
}
