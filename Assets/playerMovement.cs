using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Transform mainCamera;
    public CharacterController2D controller;
    public Animator animator;
    public Collider2D fallCheck;
    public SpriteRenderer playerRenderer;
    float horizontalMove = 0f;
    public float runSpeed = 40f;
    bool jumpCheck = false;
    bool crouchCheck = false;
    Color healthColor;
    [Range(0, 100)] [SerializeField] public float health = 100f;

    // Update is called once per frame
    void Update()
    {
        
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump"))
        {
            jumpCheck = true;
            animator.SetBool("Jump", true);
            
        }
        //Debug.Log(fallCheck);
        
        if (Input.GetButtonDown("Crouch"))
        {

            crouchCheck = true;
            animator.SetBool("Crouch", true);

        } else if (Input.GetButtonUp("Crouch"))
        {

            crouchCheck = false;
            
        }
        if(!controller.m_Grounded && jumpCheck == false)
        {
            animator.SetBool("Jump", true);
        }
        if(controller.m_Grounded && jumpCheck == false)
        {
            animator.SetBool("Jump", false);
        }
        if (Input.GetButtonDown("Melee"))
        {
            animator.SetBool("Melee", true);

        }
        if (Input.GetButtonUp("Melee"))
        {
            animator.SetBool("Melee", false);
        }

        healthColor = new Color(1f, (health/100f), (health/100f));

        playerRenderer.color = healthColor;


    }
    void FixedUpdate()
    {
        Vector3 cameraPos = new Vector3 (this.transform.position.x, this.transform.position.y, -10f);
        mainCamera.position = cameraPos;
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouchCheck, jumpCheck);
        jumpCheck = false;
    }

    public void OnJump()
    {
        animator.SetBool("Jump", false);
        
    }

    public void OnCrouch(bool crouchCheck)
    {

        animator.SetBool("Crouch", crouchCheck);
    }
    
}
    