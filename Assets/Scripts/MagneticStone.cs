using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticStone : MonoBehaviour
{
    public float magneticRadius = 10.0f; // �ڼ� ����
    public float magneticForce = 200.0f; // �ڼ� ��

    void FixedUpdate()
    {
        // �ڼ� ���� ���� ��� ������Ʈ�� ã��
        Collider[] colliders = Physics.OverlapSphere(transform.position, magneticRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rbNearby = nearbyObject.GetComponent<Rigidbody>();
            if (rbNearby != null)
            {
                // �ڼ� ȿ�� ���� (������Ʈ�� �ڽſ��� ���)
                Vector3 forceDirection = transform.position - rbNearby.transform.position;
                rbNearby.AddForce(forceDirection.normalized * magneticForce * Time.fixedDeltaTime);
            }
        }
    }
}
