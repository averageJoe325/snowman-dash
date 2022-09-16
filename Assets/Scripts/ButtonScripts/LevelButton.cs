using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//  Controls level-selecting buttons.
[RequireComponent(typeof(Button))]
internal class LevelButton : MonoBehaviour
{private void Awake()
    {
        //  Set the color of the buttons based on whether or not the level is locked
        Image button = gameObject.GetComponent<Image>();
        int level = int.Parse(gameObject.GetComponentInChildren<Text>().text);
        if (LevelManager.levelsUnlocked[level - 1])
            button.color = new Color(0, 1, 1);
        else
            button.color = new Color(.25f, .5f, .5f);

        gameObject.GetComponent<Button>().onClick.AddListener(delegate
        {
            //  Only allow levels to be accessed if they're unlocked.
            Text errorText = GameObject.FindGameObjectWithTag("ErrorText").GetComponent<Text>();
            if (!LevelManager.levelsUnlocked[level - 1])
                errorText.text = "You haven't unlocked this level yet.";
            else
            {
                LevelManager.currentLevel = level;
                SceneManager.LoadScene("Level " + level);
            }
        });
    }
}
