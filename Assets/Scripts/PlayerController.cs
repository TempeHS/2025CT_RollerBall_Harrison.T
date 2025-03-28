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
    private float moveHorizontal;
    private float moveVertical;
    private float jump;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent <Rigidbody>();  
       count = 0;

       SetCountText();
       winTextObject.SetActive(false);
    }


    void OnMove(InputValue movementValue)
     {
          Vector2 movementVector = movementValue.Get<Vector2>();   

          movementX = movementVector.x; 
          movementY = movementVector.y; 
     }
       void SetCountText()
     {
         countText.text = "Count: " + count.ToString();
         if(count >= 12) 
         {
              winTextObject.SetActive(true);
              Destroy(GameObject.FindGameObjectWithTag("Enemy"));
         }
     }
    
   
   void Update ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 jump = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Vector3 movement = new Vector3 (0f, 0.0f, 1f);

         

        GetComponent<Rigidbody>().AddForce (movement * speed * Time.deltaTime);

        

        {
            if (Input.GetKeyDown ("space") && GetComponent<Rigidbody>().transform.position.y <= 5.0f) {
                Vector3 jump2 = new Vector3 (0.0f, 300.0f,   0.0f);

                GetComponent<Rigidbody>().AddForce (jump2);
            }
        }
        

    }

   private void OnCollisionEnter(Collision collision)
{
   if (collision.gameObject.CompareTag("Enemy"))
   {
       // Destroy the current object
       Destroy(gameObject); 
       // Update the winText to display "You Lose!"
       winTextObject.gameObject.SetActive(true);
       winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
   }
}
   
       void OnTriggerEnter(Collider other)
      {
         if(other.gameObject.CompareTag("Pickup"))
         {
          other.gameObject.SetActive(false);
          count = count + 1;

          SetCountText();
         }
         
      } 
}