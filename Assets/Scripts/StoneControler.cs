using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class StoneControler : MonoBehaviour
{
    private int maxBalls = 5;
    private int currentBallCount = 0;
    public PlayerMovement playerMovement; // 플레이어 움직임 제어

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

    // PlayerMovement에게 상태를 알려주어야해서 퍼블릭 변경
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

                    if (powerSlider != null) // Charging 상태로 들어갈 때 슬라이더를 활성화
                    {
                        powerSlider.gameObject.SetActive(true);
                    }

                    ///UpdatePowerSliderPosition();
                }
                break;

            case ThrowState.Charging:
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    if (currentBallCount < maxBalls) // 발사 가능한 경우
                    {
                        Throw(currentDirection);
                        currentState = ThrowState.Aiming;

                        CreatePreviewStone(); // 발사 후에만 새로운 previewStone을 생성, 다른곳 생성 x

                        currentPower = minPower; // 발사한뒤, 파워랑 슬라이더 초기화
                        powerSlider.value = currentPower;

                        if (powerSlider != null) // Aiming 상태로 돌아갈 때 슬라이더를 비활성화
                        {
                            powerSlider.gameObject.SetActive(false);
                        }

                        currentBallCount++; // 볼 수 증가
                    }
                    else
                    {
                        if (currentState == ThrowState.Charging)
                        {
                            currentState = ThrowState.Aiming;
                            powerSlider.gameObject.SetActive(false);  // 슬라이더 비활성화
                        }
                        StartCoroutine(EndGame());
                    }
                }
                break;
        }

        // 돌 타입 변경 테스트
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
        // 새로운 예시 돌 생성
        // 아래 위치에서 변경
        CreatePreviewStone();


        if (previewStone != null)
        {
            Rigidbody rb = previewStone.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;  // 물리 효과 활성화
                // rb.AddForce(direction * currentPower, ForceMode.Impulse); // 물리적 힘 적용
            }

            // 반사 적용
            PhysicMaterial physicMat = new PhysicMaterial();
            physicMat.bounciness = 1;
            previewStone.GetComponent<Collider>().material = physicMat;

            // 힘 다시 추가
            rb.AddForce(direction * currentPower, ForceMode.Impulse); // 현재 파워를 날림

            previewStone = null;  // 기존 참조 제거
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

        // 화살표의 위치와 방향을 currentDirection에 맞춤
        if (projectileArrow != null)
        {
            projectileArrow.transform.position = transform.position + currentDirection; // 화살표의 위치를 공의 날아갈 방향에 맞춤
            projectileArrow.transform.rotation = Quaternion.LookRotation(currentDirection); // 화살표의 방향을 공의 날아갈 방향에 맞춤
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

        yield return new WaitForSeconds(5);  // 5초 대기

        if (scoreManager.totalScore > 20)  // 점수가 20을 넘으면
        {
            SceneManager.LoadScene("ClearScene");  // 클리어 씬
        }
        else  // 점수가 20을 넘지 않으면
        {
            SceneManager.LoadScene("DefeatScene");  // 패배 씬
        }
    }

}