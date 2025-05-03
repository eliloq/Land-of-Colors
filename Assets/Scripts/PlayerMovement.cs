using UnityEngine;
using UnityEngine.SceneManagement;

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
        // گرفتن مرجع Rigidbody2D و Colliders
        rb = GetComponent<Rigidbody2D>();
        waterCollider1 = river1.gameObject.GetComponent<EdgeCollider2D>();
        waterCollider2 = river2.gameObject.GetComponent<EdgeCollider2D>();
    }

    void Update()
    {
        // گرفتن ورودی حرکت افقی
        Move = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(Move)); // تنظیم مقدار سرعت برای انیمیشن

        // اعمال حرکت به Rigidbody
        rb.velocity = new Vector2(Move * speed, rb.velocity.y);

        // بررسی جهت حرکت و چرخاندن کاراکتر
        if (Move > 0 && !isFacingRight)
        {
            Flip(); // چرخاندن کاراکتر به سمت راست
        }
        else if (Move < 0 && isFacingRight)
        {
            Flip(); // چرخاندن کاراکتر به سمت چپ
        }

        // بررسی ورودی پرش
        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump)); // اعمال نیروی پرش
            animator.SetBool("IsJumping", true); // فعال کردن انیمیشن پرش
            Debug.Log("Jumping");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // بررسی برخورد با زمین یا آب یا بشکه
        if (
            other.gameObject.CompareTag("Ground")
            || other.gameObject.CompareTag("Water")
            || other.gameObject.CompareTag("Barrel")
        )
        {
            isJumping = false; // تنظیم وضعیت پرش به حالت غیر فعال
            animator.SetBool("IsJumping", false); // غیرفعال کردن انیمیشن پرش
            Debug.Log("Grounded"); // نمایش پیام در کنسول
        }

        // بررسی برخورد با الماس
        if (other.gameObject.CompareTag("Diamond"))
        {
            string currentScene = SceneManager.GetActiveScene().name;

            // بررسی نام صحنه و بارگذاری صحنه بعدی
            if (currentScene == "Level1")
            {
                waterCollider1.enabled = true; // فعال کردن Collider رودخانه 1
                waterCollider2.enabled = true; // فعال کردن Collider رودخانه 2
                Debug.Log("Water Colliders Enabled!");
            }
            if (currentScene == "Level3")
            {
                jump = 6.5f;
                Debug.Log("Jumping Increased");
            }

            Debug.Log("Power Up Activated!");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        // بررسی خروج از برخورد با زمین یا آب
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Water"))
        {
            isJumping = true; // تنظیم وضعیت پرش به حالت فعال
            Debug.Log("on the fly");
        }
    }

    private void Flip()
    {
        // تغییر جهت کاراکتر
        isFacingRight = !isFacingRight; // تغییر وضعیت جهت
        Vector3 theScale = transform.localScale; // گرفتن مقیاس فعلی
        theScale.x *= -1; // معکوس کردن مقیاس محور X
        transform.localScale = theScale; // اعمال مقیاس جدید
    }
}
