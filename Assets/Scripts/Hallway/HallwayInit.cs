using UnityEngine;

public class HallwayInit : MonoBehaviour
{
    [SerializeField] private GameAssetsManager gAssets;
    [SerializeField] private int index;
    [SerializeField] private PlayerSO pSO;

    public int GetIndex() { return index; }

    void OnEnable()
    {
        GameObject[] assets = gAssets.GetGameAssets();
        for (int i = 0; i < assets.Length; i++)
        {
            if (gAssets.IsItemAltered(i))
                assets[i].SetActive(false);
        }
    }

    void Start()
    {
        Vector2 savedPos = pSO.LoadHallway(index);
        transform.position = savedPos;
    }

    public void SaveData()
    {
        pSO.SaveHallway(index, transform.position);
    }
}
