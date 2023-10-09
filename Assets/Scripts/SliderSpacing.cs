using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SliderSpacing : MonoBehaviour
{
    public Slider slider;
    public GameObject fillContainer;
    public GameObject fillPrefab;  // 파란색 블록
    public GameObject emptyPrefab; // 흰색 블록
    public int totalFills = 10;
    public float spacing = 5f;

    private List<GameObject> fills = new List<GameObject>();

    void Start()
    {
        float positionX = 0;

        for (int i = 0; i < totalFills; i++)
        {
            // 파란색 블록 추가
            GameObject fill = Instantiate(fillPrefab, fillContainer.transform);
            fill.transform.localPosition = new Vector3(positionX, 0, 0);
            fills.Add(fill);

            positionX += fill.GetComponent<RectTransform>().rect.width + spacing;

            // 흰색 블록 추가
            GameObject emptyFill = Instantiate(emptyPrefab, fillContainer.transform);
            emptyFill.transform.localPosition = new Vector3(positionX, 0, 0);

            positionX += emptyFill.GetComponent<RectTransform>().rect.width + spacing;
        }
    }

    void Update()
    {
        int activeFills = Mathf.FloorToInt(slider.value * (totalFills * 2));  // 파란색 블록과 흰색 블록을 합한 개수

        for (int i = 0; i < fills.Count; i++)
        {
            fills[i].SetActive(i * 2 < activeFills);  // 파란색 블록만 활성/비활성화
        }
    }
}
