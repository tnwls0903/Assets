using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DiceGame : MonoBehaviour
{
    public DataController dataController;
    public Text goldText;
    public InputField betInput;
    public Button buttonA;
    public Button buttonB;
    public Button buttonDraw;
    public Animator diceAnimator;
    public Animator DiceWin;
    public Image[] diceImages;

    private int betAmount;
    private int diceA;
    private int diceB;

    private Color originalButtonColor;

    private void Start()
    {
        dataController = FindObjectOfType<DataController>();

        buttonA.onClick.AddListener(ButtonAClicked);
        buttonB.onClick.AddListener(ButtonBClicked);
        buttonDraw.onClick.AddListener(ButtonDrawClicked);

        goldText.text = "Gold: " + dataController.GetGold().ToString();

        betInput.onValueChanged.AddListener(delegate { SetBetAmount(); });

        originalButtonColor = buttonA.colors.normalColor;
    }

    private void ButtonAClicked()
    {
        if (betAmount <= dataController.GetGold() && betAmount > 0)
        {
            StartCoroutine(PlayDiceAnimation("A"));
        }
        else
        {
            Debug.Log("Not enough gold to bet.");
        }
    }

    private void ButtonBClicked()
    {
        if (betAmount <= dataController.GetGold() && betAmount > 0)
        {
            StartCoroutine(PlayDiceAnimation("B"));
        }
        else
        {
            Debug.Log("Not enough gold to bet.");
        }
    }

    private void ButtonDrawClicked()
    {
        if (betAmount <= dataController.GetGold() && betAmount > 0)
        {
            StartCoroutine(PlayDiceAnimation("Draw"));
        }
        else
        {
            Debug.Log("Not enough gold to bet.");
        }
    }

    private void RollDice()
    {
        diceA = Random.Range(1, 7);
        diceB = Random.Range(1, 7);
    }

    private void CompareDice(string selected)
    {
        int winnings = 0; // 승리 시 얻는 금액

        
        
            switch (selected)
            {
                case "A":
                    if (diceA > diceB)
                    {
                        winnings = betAmount; // 배팅 금액만큼 얻음
                    }
                    break;
                case "B":
                    if (diceB > diceA)
                    {
                        winnings = betAmount; // 배팅 금액만큼 얻음
                    }
                    break;
                case "Draw":
                    if (diceA == diceB) // 무승부
                    {
                        winnings = betAmount*3; // 배팅 금액을 3배로 증가
                    }
                    break;
                default:
                    break;
            }
    

        if (winnings > 0)
        {
            dataController.AddGold(winnings);
            goldText.text = "Gold: " + dataController.GetGold().ToString();
        }
        else
        {
            dataController.SubGold(betAmount);
            goldText.text = "Gold: " + dataController.GetGold().ToString();
        }

        StartCoroutine(UpdateTextWithDelay("Dice A: " + diceA + "\nDice B: " + diceB, 4f));

        switch (selected)
        {
            case "A":
                DiceWin.SetTrigger("AWinTrigger");
                break;
            case "B":
                DiceWin.SetTrigger("BWinTrigger");
                break;
            case "Draw":
                DiceWin.SetTrigger("DrawTrigger");
                break;
            default:
                break;
        }

        buttonA.image.color = originalButtonColor;
    }

    public void SetBetAmount()
    {
        int.TryParse(betInput.text, out betAmount);
        if (betAmount > 0)
        {
            int remainingGold = dataController.GetGold() - betAmount;
            if (remainingGold >= 0)
            {
                goldText.text = "Gold: " + remainingGold.ToString();
            }
            else
            {
                goldText.text = "Not enough gold to bet.";
            }
        }
    }

    private void HideDiceImages()
    {
        for (int i = 0; i < diceImages.Length; i++)
        {
            diceImages[i].gameObject.SetActive(false);
        }
    }

    private IEnumerator PlayDiceAnimation(string selected)
    {
        HideDiceImages();
        diceAnimator.SetBool("DiceRoll", true);
        yield return new WaitForSeconds(4f);
        RollDice();
        CompareDice(selected);
        diceAnimator.SetBool("DiceRoll", false);

        int diceIndexA = diceA - 1;
        int diceIndexB = diceB - 1;

        if (diceIndexA >= 0 && diceIndexA < diceImages.Length)
        {
            diceImages[diceIndexA].gameObject.SetActive(true);
        }

        if (diceIndexB >= 0 && diceIndexB < diceImages.Length)
        {
            diceImages[diceIndexB + 6].gameObject.SetActive(true);
        }
    }

    private IEnumerator UpdateTextWithDelay(string text, float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log(text);
    }
}