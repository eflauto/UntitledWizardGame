using UnityEngine;


// TODO: Camera movement, player movement
// throw a ball has player movement 
// wall knocking game has camera and player movement
// add jump


public class FirstPOVController : MonoBehaviour
{
    [Header("First Person POV Camera Settings")]
    [Tooltip("Mouse sensitivity for rotating, lower if rotating is too fast")]
    private float rotation = 0f;
    [Tooltip("Place Player object here for horizontal rotation")]
    public Transform player; // rotation for camera
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    void Update()
    {


        // mouse movement moves camera 
        float horizontal = Input.GetAxisRaw("Mouse X") * MainManager.Instance.mouseSensitivityX;
        float vertical = Input.GetAxisRaw("Mouse Y") * MainManager.Instance.mouseSensitivityY;

        if (MainManager.Instance.invertX)
        {
            horizontal *= -1;
        }

        if (MainManager.Instance.invertY)
        {
            vertical *= -1;
        }
        
        rotation -= vertical; // subtract the rotation value by the vertical input 
        // vertical movement
        rotation = Mathf.Clamp(rotation, -90f, 90f);  // clamp the rotation value between 90 and -90 to avoid overrotation
        transform.localEulerAngles = Vector3.right * rotation; //EulerAngles is for rotation, represents the rotation of a transform component as eular angles (x,y,z) relative to the parent's rotation
        

        //horizontal movement
        player.Rotate(Vector3.up * horizontal);








        
    }
}
