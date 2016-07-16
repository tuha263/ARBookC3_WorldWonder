using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CreateCodeScript : MonoBehaviour {

    string fileName = "CodeList.txt";
    List<string> listCode = new List<string>();

	// Use this for initialization
	void Start () {
        WriteFile();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void WriteFile()
    {
        if (File.Exists(fileName))
        {
            Debug.Log(fileName + " already exists.");
            return;
        }
        var sr = File.CreateText(fileName);
        for (int i = 0; i < 2005; i++)
        {
            sr.WriteLine(GetCode());
        }
        sr.Close();
        

    }
    string GetCode()
    {
        string s = null;
        for (int i = 0; i < 9; i++)
        {
            /*if (i % 3 == 0 && i > 0)
            {
                s += " ";
            }*/
            //make random char
            int k = Random.RandomRange(65, 101);
            while (k == 79 || k == 100)
            {
                k = Random.RandomRange(65, 101);
            }

            if (k < 91)
            {
                s += ((char)k).ToString();
            }
            else
            {
                s += (100 - k).ToString();
            }
        }
        for (int i = 0; i < listCode.Count; i++)
        {
            if (listCode[i].Equals(s))
            {
                s = GetCode();
                return s;
            }
        }
        return s;

    }
}
