using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;


public class testScipts : MonoBehaviour
{
    public int number;
    string YN = "";

    public int[] numbers = { 1, 5, 10, 2, 6 };

    // Start is called before the first frame update
    void Start()
    {
        //Math();
        //factorial();
        //PrintODDandEVEN();
        //TimeInSeconds();
        //print("you are: " + number + " You " + CalculateNum() + " drink");
        //MoneyProblem();
        //PalindromeProblem();
        //findHighestNumber();
    }

    // Update is called once per frame
    void findHighestNumber()
    {
        int[] playerScores = { -450, -1200, -340, -890, 2100, -150, 780, 1050, -2222222 };
        int biggestnumber = playerScores[0];

        for (int i = 1; i < playerScores.Length; i++)
        {
            if (playerScores[i] > biggestnumber)
            {
                biggestnumber = playerScores[i];
            }
        }
        print("Real biggest: " + biggestnumber);
    }


    string CalculateNum()
    {
        if (number >= 19)
        {
            YN = "can";
        }
        else
        {
            YN = "can't";
        }
        return YN;
    }

    void factorial()
    {
        string finalString = "";
        int HoldValue = 0;
        int FinalNumber = 0;
        for (int i = number; i > 0; i--)
        {
            
            FinalNumber = i + HoldValue;
            finalString += i + " + "; 
            HoldValue = FinalNumber;
            
        }
        print(finalString + " = " + FinalNumber);
        //print("Final: " + FinalNumber);
    }

    void PrintODDandEVEN()
    {
        int Odd = 0;
        int Even = 0;
        for (int i = numbers.Length - 1; i >= 0; i--)
        {
            float Num = numbers[i];
            if (Num % 2 == 0)
            {
                //Even
                Even++;
                print("List Number: " + numbers[i] + " and its Even");
            }
            else
            {
                //Odd
                Odd++;
                print("List Number: " + numbers[i] + " and its Odd");
            }


        }

        print("Number of Even's: " + Even);

        print("Number of Odd: " + Odd);
    }


    void TimeInSeconds()
    {
        int anwser = number * 60;

        print("Input in minutes: " + number + " Number in Seconds: "  + anwser);
    }


    void MoneyProblem()
    {
        // need to redo this / try again

        number = Random.Range(1, 1000000);
        int Temp = number;
        int[] notes = { 100, 50, 20, 10, 5, 2, 1 };

        foreach (int note in notes)
        {
            int count = Temp / note;
            print("Number of " + note + "'s: " + count);
            Temp = Temp % note;
        }

    }

    void PalindromeProblem()
    {
        string Palindrome = "racecar";
        bool isPalindrome = true;
        Palindrome.ToLower();
        for (int i = 0; i < Palindrome.Length/ 2; i++)
        {
            if (Palindrome[i] == Palindrome[Palindrome.Length - (1 + i)])
            {
                isPalindrome = true;
            }
            else
            {
                isPalindrome = false;
            }
        }

        print(isPalindrome);
    }

    // 450, 1200, 340, 890, 2100, 150, 780, 1050

}
