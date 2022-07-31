using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HareAnimation : MonoBehaviour
{
    private HareMovement movement;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponentInParent<HareMovement>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isMoving", getIsMoving());
        animator.SetBool("isEating", getIsEating() || getIsDrinking());
        animator.SetBool("isAlive", getIsAlive());
    }

    public bool getIsMoving()
    {
        return (movement.isWandering || movement.isFleeing || (movement.hare.isHungry && movement.hare.hasFoundGrass() || movement.hare.isThirsty)) && !movement.hare.isEating && !movement.hare.isDrinking && movement.hare.isAlive;
    }

    public bool getIsEating()
    {
        return movement.hare.isEating && movement.hare.isAlive;
    }

    public bool getIsAlive()
    {
        return movement.hare.isAlive;
    }

    public bool getIsDrinking()
    {
        return movement.hare.isDrinking;
    }
}
