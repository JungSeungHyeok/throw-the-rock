//using UnityEngine;
//using UnityEngine.UI;

//public class PowerSlider : MonoBehaviour
//{
//    public Slider slider;
//    public GameObject targetObject;  // Empty GameObject�� ���� ��ũ��Ʈ�� ����Ű�� ����

//    void Start()
//    {
//        slider.onValueChanged.AddListener(OnSliderValueChanged);
//    }

//    void OnSliderValueChanged(float value)
//    {
//        // Target GameObject�� Script�� �����Ͽ� Spacing ���� ����
//        var script = targetObject.GetComponent<StoneControler>();  // YourScriptName�� Spacing ������ �ִ� ��ũ��Ʈ �̸�
//        script.Spacing = value;
//    }
//}
