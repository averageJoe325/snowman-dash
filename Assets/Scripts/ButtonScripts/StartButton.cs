using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//  Controls the start button.
[RequireComponent(typeof(Button))]
internal class StartButton : MonoBehaviour
{
    private void Awake()
    {
        LevelManager.loadLevels();
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene("Levels"); });
    }
}
