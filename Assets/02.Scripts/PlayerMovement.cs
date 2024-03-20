using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float limitX;
    [SerializeField] private float sidewaySpeed;
    [SerializeField] private Transform playerModel;
    public GameObject bulletPrefab; // 인스펙터에서 지정할 총알 프리팹
    public Transform bulletSpawnPoint; // 총알이 발사될 위치
    public float fireRate = 1.0f; // 초당 발사 속도

    private float nextFireTime = 0.0f; // 다음 발사 가능 시간

    private bool lockControls;
    private float _finalPos;
    private float _currentPos;

    private void Update()
    {
        MovePlayer();

        // 현재 시간이 다음 발사 시간보다 클 경우 총알 발사
        if (Time.time > nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + 1.0f / fireRate; // 다음 발사 시간 업데이트
        }
    }

    void FireBullet()
    {
        // 총알 프리팹 인스턴스화 및 발사 위치와 방향 설정
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
