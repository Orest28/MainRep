using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCardHP : MonoBehaviour
{
    public int inDestroyID;
    public GameObject cloneExplosion;
    public GameObject Explosion;
    public GameObject destroyCard;
    public static bool was = false;
    public static bool was2 = false;

    void Update()
    {
        if (destroyCard.GetComponent<CardGiven>().ID == GameManagerSrc.destroyID && destroyCard.GetComponent<CardGiven>().onDesk == true && was == false)
        {
            cloneExplosion = Instantiate(Explosion, destroyCard.transform.position, destroyCard.transform.rotation = Quaternion.identity);
            cloneExplosion.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, true);
            Destroy(destroyCard);
            was = true;
        }
    }
}
