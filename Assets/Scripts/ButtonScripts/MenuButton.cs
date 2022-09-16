using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//  Controls the menu buttons.
[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene("Start"); });
    }
}
