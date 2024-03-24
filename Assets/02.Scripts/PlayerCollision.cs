using UnityEngine;
using DG.Tweening;

public class PlayerCollision : MonoBehaviour
{
/*    [SerializeField] private GameObject bloodParticles;
    private Animator playerAnim;*/

    private void Awake()
    {
/*        playerAnim = GetComponent<Animator>();
        bloodParticles.SetActive(false);*/
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BulletRate")
        {
            GameEvents.instance.bulletFireRate.Value += 1;
            other.GetComponent<Collider>().enabled = false;
            other.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
            {
                Destroy(other.gameObject);
            });
        }
        if (other.tag == "Obstacle")
        {
            //playerAnim.SetTrigger();
            other.GetComponent<Block>().CheckHit();
        }
    }
}