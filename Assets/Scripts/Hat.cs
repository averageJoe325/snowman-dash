using UnityEngine;

//  Keeps the hat over the player.
internal class Hat : MonoBehaviour
{
    private GameObject snowman;

    private void Awake()
    {
        snowman = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        gameObject.transform.localPosition = snowman.transform.localPosition + new Vector3(0, 1.9375f, 0) * snowman.transform.GetChild(0).localScale.y + new Vector3(0, .3125f, 0);
    }
}