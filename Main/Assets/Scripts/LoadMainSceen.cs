using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadMainSceen : MonoBehaviour
{
    public Button startGameButton;


    void Start()
    {
        startGameButton.onClick.AddListener(startGame);
    }

    public void startGame()
    {
        SceneManager.LoadScene("MainScene");
    }

}
