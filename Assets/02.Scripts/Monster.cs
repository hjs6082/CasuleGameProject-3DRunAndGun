using UnityEngine;
using TMPro;
using DG.Tweening;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private int hp; //������ ü��
    [SerializeField]
    private int atk; // ������ ���ݷ� (�ʿ信 ���� ���)
    [SerializeField]
    private TextMeshPro hpText;

    private void Awake()
    {
        hpText.text = hp.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ���� ������Ʈ�� �±װ� "Bullet"���� Ȯ��
        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("�ε������ϴ�.");
            // ü�� ����
            hp -= atk;
            hpText.text = hp.ToString();

            // �Ѿ� ���� ������Ʈ ����
            Destroy(other.gameObject);

            // ������ ü���� 0 ���ϸ� ���� ����
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
        else if(other.tag == "Player")
        {
            Handheld.Vibrate();
            Camera.main.transform.DOShakePosition(0.1f, 0.5f, 5);

            GameEvents.instance.gameLost.SetValueAndForceNotify(true);
        }
    }
}
