using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Game
{
    public List<Card> EnemyDeck, PlayerDeck;
    public int Rand = 0;        

    public Game ()
    {
        EnemyDeck = GiveDeckCard();
        PlayerDeck = GiveDeckCard();
    }
    
    List<Card> GiveDeckCard()
    {
        List<Card> list = new List<Card>();

        for (int i = 0; i < 18; i++)
        {
            Rand = Random.Range(0, ChosenCards.copySelectedCards.Count);
            list.Add(ChosenCards.copySelectedCards[Rand]);
            ChosenCards.copySelectedCards.RemoveAt(Rand);
            Rand = 0;
        }
        return list;
    }
}

public class GameManagerSrc : MonoBehaviour
{

    public static List<Card> PlayerFirstLine = new List<Card>();
    public static List<Card> PlayerSecondLine = new List<Card>();
    public static List<Card> EnemyFirstLine = new List<Card>();
    public static List<Card> EnemySecondLine = new List<Card>();


    public Game CurrentGame;
    public Transform EnemyHand, PlayerHand,
                     EnemyFirstField, EnemySecondField;
    public GameObject CardPref;
    int turn, turnTime = 30;

    public TextMeshProUGUI turnTimeTxt;
    public Button endTurnButton;

    public Text Gold, EnemyGold;

    public GameObject preview;
    public GameObject previewInGame;
    public GameObject enemyBullet;
    public GameObject clone;
    public GameObject clone2;
    public GameObject cloneExplosion;
    public GameObject Explosion;
    public GameObject SimpleCard;

    public static int a = 350, b = 250, c = 0, destroyID;
    public static int changeEnemyHp; // change hp for enemy attack
    public int IDforEnemyChange = 0; // p----
    public bool doIt = false;
    public bool melee = false;
    public static int rand = 0;
    public static int rand2 = 0;
    public static int TURN;
    public static string ResultText;

    public Text EnemyMain, PlayerMain, EnemyHPBuldTxt, EnemyGoldBuldTxt, PlayerHPBuldTxt, PlayerGoldBuldTxt;
    public static string ChangeEnemyMain;

    public static GameManagerSrc GMS;
    public AnimationAttack at;

    public int attackOrPut = 0; // 1 - attack , 0 = put

    public List<CardGiven> PlayerHandCards = new List<CardGiven>(),
                             PlayerFieldCards = new List<CardGiven>(),
                             PlayerBuildFieldCards = new List<CardGiven>(),
                             EnemyHandCards = new List<CardGiven>(),
                             EnemyBuildFieldCards = new List<CardGiven>();
    public static List<CardGiven>EnemyFieldCards = new List<CardGiven>();



    void Start()
    {
        ChangeEnemyMain = EnemyMain.text;
        endTurnButton.onClick.AddListener(startGame);
        turn = 0;
        CurrentGame = new Game();
        GMS = new GameManagerSrc();
        at = new AnimationAttack();
        GiveHandCards(CurrentGame.EnemyDeck, EnemyHand);
        GiveHandCards(CurrentGame.PlayerDeck, PlayerHand);

    }
    public void startGame()
    {
        ChangeEnemyMain = (int.Parse(EnemyMain.text) + int.Parse(EnemyHPBuldTxt.text)).ToString();
        PlayerMain.text = (int.Parse(PlayerMain.text) + int.Parse(PlayerHPBuldTxt.text)).ToString();

        EnemyGold.text = (int.Parse(EnemyGoldBuldTxt.text) + int.Parse(EnemyGold.text)).ToString();
        Gold.text = (int.Parse(PlayerGoldBuldTxt.text) + int.Parse(Gold.text)).ToString();
        if (TURN % 5 == 0 && TURN != 0)
        {
            EnemyHPBuldTxt.text = (int.Parse(EnemyHPBuldTxt.text) + 5).ToString();
            EnemyGoldBuldTxt.text = (int.Parse(EnemyGoldBuldTxt.text) + 5).ToString();
            PlayerHPBuldTxt.text = (int.Parse(PlayerHPBuldTxt.text) + 5).ToString();
            PlayerGoldBuldTxt.text = (int.Parse(PlayerGoldBuldTxt.text) + 5).ToString();
        }
        TURN++;
        AnimationAttack.click = 0;
        AnimationAttack.firstTurn = true;
        EnemyTurn(EnemyHandCards);
    }

