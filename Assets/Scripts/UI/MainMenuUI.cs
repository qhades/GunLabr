using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("CharacterSelectorScene", LoadSceneMode.Additive);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("MainGameScene");
    }
}
