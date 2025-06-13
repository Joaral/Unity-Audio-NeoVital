using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DamageType { None, Fire, Ice };

public class LifeTaker : MonoBehaviour
{
    [Header("Damage Settings")]
    public int damage;
    public bool skipInvencible = false;
    public string tagTarget;
    public bool destroyOnAttack;
    private bool hitSomeone = false;
    
    public DamageType dmgType;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hitSomeone)
        {
            if (string.IsNullOrWhiteSpace(tagTarget) || collision.CompareTag(tagTarget))
            {
                if (damage > 0)
                {
                    hitSomeone = true;
                    collision.GetComponent<LifeContainer>().Damage(damage, dmgType);
                    Debug.Log(this + "collisioned with " + collision);

                }
                if (destroyOnAttack)
                {
                    hitSomeone = true;
                    Destroy(gameObject);
                }
            }
        }
        
    }
}
