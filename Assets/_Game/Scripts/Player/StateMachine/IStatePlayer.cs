using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatePlayer
{
    void OnEnter(Player player);
    void OnExecute(Player player);
    void OnExit(Player player);
}
