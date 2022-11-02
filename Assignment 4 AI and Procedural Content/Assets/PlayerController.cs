using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public float jumpForce = 7f;

    private Rigidbody2D rb2D;
    private bool isJumping = false;
    private float moveHorizontal;
    private float moveVertical;

    // Start is called before the first frame update
    void Start()
    {
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
        if (moveHorizontal >= 0.01f || moveHorizontal <= -0.01f)
        {
            // AddForce() has applied "* Time.deltatime" as default in ForceMode
            rb2D.AddForce(new Vector2(moveHorizontal * (speed / 4), 0f), ForceMode2D.Impulse);
        }

        if (!isJumping && moveVertical >= 0.01f)
        {
            // AddForce() has applied "* Time.deltatime" as default in ForceMode
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = true;
        }
    }
}
