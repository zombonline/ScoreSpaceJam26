using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortingLayerManager : MonoBehaviour
{
    public List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    private void Awake()
    {
        foreach(SpriteRenderer sprite in FindObjectsOfType<SpriteRenderer>())
        {
            sprites.Add(sprite);
        }

    }

    private void Update()
    {
        UpdateListOrder();
    }


    private void UpdateListOrder()
    {

        foreach (SpriteRenderer sprite in sprites)
        {
            var distanceFromCamera = Vector3.Distance(Camera.main.transform.position, sprite.transform.position);
            sprite.sortingOrder = -(UnityEngine.Mathf.RoundToInt((System.MathF.Round(distanceFromCamera, 2) * 100)));
        }
    }
}
