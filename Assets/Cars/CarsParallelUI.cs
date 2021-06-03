using UnityEngine;

namespace Cars
{
    public class CarsParallelUI : CarsUIBase
    {
        [SerializeField] protected CarsMultiple cars;

        private void Start()
        {
            carsData = cars;
        }
    }
}