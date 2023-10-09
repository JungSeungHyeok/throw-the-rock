using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class StoneControler : MonoBehaviour
{
    private int maxBalls = 5;
    private int currentBallCount = 0;
    public PlayerMovement playerMovement; // �÷��̾� ������ ����

    public ScoreManager scoreManager;

    public StoneManager stoneManager;
    public float throwSpeed = 10f;

    public Slider powerSlider;
    public float minPower = 10f;
    public float maxPower = 20f;

    public GameObject projectileArrow;
    public float angleIncrement = 20f;
    public float powerIncrement = 20f;

    public float currentPower;
    private float currentAngle;
    private Vector3 currentDirection;
    private GameObject previewStone;
    public string arrowTag = "AngleArrow";

    private bool isLeftndRight = true;

    // PlayerMovement���� ���¸� �˷��־���ؼ� �ۺ� ����
    public enum ThrowState { Aiming, Charging, ReadyToThrow }
    public ThrowState currentState = ThrowState.Aiming;

    private int weight = 2;

    private void Start()
    {
        powerSlider.minValue = minPower;
        powerSlider.maxValue = maxPower;
        currentPower = minPower;
        powerSlider.value = currentPower;

        if (powerSlider != null)
        {
            powerSlider.gameObject.SetActive(false);
        }

        CreatePreviewStone();
    }

    public void FixedUpdate()
    {
        switch (currentState)
        {
            case ThrowState.Aiming:
                UpdateAngle();
                break;

            case ThrowState.Charging:
                UpdatePower();
                break;
        }
    }

    private void Update()
    {

        switch (currentState)
        {
            case ThrowState.Aiming:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    currentState = ThrowState.Charging;

                    if (powerSlider != null) // Charging ���·� �� �� �����̴��� Ȱ��ȭ
                    {
                        powerSlider.gameObject.SetActive(true);
                    }

                    ///UpdatePowerSliderPosition();
                }
                break;

            case ThrowState.Charging:
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    if (currentBallCount < maxBalls) // �߻� ������ ���
                    {
                        Throw(currentDirection);
                        currentState = ThrowState.Aiming;

                        CreatePreviewStone(); // �߻� �Ŀ��� ���ο� previewStone�� ����, �ٸ��� ���� x

                        currentPower = minPower; // �߻��ѵ�, �Ŀ��� �����̴� �ʱ�ȭ
                        powerSlider.value = currentPower;

                        if (powerSlider != null) // Aiming ���·� ���ư� �� �����̴��� ��Ȱ��ȭ
                        {
                            powerSlider.gameObject.SetActive(false);
                        }

                        currentBallCount++; // �� �� ����
                    }
                    else
                    {
                        if (currentState == ThrowState.Charging)
                        {
                            currentState = ThrowState.Aiming;
                            powerSlider.gameObject.SetActive(false);  // �����̴� ��Ȱ��ȭ
                        }
                        StartCoroutine(EndGame());
                    }
                }
                break;
        }

        // �� Ÿ�� ���� �׽�Ʈ
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            stoneManager.ChangeStoneToType(0);
            CreatePreviewStone();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            stoneManager.ChangeStoneToType(1);
            CreatePreviewStone();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            stoneManager.ChangeStoneToType(2);
            CreatePreviewStone();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            stoneManager.ChangeStoneToType(3);
            CreatePreviewStone();
        }
    }

    public void Throw(Vector3 direction)
    {
        // ���ο� ���� �� ����
        // �Ʒ� ��ġ���� ����
        CreatePreviewStone();


        if (previewStone != null)
        {
            Rigidbody rb = previewStone.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;  // ���� ȿ�� Ȱ��ȭ
                // rb.AddForce(direction * currentPower, ForceMode.Impulse); // ������ �� ����
            }

            // �ݻ� ����
            PhysicMaterial physicMat = new PhysicMaterial();
            physicMat.bounciness = 1;
            previewStone.GetComponent<Collider>().material = physicMat;

            // �� �ٽ� �߰�
            rb.AddForce(direction * currentPower, ForceMode.Impulse); // ���� �Ŀ��� ����

            previewStone = null;  // ���� ���� ����
        }
    }



    private void UpdateAngle()
    {
        currentAngle += (isLeftndRight ? 1 : -1) * angleIncrement * Time.deltaTime;

        if (currentAngle >= 30f || currentAngle <= 0f)
        {
            isLeftndRight = !isLeftndRight;
        }

        currentDirection = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward;

        // ȭ��ǥ�� ��ġ�� ������ currentDirection�� ����
        if (projectileArrow != null)
        {
            projectileArrow.transform.position = transform.position + currentDirection; // ȭ��ǥ�� ��ġ�� ���� ���ư� ���⿡ ����
            projectileArrow.transform.rotation = Quaternion.LookRotation(currentDirection); // ȭ��ǥ�� ������ ���� ���ư� ���⿡ ����
        }
    }


    private void UpdatePower()
    {
        currentPower += powerIncrement * Time.deltaTime * weight;

        if (currentPower >= maxPower || currentPower <= minPower)
        {
            powerIncrement = -powerIncrement;
        }

        powerSlider.value = currentPower;
    }



    private void CreatePreviewStone()
    {
        if (previewStone != null)
        {
            Destroy(previewStone);
        }

        previewStone = stoneManager.CreateStone(transform.position, Quaternion.identity);
        Rigidbody rb = previewStone.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    public void UpdatePreviewStonePosition(Vector3 newPosition)
    {
        if (previewStone != null)
        {
            previewStone.transform.position = newPosition;
        }
    }


    IEnumerator EndGame()
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        yield return new WaitForSeconds(5);  // 5�� ���

        if (scoreManager.totalScore > 20)  // ������ 20�� ������
        {
            SceneManager.LoadScene("ClearScene");  // Ŭ���� ��
        }
        else  // ������ 20�� ���� ������
        {
            SceneManager.LoadScene("DefeatScene");  // �й� ��
        }
    }

}