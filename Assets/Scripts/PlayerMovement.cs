using UnityEngine;

public class Movement : MonoBehaviour
{
    // متغیرهای عمومی برای تنظیمات حرکت و انیمیشن
    public Animator animator; // کنترل انیمیشن‌های کاراکتر
    public float speed; // سرعت حرکت کاراکتر
    private float Move; // مقدار ورودی حرکت افقی

    // متغیرهای مربوط به Rigidbody و Collider
    private Rigidbody2D rb; // مرجع به Rigidbody2D کاراکتر
    private EdgeCollider2D waterCollider1; // مرجع به اولین Collider رودخانه
    private EdgeCollider2D waterCollider2; // مرجع به دومین Collider رودخانه
    public GameObject river1; // آبجکت مربوط به رودخانه 1
    public GameObject river2; // آبجکت مربوط به رودخانه 2

    // متغیرهای مربوط به پرش
    public float jump; // نیروی پرش
    public bool isJumping; // بررسی وضعیت پرش (آیا کاراکتر در حال پرش است یا خیر)

    // متغیر برای بررسی جهت کاراکتر
    private bool isFacingRight = true; // آیا کاراکتر به سمت راست نگاه می‌کند؟

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
            Debug.Log("Jumping"); // نمایش پیام در کنسول
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // بررسی برخورد با زمین یا آب
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Water"))
        {
            isJumping = false; // تنظیم وضعیت پرش به حالت غیر فعال
            animator.SetBool("IsJumping", false); // غیرفعال کردن انیمیشن پرش
            Debug.Log("Grounded"); // نمایش پیام در کنسول
        }

        // بررسی برخورد با الماس
        if (other.gameObject.CompareTag("Diamond"))
        {
            waterCollider1.enabled = true; // فعال کردن Collider رودخانه 1
            waterCollider2.enabled = true; // فعال کردن Collider رودخانه 2
            Debug.Log("Power Up Activated!"); // نمایش پیام در کنسول
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        // بررسی خروج از برخورد با زمین یا آب
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Water"))
        {
            isJumping = true; // تنظیم وضعیت پرش به حالت فعال
            Debug.Log("on the fly"); // نمایش پیام در کنسول
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
