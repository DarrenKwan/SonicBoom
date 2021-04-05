using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float speed = 12f;

    private Vector3 velocity;

    public float gravity = -9.81f;

    public Transform groundchecker;

    public float groundDistance = 0.4f;

    public LayerMask groundMask;

    private bool isGrounded;

    public float jumpHeight = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundchecker.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        Vector3 moveMovement = transform.right * xMovement + transform.forward * zMovement;

        rb.AddForce(moveMovement * speed * Time.deltaTime);
        /*
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        rb.AddForce(velocity * Time.deltaTime);
        */
    }
}
