using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float speed = 1f;
    public float jumpForce = 10f;

    private Rigidbody2D rb2D;
    private bool isJumping = false;
    private float moveHorizontal;
    private float moveVertical;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        float horizontalSpeed = moveHorizontal * (speed / 4);
        animator.SetFloat("Speed", Mathf.Abs(horizontalSpeed));

        if (moveHorizontal >= 0.01f || moveHorizontal <= -0.01f)
        {
            if (moveHorizontal >= 0.01f)
            {
                transform.localScale = new Vector3(5f, 5f, 5f);
            }
            else if (moveHorizontal <= -0.01f)
            {
                transform.localScale = new Vector3(-5f, 5f, 5f);
            }

            // AddForce() has applied "* Time.deltatime" as default in ForceMode
            //rb2D.AddForce(new Vector2(moveHorizontal * (speed / 4), 0f), ForceMode2D.Impulse);
            rb2D.AddForce(new Vector2(horizontalSpeed, 0f), ForceMode2D.Impulse);
        }

        if (!isJumping && moveVertical >= 0.01f)
        {
            // AddForce() has applied "* Time.deltatime" as default in ForceMode
            animator.SetBool("isJumping", true);
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
        }
    }
}
