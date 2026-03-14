using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.WSA;

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
        PrintODDandEVEN();
        //print("you are: " + number + " You " + CalculateNum() + " drink");
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
