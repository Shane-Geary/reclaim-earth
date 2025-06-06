//using UnityEngine;

//public class PlayerController : MonoBehaviour
//{
//    public float speed = 5f;

//    void Update()
//    {
//        if (Input.touchCount > 0) // Check if there is any touch
//        {
//            Touch touch = Input.GetTouch(0); // Get the first touch

//            if (touch.phase == TouchPhase.Moved) // If the touch is moving
//            {
//                // Convert touch position to world position
//                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));

//                // Restrict movement to x-axis
//                Vector3 newPosition = new Vector3(touchPosition.x, transform.position.y, transform.position.z);

//                // Move the character
//                transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
//            }
//        }
//    }
//}
