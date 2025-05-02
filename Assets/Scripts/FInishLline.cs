using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        // بررسی اینکه آیا برخورد با پلیر بوده است
        if (other.gameObject.CompareTag("Player"))
        {
            // گرفتن نام صحنه فعلی
            string currentScene = SceneManager.GetActiveScene().name;

            // بررسی نام صحنه و بارگذاری صحنه بعدی
            if (currentScene == "Level1")
            {
                // بارگذاری صحنه "Level2"
                SceneManager.LoadScene("Level2");
                Debug.Log("Entering Level2!"); // نمایش پیام در کنسول
            }
            else if (currentScene == "Level2")
            {
                // بارگذاری صحنه "Level3"
                SceneManager.LoadScene("Level3");
                Debug.Log("Entering Level3!"); // نمایش پیام در کنسول
            }
        }
    }
}
