using System.Collections.Generic;
using UnityEngine;

public class WorldInfo : MonoBehaviour
{
    private PlayerController playerController;
    private GameDirector gameDirector;

    public static WorldInfo Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        playerController = GameObject.FindGameObjectWithTag(GameTags.Player).GetComponent<PlayerController>();
        gameDirector = GetComponent<GameDirector>();
    }

    public PlayerController GetPlayer() => playerController;
    public GameDirector GetGameDirector() => gameDirector;

}
