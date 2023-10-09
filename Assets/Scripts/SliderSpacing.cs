using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SliderSpacing : MonoBehaviour
{
    public Slider slider;
    public GameObject fillContainer;
    public GameObject fillPrefab;  // �Ķ��� ���
    public GameObject emptyPrefab; // ��� ���
    public int totalFills = 10;
    public float spacing = 5f;

    private List<GameObject> fills = new List<GameObject>();

    void Start()
    {
        float positionX = 0;

        for (int i = 0; i < totalFills; i++)
        {
            // �Ķ��� ��� �߰�
            GameObject fill = Instantiate(fillPrefab, fillContainer.transform);
            fill.transform.localPosition = new Vector3(positionX, 0, 0);
            fills.Add(fill);

            positionX += fill.GetComponent<RectTransform>().rect.width + spacing;

            // ��� ��� �߰�
            GameObject emptyFill = Instantiate(emptyPrefab, fillContainer.transform);
            emptyFill.transform.localPosition = new Vector3(positionX, 0, 0);

            positionX += emptyFill.GetComponent<RectTransform>().rect.width + spacing;
        }
    }

    void Update()
    {
        int activeFills = Mathf.FloorToInt(slider.value * (totalFills * 2));  // �Ķ��� ��ϰ� ��� ����� ���� ����

        for (int i = 0; i < fills.Count; i++)
        {
            fills[i].SetActive(i * 2 < activeFills);  // �Ķ��� ��ϸ� Ȱ��/��Ȱ��ȭ
        }
    }
}
