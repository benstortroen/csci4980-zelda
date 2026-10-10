using Unity.VisualScripting;
using UnityEngine;

public class TeleportBowRoom : MonoBehaviour
{
    [SerializeField] bool isEnter = false;
    [SerializeField] Vector3 enter_player_pos;
    [SerializeField] Vector3 enter_camera_pos;
    [SerializeField] Vector3 exit_player_pos;
    [SerializeField] Vector3 exit_camera_pos;

    [SerializeField] Transform player;
    [SerializeField] Transform camera;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (isEnter)
            {
                player.position = enter_player_pos;
                camera.position = enter_camera_pos;
            }

            else
            {
                player.position = exit_player_pos;
                camera.position = exit_camera_pos;
            }
        }
    }
}
