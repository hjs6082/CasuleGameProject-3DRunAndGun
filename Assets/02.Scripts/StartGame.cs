using UnityEngine;

public class StartGame : MonoBehaviour
{
    public void GameStart()
    {
        GameEvents.instance.gameStarted.SetValueAndForceNotify(true);
    }
}