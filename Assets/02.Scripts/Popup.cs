using UnityEngine;
using DG.Tweening;

public class Popup : MonoBehaviour
{
    [SerializeField] private float delay;

    private void Awake()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 1).SetDelay(delay);
    }
}