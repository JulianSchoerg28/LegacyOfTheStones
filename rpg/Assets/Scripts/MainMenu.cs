using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Wechsel von Hauptmen� in Charakterauswahlszene
    public void GoToCharacterSelection()
    {
        Debug.Log("Switching to character selection scene...");
        SceneManager.LoadScene("PlaySecondScene"); // Ersetze "CharacterSelection" durch den Namen der Auswahlszene
    }

    // Setzt den Avatar auf "male" und startet das Spiel
    public void SetAvatarToMale()
    {
        PlayerPrefs.SetString("SelectedAvatar", "male");
        Debug.Log("Avatar set to male");
        StartGame();
    }

    // Setzt den Avatar auf "female" und startet das Spiel
    public void SetAvatarToFemale()
    {
        PlayerPrefs.SetString("SelectedAvatar", "female");
        Debug.Log("Avatar set to female");
        StartGame();
    }

    // Startet die Spielszene
    private void StartGame()
    {
        Debug.Log("Starting game...");
        SceneManager.LoadSceneAsync(1); // Ersetze "GameScene" durch den Namen deiner Spielszene
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);

    }

    // Beendet das Spiel
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
