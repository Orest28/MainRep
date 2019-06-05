using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public static class ChosenCards
{
    public static List<Card> selectedCards = new List<Card>();
    public static List<Card> copySelectedCards = new List<Card>();
}
public class ChoseCard : MonoBehaviour, IPointerClickHandler
{
    public Image Hp, Defense, Attack, Leader, Cost, Upkeep, Skills;
    public Text Thp, TDef, Tattack, Tlead, Tcost, Tupkeep;
    public Image choseCard;
    public int ID;

    public void OnPointerClick(PointerEventData eventData)
    {
        ID = GetComponent<CardInfoSrc>().ID;       
        Debug.Log("Test" + ChosenCards.selectedCards.Count);
        Debug.Log("ID" + ID);
        foreach(Card element in CardManager.AllCards)
        {
            if(element.ID == ID && choseCard.sprite != Resources.Load<Sprite>("Cards/BG/BackgroundForEmpire"))           
            {
                ChosenCards.selectedCards.Add(new Card(element.ID, element.Name, element.LogoPath, element.HP, 
                                        element.Defense, element.Attack, element.Leader, element.Cost, 
                                        element.Upkeep, element.Status));
                ChosenCards.copySelectedCards.Add(new Card(element.ID, element.Name, element.LogoPath, element.HP,
                                        element.Defense, element.Attack, element.Leader, element.Cost,
                                        element.Upkeep, element.Status));

                var tempColor = Hp.color;
            tempColor.a = 0f;
            Hp.color = Defense.color = Attack.color = Leader.color = Cost.color = Upkeep.color = Skills.color = tempColor;
            Thp.text = TDef.text = Tattack.text = Tlead.text = Tcost.text = Tupkeep.text = " ";
            }
        }
        
        choseCard.sprite = Resources.Load<Sprite>("Cards/BG/BackgroundForEmpire");
    }
}
