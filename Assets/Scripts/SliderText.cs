using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    int n;
    public TextMeshProUGUI myText;
    public Slider mySlider;

    private BossAi myboss;
    void Update()
    {
        myText.text = mySlider.value + "/" + mySlider.maxValue;
    }
}
