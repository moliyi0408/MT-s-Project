using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    ////�T�{���
    //[Header("Confirmation")]
    //[SerializeField] private GameObject confirmationPrompt = null;


    //�����]�w
    [Header("Levels to Load")] //Unity����ܼ��D
    //public string newgamelevel;
    private string leveltoload;
    [SerializeField] private GameObject noSaveGame = null;


    //=========�D�����]�m=======
    public void NewGameYes()
    {
        //SceneManager.LoadScene(newgamelevel); //�[������
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
            //�p�G�S���i�[���ɮױ��p
            noSaveGame.SetActive(true);
        }
    }
    public void ExitGame()
    {
       // Debug.Log("Quit");
        Application.Quit();//�����C��
    }
}
