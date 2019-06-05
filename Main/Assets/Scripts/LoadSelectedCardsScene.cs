using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSelectedCardsScene : MonoBehaviour
{
    public Button selectedCardsScene; 
    void Start()
    {
        selectedCardsScene.onClick.AddListener(goToYourCards);
    }
    public void goToYourCards()
    {
        SceneManager.LoadScene("SelectedCardsScene");
    }
}
