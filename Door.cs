using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform doorEntryPosition;

    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public void OpenDoor()
    {
        player.transform.position = doorEntryPosition.position;
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
    }
}
