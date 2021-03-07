using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity;

public class HealerAttack : PlayerAttack
{
    //above defined

    //protected bool isAttacking;
    //protected int activeAttack = -1;
    //private float[] attackLatency;

    [SerializeField]
    private GameObject healProjectile;

    [SerializeField]
    private float healStartDelay;

    [SerializeField]
    private GameObject damagePotionProjectile;

    [SerializeField]
    private float damagePotionStartDelay;


    public override void PerformAttack(int attackNumber, Direction dir)
    {
        
        if (!isAttacking)
        {
            if (attackNumber >=0  && attackNumber < 2)
            {//if we are doing a valid attack

                isAttacking = true;
                activeAttack = attackNumber;

                switch (attackNumber)
                {
                    case 0:
                        StartCoroutine(heal(dir));
                        break;

                    case 1:
                        StartCoroutine(damagePotion(dir));
                        break;
                }

                StartCoroutine(resetAttack(attackNumber));
            }
        }
    }


    //heal is attack 0
    private IEnumerator heal(Direction dir)
    {
        yield return new WaitForSeconds(healStartDelay);

        GameObject projectile = Instantiate(healProjectile, transform.position, Quaternion.identity);

        HealerHealProjectile proj = projectile.GetComponent<HealerHealProjectile>();

        proj.DestroyLater();
        proj.direction = dir.GetVector2();

    }

    //damage potion is attack 1
    private IEnumerator damagePotion(Direction dir)
    {
        yield return new WaitForSeconds(damagePotionStartDelay);

        GameObject projectile = Instantiate(damagePotionProjectile, transform.position, Quaternion.identity);
        HealerDamagePotion proj = projectile.GetComponent<HealerDamagePotion>();

        proj.DestroyLater();
        proj.direction = dir.GetVector2();

    }

    
}
