using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivingStatePlayer : IStatePlayer
{
    float randomTime;
    float timer;

    public void OnEnter(Player player)
    {
        timer = 0;
        randomTime = Random.Range(2f, 4f);
    }

    public void OnExecute(Player player)
    {
        timer += Time.deltaTime;
    }

    public void OnExit(Player player)
    {

    }
}
