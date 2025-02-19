using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Rigidbody of the player.
    private Rigidbody rb;

    // Variable to keep track of collected "PickUp" objects.
    private int count;

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    public float speed = 0;

    // UI text component to display count of "PickUp" objects collected.
    public TextMeshProUGUI countText;

    // UI object to display winning text.
    public GameObject winTextObject;

    public playerState state = playerState.unboosted;

    // Renderer of the player to change its color and transparency
    private Renderer playerRenderer;
    private Material playerMaterial;

    public enum playerState
    {
        unboosted,
        boosted,
        hidden // Estado 'hidden' añadido
    }

    // Start is called before the first frame update.
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();

        // Get the Renderer component and the material of the player to modify transparency.
        playerRenderer = GetComponent<Renderer>();
        playerMaterial = playerRenderer.material;

        // Initialize count to zero.
        count = 0;

        // Update the count display.
        SetCountText();

        // Initially set the win text to be inactive.
        winTextObject.SetActive(false);
    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        //Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player.
        //rb.AddForce(movement * speed);
        
        Vector3 dir = Vector3.zero;
        dir.x = Input.acceleration.x;
        dir.z = Input.acceleration.y;
        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        dir *= Time.deltaTime;
        transform.Translate(dir * speed, Space.World);


        // Change speed, color, and transparency based on player state
        if (state == playerState.boosted)
        {
            if (speed < 12)
            {
                speed += 2;
            }

            // Change color to indicate boosted state
            playerRenderer.material.color = Color.red;

            // Make the object fully opaque
            Color color = playerMaterial.color;
            color.a = 1f;  // Set to fully opaque
            playerMaterial.color = color;
        }
        else if (state == playerState.hidden)
        {
            // Change transparency to indicate hidden state
            Color color = playerMaterial.color;
            color.a = 0.3f;  // Make the object more transparent
            playerMaterial.color = color;
        }
        else
        {
            // Reset color and transparency when unboosted
            playerRenderer.material.color = Color.white;

            // Make the object fully opaque when unboosted
            Color color = playerMaterial.color;
            color.a = 1f;  // Set to fully opaque
            playerMaterial.color = color;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with has the "PickUp" tag.
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);

            // Increment the count of "PickUp" objects collected.
            count = count + 1;

            if (count >= 5)
            {
                SetBoosted();
            }

            // Update the count display.
            SetCountText();
        }
    }

    // Function to update the displayed count of "PickUp" objects collected.
    void SetCountText()
    {
        // Update the count text with the current count.
        countText.text = "Count: " + count.ToString();

        // Check if the count has reached or exceeded the win condition.
        if (count >= 10)
        {
            // Display the win text.
            winTextObject.SetActive(true);

            // Destroy the enemy GameObject.
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
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

    // Call this function to set the state to "boosted"
    public void SetBoosted()
    {
        state = playerState.boosted;
    }

    // Call this function to set the state to "hidden"
    public void SetHidden()
    {
        state = playerState.hidden;
    }

    // Call this function to set the state back to "unboosted"
    public void SetUnboosted()
    {
        state = playerState.unboosted;
    }

       //get count
       public int GetCount(){
              return count;
       }
}
