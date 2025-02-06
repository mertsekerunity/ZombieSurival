using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDamage : MonoBehaviour
{
    [SerializeField] Canvas damageCanvas;
    [SerializeField] List<GameObject> bloodImages;
    [SerializeField] float impactTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var image in bloodImages)
        {
            image.SetActive(false);
        }
    }

    public void ShowDamageImpact()
    {
        StartCoroutine(ShowSplatter());
    }

    IEnumerator ShowSplatter()
    {
        int randomIndex = Random.Range(0, bloodImages.Count);
        GameObject selectedImage = bloodImages[randomIndex];

        selectedImage.SetActive(true);
        yield return new WaitForSeconds(impactTime);
        selectedImage.SetActive(false);
    }
}
