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
    public GameObject enemy;

    public static int score;
    

    private Vector2 wallOffset = new Vector2(0, 0);

    void Start()
    {
        score = 0;
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
            int floor = score / 5;

            Random.InitState(floor);
            float hue = Random.value;

            Color color = Color.HSVToRGB(hue, 1f, 1f);
            wall.color = color;

            Vector3 pos = enemy.transform.position;
            pos.z = Random.Range(140f, 300f);
            enemy.transform.position = pos;

            Skeleton skeleton = enemy.GetComponent<Skeleton>();

            StartCoroutine(WalkForward(enemy.transform));
            yield return StartCoroutine(skeleton.Walk());

            yield return StartCoroutine(
                equationGenerator.RunEquation(Random.Range(3, floor + 4), floor)
            );
            if (equationGenerator.equationSolved)
            {
                score += 1;
            }
            scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score;

            floor = score / 5;
            floorText.GetComponent<TextMeshProUGUI>().text = "Floor: " + floor;
        }
    }

    IEnumerator WalkForward(Transform enemy)
    {
        while (enemy.position.z > 30)
        {
            wallOffset.y -= 1f * Time.deltaTime;
            wall.mainTextureOffset = wallOffset;

            yield return null;
        }
    }
}