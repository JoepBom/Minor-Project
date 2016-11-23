using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PhilMovement : MonoBehaviour {

    public float speed;
    private Rigidbody rb;
    private Quaternion Rotation;

    public Text healthText;
    public Text winText;
    private int healthScore = 100;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        setHealthScoreText();
        winText.text = "";
    }
    
    void Update()
    {
        setHealthScoreText();
    }

    void LateUpdate()
    {
        if (healthScore != 0)
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            // movement
            Vector3 movement = new Vector3(h, 0f, v);
            movement = movement.normalized * speed * Time.deltaTime;
            rb.MovePosition(transform.position + movement);

            // turning
            if (movement == new Vector3(0, 0, 0))
            {
                rb.MoveRotation(Rotation);
            }
            else
            {
                Rotation = Quaternion.LookRotation(movement.normalized);
                rb.MoveRotation(Rotation);
            }
        }
    }

    void setHealthScoreText()
    {
        healthText.text = "Health: " + healthScore.ToString();
        if(healthScore == 0)
        winText.text = "K.O.! Please try agiain";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Enemy"))
        {
            healthScore = Max(healthScore - 20,0);
            
        }
        if(other.name.Contains("Pick Up"))
        {
            healthScore = Min(healthScore + 10, 100);
            other.gameObject.SetActive(false);
        }
    }

    private int Min(int i, int j)
    {
        if (i >= j)
            return j;
        return i;
    }

    private int Max(int i, int j)
    {
        if(i >=j)
        return i;
        return j;
    }
}
