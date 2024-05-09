using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator _anim;
    public float hp;
    public bool isDead => hp <= 0;
    [HideInInspector] public int currentAnimID;
    [SerializeField] protected CombatText combatTextPrefab;

    public virtual void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        hp = 100;
    }

    public virtual void OnDespawn()
    {

    }

    protected virtual void OnDead()
    {
        Invoke(nameof(OnDespawn), 1f);
    }

    protected void ChangeAnim(int animID)
    {
        if (currentAnimID != animID)
        {
            _anim.ResetTrigger(currentAnimID);
            currentAnimID = animID;
            _anim.SetTrigger(currentAnimID);
        }
    }

    public void OnHit(float damage)
    {
        if (!isDead)
        {
            hp -= damage;
            Instantiate(combatTextPrefab, transform.position, Quaternion.identity).OnInit(damage);
        }
    }
}
