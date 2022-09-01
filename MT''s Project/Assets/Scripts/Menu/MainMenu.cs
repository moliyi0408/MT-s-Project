using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    ////確認文件
    //[Header("Confirmation")]
    //[SerializeField] private GameObject confirmationPrompt = null;


    //場景設定
    [Header("Levels to Load")] //Unity裡顯示標題
    //public string newgamelevel;
    private string leveltoload;
    [SerializeField] private GameObject noSaveGame = null;


    //=========主場景設置=======
    public void NewGameYes()
    {
        //SceneManager.LoadScene(newgamelevel); //加載場景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
     
    }

    public void LoadGameYes()
    {
        if (PlayerPrefs.HasKey("SaveLevel"))
        {
            leveltoload = PlayerPrefs.GetString("SaveLevel");
            SceneManager.LoadScene(leveltoload);

        }
        else
        {
            //如果沒有可加載檔案情況
            noSaveGame.SetActive(true);
        }
    }
    public void ExitGame()
    {
       // Debug.Log("Quit");
        Application.Quit();//關掉遊戲
    }
}
