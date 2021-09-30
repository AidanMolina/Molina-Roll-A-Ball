using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb; 
    private int count;
    private float movementX;
    private float movementY;

    private Vector3 scale = new Vector3(0.2f, 0.2f, 0.2f);
    private Vector3 checkPoint = new Vector3(0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);

        checkPoint = gameObject.transform.position;
        checkPoint.y += 5f;
    }

    void OnMove(InputValue movementValue){
            Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText(){
        countText.text = "Count: " + count.ToString();
        if(count >= 13){
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate(){
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);

        if(gameObject.transform.position.y < -10){
            gameObject.transform.position = checkPoint;
            rb.velocity = Vector3.zero;
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Pickup")){
            other.gameObject.SetActive(false);
            count++;

            SetCountText();

            gameObject.transform.localScale += scale;
        }
    }
}
