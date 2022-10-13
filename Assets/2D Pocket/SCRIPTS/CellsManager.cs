using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellsManager : MonoBehaviour
{
    public GameObject cell;

    GameObject[] cells;

    GameManager gameManager;

    public int xSize = 3;
    public int ySize = 3;

    public GridLayoutGroup gridLG;

    public bool tutorialActive = true;

    public void CleanDesk()
    {
        foreach (Transform c in transform)
        {
            Destroy(c.gameObject);
        }
    }

    void CellSpawn()
    {
        for (int i = 0; i < xSize; i++)
            for (int j = 0; j < ySize; j++)
            {
                GameObject celltmp = Instantiate(cell, new Vector2(0, 0), Quaternion.identity);
                celltmp.transform.SetParent(gameObject.transform);
                celltmp.transform.localPosition = new Vector2(0, 0);
                celltmp.transform.localScale = new Vector3(1, 1, 1);
            }

        if (!PlayerPrefs.HasKey("tutorialPassed"))
        {
            Debug.Log("Tut");
            TutorialFill();
            PlayerPrefs.SetInt("tutorialPassed", 1);
        }
    }

    void TutorialFill()
    {
        transform.GetChild(3).GetComponent<Cell>().SpawnItem();
        transform.GetChild(5).GetComponent<Cell>().SpawnItem();
    }

    public void Restart()
    {
        CleanDesk();

        gridLG.cellSize = new Vector2(gridLG.GetComponent<RectTransform>().rect.width / xSize, gridLG.GetComponent<RectTransform>().rect.width / xSize);
        gridLG.constraintCount = ySize;

        CellSpawn();
        Load();
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        Restart();
    }

    public void Save()
    {
        PlayerPrefs.SetInt("CellsCountX", xSize);
        PlayerPrefs.SetInt("CellsCountY", ySize);

        string cellsFilled = "";

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Cell>().fill)
            {
                cellsFilled += i + ",";
            }
        }

        PlayerPrefs.SetString("CellsFilled", cellsFilled);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("CellsCountX"))
        {
            xSize = PlayerPrefs.GetInt("CellsCountX");
        }
        if (PlayerPrefs.HasKey("CellsCountY"))
        {
            ySize = PlayerPrefs.GetInt("CellsCountY");
        }

        if (PlayerPrefs.HasKey("CellsFilled"))
        {

            string[] cellsFilled;
            cellsFilled = PlayerPrefs.GetString("CellsFilled").Split(',');
            for (int i = 0; i < cellsFilled.Length; i++)
            {
                if (!transform.GetChild(int.Parse(cellsFilled[i])).GetComponent<Cell>().fill)
                transform.GetChild(int.Parse(cellsFilled[i])).GetComponent<Cell>().SpawnItem();
            }
        }

    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
