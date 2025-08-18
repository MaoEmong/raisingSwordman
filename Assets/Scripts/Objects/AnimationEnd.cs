using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEnd : MonoBehaviour
{
    Animator anim;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
	}

    void Update()
    {
        if (anim != null)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
                Managers.Pool.Push(gameObject);
        }
        


    }
}
