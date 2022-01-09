using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SavedScoresScreen : MonoBehaviour
{
    private List<Texture2D> savedScores;
    public Image selectedScore;
    public GameObject itemTemplate;
    private Vector2 initTransform;
    private Vector2 initTransformSelectedScore;
    private void Start()
    {
        //PlayerPrefs.SetInt("ScoreCounter", 0);
        //PlayerPrefs.Save();
        initTransform = gameObject.transform.localScale;
        initTransformSelectedScore = selectedScore.transform.localScale;
        transform.localScale = Vector2.zero;
        selectedScore.transform.localScale = Vector2.zero;
    }
    public void fillScoresList()
    {
        savedScores = new List<Texture2D>();
        for (int i = 1; i <= PlayerPrefs.GetInt("ScoreCounter" , 0); i++)
        {
            byte[] bytes = System.IO.File.ReadAllBytes(Application.dataPath + "/SavedScores/SavedScore" + i + ".png");
            Texture2D tex = new Texture2D(1440, 900);
            tex.LoadImage(bytes);
            savedScores.Add(tex);

        }
        //Object[] subListObjects = Resources.LoadAll("SavedScores", typeof(Texture2D));
    }

    public void ShowScoreScreen()
    {
        ClearScoreTab();
        FillScoreScreen();
        transform.LeanScale(initTransform, 0.3f);
    }
    public void HideScoreScreen()
    {
        transform.LeanScale(Vector2.zero, 0.3f);
    }
     
    public void HideSelectedScore()
    {
        selectedScore.transform.LeanScale(Vector2.zero, 0.3f);
    }
    public void FillScoreScreen()
    {
        fillScoresList();
        if (savedScores.Count > 0)
        {
            foreach (Texture2D score in savedScores)
            {               
                GameObject obj = Instantiate(itemTemplate);
                obj.transform.SetParent(transform);
                obj.transform.SetAsFirstSibling();
                obj.transform.LeanScale(Vector2.one, 0.3f);
                obj.GetComponent<Button>().onClick.AddListener(delegate { ClickedScore(obj); });
                obj.GetComponentInChildren<Image>().sprite = Sprite.Create(score, new Rect(0, 0, score.width, score.height), new Vector2(0.5f, 0.5f));
            }
        }
        else
            Debug.Log("Nu merge pozaa");
        

    }
    public void ClickedScore(GameObject obj)
    {
        selectedScore.sprite = obj.GetComponentInChildren<Image>().sprite;
        selectedScore.transform.LeanScale(initTransformSelectedScore, 0.3f);
        Debug.Log("works");
    }

    public void ClearScoreTab()
    {
        var gameObjects = GameObject.FindGameObjectsWithTag("ScoreScreenItemTemplate");

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }
}
