using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.iOS;

public class SpawnPockets : MonoBehaviour
{
    public GameObject cell;

    GameObject[] cells;

    GameManager gameManager;

    int cc;
    int loosecount = 0;

    public int xSize = 3;
    public int ySize = 3;

    public GridLayoutGroup gridLG;

    public void cleanDesk()
    {
        for (int i = 0; i < loosecount; i++)
        {
            cells[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

        RandomSpawn();
        RandomSpawn();
    }

    void CellSpawn()
    {
        for(int i = 0; i < xSize; i++)
            for(int j = 0; j<ySize; j++)
            {
                GameObject celltmp = Instantiate(cell, new Vector2(0,0), Quaternion.identity);
                celltmp.transform.SetParent(gameObject.transform);
                celltmp.transform.localPosition = new Vector2(0,0);
                celltmp.transform.localScale = new Vector3(1,1,1);
                loosecount++;
            }


        cc = gameObject.transform.childCount;
        cells = new GameObject[cc];

        for (int c = 0; c < cc; c++)
        {
            cells[c] = gameObject.transform.GetChild(c).gameObject;
        }

        RandomSpawn();
        RandomSpawn();

    }

    void RandomSpawn()
    {
        int rc = Random.Range(0, cc);
        int lc = loosecount;


        while (cells[rc].transform.GetChild(0).gameObject.activeSelf)
        {
            rc = Random.Range(0, cc);
            lc--;
            if (lc < 0) return;
        }
        cells[rc].transform.GetChild(0).gameObject.SetActive(true);

    }

    private void Start()
    {
        gridLG.cellSize = new Vector2(gridLG.GetComponent<RectTransform>().rect.width / xSize, gridLG.GetComponent<RectTransform>().rect.width / xSize);

        gridLG.constraintCount = ySize;
        gameManager = FindObjectOfType<GameManager>();

        CellSpawn();
    }

    void Update()
    {

    }

}
