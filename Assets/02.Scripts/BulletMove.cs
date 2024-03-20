using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed = 10.0f; // 총알 속도

    void Update()
    {
        // 총알을 앞으로 이동시킴
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject); // 화면 밖으로 나가면 총알 제거
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(gameObject); // 물체에 닿으면 총알 제거.
        // TODO 총알에 닿은 오브젝트 체력 감소
    }
}
