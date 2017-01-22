using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    private int count;
    public Text countText;
    public Text winText;
    public float speed;

    private void Start()
    {
        rb = GetComponent < Rigidbody >();
        count = 0;
        winText.text = "";
        setCountText();
    }

    void Update()
     {
       
        
     }

     void FixedUpdate()
     {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            setCountText();
        }
    }

    void setCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 7)
        {
            winText.text = "You Win";
        }
    }

    
}
