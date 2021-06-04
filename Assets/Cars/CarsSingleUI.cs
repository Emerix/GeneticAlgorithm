using Cars;
using UnityEngine;
using UnityEngine.UI;

public class CarsSingleUI : CarsUIBase
{
    [SerializeField] private Text poolIndexText;

    [SerializeField] private CarsSingle cars;


    private void Start()
    {
        carsData = cars;
    }

    protected override void Update()
    {
        base.Update();
        poolIndexText.text = $"Slot in Generation {carsData.CurrentSlot.ToString()}";
    }
}