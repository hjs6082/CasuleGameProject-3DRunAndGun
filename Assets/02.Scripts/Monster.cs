using UnityEngine;
using TMPro;

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

    void OnCollisionEnter(Collision collision)
    {
        // �浹�� ���� ������Ʈ�� �±װ� "Bullet"���� Ȯ��
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("�ε������ϴ�.");
            // ü�� ����
            hp -= atk;
            hpText.text = hp.ToString();

            // �Ѿ� ���� ������Ʈ ����
            Destroy(collision.gameObject);

            // ������ ü���� 0 ���ϸ� ���� ����
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
