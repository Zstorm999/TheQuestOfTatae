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

        //appying corrections
        float xMovCorrect = applyDeadZone(xMov, xMovPrev);
        float yMovCorrect = applyDeadZone(yMov, yMovPrev);
        //we can delay applying the correction to a later time inside update, since FixedUpdate and Update are never called concurrently (checked on the doc)


        //checking input for direction (independantly from the fact that we should move)
        if(xMov > 0)
        {
            setNewDir(Direction.RIGHT);
        }
        else if(xMov < 0)
        {
            setNewDir(Direction.LEFT);
        }

        if(yMov > 0)
        {
            setNewDir(Direction.UP);
        }
        else if(yMov < 0)
        {
            setNewDir(Direction.DOWN);
        }

        if(xMovCorrect != 0 || yMovCorrect != 0) //if one of the two is not 0, we should move
        {
            action = Action.WALK;
        }
        else
        {
            action = Action.NONE;
        }

        

        if (!hasMaintainedDir && newDirPrimary != Direction.NONE)
        {
            dirPrimary = newDirPrimary;
        }

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