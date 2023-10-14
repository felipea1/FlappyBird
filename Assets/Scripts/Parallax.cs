using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public float animationSpeed = 1f;
    public Texture2D dayTexture;
    public Texture2D nightTexture; 
    private float timeElapsed = 0f;
    public float timeToChangeTexture = 60f; 
    public bool shouldChangeTexture = true;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeToChangeTexture)
        {
            ToggleDayNightTexture();
            timeElapsed = 0f; 
        }

        meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
    }

    private void ToggleDayNightTexture()
    {
        if (meshRenderer.material.mainTexture == dayTexture)
        {
            meshRenderer.material.mainTexture = nightTexture;
        }
        else
        {
            meshRenderer.material.mainTexture = dayTexture;
        }
    }
}