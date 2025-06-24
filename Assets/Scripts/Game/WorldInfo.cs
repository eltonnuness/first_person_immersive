using System.Collections.Generic;
using UnityEngine;

public class WorldInfo : MonoBehaviour
{
    private PlayerController playerController;

    public static WorldInfo Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        playerController = GameObject.FindGameObjectWithTag(GameTags.Player).GetComponent<PlayerController>();
    }

    public PlayerController GetPlayer() => playerController;

}
