using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleControllerTestVersion : MonoBehaviour
{
    public Color[] bottleColors;
    public SpriteRenderer bottleMaskSR;
    public AnimationCurve sarmCurve, fillCurve;

    public float timeToRotate = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        UpdateColors();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(RotateBottle());
        }
    }

    void UpdateColors()
    {
        bottleMaskSR.material.SetColor("_Color4", bottleColors[0]);
        bottleMaskSR.material.SetColor("_Color3", bottleColors[1]);
        bottleMaskSR.material.SetColor("_Color2", bottleColors[2]);
        bottleMaskSR.material.SetColor("_Color1", bottleColors[3]);
    }

    IEnumerator RotateBottle()
    {
        float t = 0, lerpValue, angleValue;

        while (t < timeToRotate){
            lerpValue = t / timeToRotate;
            angleValue = Mathf.Lerp(0.0f, 90.0f, lerpValue);

            transform.eulerAngles = new Vector3(0, 0, angleValue);
            bottleMaskSR.material.SetFloat("_Sarm", sarmCurve.Evaluate(angleValue));
            t += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        angleValue = 90.0f;
        transform.eulerAngles = new Vector3(0, 0, angleValue);
        bottleMaskSR.material.SetFloat("_Sarm", sarmCurve.Evaluate(angleValue));
    }
}
