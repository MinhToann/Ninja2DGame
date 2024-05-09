using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] TextMeshProUGUI textMesh;
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    public void SetCoin(int coin)
    {
        textMesh.text = coin.ToString();
    }
}
