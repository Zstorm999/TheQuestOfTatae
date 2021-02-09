﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void changeAnimState(Entity.Direction dir, Entity.Action act, float speed)
    {
        if (currentSate.direction == dir && currentSate.action == act)
        {
            anim.speed = speed;
            return;
        }

        try
        {
            currentSate = searchForAnim(dir, act);
        }
        catch(KeyNotFoundException e)
        {
            Debug.Log(e.ToString());
            currentSate = states[0];
        }


        anim.Play(currentSate.name);
        anim.speed = speed;
        


    }

    private Animation searchForAnim(Entity.Direction dir, Entity.Action act)
    {
        foreach(Animation a in states){

            if (a.direction == dir && a.action == act)
                return a;

        }

        throw new KeyNotFoundException("Unable to find an animation for this combination of states:(" + dir + ", " + act + ")");

    }

    [System.Serializable]
    public struct Animation
    {
        public string name;
        public Entity.Direction direction;
        public Entity.Action action;
    }

}