using UnityEngine;

public class PlayerIdle : MonoBehaviour
{
    public void PlayerIdleReset()
    {
        GameObject.FindGameObjectWithTag("CombatEncounter").GetComponent<CombatManager>().PlayerIdle();
    }
}
