using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();

    [SerializeField] private float limitX;
    [SerializeField] private float sidewaySpeed;
    [SerializeField] private Transform playerModel;
    public GameObject bulletPrefab; // 인스펙터에서 지정할 총알 프리팹
    public Transform bulletSpawnPoint; // 총알이 발사될 위치

    private float nextFireTime = 0.0f; // 다음 발사 가능 시간

    private bool lockControls;
    private float _finalPos;
    private float _currentPos;

    private void OnEnable()
    {
        StartCoroutine(Subscribe());
    }
    private IEnumerator Subscribe()
    {
        yield return new WaitUntil(() => GameEvents.instance != null);
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButton(0))
            .Subscribe(x =>
            {
                if (GameEvents.instance.gameStarted.Value && !GameEvents.instance.gameLost.Value
                && !GameEvents.instance.gameWon.Value)
                {
                    MovePlayer();
                }
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameWon.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    lockControls = true;
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameLost.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    lockControls = true;
            })
            .AddTo(subscriptions);
    }
    private void OnDisable()
    {
        subscriptions.Clear();
    }

    private void Update()
    {
        //TODO: UI 생성
        if(Input.GetKeyDown(KeyCode.K))
            GameEvents.instance.gameStarted.SetValueAndForceNotify(true);

        MovePlayer();

        if (GameEvents.instance.gameStarted.Value
              && !GameEvents.instance.gameWon.Value && !GameEvents.instance.gameLost.Value)
        {
            // 현재 시간이 다음 발사 시간보다 클 경우 총알 발사
            if (Time.time > nextFireTime)
            {
                FireBullet();
                nextFireTime = Time.time + 1.0f / GameEvents.instance.bulletFireRate.Value; // 다음 발사 시간 업데이트
            }
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
    
    //플레이어의 방향을 조절
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
