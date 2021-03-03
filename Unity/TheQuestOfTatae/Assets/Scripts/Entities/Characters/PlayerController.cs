using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity;

public class PlayerController : MonoBehaviour
{

    private PlayerAnimation animator;
    private float playSpeed;

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

        float xMovTmp = applyDeadZone(xMov, xMovPrev);
        float yMovTmp = applyDeadZone(yMov, yMovPrev);

        xMovPrev = xMov;
        yMovPrev = yMov;

        xMov = xMovTmp;
        yMov = yMovTmp;

        normalizeVelocity();

        playSpeed = Mathf.Sqrt(xMov * xMov + yMov * yMov);

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
            action = Action.WALK;
            setNewDir(Direction.UP);
        }
        else if (yMov < 0)
        {
            action = Action.WALK;
            setNewDir(Direction.DOWN);

        }

        //IDLE
        if (xMov == 0 && yMov == 0)
        {
            action = Action.NONE;
            playSpeed = 1f;

        }

        if (!hasMaintainedDir && newDirPrimary != Direction.NONE)
        {
            dirPrimary = newDirPrimary;
        }

        animator.changeAnimState(dirPrimary, action, playSpeed);
    }

    //Fixed Update for physic calculations that need stable time interval
    private void FixedUpdate()
    {
        //xMov = Mathf.Round(xMov);
        //yMov = Mathf.Round(yMov);



        //Debug.Log("x: " + xMov + ",y: " + yMov);
        Debug.Log(xMov * xMov + yMov * yMov);

        Vector3 newPos = new Vector3(xMov, yMov, 0) * Time.deltaTime * velocity;

        transform.position += newPos;

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

    private void setNewDir(Direction dir)
    {
        if (dir == dirPrimary)
            hasMaintainedDir = true;
        else
            newDirPrimary = dir;

    }
}