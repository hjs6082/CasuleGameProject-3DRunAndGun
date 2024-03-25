using UnityEngine;
using TMPro;
using DG.Tweening;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private int hp; //몬스터의 체력

    [SerializeField]
    private TextMeshPro hpText;

    private Animator anim;

    private void Awake()
    {
        hpText.text = hp.ToString();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hp <= 0)
            return;

        // 충돌한 게임 오브젝트의 태그가 "Bullet"인지 확인
        if (other.gameObject.tag == "Bullet")
        {
            // 체력 감소
            hp -= 1;
            hpText.text = hp.ToString();

            // 총알 게임 오브젝트 제거
            Destroy(other.gameObject);

            // 몬스터의 체력이 0 이하면 몬스터 제거
            if (hp <= 0)
            {
                anim.SetTrigger("die");
                hpText.enabled = false;
            }
        }
        else if(other.tag == "Player")
        {
            Handheld.Vibrate();
            Camera.main.transform.DOShakePosition(0.1f, 0.5f, 5);

            anim.SetTrigger("attack");
            GameEvents.instance.gameLost.SetValueAndForceNotify(true);
        }
    }
}
