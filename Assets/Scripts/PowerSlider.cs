//using UnityEngine;
//using UnityEngine.UI;

//public class PowerSlider : MonoBehaviour
//{
//    public Slider slider;
//    public GameObject targetObject;  // Empty GameObject에 붙은 스크립트를 가리키는 변수

//    void Start()
//    {
//        slider.onValueChanged.AddListener(OnSliderValueChanged);
//    }

//    void OnSliderValueChanged(float value)
//    {
//        // Target GameObject의 Script에 접근하여 Spacing 값을 변경
//        var script = targetObject.GetComponent<StoneControler>();  // YourScriptName은 Spacing 변수가 있는 스크립트 이름
//        script.Spacing = value;
//    }
//}
