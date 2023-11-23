using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPart2Controller : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject Player;
    [SerializeField] float rotateSpeed;
    private bool isStanding;
    private bool isWalking;
    private bool isCrouching;

    // Start is called before the first frame update
    void Start()
    {
        isWalking = true;   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = Input.GetAxisRaw("Horizontal") * Vector3.right + Input.GetAxisRaw("Vertical") * Vector3.forward;
        moveInput = Vector3.ClampMagnitude(moveInput, 1);

        transform.Translate(moveInput * Time.deltaTime * 4); //The last number is the speed

        // Set local variables to Animator
        animator.SetBool("isStanding", isStanding);
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isCrouching", isCrouching);

        Quaternion playerRotation = Player.transform.rotation;
        Quaternion targetRotation = Quaternion.identity;

        if (moveInput != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(moveInput);
            Player.transform.rotation = Quaternion.Slerp(playerRotation, targetRotation, rotateSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stand"))
        {
            isStanding = true;
            isWalking = false;
        }
        if (other.CompareTag("Crouch"))
        {
            isCrouching = true;
            isWalking = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Stand"))
        {
            isStanding = false;
            isWalking = true;
        }
        if (other.CompareTag("Crouch"))
        {
            isCrouching = false;
            isWalking = true;
        }
    }
}
