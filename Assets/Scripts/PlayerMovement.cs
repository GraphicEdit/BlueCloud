using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float backSpeed;


    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;

    private CharacterController controller;
    private Animator anim;

    private float moveWay;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

    }


    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        controller.Move(velocity * Time.deltaTime);
        moveWay = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(0, 0, moveWay);

        moveDirection = transform.TransformDirection(moveDirection);
        velocity.y += gravity * Time.deltaTime;

        Move();

    }


    private void Move()
    {

        //transform.rotation = Quaternion.Euler(0.0f, -90f, 0.0f);

        //Walk
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {

            anim.SetBool("toRun", false);
            anim.SetBool("toBack", false);
            anim.SetBool("toJump", false);
            anim.SetBool("toWalk", true);
            moveDirection *= walkSpeed;
            controller.Move(moveDirection * Time.deltaTime);
        }

        //Run
        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            anim.SetBool("toBack", false);
            anim.SetBool("toWalk", false);
            anim.SetBool("toRun", true);
            moveDirection *= runSpeed;
            controller.Move(moveDirection * Time.deltaTime);
        }

        //Jump
        //else if (moveDirection == Vector3.zero && Input.GetKeyDown(KeyCode.Space) && isGrounded)
        else if (Input.GetKeyDown(KeyCode.Space) && !Input.GetKey(KeyCode.LeftArrow) && isGrounded)
        {

            anim.SetBool("toJump", true);
            velocity.y = math.sqrt(jumpHeight * gravity * -2);
            transform.Translate(0, 0, 0.2f);

        }

        //Idle
        else if (moveDirection == Vector3.zero && isGrounded)
        {
            anim.SetBool("toRun", false);
            anim.SetBool("toBack", false);
            anim.SetBool("toWalk", false);
            anim.SetBool("toJump", false);
            anim.SetBool("toIdle", true);


        }

        //Back
        if (Input.GetKey(KeyCode.LeftArrow) && isGrounded)
        {
            anim.SetBool("toRun", false);
            anim.SetBool("toWalk", false);
            anim.SetBool("toJump", false);
            anim.SetBool("toBack", true);
            moveDirection *= backSpeed;
            controller.Move(moveDirection * Time.deltaTime);

        }






        //controller.Move(moveDirection * Time.deltaTime);
        //velocity.y += gravity * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);



    }




}