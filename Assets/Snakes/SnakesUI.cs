using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakesUI : MonoBehaviour
{
    [SerializeField]
    Snakes snakes;

    [SerializeField]
    Text timeText;
    [SerializeField]
    Text poolIndexText;
    [SerializeField]
    Text generationText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = $"Time left {snakes.CurrentTime.ToString()}";
        poolIndexText.text = $"Slot in Generation {snakes.CurrentSlot.ToString()}";
        generationText.text = $"Generation {snakes.CurrentIteration.ToString()}";
    }
}
