using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPause(InputValue value)
    {
        if (value.isPressed)
        {
            TogglePause();
        }
    }

    public void PauseButton()
    {
        TogglePause();
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }
}
