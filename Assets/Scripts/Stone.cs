using UnityEngine;

public class Stone : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Vector3 averageNormal = Vector3.zero;

            foreach (ContactPoint contact in collision.contacts)
            {
                // �� ���� ������ ���� ���͸� ����
                averageNormal += contact.normal;
            }

            // ��� ���� ���͸� ���
            averageNormal = averageNormal / collision.contacts.Length;

            // �ݻ� ���� ���
            Vector3 reflectedVelocity = rb.velocity - 2 * (Vector3.Dot(rb.velocity, averageNormal)) * averageNormal;

            // ���� ����
            rb.AddForce(reflectedVelocity - rb.velocity, ForceMode.VelocityChange);
        }
    }
}
