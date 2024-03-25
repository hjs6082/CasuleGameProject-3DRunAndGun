using UnityEngine;
using System.Collections;

public class BulletMove : MonoBehaviour
{
    public float speed = 10.0f; // 총알 속도

    private void Awake()
    {
        StartCoroutine(BulletRemoveCoroutine());
    }

    void Update()
    {
        // 총알을 앞으로 이동시킴
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    IEnumerator BulletRemoveCoroutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject); // 화면 밖으로 나가면 총알 제거
    }
}
