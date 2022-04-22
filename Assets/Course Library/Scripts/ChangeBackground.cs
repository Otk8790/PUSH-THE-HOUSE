using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackground : MonoBehaviour
{
    [SerializeField]
    private GameObject background;

    [SerializeField]
    private Texture texture;
    
    private Renderer backgroundRenderer;

    // Start is called before the first frame update
    void Start()
    {
        backgroundRenderer = background.GetComponent<Renderer>();
        gameObject.GetComponent<Button>().onClick.AddListener(ChangeBackgroundTexture);
    }

    private void ChangeBackgroundTexture()
    {
        backgroundRenderer.material.mainTexture = texture;
    }
}
