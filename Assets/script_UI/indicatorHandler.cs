using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class indicatorHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Access the TextMeshPro component
        TextMeshProUGUI textmeshPro = GetComponentInChildren<TextMeshProUGUI>();

        // Get the preferred values
        Vector2 textSize = textmeshPro.GetPreferredValues();

        // Set the size of the GameObject to fit the text
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = textSize;
    }
}
