using UnityEngine;
using UnityEngine.UI;

public class ButtonActivation : MonoBehaviour

{
    public Button minus;
    public Button mult;
    void Start()
    {
        minus.gameObject.SetActive(false);
        mult.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.floor == 1)
        {
            minus.gameObject.SetActive(true);
        }
        if(GameManager.floor == 2)
        {
            mult.gameObject.SetActive(true);
        }
    }
}
