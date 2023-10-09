// PlayerMovement.cs
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public StoneControler stoneController;

    void Update()
    {
        if (stoneController.currentState == StoneControler.ThrowState.Charging)
        {
            return;
        }

        float moveHorizontal = 0;

        if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal = 1;
        }

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        transform.position += movement * speed * Time.deltaTime;

        if (stoneController != null)  // StoneController���� �÷��̾��� �� ��ġ �˸�
        {
            stoneController.UpdatePreviewStonePosition(transform.position);
        }
    }
}
