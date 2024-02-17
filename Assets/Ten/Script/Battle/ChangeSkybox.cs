using UnityEngine;

public class ChangeSkybox : MonoBehaviour
{
    public void Change(Material mat)
    {
        RenderSettings.skybox = mat;
    }
}
