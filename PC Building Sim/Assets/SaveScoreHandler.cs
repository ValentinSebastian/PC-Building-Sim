using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class SaveScoreHandler : MonoBehaviour
{
    private Camera scoreScreenshotCamera;
    private bool takeScreenshot;
    private int counter;

    private void Awake()
    {
        counter = PlayerPrefs.GetInt("ScoreCounter" , 0);
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
            counter++;
            System.IO.File.WriteAllBytes(Application.dataPath + "/SavedScores/SavedScore" + counter + ".png", byteArray); //+ System.DateTime.Now.ToString("HH_mm_dd_MMMM_yyyy") + ".png"
            Debug.Log("Saved Screenshot");
            PlayerPrefs.SetInt("ScoreCounter", counter);
            PlayerPrefs.Save();
            scoreScreenshotCamera.targetTexture = null;
        }
    }
    public void TakeScreenshot()
    {
        if (!Directory.Exists(Application.dataPath + "/SavedScores"))
        {
            Directory.CreateDirectory(Application.dataPath + "/SavedScores");
        }
        scoreScreenshotCamera.targetTexture = RenderTexture.GetTemporary(1920, 1080 , 16);
        takeScreenshot = true;
    }

}
