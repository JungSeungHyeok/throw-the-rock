using UnityEngine;
using UnityEngine.UI;

public class LinearHealthBar : MonoBehaviour
{
    [Header("Data")]
    public float maxValue = 100f;
    public float currentValue = 100f;

    [Header("UI")]
    public Slider healthSlider;

    private void Start()
    {
        if (healthSlider == null)
        {
            Debug.LogError("LinearHealthBar: No Slider attached!");
            return;
        }

        healthSlider.maxValue = maxValue;
        healthSlider.value = currentValue;
    }

    public void SetValue(float value)
    {
        currentValue = Mathf.Clamp(value, 0, maxValue);
        healthSlider.value = currentValue;
    }

    public void AddValue(float valueToAdd)
    {
        SetValue(currentValue + valueToAdd);
    }
}
