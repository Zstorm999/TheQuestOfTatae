using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity;

public class PlayerController : MonoBehaviour
{

    private PlayerAnimation animator;

    [SerializeField]
    private float velocity;

    [SerializeField]
    [Range(0f, .5f)]
    private float deadZone;

    private float xMov, yMov;

    [SerializeField]
    private Direction dirPrimary;
    //private Direction dirSecundary;

    private bool hasMaintainedDir;

    [SerializeField]
    private Action action;

    private Direction newDirPrimary;
   

    private void Start()
    {
        animator = GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    private void Update()
    {
        newDirPrimary = Direction.NONE;
        hasMaintainedDir = false;

        xMov = Input.GetAxis("Horizontal");
        yMov = Input.GetAxis("Vertical");

        xMov = applyDeadZone(xMov);
        yMov = applyDeadZone(yMov);

        if(xMov > 0)
        {
            action = Action.WALK;
            setNewDir(Direction.RIGHT);
        }
        else if(xMov < 0)
        {
            action = Action.WALK;
            setNewDir(Direction.LEFT);
        }

        if (yMov > 0)
        {
            //animator.changeAnimState(Direction.UP, Action.WALK);
            action = Action.WALK;
            setNewDir(Direction.UP);
        }
        else if (yMov < 0)
        {
            //animator.changeAnimState(Direction.DOWN, Action.WALK);
            action = Action.WALK;
            setNewDir(Direction.DOWN);

        }

        //IDLE
        if(xMov == 0 && yMov == 0)
        {
            action = Action.NONE;

        }

        if (!hasMaintainedDir && newDirPrimary != Direction.NONE)
        {
            dirPrimary = newDirPrimary;
        }

        animator.changeAnimState(dirPrimary, action);
    }

    //Fixed Update for physic calculations that need stable time interval
    private void FixedUpdate()
    {
        //xMov = Mathf.Round(xMov);
        //yMov = Mathf.Round(yMov);

        

        Debug.Log("x: " + xMov + ",y: " + yMov);
        Vector3 newPos = new Vector3(xMov, yMov, 0)*Time.deltaTime*velocity;

        transform.position += newPos;

    }

    private float applyDeadZone(float var)
    {
        if (var == 0) return 0;

        int sign = (var < 0) ? -1 : 1;

        var = Mathf.Abs(var);

        if (var < deadZone) return 0;
        return sign;
    }

    private void setNewDir(Direction dir)
    {
        if (dir == dirPrimary)
            hasMaintainedDir = true;
        else
            newDirPrimary = dir;

    }
}
