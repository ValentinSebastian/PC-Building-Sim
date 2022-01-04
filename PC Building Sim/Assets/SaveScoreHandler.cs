using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SaveScoreHandler : MonoBehaviour
{
    private Camera scoreScreenshotCamera;
    private static SaveScoreHandler instance;
    private bool takeScreenshot;

    private void Awake()
    {
        instance = this;
        scoreScreenshotCamera = gameObject.GetComponent<Camera>();
    }
    void OnEnable()
    {
        RenderPipelineManager.endCameraRendering += RenderPipelineManager_endCameraRendering;
    }
    void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= RenderPipelineManager_endCameraRendering;
    }
    private void RenderPipelineManager_endCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        OnPostRender();
    }
    private void OnPostRender()
    {
        if(takeScreenshot)
        {
            takeScreenshot = false;
            RenderTexture rT = scoreScreenshotCamera.targetTexture;
            Texture2D renderResult = new Texture2D(rT.width, rT.height, TextureFormat.ARGB32 , false);
            Rect rect = new Rect(0, 0, rT.width, rT.height);
            renderResult.ReadPixels(rect, 0, 0);
            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/Resources/SavedScores/SavedScore" + System.DateTime.Now.ToString("HH_mm_dd_MMMM_yyyy") + ".png", byteArray); 
            Debug.Log("Saved Screenshot");
            scoreScreenshotCamera.targetTexture = null;
        }
    }
    public void TakeScreenshot()
    {
        scoreScreenshotCamera.targetTexture = RenderTexture.GetTemporary(1000 , 1000 , 16);
        takeScreenshot = true;
    }

}
