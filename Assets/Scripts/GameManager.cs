using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private EquationGenerator equationGenerator;
    public Material wall;

    private Vector2 wallOffset = new Vector2(0, 0);

    void Start()
    {
        equationGenerator = FindAnyObjectByType<EquationGenerator>();
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        while (true)
        {
            yield return StartCoroutine(WalkForward());
            yield return StartCoroutine(equationGenerator.RunEquation());
        }
    }

    IEnumerator WalkForward()
    {
        float duration = Random.Range(2f, 4f);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            yield return null;

            wallOffset.y += -1f * Time.deltaTime;
            wall.mainTextureOffset = wallOffset;

            elapsed += Time.deltaTime;
        }
    }
}