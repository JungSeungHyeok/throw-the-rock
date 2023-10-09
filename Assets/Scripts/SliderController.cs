using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider powerSlider;
    public float minPower = 10f;
    public float maxPower = 20f;
    public float powerIncrement = 20f;

    public StoneControler stoneControler;

    public void Start()
    {
        powerSlider.minValue = minPower;
        powerSlider.maxValue = maxPower;
        stoneControler.currentPower = minPower; // 연동
        powerSlider.value = stoneControler.currentPower;
    }

    public void Update()
    {
        if (stoneControler.currentState == StoneControler.ThrowState.Charging)
        {
            UpdatePower();
        }
    }

    private void UpdatePower()
    {
        stoneControler.currentPower += powerIncrement * Time.deltaTime; // 연동

        if (stoneControler.currentPower >= maxPower || stoneControler.currentPower <= minPower)
        {
            powerIncrement = -powerIncrement;
        }

        powerSlider.value = stoneControler.currentPower; // 연동
    }
}
