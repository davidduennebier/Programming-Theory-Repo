using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class GameUIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI currencyText;

    // Start is called before the first frame update
    void Start()
    {
        currencyText.text = "Gold: " + GameManager.Instance.GetCurrency().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        currencyText.text = "Gold: " + GameManager.Instance.GetCurrency().ToString();
    }
}
