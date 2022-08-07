using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CollisionChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObj = other.gameObject;
        if (gameObj.tag == "Player")
        {
            ScoreControllerNetwork.GetInstance().AddScore(GetComponentInParent<Player>());
            StartCoroutine(ChangePlayerState(gameObj));
        }
    }
    public IEnumerator ChangePlayerState(GameObject player)
    {
        MeshRenderer meshPlayer = player.GetComponent<MeshRenderer>();
        Physics.IgnoreCollision(player.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), true);
        meshPlayer.material.color = Color.red;
        yield return new WaitForSeconds(Settings.GetInstance().TimeDelay);
        meshPlayer.material.color = Color.white;
        Physics.IgnoreCollision(player.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), false);
    }
}
