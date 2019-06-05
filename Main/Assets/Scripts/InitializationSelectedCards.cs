using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class InitializationSelectedCards : MonoBehaviour
{
    public Card SelfCard;
    public Image image = null;
    public Image HP, Cost, leader, attack, def, upkeep;
    public Text Thp, TDef, Tattack, Tlead, Tcost, Tupkeep;
    public int ID;
    public void showSelectedCards(Card card)
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
    }
    void Start()
    {
        try
        {
            showSelectedCards(ChosenCards.selectedCards[transform.GetSiblingIndex() - 1]);
            CardManager.AllCards.Clear();
        }
        catch
        {
            image.sprite = Resources.Load<Sprite>("Cards/BG/BackgroundForEmpire");
            HP.sprite = null;
            Cost.sprite = null;
            leader.sprite = null;
            attack.sprite = null;
            def.sprite = null;
            upkeep.sprite = null;
            var tempColor = HP.color;
            tempColor.a = 0f;
            HP.color = def.color = attack.color = Cost.color = leader.color = upkeep.color = tempColor;
        }
    }
 
}
