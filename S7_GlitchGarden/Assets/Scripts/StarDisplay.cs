using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class StarDisplay : MonoBehaviour {
    private Text text;
    public int nbStars = 100;

    public enum Status { SUCCESS, FAILURE }

    void Start () {
        text = GetComponent<Text>();
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

    public Status UseStars(int amount)
    {
        if (nbStars >= amount)
        {
            nbStars -= amount;
            UpdateDisplay();
            return Status.SUCCESS;
        }
        return Status.FAILURE;
    }
}
