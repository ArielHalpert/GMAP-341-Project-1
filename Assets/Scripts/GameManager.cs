using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    private EquationGenerator equationGenerator;
    public Material wall;
    public GameObject scoreText;

    private int score;

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
            score += 1;
            scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
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