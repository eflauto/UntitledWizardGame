using UnityEngine;
using UnityEngine.SceneManagement;

/*  Change Scenes Script
    by Jonathan Moster
    
    ############### Instructions & Notes ###############
    This script was designed to work with game objects that have 
    "Is Trigger" bool set to "true". The package this script 
    comes with also includes invisibile sphere and cube prefab 
    objects that can be parented under other objects or placed 
    in the scene on their own.

    IMPORTANT:
    Scenes must be in the build profile/scene list in order for 
    the script to be able to change to a new scene!
*/


public class ChangeScenes : MonoBehaviour
{
    // ############### VARIABLES ###############

    [Tooltip("The string name of the destination Scene.")]
    public string DestinationSceneString = null;

    public string destinationPosition;

    [Tooltip("Enabling the tag filter requires a colliding entity to have a tag that matches the tag written in the 'Colliding Tag' variable.")]
    public bool EnableTagFilter = false;

    [Tooltip("The exact string of the tag that can trigger a scene change.")]
    public string CollidingTag = null;

    // ############### FUNCTIONS ###############

    // When the collider of the object this script is attached to is encountered by another, the scene is changed. Optionally, the script checks for a user-specified tag before changing the scene.
    void OnTriggerEnter(Collider other)
    {
        // Ensures a destination is designated, and checks for tag filter
        if (DestinationSceneString != null && (!EnableTagFilter || (EnableTagFilter && ((CollidingTag != null) && (other.CompareTag(CollidingTag))))))
        {
            ChangeScene(DestinationSceneString);
        }
    }

    // Change the scene to a scene specified in the string field
    public void ChangeScene(string DestinationScene)
    {
        MainManager.Instance.SetWaypoint(destinationPosition);
        SceneManager.LoadScene(DestinationScene);
    }


}

