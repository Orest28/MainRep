using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CardInfoSrc : MonoBehaviour
{
    public Card SelfCard;
    public Image image = null;
    public Text Thp, TDef, Tattack, Tlead, Tcost, Tupkeep;
    public int ID;

    public void ShowCardInfo(Card card)
    {

        ID = card.ID;
        image.sprite = card.Logo;
        SelfCard = card;
        Thp.text = card.HP.ToString();
        TDef.text = card.Defense.ToString();
        Tattack.text = card.Attack.ToString();
        Tlead.text = card.Leader.ToString();
        Tcost.text = card.Cost.ToString();
        Tupkeep.text = card.Upkeep.ToString();
        image.preserveAspect = true;
        for (int i = 0; i < ChosenCards.selectedCards.Count; i++)
            {
                if (ChosenCards.selectedCards[i].ID == card.ID)
                {
                    image.sprite = Resources.Load<Sprite>("Cards/BG/BackgroundForEmpire");
                }
            }
    }

    private void Start()
    {
        ShowCardInfo(CardManager.AllCards[transform.GetSiblingIndex()]);   
    }

    public void HideCardInfo(Card card)
    {
        SelfCard = card;
        image.sprite = null;
    }
}
