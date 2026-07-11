using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NameManager : MonoBehaviour
{
    public TMP_InputField player1Input;
    public TMP_InputField player2Input;

    public void SaveNamesAndLoadScene(string sceneName)
    {
        PlayerPrefs.SetString(
            "Player1Name",
            string.IsNullOrWhiteSpace(player1Input.text) ? "Player1" : player1Input.text
        );

        PlayerPrefs.SetString(
            "Player2Name",
            string.IsNullOrWhiteSpace(player2Input.text) ? "Player2" : player2Input.text
        );

        PlayerPrefs.Save();

        SceneManager.LoadScene(sceneName);
    }
}