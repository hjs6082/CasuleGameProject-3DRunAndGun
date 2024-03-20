using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float limitX;
    [SerializeField] private float sidewaySpeed;
    [SerializeField] private Transform playerModel;
    public GameObject bulletPrefab; // �ν����Ϳ��� ������ �Ѿ� ������
    public Transform bulletSpawnPoint; // �Ѿ��� �߻�� ��ġ
    public float fireRate = 1.0f; // �ʴ� �߻� �ӵ�

    private float nextFireTime = 0.0f; // ���� �߻� ���� �ð�

    private bool lockControls;
    private float _finalPos;
    private float _currentPos;

    private void Update()
    {
        MovePlayer();

        // ���� �ð��� ���� �߻� �ð����� Ŭ ��� �Ѿ� �߻�
        if (Time.time > nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + 1.0f / fireRate; // ���� �߻� �ð� ������Ʈ
        }
    }

    void FireBullet()
    {
        // �Ѿ� ������ �ν��Ͻ�ȭ �� �߻� ��ġ�� ���� ����
        if (bulletPrefab && bulletSpawnPoint)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        }
    }

    private void MovePlayer()
    {
        if (Input.GetMouseButton(0))
        {
            float percentageX = (Input.mousePosition.x - Screen.width / 2) / (Screen.width * 0.5f) * 2;
            percentageX = Mathf.Clamp(percentageX, -1.0f, 1.0f);
            _finalPos = percentageX * limitX;
        }

        float delta = _finalPos - _currentPos;
        _currentPos += (delta * Time.deltaTime * sidewaySpeed);
        _currentPos = Mathf.Clamp(_currentPos, -limitX, limitX);
        playerModel.localPosition = new Vector3(_currentPos, 0, 0);
    }
}
