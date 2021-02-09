using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimation : MonoBehaviour
{

    [SerializeField]
    private List<string> states;

    [SerializeField]
    protected State currentSate;

    [SerializeField]
    protected Animator anim;

    public void changeAnimState(State newState)
    {
        //no need to update state
        if (newState == currentSate) return;

        anim.Play("Walking_up");

        currentSate = newState;

    }
}

public enum State { IDLE, UP, DOWN, LEFT, RIGHT };