using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetFloat("PoseValue", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
