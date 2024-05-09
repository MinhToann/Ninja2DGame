using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathBar : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Character character;
    GameObject _camera;
    float maxHP;

    public void Start()
    {
        OnInit();
    }

    public void Update()
    {
        if (maxHP == 0)
        {
            maxHP = character.hp;
        } 
        transform.forward = _camera.transform.forward;
        image.fillAmount = Mathf.Lerp(image.fillAmount, character.hp / maxHP, Time.deltaTime * 2f);
    }

    public void OnInit()
    {
        image.fillAmount = 1;
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void OnDespawn()
    {
        
    }
}
