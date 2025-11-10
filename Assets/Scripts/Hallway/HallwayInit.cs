using UnityEngine;

public class HallwayInit : MonoBehaviour
{
    [SerializeField] private GameAssetsManager gAssets;

    void OnEnable()
    {
        GameObject[] assets = gAssets.GetGameAssets();
        for (int i = 0; i < 9; i++)
        {
            if (gAssets.IsItemAltered(i)) assets[i].SetActive(false);
        }
    }
}