using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{

    public Rigidbody rb;
    public float speed;

    public Text countText;
    public Text winText;
    private int count;
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        speed = 10;
        count = 0;
        setCountText();
        winText.text = "";
	}

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = Max(30,speed + 5);
        }
        if(Input.GetKeyDown(KeyCode.Space) && rb.position.y <=0.54)
        {
            
            movement = new Vector3(moveHorizontal, 20, moveVertical);
        }
        speed = Max(10, speed - 2);
        rb.AddForce(movement*speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            setCountText();
        }
    }

    void setCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count == 8)
        {
            winText.text = "Congrats, you collected all the items!";
        }
    }

    public float Max(float f1, float f2)
    {
        if (f1 < f2)
            return f2;
        else return f1;
    }
}
