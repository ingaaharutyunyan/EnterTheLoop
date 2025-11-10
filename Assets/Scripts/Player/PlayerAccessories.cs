using UnityEngine;

public class PlayerAccessories : MonoBehaviour
{
    [SerializeField] private PlayerSO pSO;
    [SerializeField] private GameObject duck, screwdriver, flashlight;

    void OnEnable()
    {
        if (pSO.GetDuckActive()) duck.SetActive(true);
        else duck.SetActive(false);
        
        if (pSO.GetScrewDriverActive()) screwdriver.SetActive(true);
        else screwdriver.SetActive(false);

        if (pSO.GetFlashlightActive()) flashlight.SetActive(true);
        else flashlight.SetActive(false);
        
        pSO.SetDuckActive += SetDuckActive;
        pSO.SetScrewdriverActive += SetScrewdriverActive;
        pSO.SetFlashlightActive += SetFlashlightActive;
    }

    void OnDisable()
    {
        pSO.SetDuckActive -= SetDuckActive;
        pSO.SetScrewdriverActive -= SetScrewdriverActive;
        pSO.SetFlashlightActive -= SetFlashlightActive;
    }

    void SetDuckActive()
    {
        duck.SetActive(true);
    }

    void SetScrewdriverActive()
    {
        screwdriver.SetActive(true);
    }

    void SetFlashlightActive()
    {
        flashlight.SetActive(true);
    }
}
