using System.Globalization;
using PhysicsScenes;
using UnityEngine;
using UnityEngine.UI;

namespace Cars
{
    public class CarsUIBase : MonoBehaviour
    {

        [SerializeField]
        Text timeText;
        [SerializeField]
        Text generationText;
        [SerializeField]
        Text timeScaleText;
        [SerializeField]
        Slider slider;
        [SerializeField]
        Text timeScaleNumText;
        
        protected ICarsData carsData;

        protected virtual void Update()
        {
            timeText.text = $"Time left {carsData.CurrentTime.ToString(CultureInfo.InvariantCulture)}";
            generationText.text = $"Generation {carsData.CurrentIteration.ToString()}";

            float timeScale = slider.value;
            timeScaleText.text = $"TimeScale {timeScale}";
            Time.timeScale = timeScale;
            timeScaleNumText.text = timeScale.ToString(CultureInfo.InvariantCulture);
        }
    }
}