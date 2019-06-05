/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomCardFilling : MonoBehaviour
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
        if (CardMovementScript.flag)
        {
            foreach (Card element in CardManager.AllCards)
            {
                if(element.Logo == CardMovementScript.ZoomLogo)
                {
                    Logo.sprite = element.Logo;
                    var tempColor = Logo.color;
                    tempColor.a = 255f;
                    Logo.color = tempColor;
                }
            }
        }
        else
        {
            Logo.sprite = null;
            var tempColor = Logo.color;
            tempColor.a = 0f;
            Logo.color = tempColor;
        }
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomCardFilling : MonoBehaviour
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
        if (CardMovementScript.flag)
        {
            foreach (Card element in ChosenCards.selectedCards)
            {
                if (element.ID == CardMovementScript.id)
                {
                    Thp.text = element.HP.ToString();
                    TDef.text = element.Defense.ToString();
                    Tattack.text = element.Attack.ToString();
                    Tlead.text = element.Leader.ToString();
                    Tcost.text = element.Cost.ToString();
                    Tupkeep.text = element.Upkeep.ToString();
                    Logo.sprite = element.Logo;

                    Logo.sprite = element.Logo;
                    var tempColor = Logo.color;
                    tempColor.a = 255f;
                    Logo.color = tempColor;

                    Logo.color = Hp.color = Defense.color = Attack.color = Defense.color = Leader.color = Cost.color = Upkeep.color = tempColor;

                }
            }
        }
        else
        {
            Logo.sprite = null;
            var tempColor = Logo.color;
            tempColor.a = 0f;
            Logo.color = tempColor;
            Thp.text = TDef.text = Tattack.text = Tlead.text = Tcost.text = Tupkeep.text = " ";
            Logo.color = Hp.color = Defense.color = Attack.color = Defense.color = Leader.color = Cost.color = Upkeep.color = tempColor;
        }
    }
}