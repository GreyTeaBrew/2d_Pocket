using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public bool fill = false;
    public bool canGrab = false;
    bool isGrabed = false;

    public GameObject item;

    GameManager gameManager;

    public bool bigPocket = false;
    public bool smallPocket = false;

    public void SpawnItem()
    {
        GameObject newItem = Instantiate(item, Vector2.zero, Quaternion.identity);
        newItem.transform.SetParent(gameObject.transform);
        newItem.transform.localPosition = Vector2.zero;
        newItem.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        fill = true;
    }

    public void Grab()
    {
        if (canGrab && fill)
        {
            isGrabed = true;
            gameManager.mouseGrab = true;
            gameManager.potential = null;
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Canvas>().sortingOrder = 2;
        }
    }

    public void DestroyItem()
    {
        Destroy(gameObject.transform.GetChild(0).gameObject);
    }

    public void UnGrab()
    {
        if (gameManager.mouseGrab)
        {
            isGrabed = false;
            gameManager.mouseGrab = false;
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Canvas>().sortingOrder = 1;

            if (gameManager.potential && !gameManager.potential.GetComponent<Cell>().fill)
            {
                
                fill = false;
                Destroy(gameObject.transform.GetChild(0).gameObject);
                gameManager.potential.GetComponent<Cell>().SpawnItem();
                gameManager.potential.GetComponent<Cell>().gameObject.GetComponent<Animator>().Play("Cell_Exit");
                gameManager.WipeCheck();

                return;
            }

            gameObject.transform.GetChild(0).transform.localPosition = Vector2.zero;
        }
    }

    public void CheckFill()
    {
        if (gameManager.mouseGrab && !fill)
        {
            gameObject.GetComponent<Animator>().Play("Cell_Enter");
            gameManager.potential = gameObject;
        }
    }

    public void BackToNormal()
    {
        if (gameManager.mouseGrab && !fill)
        {
            gameObject.GetComponent<Animator>().Play("Cell_Exit");
        }
        gameManager.potential = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (bigPocket)
        {
            if (PlayerPrefs.HasKey("BigPocket"))
            {
                if ((PlayerPrefs.GetInt("BigPocket") == 1) && !fill)
                {
                    SpawnItem();
                }
                if (PlayerPrefs.GetInt("BigPocket") == 0)
                {
                    if (transform.childCount > 0)
                    {
                        Destroy(transform.GetChild(0).gameObject);
                        fill = false;
                    }
                }
            }
        }

        if (smallPocket)
        {
            if (PlayerPrefs.HasKey("SmallPocket"))
            {
                if ((PlayerPrefs.GetInt("SmallPocket") == 1) && !fill)
                {
                    SpawnItem();
                }
                if (PlayerPrefs.GetInt("SmallPocket") == 0)
                {
                    if (transform.childCount > 0)
                    {
                        Destroy(transform.GetChild(0).gameObject);
                        fill = false;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrabed)
        {
            gameObject.transform.GetChild(0).transform.position = Input.mousePosition;
        }
    }

    private void OnApplicationQuit()
    {
        if (bigPocket)
        {
            if(fill)
                PlayerPrefs.SetInt("BigPocket", 1);
            else
                PlayerPrefs.SetInt("BigPocket", 0);
        }

        if (smallPocket)
        {
            if (fill)
                PlayerPrefs.SetInt("SmallPocket", 1);
            else
                PlayerPrefs.SetInt("SmallPocket", 0);
        }
    }
}
