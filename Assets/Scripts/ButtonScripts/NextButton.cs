using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//  Controls retry buttons
[RequireComponent(typeof(Button))]
public class NextButton : MonoBehaviour
{
    private void Awake()
    {
        if (LevelManager.currentLevel != LevelManager.levelsUnlocked.Length)
        {
            gameObject.GetComponent<Button>().onClick.AddListener(delegate
            {
                LevelManager.currentLevel++;
                SceneManager.LoadScene("Level " + LevelManager.currentLevel);
            });
        }
        else
            gameObject.GetComponent<Button>().interactable = false;
    }
}
