using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed = 10.0f; // �Ѿ� �ӵ�

    void Update()
    {
        // �Ѿ��� ������ �̵���Ŵ
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject); // ȭ�� ������ ������ �Ѿ� ����
    }

    // �̺κп� ü�� �ٴ� hp�� �߰�
    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(gameObject); // ��ü�� ������ �Ѿ� ����.
        // TODO �Ѿ˿� ���� ������Ʈ ü�� ����
    }
}
