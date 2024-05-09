using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingCheckPlayer : MonoBehaviour
{
    [SerializeField] Player player;
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            player.ChangeState(new SwimmingStatePlayer());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            player.ChangeState(new OnGroundStatePlayer());
        }
    }
}
