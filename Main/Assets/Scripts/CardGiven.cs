using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGiven : MonoBehaviour
{
    public Card SelfCard;
    public Image image = null;
    public int HP, Defense, Attack, Leader, Cost, Upkeep, ID;
    public string Name, Status, LogoPath;
    public bool onDesk = false;

    public GameObject HideObj;


    public void ShowCardInfo(Card card)
    {
        SelfCard = card;
        image.GetComponent<Image>().sprite = card.Logo;
        Cost = card.Cost;
        HP = card.HP;
        Defense = card.Defense;
        Attack = card.Attack;
        Leader = card.Leader;
        Upkeep = card.Upkeep;
        ID = card.ID;
        Name = card.Name;
        LogoPath = card.LogoPath;
        Status = card.Status;
        onDesk = true;
        image.preserveAspect = true;
        HideObj.SetActive(false);
    }

    public void HideCardInfo(Card card)
    {
        Cost = card.Cost;
        HP = card.HP;
        Defense = card.Defense;
        Attack = card.Attack;
        Leader = card.Leader;
        Upkeep = card.Upkeep;
        ID = card.ID;
        Name = card.Name;
        Status = card.Status;
        LogoPath = card.LogoPath;
        SelfCard = card;
        onDesk = true;
        HideObj.SetActive(true);
        //image.sprite = card.Logo;
    }
}
