using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    // مرجع به آبجکت پلیر
    public GameObject player;

    // نقطه شروع مجدد (Spawn Point) برای پلیر
    public GameObject startPoint;

    private void OnCollisionEnter2D(Collision2D other)
    {
        // بررسی برخورد با پلیر
        if (other.gameObject.CompareTag("Player"))
        {
            // بازگرداندن پلیر به نقطه شروع
            player.transform.position = startPoint.transform.position;
        }
    }
}
