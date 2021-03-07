using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity;

public abstract class PlayerAttack : MonoBehaviour
{

    public bool isAttacking { get; protected set; } = false;

    public int activeAttack { get; protected set; } = -1;

    [SerializeField]
    [Range(0f, 5f)]
    private float[] attackLatency;


    public abstract void PerformAttack(int attackNumber, Direction dir);


    //callback method to reset the attack state
    protected IEnumerator resetAttack(int attackNumber)
    {
        yield return new WaitForSeconds(attackLatency[attackNumber]);
        activeAttack = -1;
        isAttacking = false;
        Debug.Log("Attack " + attackNumber + " finished");
    }
}
