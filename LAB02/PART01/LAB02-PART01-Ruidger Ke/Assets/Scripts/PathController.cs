using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [SerializeField] public PathManger pathManager;
    [SerializeField] private bool idleOnHit;

    List<Waypoints> thePath;
    Waypoints target;

    public float MoveSpeed;
    public float RotateSpeed;

    public Animator animator;
    bool isWalking;

    // Start is called before the first frame update
    void Start()
    {
        isWalking = false;
        animator.SetBool("isWalking", isWalking);
        thePath = pathManager.Getpath();
        if (thePath != null && thePath.Count > 0)
        {
            target = thePath[0];
        }

    }

    void rotateTowardsTarget()
    {
        float stepSize = RotateSpeed * Time.deltaTime;

        Vector3 targetDir = target.pos - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepSize, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
    void moveForward()
    {
        float stepSize = Time.deltaTime * MoveSpeed;
        float distanceToTarget = Vector3.Distance(transform.position, target.pos);
        if (distanceToTarget < stepSize)
        {
            return;
        }
        Vector3 moveDir = Vector3.forward;
        transform.Translate(moveDir * stepSize);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            isWalking = !isWalking;
            animator.SetBool("isWalking", isWalking );

        }

        if (isWalking)
        {
            rotateTowardsTarget();
            moveForward();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        target = pathManager.GetNextTarget();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (idleOnHit && collision.transform.CompareTag("Collide"))
        {
            isWalking = !isWalking;
            animator.SetBool("isWalking", isWalking);
        }
    }
}
