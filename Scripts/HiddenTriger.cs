using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    public PlayerController playerController; // Referencia al PlayerController

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerController != null)
        {
            playerController.SetHidden(); // Cambia el estado a Hidden cuando entra
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && playerController != null)
        {
            int count = playerController.GetCount();

            if (count < 5)
            {
                playerController.SetUnboosted();
            }
            else
            {
                playerController.SetBoosted();
            }
        }
    }
}