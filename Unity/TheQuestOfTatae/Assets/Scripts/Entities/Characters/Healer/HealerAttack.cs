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
    [Range(0f, 1f)]
    private float healStartDelay;

    public override void PerformAttack(int attackNumber, Direction dir)
    {
        
        if (!isAttacking)
        {
            if (attackNumber >=0  && attackNumber < 1)
            {//if we are doing a valid attack

                isAttacking = true;
                activeAttack = attackNumber;

                switch (attackNumber)
                {
                    case 0:
                        StartCoroutine(heal(dir));
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

        GameObject projectile =  Instantiate(healProjectile, transform.position, Quaternion.identity);

        HealerHealProjectile proj = projectile.GetComponent<HealerHealProjectile>();

        proj.DestroyLater();
        proj.direction = dir;

    }

    
}
