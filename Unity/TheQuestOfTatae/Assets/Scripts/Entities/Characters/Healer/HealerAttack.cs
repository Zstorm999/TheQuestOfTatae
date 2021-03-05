using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerAttack : PlayerAttack
{
    //above defined

    //protected bool isAttacking;
    //protected int activeAttack = -1;
    //private float[] attackLatency;

    public override void PerformAttack(int attackNumber)
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
                        heal(); 
                        break;
                }

                StartCoroutine(resetAttack(attackNumber));
            }
        }
    }


    //heal is attack 0
    private void heal()
    {
        Debug.Log("Heal");
    }

    
}
