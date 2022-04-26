using System.Collections;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public void StartRevive(GameObject corpse, float time)
    {
        StartCoroutine(Revive(corpse, time));
    }

    IEnumerator Revive(GameObject corpse, float time)
    {
        corpse.SetActive(false);
        yield return new WaitForSeconds(time);
        corpse.SetActive(true);
    }

}
