using MiniGameCollection.Games2025.Team05;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortLayer : MonoBehaviour
{

    public SpriteRenderer foregroundSR;
    public SpriteRenderer backgroundSR;

    int foregroundSortLayer = 5;
    int backgroundSortLayer = 4;


    // Start is called before the first frame update
    void Start()
    {
        SetHigherSortOrder();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSortOrder();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<TagPlayer>() != null)
        {
            this.SetLowerSortOrder();
        }
    }

    private void UpdateSortOrder()
    {
        foregroundSR.sortingOrder = foregroundSortLayer;
        backgroundSR.sortingOrder = backgroundSortLayer;
    }

    private void SetLowerSortOrder()
    {
        foregroundSortLayer = 13;
        backgroundSortLayer = 12;
    }

    private void SetHigherSortOrder()
    {
        foregroundSortLayer = 3;
        backgroundSortLayer = 2;
    }
}
