using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearTurn : MonoBehaviour
{
    public Button goToShopScene;


    void Start()
    {
        goToShopScene.onClick.AddListener(Clear);
    }

    public void Clear()
    {
        AnimationAttack.click = 0;
    }
}
