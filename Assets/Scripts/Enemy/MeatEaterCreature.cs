using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class MeatEaterCreature : MonoBehaviour
{
    [SerializeField] private GameObject meatEater, player;
    void Start()
    {
        GameManager.instance.GetMeatEaterManager().OnBeastInvoked += AddBeast;
    }

    private void AddBeast()
    {
        Instantiate(
            meatEater,
            new Vector3(player.transform.position.x + 30f, player.transform.position.y, player.transform.position.z),
            Quaternion.identity
        );
        StartMoving();
    }

    private void StartMoving()
    {
        float distance = Math.Abs(meatEater.transform.position.x - player.transform.position.x);
        meatEater.transform.DOMoveX(player.transform.position.x, distance).OnComplete(StartMoving);
    }
}
