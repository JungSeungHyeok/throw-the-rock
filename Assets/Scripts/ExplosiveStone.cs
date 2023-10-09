using UnityEngine;

public class ExplosiveStone : Stone
{
    public float explosionRadius = 5.0f;
    public float explosionForce = 10.0f;

    private float timeSinceSpawn = 0.0f; // �߰��� ����

    void FixedUpdate()
    {
        timeSinceSpawn += Time.deltaTime; // �ð� ����

        if (timeSinceSpawn < 3.0f) // 1�� �̳���� �ƹ��͵� ���� �ʰ� ��ȯ
        {
            return;
        }

        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb.velocity.magnitude < 0.1f)
        {
            // �ֺ� ������Ʈ�� ���� ȿ�� ����
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rbNearby = nearbyObject.GetComponent<Rigidbody>();
                if (rbNearby != null)
                {
                    rbNearby.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }

            Destroy(gameObject);
        }
    }
}