    void GiveHandCards(List<Card> deck, Transform hand)
    {
        int i = 0;
        while (i++ < 5)
            GiveCardToHand(deck, hand);
    }

    void GiveCardToHand(List<Card> deck, Transform hand)
    {
        if (deck.Count == 0)
            return;

        Card card = deck[0];

        GameObject cardGO = Instantiate(CardPref, hand, false);

        if (hand == EnemyHand)
        {
            cardGO.GetComponent<CardGiven>().HideCardInfo(card);
            EnemyHandCards.Add(cardGO.GetComponent<CardGiven>());
        }
        else
        {
            a += 150;
            previewInGame = Instantiate(preview, new Vector3(a, b, c), transform.rotation = Quaternion.identity);
            previewInGame.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, true);
            previewInGame.GetComponent<Image>().sprite = card.Logo;
            Destroy(previewInGame, 3f);
            cardGO.GetComponent<CardGiven>().ShowCardInfo(card);
            PlayerHandCards.Add(cardGO.GetComponent<CardGiven>());
        }
        deck.RemoveAt(0);
    }


    void GiveNewCard()
    {
        GiveCardToHand(CurrentGame.EnemyDeck, EnemyHand);
        a = 350;
        GiveCardToHand(CurrentGame.PlayerDeck, PlayerHand);
    }


    public void EnemyTurn(List<CardGiven> cards)
    {
        GiveNewCard();
        AnimationAttack.firstTurn = true;
        if (attackOrPut == 0 && EnemyHandCards.Count != 0 && int.Parse(EnemyGold.text) - cards[0].Cost >= 0)
        {
            EnemyPutCard(cards);
            Debug.Log("ENEMY COUNT CARDS:" + EnemyFieldCards.Count);
            Debug.Log("PLAYER COUNT CARDS:" + PlayerFieldCards.Count);
        }
        else
        if (attackOrPut == 1)
        {
            {
                EnemyAttackCard();
            }
        }

    }

    public void EnemyAttackCard()
    {

        rand = Random.Range(0, EnemyFieldCards.Count);
        Debug.Log("RANDOM1:" + rand);
        rand2 = Random.Range(0, PlayerFieldCards.Count);
        Debug.Log("RANDOM2:" + rand2);

        clone = Instantiate(enemyBullet, new Vector3(500, 700, 0), Quaternion.identity);
        clone.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        clone.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, true);
        clone.GetComponent<Image>().sprite = Resources.Load<Sprite>(EnemyFieldCards[rand].LogoPath);
        Debug.Log("DEBUG TEST:" + EnemyFieldCards[rand].ID);
        for(int i = 0; i < ChosenCards.selectedCards.Count; i++)
        {
            if(ChosenCards.selectedCards[i].ID == EnemyFieldCards[rand].ID)
            {
                AnimationAttack.TempLead1 = ChosenCards.selectedCards[i].Leader;
                AnimationAttack.TempAtt1 = ChosenCards.selectedCards[i].Attack;
            }
        }

        Debug.Log("ENEMY COUNT CARDS:" + EnemyFieldCards.Count);

        clone2 = Instantiate(enemyBullet, new Vector3(500, 0, 0), Quaternion.identity);
        clone2.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        clone2.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, true);
        clone2.GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerFieldCards[rand2].LogoPath);
        IDforEnemyChange = PlayerFieldCards[rand2].ID;
        Debug.Log("PLAYER COUNT CARDS:" + PlayerFieldCards.Count);
        for (int i = 0; i < ChosenCards.selectedCards.Count; i++)
        {
            if (ChosenCards.selectedCards[i].ID == PlayerFieldCards[rand2].ID)
            {
                AnimationAttack.TempHP2 = ChosenCards.selectedCards[i].HP ;
                AnimationAttack.TempDef2 = ChosenCards.selectedCards[i].Defense;
            }
        }
        AnimationAttack.TempChecking = AnimationAttack.TempDef2 - (AnimationAttack.TempAtt1 + AnimationAttack.TempLead1);
        if (AnimationAttack.TempChecking < 0)
        {
            Debug.Log("CHECK" + AnimationAttack.TempChecking);
            AnimationAttack.formula = AnimationAttack.TempHP2 + AnimationAttack.TempChecking;
        }
        else
        {
            AnimationAttack.formula = AnimationAttack.TempHP2;
        }


        clone2.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 20) * 620);
        clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -20) * 520);
        attackOrPut = 0;
        Invoke("delayattack", 1.62f);
    }

    public void EnemyPutCard(List<CardGiven> cards)
    {

        int field = Random.Range(0, 2);
        int counts = Random.Range(1, EnemyHandCards.Count);             // 2 card

        for (int i = 0; i < counts; i++)
        {

            if (field == 1)
            {
                if ((int.Parse(EnemyGold.text) - cards[0].Cost) > 0)
                {
                    EnemyGold.text = (int.Parse(EnemyGold.text) - cards[0].Cost).ToString();
                    cards[0].ShowCardInfo(cards[0].SelfCard);
                    cards[0].transform.SetParent(EnemyFirstField);
                }
                else return;
            }
            else
            {
                //Debug.Log((int.Parse(EnemyGold.text) - cards[0].Cost).ToString());
                Debug.Log(cards[0].Cost);
                if ((int.Parse(EnemyGold.text) - cards[0].Cost) > 0)
                {
                    EnemyGold.text = (int.Parse(EnemyGold.text) - cards[0].Cost).ToString();
                    cards[0].ShowCardInfo(cards[0].SelfCard);
                    cards[0].transform.SetParent(EnemySecondField);
                }
                else
                {
                    return;
                }
            }

            EnemyFieldCards.Add(cards[0]);
            EnemyHandCards.Remove(cards[0]);
            attackOrPut = 1;

        }
    }
    public void delayattack()
    {
        cloneExplosion = Instantiate(Explosion, clone.transform.position, clone.transform.rotation = Quaternion.identity);
        cloneExplosion.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, true);
        Destroy(clone);
        Destroy(clone2);
        attackOrPut = 0;
        doIt = false;
        ChangeForEnemyAttack();
    }
    public void ChangeForEnemyAttack()
    {
        changeEnemyHp = AnimationAttack.formula;
        Debug.Log("TEMPP:" + IDforEnemyChange);
        for (int i = 0; i < ChosenCards.selectedCards.Count; i++)
        {
            if (ChosenCards.selectedCards[i].ID == IDforEnemyChange)
            {
                Debug.Log("ZAISHLO");
                
                var Temp = ChosenCards.selectedCards[i];
                Temp.HP = changeEnemyHp;
                Debug.Log(Temp.HP);
                if (changeEnemyHp > 0)
                {
                    Temp.Leader--;
                }
                if (Temp.HP <= 0)
                {
                    destroyID = IDforEnemyChange;
                    if (int.Parse(PlayerMain.text) - 50 < 0)
                    {
                        ResultText = "YOU ARE LOSER";
                        SceneManager.LoadScene("ResultScene");
                        Debug.Log("END GAME");
                    }
                    else
                    {
                        PlayerMain.text = (int.Parse(PlayerMain.text) - 50).ToString();
                    }
                    PlayerFieldCards.RemoveAt(rand2);
                }
                ChosenCards.selectedCards[i] = Temp;
                DestroyCardHP.was = false;
            }
        }
    }

    public static void ChangeForPlayerAttack(Card card)
    {
        changeEnemyHp = AnimationAttack.formula;
        DestroyCardHP.was = false;
        Debug.Log("Im HERE");
        Debug.Log("Compare:" + card.ID);
       
        for (int i = 0; i < ChosenCards.selectedCards.Count; i++)
        {
            if (ChosenCards.selectedCards[i].ID == card.ID)
            {
                var Temp = ChosenCards.selectedCards[i];
                Temp.HP = changeEnemyHp;

                if (changeEnemyHp > 0)
                {
                    Temp.Leader--;
                }
                if (Temp.HP <= 0)
                {
                    destroyID = card.ID;
                    if (int.Parse(ChangeEnemyMain) - 50 < 0)
                    {
                        ResultText = "YOU ARE WINNER";
                        SceneManager.LoadScene("ResultScene");
                    }
                    else
                    {
                        ChangeEnemyMain = (int.Parse(ChangeEnemyMain) - 50).ToString();
                    }
                    for (int j = 0; j < EnemyFieldCards.Count; j++)
                    {
                        if (EnemyFieldCards[j].ID == card.ID)
                        {
                            EnemyFieldCards.RemoveAt(j);
                        }
                    }
                }
                ChosenCards.selectedCards[i] = Temp;
                AnimationAttack.tempAttackCard.RemoveAt(0);
            }
        }
    }
    private void Update()
    {
        EnemyMain.text = ChangeEnemyMain;
    }
}