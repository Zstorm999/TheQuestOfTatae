using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimation : MonoBehaviour
{

    [SerializeField]
    private List<Animation> states;

    private Animation currentSate;

    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void changeAnimState(Entity.Direction dir, Entity.Action act)
    {
        if (currentSate.direction == dir && currentSate.action == act)
        {
            //no need to update if we are already in the current state
            return;
        }

        try
        {
            currentSate = searchForAnim(dir, act);
        }
        catch(KeyNotFoundException e)
        {
            Debug.Log(e.StackTrace);
            currentSate = states[0];
        }


        anim.Play(currentSate.animationName);
        
    }

    private Animation searchForAnim(Entity.Direction dir, Entity.Action act)
    {

        //remove 1 since dir starts with NONE, which is actually never used for the animation
        int tryPos = 4 * (int)act + (int)dir - 1;

        if (tryPos < states.Count) {
            Animation a = states[tryPos];
            if (a.direction == dir && a.action == act)
            {
                //this is a standard list !
                return a;
            }
        }

        foreach(Animation a in states){

            if (a.direction == dir && a.action == act)
                return a;

        }


        throw new KeyNotFoundException("Unable to find an animation for this combination of states:(" + dir + ", " + act + ")");

    }

    
    [System.Serializable]
    public struct Animation
    {
        [Tooltip("Displayed name, has no effect outside of the editor")]
        public string displayName;

        [Tooltip("Must match the name given to the state in the Unity Animator")]
        public string animationName;
        
        public Entity.Direction direction;
        
        public Entity.Action action;
    }

}
