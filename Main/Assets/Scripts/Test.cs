using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject test1;
    public GameObject test2;

    private void Start()
    {
        test1.GetComponent<Rigidbody2D>().AddForce(new Vector2(-20, 0) * 150);
        test1.tag = "enemyCards";
        test2.GetComponent<Rigidbody2D>().AddForce(new Vector2(20, 0) * 150);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemyCards"))
        {
            Destroy(test1);
            Destroy(test2);
        }
    }

}
