using UniRx;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class BulletFireRate : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();
    [SerializeField] private TextMeshPro fireRateText;

    private void OnEnable()
    {
        StartCoroutine(Subscribe());
    }
    private IEnumerator Subscribe()
    {
        yield return new WaitUntil(() => GameEvents.instance != null);
        GameEvents.instance.bulletFireRate.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                fireRateText.text = value.ToString();
                fireRateText.transform.parent.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.25f);
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameWon.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    fireRateText.transform.parent.DOScale(Vector3.zero, 0.25f);
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameLost.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    fireRateText.transform.parent.DOScale(Vector3.zero, 0.25f);
            })
            .AddTo(subscriptions);
    }
    private void OnDisable()
    {
        subscriptions.Clear();
    }
}