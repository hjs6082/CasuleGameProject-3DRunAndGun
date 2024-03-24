using TMPro;
using UnityEngine;
using DG.Tweening;

public class Block : MonoBehaviour
{
    [Header("Size & Color")]
    [SerializeField] private int blockHp;
    [SerializeField] private Material[] blockColor;
    [SerializeField] private MeshRenderer blockMesh;

    [Header("References")]
    [SerializeField] private GameObject completeBlock;
    [SerializeField] private GameObject brokenBlock;
    [SerializeField] private TextMeshPro blockSizeText;

    private void Awake()
    {
        completeBlock.SetActive(true);
        brokenBlock.SetActive(false);
        blockSizeText.text = blockHp.ToString();
        AssignColor();
    }

    private void AssignColor()
    {
        int colorIndex = Random.Range(0,blockColor.Length);
        blockMesh.material = blockColor[colorIndex];
    }

    public void CheckHit()
    {
        if (completeBlock.activeSelf)
        {
            Handheld.Vibrate();
            Camera.main.transform.DOShakePosition(0.1f, 0.5f, 5);

            GameEvents.instance.gameLost.SetValueAndForceNotify(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            if (completeBlock.activeSelf)
            {
                Destroy(other.gameObject);
            }
            blockHp--;
            if (blockHp <= 0)
            {
                completeBlock.SetActive(false);
                brokenBlock.SetActive(true);
                blockSizeText.gameObject.SetActive(false);
                return;
            }
            blockSizeText.text = blockHp.ToString();
        }
    }
}