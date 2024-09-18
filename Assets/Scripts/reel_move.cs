using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class reel_move : MonoBehaviour
{
    private RectTransform[] symbols;
    private float speed = -100f;
    private float resetPositionY = -32f;
    private float startPositionY = 16f;
    public float stopAfterSeconds;
    private bool isMoving = false;
    private Image imgSelector;
    private int butonCheck = 0;
    private float spintime;
    private bool activeButton = false;
    private float slowReel = 0f;
    private string pathSymbol = "Simbols/";
    private string[] imgSymols= {"S01","S02","S03","S04"};

    internal void ExecuteFullScript(int delay)
    {
        Update();
        isMoving = true;
        spintime+= delay;
    }
    void Start()
    {
        symbols = new RectTransform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            symbols[i] = transform.GetChild(i).GetComponent<RectTransform>();
        }
        spintime= Time.time+ 5f;
        
    }

    void Update()
    {
        if (isMoving == true)
        {
            slowReel += 0.11f;
            foreach (RectTransform symbol in symbols)
            {
                float newYPosition =speed + slowReel;
                if (spintime == 8f)
                {
                     newYPosition = Mathf.Min(newYPosition, -10f);
                }
                else
                {
                    newYPosition = Mathf.Min(newYPosition, -10f);
                }
                symbol.anchoredPosition += new Vector2(0, newYPosition * Time.deltaTime);
                
                if (symbol.anchoredPosition.y <= resetPositionY)
                {
                    int randNumber = Random.Range(0, 3);
                    symbol.anchoredPosition = new Vector2(symbol.anchoredPosition.x, startPositionY);
                    imgSelector = symbol.GetComponent<Image>();
                    Sprite newSprite = Resources.Load<Sprite>(pathSymbol + imgSymols[randNumber]);
                    imgSelector.sprite = newSprite;
                }
                if (Time.time > spintime && symbol.anchoredPosition.y < 1f && symbol.anchoredPosition.y > 0.70f)
                {
                    isMoving = false;
                    slowReel= 0f;
                    spintime= Time.time+ 5f;
                }
            }
            
            
        }
            
    }

    
}
