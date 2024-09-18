using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controller : MonoBehaviour
{
    private reel_move[] reelMoves;
    public GameObject prefab;
    public Canvas targetCanvas;
    public int numReel;
    public Button myButton;
    private static Button staticButton;
    private int xpReel = -700;
    private int delay = 0;
    public void Start(){

        staticButton = myButton;
        
        for (int i = 0; i < numReel; i++)
        {
            GameObject newPrefab = Instantiate(prefab, targetCanvas.transform);
            Transform prefabPosition = newPrefab.GetComponent<Transform>();
            prefabPosition.localPosition = new Vector2(xpReel, 0);
            xpReel += 400;
        }

    }
    public void OnSpinButtonClick()
    {
        myButton.interactable = false;
        StartCoroutine(ExecuteSpin());
    }
    public IEnumerator SetButtonActive()
    {
        yield return new WaitForSeconds(6.5f);
        staticButton.interactable = true;
    }
    
    // Start is called before the first frame update
    private IEnumerator ExecuteSpin()
    {
        // Obtén todos los componentes reel_move en los hijos
        reelMoves = GameObject.FindObjectsOfType<reel_move>();

        if (reelMoves.Length == 0)
        {
            Debug.LogError("No se encontraron componentes 'reel_move'.");
            yield break;
        }

        // Ejecutar el script en cada reel_move encontrado
        for (int i = reelMoves.Length - 1; i >= 0; i--)
        {
            reel_move script = reelMoves[i];
            script.ExecuteFullScript(delay);
            delay++;
            Debug.Log(delay);
            yield return new WaitForSeconds(0.6f);
        }
         StartCoroutine(SetButtonActive());
    }

}
