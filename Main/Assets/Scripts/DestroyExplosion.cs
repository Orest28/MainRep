using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExplosion : MonoBehaviour
{
    private void FixedUpdate()
    {
        Destroy(gameObject, 3.0f);
    }
}
