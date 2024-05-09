using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundStatePlayer : IStatePlayer
{
    public void OnEnter(Player player)
    {
        player.StartOnGroundState();
    }

    public void OnExecute(Player player)
    {
        player.UpdateOnGroundState();
    }

    public void OnExit(Player player)
    {
        player.ExitOnGroundState();
    }
}
