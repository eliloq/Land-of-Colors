using UnityEngine;

public class PushableBarrel : MonoBehaviour
{
    private Rigidbody rb;
    public float pushSpeed = 2f;
    private bool isPushing = false;
    private float inputX;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isPushing)
        {
            Vector3 move = new Vector3(inputX * pushSpeed * Time.fixedDeltaTime, 0, 0);
            rb.MovePosition(rb.position + move);
        }

        // جلوگیری از اینرسی ناخواسته
        if (!isPushing)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        isPushing = false; // هر فریم ریست میشه تا فقط هنگام تماس دوباره فعال شه
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float input = Input.GetAxisRaw("Horizontal");
            if (Mathf.Abs(input) > 0.1f)
            {
                isPushing = true;
                inputX = input;
            }
        }
    }
}
