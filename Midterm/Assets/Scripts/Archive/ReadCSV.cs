using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static ReadCSV;

public class ReadCSV : MonoBehaviour
{
    public TextAsset textAssetData;
    public int dataColumns = 15;
    public int dataRows = 63;

    [System.Serializable]
    public class COVIDCase
    {
        public float x;
        public float y;
        public string id;
        public string state;
        public string country;
        public string lastUpdate;
        public float latitude;
        public float longitude;
        public float confirmedCasesAmt;
        public float deathsAmt;
        public string county;
        public float fips;
        public float incidentRate;
    }

    [System.Serializable]
    public class COVIDCaseList
    {
        public COVIDCase[] covidCase;
        public int size;
    }

    public COVIDCaseList covidCaseList = new COVIDCaseList();

    void Awake()
    {
        SaveCSVData();
    }

    void SaveCSVData()
    {
        string[] data = textAssetData.text.Split(new string[] {",", "\n" }, System.StringSplitOptions.None);

        int tableSize = data.Length / dataColumns - 1;
        covidCaseList.covidCase = new COVIDCase[tableSize];
        covidCaseList.size = 0;

        for (int i = 0; i < tableSize; i++)
        {
            covidCaseList.size++;
            covidCaseList.covidCase[i] = new COVIDCase();
            covidCaseList.covidCase[i].x = float.Parse(data[dataColumns * (i+ 1)]);
            covidCaseList.covidCase[i].y = float.Parse(data[dataColumns * (i + 1) + 1]);
            covidCaseList.covidCase[i].id = data[dataColumns * (i + 1) + 2];
            covidCaseList.covidCase[i].state = data[dataColumns * (i + 1) + 3];
            covidCaseList.covidCase[i].country = data[dataColumns * (i + 1) + 4];
            covidCaseList.covidCase[i].lastUpdate = data[dataColumns * (i + 1) + 5];
            covidCaseList.covidCase[i].latitude = float.Parse(data[dataColumns * (i + 1) + 6]);
            covidCaseList.covidCase[i].longitude = float.Parse(data[dataColumns * (i + 1) + 7]);
            covidCaseList.covidCase[i].confirmedCasesAmt = float.Parse(data[dataColumns * (i + 1) + 8]);
            covidCaseList.covidCase[i].deathsAmt = float.Parse(data[dataColumns * (i + 1) + 9]);
            covidCaseList.covidCase[i].county = data[dataColumns * (i + 1) + 10];
            covidCaseList.covidCase[i].fips = float.Parse(data[dataColumns * (i + 1) + 11]);
            covidCaseList.covidCase[i].incidentRate = float.Parse(data[dataColumns * (i + 1) + 12]);

        }
    }
}
