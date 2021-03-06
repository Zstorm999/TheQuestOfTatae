﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Entity;

[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerAttack))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset playerActionsAsset;

    //actions input
    private InputActionMap playerActions;

    //animation
    private PlayerAnimation animator;

    //attack
    private PlayerAttack attack;


    //player movement
    [SerializeField]
    private float velocity;

    [SerializeField]
    [Range(0f, 1f)]
    private float startDeadZone;

    [SerializeField]
    [Range(0f, 1f)]
    private float endDeadZone;

    private float xMov, yMov;
    private float xMovPrev, yMovPrev;


    //State
    [SerializeField]
    private Direction dirPrimary;

    private Vector2 TurretDirection;

    [SerializeField]
    private Action action;


    private void Awake()
    {
        //moveAction.performed += onMove;
        //attack1Action.performed += onAttack1;

        playerActions = playerActionsAsset.FindActionMap("Player");
    }

    private void OnEnable()
    {
        playerActions.Enable();
    }

    private void Start()
    {
        animator = GetComponent<PlayerAnimation>();
        attack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    private void Update()
    {

        //reading input
        Vector2 mov = playerActions["Move"].ReadValue<Vector2>();
        bool isAttack0Pressed = playerActions["Attack 0"].triggered;
        bool isAttack1Pressed = playerActions["Attack 1"].triggered;

        this.TurretDirection = playerActions["AimPoint"].ReadValue<Vector2>();

        //FIXME
        /**the mouse position is currently giving the position on the screen;
         * we need the position relative to the player
         */
        this.TurretDirection.Normalize();

        xMov = mov.x;
        yMov = mov.y;

        //appying corrections
        float xMovCorrect = applyDeadZone(xMov, xMovPrev);
        float yMovCorrect = applyDeadZone(yMov, yMovPrev);
        //we can delay applying the correction to a later time inside update, since FixedUpdate and Update are never called concurrently (checked on the doc)

        Direction newDir =  computeDirection();


        //now we compute the action performed
        if(xMovCorrect != 0 || yMovCorrect != 0) //if one of the two is not 0, we should move
        {
            action = Action.WALK;
        }
        else
        {
            action = Action.NONE;
        }

        if (isAttack0Pressed) //button pressed
        {
            attack.PerformAttack(0, newDir, TurretDirection);
        }
        else if (isAttack1Pressed)
        {
            attack.PerformAttack(1, newDir, TurretDirection);
        }



        switch (attack.activeAttack)
        {
            case 0:
                action = Action.ATTACK_0;
                newDir = dirPrimary; //attack maintains direction
                break;

            case 1:
                action = Action.ATTACK_1;
                newDir = dirPrimary; //attack maintains directions
                break;
        }


        dirPrimary = newDir;

        animator.changeAnimState(dirPrimary, action);


        //appyling corrections
        //prev values take the real value, not the corrected one
        xMovPrev = xMov;
        yMovPrev = yMov;

        xMov = xMovCorrect;
        yMov = yMovCorrect;
        normalizeVelocity();

    }

    //Fixed Update for physic calculations that need stable time interval
    private void FixedUpdate()
    {
        switch (action)
        {
            case Action.WALK:
                Vector3 newPos = new Vector3(xMov, yMov, 0) * Time.fixedDeltaTime * velocity;
                transform.position += newPos;
                break;
        }

    }

    private float applyDeadZone(float var, float varPrev)
    {
        if (var == 0) return 0;

        int sign = (var < 0) ? -1 : 1;


        //here we have to check if we are accelerating or decelerating.

        float varAbs = Mathf.Abs(var);
        float varPrevAbs = Mathf.Abs(varPrev);


        if (varAbs >= varPrevAbs)
        {
            if (varAbs < startDeadZone) return 0f;
        }
        else if(varAbs < varPrevAbs)
        {
            if (varAbs < endDeadZone) return 0f;
        }

        return var;
    }

    private void normalizeVelocity()
    {
        float factor = Mathf.Sqrt(xMov * xMov + yMov * yMov);

        if(factor > 1) //we do not want to rescale if max velocity has not been reached
        {
            xMov /= factor;
            yMov /= factor;
        }
    }


    private Direction computeDirection()
    {
        Direction dir;

        float gap = 0.05f; //5% gap

        if(Mathf.Abs(xMov) > Mathf.Abs(yMov) + gap) // consider x Axis as the main direction
        {
            dir =new Vector2(xMov, 0).GetDirection();
        }
        else if(Mathf.Abs(yMov) > Mathf.Abs(xMov) + gap) // consider y Axis as the main direction
        {
            dir = new Vector2(0, yMov).GetDirection();
        }
        else //both are equal, meaning an almost 45deg angle
        {
            //in this case, we keep the direction as it was, waiting for next input
            dir = dirPrimary;
        }

        return dir;
    }


}