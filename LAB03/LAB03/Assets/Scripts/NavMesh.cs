using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour
{
    public GameObject Target;
    private NavMeshAgent agent;

    private Animator animator;
    bool IsSprinting = true;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (IsSprinting)
            agent.destination = Target.transform.position;
        else
        {
            agent.destination = transform.position;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Blue")
        {
            IsSprinting = false;
            animator.SetTrigger("Attacking");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Blue")
        {
            IsSprinting = true;
            animator.SetTrigger("Sprinting");
        }
    }

}
