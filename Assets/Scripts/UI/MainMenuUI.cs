using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    #region Header OBJECT REFERENCES
    [Space(10)]
    [Header("OBJECT REFERENCES")]
    #endregion
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private GameObject highScoreButton;
    [SerializeField] private GameObject instructionButton; 
    [SerializeField] private GameObject returnToMainMenuButton;
    private bool isInstructionSceneLoaded = false;

    private void Start()
    {
        SceneManager.LoadScene("CharacterSelectorScene", LoadSceneMode.Additive);

        returnToMainMenuButton.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    public void LoadCharacterSelector()
    {
        returnToMainMenuButton.SetActive(false);

        if (isInstructionSceneLoaded)
        {
            SceneManager.UnloadSceneAsync("InstructionScene");
            isInstructionSceneLoaded = false;
        }

        playButton.SetActive(true);
        quitButton.SetActive(true);
        highScoreButton.SetActive(true);
        instructionButton.SetActive(true);

        SceneManager.LoadScene("CharacterSelectorScene", LoadSceneMode.Additive);
    }

    public void LoadInstruction()
    {
        playButton.SetActive(false);
        quitButton.SetActive(false);
        highScoreButton.SetActive(false);
        instructionButton.SetActive(false);
        isInstructionSceneLoaded = true;

        SceneManager.UnloadSceneAsync("CharacterSelectorScene");

        returnToMainMenuButton.SetActive(true);

        SceneManager.LoadScene("InstructionScene", LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}