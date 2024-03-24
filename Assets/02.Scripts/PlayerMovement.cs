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
    public GameObject bulletPrefab; // �ν����Ϳ��� ������ �Ѿ� ������
    public Transform bulletSpawnPoint; // �Ѿ��� �߻�� ��ġ

    private float nextFireTime = 0.0f; // ���� �߻� ���� �ð�

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
        //TODO: UI ����
        if(Input.GetKeyDown(KeyCode.K))
            GameEvents.instance.gameStarted.SetValueAndForceNotify(true);

        MovePlayer();

        if (GameEvents.instance.gameStarted.Value
              && !GameEvents.instance.gameWon.Value && !GameEvents.instance.gameLost.Value)
        {
            // ���� �ð��� ���� �߻� �ð����� Ŭ ��� �Ѿ� �߻�
            if (Time.time > nextFireTime)
            {
                FireBullet();
                nextFireTime = Time.time + 1.0f / GameEvents.instance.bulletFireRate.Value; // ���� �߻� �ð� ������Ʈ
            }
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
    
    //�÷��̾��� ������ ����
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
