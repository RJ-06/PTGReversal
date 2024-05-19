using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyLabel : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = $"Money: {gameManager.money}J$";
    }
}
