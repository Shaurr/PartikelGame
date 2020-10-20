using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class Highscores : MonoBehaviour {


    [SerializeField]
    private GameObject newHSText;
    [SerializeField]
    private Text pointsText;

    private int[] highscorenr = new int[10];
    private string[] highscoretext = new string[10];
    private string names;
    private string score;
    private string currentName;
    private int points;

    // set name
    public void SetName(string name) {
        currentName = name;
    }

    // calculates points and checks for highscore
    public void CheckHighscore(int wave, int kills) {
        // calculates points and displays them
        points = wave * 100 + kills;
        pointsText.text = "Score: " + points;

        // check for new highscore
        newHSText.SetActive(false);
        if (points > highscorenr[9]) {
            // activate new highscore Text
            newHSText.SetActive(true);

            if (points > highscorenr[0]) {
                // highest score, move all other scores in scorelist one position down
                for (int i = 9; i > 0; i--) {

                    highscorenr[i] = highscorenr[i - 1];
                    highscoretext[i] = highscoretext[i - 1];

                }
                highscorenr[0] = points;
                highscoretext[0] = currentName;

            }
            else {
                // search for place in scorelist and save it there
                for (int i = 9; i > 0; i--) {
                    if (points > highscorenr[i - 1]) {
                        highscorenr[i] = highscorenr[i - 1];
                        highscoretext[i] = highscoretext[i - 1];
                    }
                    else {
                        highscorenr[i] = points;
                        highscoretext[i] = currentName;
                        break;
                    }

                }
            }
            // save scorelist on pc
            WriteScores();

        }
    }

    // write all scores and names into on string
    private void Convert() {
        names = "";
        score = "";
        for (int i = 0; i < 10; i++) {

            names = names + highscoretext[i] + "\n";
            score = score + highscorenr[i] + "\n";
        }

    }

    // returns numbers of highscores
    public string GetNumber() {
        Convert();
        return score;
    }

    // returns names of highscores
    public string GetName() {
        Convert();
        return names;
    }

    // reads scores from saved scorelist
    public void ReadScores() {
        XmlDocument doc = new XmlDocument();
        doc.Load("scores.xml");
        XmlElement element;
        XmlNodeList nodeList = doc.DocumentElement.ChildNodes;
        int i = 0;
        foreach (XmlNode score in nodeList) {
            element = (XmlElement)score;
            highscoretext[i] = element.GetAttributeNode("name").InnerXml;
            highscorenr[i] = int.Parse(element.GetAttributeNode("nummber").InnerXml);
            i++;
        }

    }

    // writes scores into scorelist on pc
    public void WriteScores() {

        XmlDocument doc = new XmlDocument();
        doc.Load("scores.xml");
        XmlElement element;
        XmlNodeList nodeList = doc.DocumentElement.ChildNodes;
        int i = 0;

        foreach (XmlNode score in nodeList) {
            element = (XmlElement)score;

            element.SetAttribute("name", highscoretext[i]);
            element.SetAttribute("nummber", highscorenr[i].ToString());
            i++;
        }
        doc.Save("scores.xml");
    }
}
