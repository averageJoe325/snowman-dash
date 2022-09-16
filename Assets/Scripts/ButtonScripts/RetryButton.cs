using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//  Controls retry buttons
[RequireComponent(typeof(Button))]
public class RetryButton : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(delegate
        {
            SceneManager.LoadScene("Level " + LevelManager.currentLevel);
        });
    }
}
