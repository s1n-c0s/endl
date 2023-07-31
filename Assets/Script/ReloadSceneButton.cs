using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReloadSceneButton : MonoBehaviour
{
    private Button reloadButton;

    void Start()
    {
        // Get the Button component attached to the GameObject
        reloadButton = GetComponent<Button>();

        // Add a listener for the button click event
        reloadButton.onClick.AddListener(OnReloadButtonClick);
    }

    void OnReloadButtonClick()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
