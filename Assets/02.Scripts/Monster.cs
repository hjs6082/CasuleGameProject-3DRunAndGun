using UnityEngine;
using TMPro;
using DG.Tweening;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private int hp; //������ ü��

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

        // �浹�� ���� ������Ʈ�� �±װ� "Bullet"���� Ȯ��
        if (other.gameObject.tag == "Bullet")
        {
            // ü�� ����
            hp -= 1;
            hpText.text = hp.ToString();

            // �Ѿ� ���� ������Ʈ ����
            Destroy(other.gameObject);

            // ������ ü���� 0 ���ϸ� ���� ����
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
