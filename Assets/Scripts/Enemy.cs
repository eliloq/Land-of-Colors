using UnityEngine;

public class Enemy : MonoBehaviour
{
    // سرعت حرکت دشمن
    public float speed = 1f;

    // نقاط شروع و پایان مسیر حرکت دشمن
    public Transform startPoint;
    public Transform endPoint;

    // هدف فعلی دشمن (نقطه‌ای که به سمت آن حرکت می‌کند)
    private Vector3 target;

    // مرجع به Rigidbody2D دشمن
    private Rigidbody2D rb;

    void Start()
    {
        // گرفتن مرجع Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // تنظیم هدف اولیه به نقطه پایان
        target = endPoint.position;
    }

    void FixedUpdate()
    {
        // محاسبه موقعیت جدید دشمن برای حرکت به سمت هدف
        Vector2 newPos = Vector2.MoveTowards(
            rb.position, // موقعیت فعلی دشمن
            new Vector2(target.x, rb.position.y), // حرکت فقط در محور X
            speed * Time.fixedDeltaTime // سرعت حرکت
        );

        // اعمال موقعیت جدید به Rigidbody2D
        rb.MovePosition(newPos);

        // بررسی رسیدن به هدف
        if (Vector2.Distance(rb.position, new Vector2(target.x, rb.position.y)) < 0.1f)
        {
            // تغییر هدف به نقطه مقابل
            target = target == startPoint.position ? endPoint.position : startPoint.position;

            // چرخاندن دشمن هنگام تغییر جهت
            Flip();
        }
    }

    void Flip()
    {
        // تغییر جهت دشمن با معکوس کردن مقیاس محور X
        Vector3 scale = transform.localScale;
        scale.x *= -1; // معکوس کردن مقدار محور X
        transform.localScale = scale; // اعمال مقیاس جدید
    }
}
