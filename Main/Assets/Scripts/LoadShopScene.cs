using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadShopScene : MonoBehaviour
{
    public Button goToShopScene;


    void Start()
    {
        goToShopScene.onClick.AddListener(startGame);
    }

    public void startGame()
    {
        SceneManager.LoadScene("ShopScene");
    }
}
