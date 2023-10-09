using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(RectTransform))]
public class SyncColliderWithUI : MonoBehaviour
{
    private BoxCollider boxCollider;
    private RectTransform rectTransform;

    public void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void Update()
    {
        // �������� ����Ͽ� BoxCollider�� ũ�� ����
        Vector3 newScale = rectTransform.localScale;
        boxCollider.size = new Vector3(rectTransform.rect.width * newScale.x, rectTransform.rect.height * newScale.y, boxCollider.size.z);

        // RectTransform�� ��ġ�� ����Ͽ� BoxCollider�� �߽� ��ġ ����
        // boxCollider.center = rectTransform.localPosition;
    }

}
