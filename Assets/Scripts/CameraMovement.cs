using UnityEngine;

//  Makes it appear as though the camera is moving with the player.
internal class CameraMovement : MonoBehaviour
{
    private GameObject player;
    private GameObject igloo;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        igloo = GameObject.FindGameObjectWithTag("Igloo");
    }

   //   Move everything to the left to keep the player centered.
   private void Update() {
        if (player != null && igloo != null && GameObject.FindGameObjectWithTag("Panel") == null)
            gameObject.transform.localPosition = new Vector3(Mathf.Clamp(-player.GetComponent<RectTransform>().localPosition.x, -igloo.GetComponent<RectTransform>().localPosition.x + 16, 0), gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
    }
}
