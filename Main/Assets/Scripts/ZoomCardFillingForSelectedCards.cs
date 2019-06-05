using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomCardFillingForSelectedCards : MonoBehaviour
{
    public Image Logo;
    public Sprite test;

    public Image Hp, Defense, Attack, Leader, Cost, Upkeep, Skills;
    public Text Thp, TDef, Tattack, Tlead, Tcost, Tupkeep;

    private void Start()
    {
        Logo = GetComponent<Image>();
    }
    public void Update()
    {
        if (ZoomCardInSelectedCards.flag && ZoomCardInSelectedCards.ZoomLogo != Resources.Load<Sprite>("Cards/BG/BackgroundForEmpire"))
        {
            foreach (Card element in ChosenCards.selectedCards)
            {
                if (element.ID == ZoomCardInSelectedCards.ID)
                {
                    Thp.text = element.HP.ToString();   
                    TDef.text = element.Defense.ToString();
                    Tattack.text = element.Attack.ToString();
                    Tlead.text = element.Leader.ToString();
                    Tcost.text = element.Cost.ToString();
                    Tupkeep.text = element.Upkeep.ToString();
                    Logo.sprite = element.Logo;
                    var tempColor = Logo.color;
                    tempColor.a = 255f;
                    Logo.color = Hp.color = Defense.color = Attack.color = Defense.color = Leader.color = Cost.color = Upkeep.color = tempColor;
                }
            }
        }
        else
        {
            Logo.sprite = null;
            var tempColor = Logo.color;
            tempColor.a = 0f;
            Thp.text = TDef.text = Tattack.text = Tlead.text = Tcost.text = Tupkeep.text = " ";
            Logo.color = Hp.color = Defense.color = Attack.color = Defense.color = Leader.color = Cost.color = Upkeep.color = tempColor;
        }
    }
}
