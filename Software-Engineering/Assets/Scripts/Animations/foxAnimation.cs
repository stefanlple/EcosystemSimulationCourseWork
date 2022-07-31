using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxAnimation : MonoBehaviour
{
    private FoxMovement movement;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponentInParent<FoxMovement>();
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
        return (movement.isWandering || movement.isHunting) && movement.fox.isAlive;
    }

    public bool getIsAlive()
    {
        return movement.fox.isAlive;
    }

    public bool getIsEating()
    {
        return movement.fox.isEating;
    }

    public bool getIsDrinking()
    {
        return movement.fox.isDrinking;
    }
}
