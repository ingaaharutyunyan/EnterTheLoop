using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistenceManager : MonoBehaviour
{
    [SerializeField] private GameObject[] targetObjects;

    void Start()
    {
        LoadPosition();
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnEnable()
    {
        LoadPosition();
    }

    private void OnSceneUnloaded(Scene scene)
    {
        SavePosition();
    }

    private void SavePosition()
    {
        for (int i = 0; i < targetObjects.Length; i++)
        {
            GameObject obj = targetObjects[i];
            string id = obj.name + "_" + i; // Unique key per object

            Vector3 pos = obj.transform.position;
            PlayerPrefs.SetFloat(id + "_x", pos.x);
            PlayerPrefs.SetFloat(id + "_y", pos.y);
            PlayerPrefs.SetFloat(id + "_z", pos.z);
        }

        PlayerPrefs.Save();
    }

    private void LoadPosition()
    {
        for (int i = 0; i < targetObjects.Length; i++)
        {
            GameObject obj = targetObjects[i];
            string id = obj.name + "_" + i;

            if (PlayerPrefs.HasKey(id + "_x"))
            {
                float x = PlayerPrefs.GetFloat(id + "_x");
                float y = PlayerPrefs.GetFloat(id + "_y");
                float z = PlayerPrefs.GetFloat(id + "_z");
                obj.transform.position = new Vector3(x, y, z);
            }
        }
    }
}
