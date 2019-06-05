using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScriptText : MonoBehaviour
{
    public Button PlayAgain;
    public Text ResultText;
    // Start is called before the first frame update
    void Start()
    {
        PlayAgain.onClick.AddListener(Play);
        ResultText.text = GameManagerSrc.ResultText;
    }
    public void Play()
    {
        ChosenCards.selectedCards.Clear();
        SceneManager.LoadScene("ShopScene"); 
    }
}
