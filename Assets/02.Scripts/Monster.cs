using UnityEngine;
using TMPro;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private int hp; //몬스터의 체력
    [SerializeField]
    private int atk; // 몬스터의 공격력 (필요에 따라 사용)
    [SerializeField]
    private TextMeshPro hpText;

    private void Awake()
    {
        hpText.text = hp.ToString();
    }

    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 게임 오브젝트의 태그가 "Bullet"인지 확인
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("부딪혔습니다.");
            // 체력 감소
            hp -= atk;
            hpText.text = hp.ToString();

            // 총알 게임 오브젝트 제거
            Destroy(collision.gameObject);

            // 몬스터의 체력이 0 이하면 몬스터 제거
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
