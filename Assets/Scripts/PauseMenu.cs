using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject panel;

    void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!panel.activeSelf)
            {
                Time.timeScale = 0f;
                panel.SetActive(true);
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1f;
                panel.SetActive(false);
                Cursor.visible = false;
            }
        }
    }

    public void quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void resume()
    {
        Time.timeScale = 1f;
        panel.SetActive(false);
        Cursor.visible = false;
    }

    public void startOver()
    {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1f;
        panel.SetActive(false);
        Cursor.visible = false;
    }
}
