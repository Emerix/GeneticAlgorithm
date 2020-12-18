using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class SnakesUI : MonoBehaviour
{
    [SerializeField]
    Snakes geneticTestBase;

    [SerializeField]
    Text timeText;
    [SerializeField]
    Text poolIndexText;
    [SerializeField]
    Text generationText;
    [SerializeField]
    Text timeScaleText;
    [SerializeField]
    Slider slider;
    [SerializeField]
    Text timeScaleNumText;
    
    void Update()
    {
        timeText.text = $"Time left {geneticTestBase.CurrentTime.ToString(CultureInfo.InvariantCulture)}";
        poolIndexText.text = $"Slot in Generation {geneticTestBase.CurrentSlot.ToString()}";
        generationText.text = $"Generation {geneticTestBase.CurrentIteration.ToString()}";

        float timeScale = slider.value;
        timeScaleText.text = $"TimeScale {timeScale}";
        Time.timeScale = timeScale;
        timeScaleNumText.text = timeScale.ToString(CultureInfo.InvariantCulture);
    }
}
