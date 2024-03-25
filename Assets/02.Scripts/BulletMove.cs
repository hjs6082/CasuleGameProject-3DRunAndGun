using UnityEngine;
using System.Collections;

public class BulletMove : MonoBehaviour
{
    public float speed = 10.0f; // �Ѿ� �ӵ�

    private void Awake()
    {
        StartCoroutine(BulletRemoveCoroutine());
    }

    void Update()
    {
        // �Ѿ��� ������ �̵���Ŵ
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    IEnumerator BulletRemoveCoroutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject); // ȭ�� ������ ������ �Ѿ� ����
    }
}
