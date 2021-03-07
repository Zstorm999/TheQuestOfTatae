using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private GameObject focusedPlayer;

    private void FixedUpdate()
    {
        Vector2 playerPos = focusedPlayer.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
    }

}
