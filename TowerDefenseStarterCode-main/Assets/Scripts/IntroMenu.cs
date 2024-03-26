using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroMenu : MonoBehaviour
{
    public InputField nameInputField;
    public Button startButton;
    public Button quitButton;

    private void Start()
    {
        // Disable start button initially
        startButton.interactable = false;

        // Register callback for name input field value changed
        if (nameInputField != null)
        {
            nameInputField.onValueChanged.AddListener(OnNameValueChanged);
        }

        // Register callbacks for button clicks
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void OnDestroy()
    {
        // Unregister callbacks
        if (nameInputField != null)
        {
            nameInputField.onValueChanged.RemoveListener(OnNameValueChanged);
        }

        startButton.onClick.RemoveListener(StartGame);
        quitButton.onClick.RemoveListener(QuitGame);
    }

    private void OnNameValueChanged(string newValue)
    {
        // Enable start button only if the name length is at least 3 characters
        startButton.interactable = newValue.Length >= 3;

        // Update player name in HighScoreManager
        HighScoreManager.Instance.PlayerName = newValue;
    }

    private void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
