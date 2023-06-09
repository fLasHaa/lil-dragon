using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound; //Sound for a new checkpoint pickup
    private Transform currentCheckpoint; //store last checkpoint here
    private Health playerHealth;
    private UI_Manager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UI_Manager>();
    }

    public void CheckRespawn()
    {
        //Check if checkpoint is available
        if(currentCheckpoint == null)
        {
            //Show game over screen
            uiManager.GameOver();

            return; //Don't execute the rest of this function
        }

        transform.position = currentCheckpoint.position; //Move player to checkpoint position
        playerHealth.Respawn(); //Restore player Health and reset Animation

        //Move camera back to checkpoint
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }

    //Activate Checkpoints

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform; //Store the checkpoint that we activated as the current one
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false; //Deactivate checkpoint collider
            collision.GetComponent<Animator>().SetTrigger("appear"); //Trigger checkpoint animation
        }
    }
}
