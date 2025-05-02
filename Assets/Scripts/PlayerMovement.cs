using UnityEngine;

public class Movement : MonoBehaviour
{
    public Animator animator;
    public float speed;
    private float Move;

    private Rigidbody2D rb;
    private EdgeCollider2D waterCollider1;
    private EdgeCollider2D waterCollider2;
    public GameObject river1;
    public GameObject river2;

    public float jump;

    public bool isJumping;

    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        waterCollider1 = river1.gameObject.GetComponent<EdgeCollider2D>();
        waterCollider2 = river2.gameObject.GetComponent<EdgeCollider2D>();
    }

    void Update()
    {
        Move = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(Move));

        rb.velocity = new Vector2(Move * speed, rb.velocity.y);

        if (Move > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (Move < 0 && isFacingRight)
        {
            Flip();
        }

        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
            animator.SetBool("IsJumping", true);
            Debug.Log("Jumping");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Water"))
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
            Debug.Log("Grounded");
        }
        if (other.gameObject.CompareTag("Diamond"))
        {
            waterCollider1.enabled = true;
            waterCollider2.enabled = true;
            Debug.Log("Power Up Activated!");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Water"))
        {
            isJumping = true;
            Debug.Log("on the fly");
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
