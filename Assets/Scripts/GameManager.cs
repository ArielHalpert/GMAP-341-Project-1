using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private EquationGenerator equationGenerator;
    public Material wall;
    public GameObject scoreText;
    public GameObject floorText;
    public GameObject healthText;

    private int score;
    

    private Vector2 wallOffset = new Vector2(0, 0);

    void Start()
    {
        equationGenerator = FindAnyObjectByType<EquationGenerator>();
        StartCoroutine(GameLoop());
    }

    private void Update()
    {
        healthText.GetComponent<TextMeshProUGUI>().text = "Health: " + equationGenerator.health.ToString();
        if (equationGenerator.health < 1)
        {
            SceneManager.LoadScene("Lose");
        }
    }

    IEnumerator GameLoop()
    {
        while (true)
        {
            int floor = (score / 3);
            yield return StartCoroutine(WalkForward());
            yield return StartCoroutine(equationGenerator.RunEquation(Random.Range(3, floor + 4), floor + 1));
            score += 1;
            scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
            floor = (score / 3);
            string floorWhateverText = floor.ToString();
            floorText.GetComponent<TextMeshProUGUI>().text = "Floor: " + floorWhateverText;
        }
    }

    IEnumerator WalkForward()
    {
        float duration = Random.Range(5f, 10f);
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