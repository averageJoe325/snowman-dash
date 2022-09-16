using UnityEngine;
using UnityEngine.UI;

//  Controls player movement and damage.
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
internal class Player : MonoBehaviour
{
    private const float FORCE = 64;
    private const float START_FORCE = 256;
    private const float JUMP_FORCE = 16384;
    private const float HOVER_FORCE = 64;
    private int groundCount = 0;

    private int health = 3;
    private GameObject[] snowballs;
    private float healthTimer = 0;
    private bool dead = false;
    private bool finished = false;

    private GameObject deathPanel;
    private GameObject finishPanel;

    private void Awake()
    {
        snowballs = GameObject.FindGameObjectsWithTag("Snowball");
        deathPanel = (GameObject)Resources.Load("Death Panel");
        finishPanel = (GameObject)Resources.Load("Finish Panel");
    }

    //  Increments and decrememnts groundCount as needed.
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Ground") || col.gameObject.tag.Equals("Damage"))
            groundCount++;
        if (col.gameObject.tag.Equals("Damage") && healthTimer <= 0)
            TakeDamage();
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Ground") || col.gameObject.tag.Equals("Damage"))
            groundCount--;
    }

    //  Makes the player take damage.
    private void OnCollisionStay2D(Collision2D col)
	{
		if (col.gameObject.tag.Equals("Damage") && healthTimer <= 0)
			TakeDamage();
	}

    //  Set finished to true when the level is finished.
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Igloo"))
            finished = true;
    }

    private void Update()
    {
        //  Update dead variable
        dead = gameObject.transform.GetChild(0).localScale.x <= 0 || health == 0 || gameObject.GetComponent<RectTransform>().localPosition.y < -64;

        //  Allow the player to move and jump if possible
        if (!dead && !finished)
        {
            if (Input.GetKey("a"))
            {
                if (gameObject.GetComponent<Rigidbody2D>().velocity.x > -256)
                    gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * START_FORCE);
                else
                    gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * FORCE);
                gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            if (Input.GetKey("d"))
            {
                if (gameObject.GetComponent<Rigidbody2D>().velocity.x < 256)
                    gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * START_FORCE);
                else
                    gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * FORCE);
                gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (Input.GetKeyDown("w") && groundCount > 0)
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * JUMP_FORCE);
            if (Input.GetKey("w") && gameObject.GetComponent<Rigidbody2D>().velocity.y > 0)
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * HOVER_FORCE);
        }

		//  Shrink the character (melting) until the size is zero or less.
		if (gameObject.transform.GetChild(0).localScale.x > 0 && health > 0)
            gameObject.transform.GetChild(0).localScale -= new Vector3(.1f * Time.deltaTime / health, .1f * Time.deltaTime / health);

        //  Lower the health timer until it reaches zero
        if (healthTimer > 0)
            healthTimer -= Time.deltaTime;

        //  Display the death screen when dead.
        if (dead && GameObject.FindGameObjectWithTag("Panel") == null)
        {
            GameObject panel = Instantiate(deathPanel);
            panel.transform.parent = GameObject.FindGameObjectWithTag("Canvas").transform;
            panel.transform.localPosition = Vector3.zero;
            panel.transform.localScale = new Vector3(1, 1, 1);
        }

        if (finished &&  GameObject.FindGameObjectWithTag("Panel") == null)
        {
            LevelManager.UnlockNext();
            LevelManager.saveLevels();
            GameObject panel = Instantiate(finishPanel);
            panel.transform.parent = GameObject.FindGameObjectWithTag("Canvas").transform;
            panel.transform.localPosition = Vector3.zero;
            panel.transform.localScale = new Vector3(1, 1, 1);
        }

        //  Run miscellaneous functions.
        Flicker();
        HatHitbox();
    }

    //  Runs whenever the player takes damage
    private void TakeDamage()
    {
        health--;
        Destroy(snowballs[health]);
        healthTimer = 1;
    }

    //  Make the player flicker when damaged
    private void Flicker()
    {
        foreach (GameObject snowball in snowballs)
        {
            if (snowball == null)
                continue;
            if (Mathf.FloorToInt(10 * healthTimer) % 2 == 1 && healthTimer > 0)
                snowball.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            else
                snowball.GetComponent<Image>().color = new Color(1, 1, 1);
        }
    }

    //  Make the hat hitbox align with the hat.
    private void HatHitbox()
    {
        gameObject.GetComponent<Collider2D>().offset = new Vector2(0, 1.9375f) * gameObject.transform.GetChild(0).localScale.y + new Vector2(0, .3125f);
    }
}
