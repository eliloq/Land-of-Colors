using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    // صدای جمع‌آوری الماس
    public AudioClip collectSound;

    // سایه‌ای که باید غیرفعال شود
    public GameObject shadow;

    // متدی که هنگام برخورد با الماس اجرا می‌شود
    private void OnCollisionEnter2D(Collision2D other)
    {
        // بررسی اینکه آیا برخورد با پلیر بوده است
        if (other.gameObject.CompareTag("Player"))
        {
            // پخش صدای جمع‌آوری الماس
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

            // حذف آبجکت الماس از صحنه
            Destroy(gameObject);

            // نمایش پیام در کنسول برای تأیید جمع‌آوری الماس
            Debug.Log("Diamond collected!");

            // غیرفعال کردن سایه
            shadow.SetActive(false);
        }
    }
}
