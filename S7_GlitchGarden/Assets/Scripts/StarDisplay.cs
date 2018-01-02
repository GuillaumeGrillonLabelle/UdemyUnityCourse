using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class StarDisplay : MonoBehaviour {
    private Text text;
    private int nbStars;

    void Start () {
        text = GetComponent<Text>();
        nbStars = 0;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        text.text = nbStars.ToString();
    }

    public void AddStars(int amount)
    {
        nbStars += amount;
        UpdateDisplay();
    }

    public bool UseStars(int amount)
    {
        if (nbStars >= amount)
        {
            nbStars -= amount;
            UpdateDisplay();
            return true;
        }
        return false;
    }
}
