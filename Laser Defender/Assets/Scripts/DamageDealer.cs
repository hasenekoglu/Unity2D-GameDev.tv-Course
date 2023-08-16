using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damge = 10;

    public int GetDamage()
    {
        return damge;
    }
    public void Hit()
    {
        Destroy(gameObject);
    }
}

