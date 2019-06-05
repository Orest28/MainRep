using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public struct Parcel
{
    Sprite Path;
    public Parcel(Sprite LogoPath)
    {
        Path = LogoPath;
    }
}

public class AnimationAttack : MonoBehaviour, IPointerClickHandler
{
    public Game curentGame;
    public static GameObject clone;
    public static GameObject clone2;
    public GameObject Explosion;
    public GameObject cloneExplosion;

    public GameObject enemyBullet;

    public float speed = 500;
    public GameObject bullet = null;
    public GameObject BG;
    public static int click = 0;
    public bool trigger = false;
    public static bool flag1 = false;
    public static bool flag2 = false;
    public static bool attacked = false;
    public static int timer;
    public static string whoAttack;
    public static string ID;
    public static string changeHp;
    public static bool firstTurn = true;
    public static bool clickYes = false;

    public static int formula = 0;
    public static int TempHP2, TempDef2, TempAtt1,TempLead1, TempChecking;

    public static List<Card> tempAttackCard = new List<Card>();


    private void Start()
    {
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(bullet.transform.parent.name != "Hand") { 
        if(firstTurn == true) {
                if (bullet.transform.parent.name == "firstLine" || bullet.transform.parent.name == "secondLine")
                {
                    if (click == 0)
                    {
                        for (int i = 0; i < ChosenCards.selectedCards.Count; i++)
                        {
                            if (bullet.GetComponent<CardGiven>().ID == ChosenCards.selectedCards[i].ID)
                            {
                                TempAtt1 = ChosenCards.selectedCards[i].Attack;
                                TempLead1 = ChosenCards.selectedCards[i].Leader;
                            }
                        }
                        clone = Instantiate(bullet, new Vector3(500, -150, 0), Quaternion.identity);
                        clone.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
                        clone.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, true);
                        flag1 = true;
                        click++;
                    }
                }
                if((bullet.transform.parent.name == "EfirstLine" || bullet.transform.parent.name == "EsecondLine" ) && click == 1)
                {
                    for (int i = 0; i < ChosenCards.selectedCards.Count; i++)
                    {
                        if (bullet.GetComponent<CardGiven>().ID == ChosenCards.selectedCards[i].ID)
                        {
                            TempHP2 = ChosenCards.selectedCards[i].HP;
                            TempDef2 = ChosenCards.selectedCards[i].Defense;
                            Debug.Log("TempHP + TempDef" + TempHP2 + "+" + TempDef2);
                        }
                    }
                    clone2 = Instantiate(bullet, new Vector3(500, 700, 0), Quaternion.identity);
                    clone2.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
                    clone2.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, true);
                    flag2 = true;
                    tempAttackCard.Add(new Card(bullet.GetComponent<CardGiven>().ID, bullet.GetComponent<CardGiven>().Name,
                                                    bullet.GetComponent<CardGiven>().LogoPath, bullet.GetComponent<CardGiven>().HP,
                                                    bullet.GetComponent<CardGiven>().Defense, bullet.GetComponent<CardGiven>().Attack,
                                                    bullet.GetComponent<CardGiven>().Leader, bullet.GetComponent<CardGiven>().Cost,
                                                    bullet.GetComponent<CardGiven>().Upkeep, bullet.GetComponent<CardGiven>().Status));
                    TempChecking = TempDef2 - (TempAtt1 + TempLead1);
                    if (TempChecking < 0)
                    {
                        formula = TempHP2 + TempChecking;

                    }
                    else
                    {
                        formula = TempHP2;
                    }
                    Debug.Log("FORMULA:" + formula);
                }
            }
        }
        else
        {
            click = 0;
        }
    }



    public void Attack(bool f1, bool f2)
    {
        if (flag1 == true && flag2 == true)
        {
            DestroyCardHP.was = false;
            clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 20) * 620);
            clone2.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -20) * 520);
            firstTurn = false;
            attacked = true;
            Invoke("delayAttack", 1.62f);
        }
    }

    public void delayAttack()
    {
        Debug.Log("hello, im delay");
        /*clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -150) * 150);
        clone2.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 150) * 150);*/
        cloneExplosion = Instantiate(Explosion, clone.transform.position, clone.transform.rotation = Quaternion.identity);
        cloneExplosion.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, true);
        Destroy(clone);
        Destroy(clone2);
        click = 0;
        flag1 = false;
        flag2 = false;
        attacked = false;
        GameManagerSrc.ChangeForPlayerAttack(tempAttackCard[0]);
    }


    /*public void enemyAttack(Card temp1, Card temp2)
    {
        Debug.Log("IM HEREEEEE");
        Debug.Log(temp1.LogoPath);
        //enemyBullet.GetComponent<Image>().sprite = Resources.Load<Sprite>(temp1.LogoPath);

    }*/
    
    private void Update()
    {

        if (attacked == false)
        {
            Attack(flag1, flag2);
        }

        if (Input.GetKey("space"))
        {
            Destroy(clone);
            Destroy(clone2);
            click = 0;
            attacked = false;
            flag1 = false;
            flag2 = false;
        }
    }
}
    