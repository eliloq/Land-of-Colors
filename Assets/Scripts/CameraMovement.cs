using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // آبجکتی که دوربین باید آن را دنبال کند (معمولاً پلیر)
    public GameObject target;

    void Update()
    {
        // تنظیم موقعیت دوربین برای دنبال کردن هدف
        // محور X دوربین با محور X هدف هماهنگ می‌شود
        // محور Y و Z دوربین ثابت می‌مانند
        transform.position = new Vector3(target.transform.position.x, transform.position.y, -10);
    }
}
