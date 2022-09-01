using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseManger : MonoBehaviour
{
    private bool isPaused ;
    public GameObject pausePanel;
    public string mainMenu;


    void Start()
    {
        pausePanel.SetActive(false);
       
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    //�~��C��
    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    //�Ȱ��C��
    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
     
    }
    //�^��D���
    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);
    }
}
