using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject item;
    public CellsManager cellsContainer;

    [HideInInspector] public GameObject potential;

    [HideInInspector] public bool mouseGrab = false;

    public void VictoryCheck()
    {
        foreach(Transform cells in cellsContainer.transform)
        {
            if (cells.GetComponent<Cell>().fill) return;
        }

        winScreen.SetActive(true);
        cellsContainer.tutorialActive = false;
    }

    public void WipeCheck()
    {
        bool checkRow = true;

        for(int i = 0; i < cellsContainer.ySize; i++)
        {
            checkRow = true;
            for(int j = 0; j < cellsContainer.xSize; j++)
            {
                if (!cellsContainer.transform.GetChild(i * cellsContainer.xSize + j).GetComponent<Cell>().fill) checkRow = false;
            }

            if (checkRow)
            {
                for (int j = 0; j < cellsContainer.xSize; j++)
                {
                    cellsContainer.transform.GetChild(i * cellsContainer.xSize + j).GetChild(0).GetComponent<Animator>().Play("Item_Destruct");
                    cellsContainer.transform.GetChild(i * cellsContainer.xSize + j).GetComponent<Cell>().fill = false;

                    cellsContainer.transform.GetChild(i * cellsContainer.xSize + j).GetComponent<Animator>().Play("Cell_Destruct");
                }
            }
        }
        cellsContainer.Save();
        VictoryCheck();
    }

    public void RestartGame()
    {
        cellsContainer.Restart();
    }
}
