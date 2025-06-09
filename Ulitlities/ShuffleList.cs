using System.Collections.Generic;
using UnityEngine;

public static class CollectionUltilies
{
    public static List<T> ShuffleList<T>(List<T> inputList)
    {
        int i = 0;
        int t = inputList.Count;
        int r = 0;
        T p;
        List<T> tempList = new List<T>(inputList);

        while (i < t)
        {
            r = Random.Range(i, tempList.Count);
            p = tempList[i];
            tempList[i] = tempList[r];
            tempList[r] = p;
            i++;
        }

        return tempList;
    }
    
    public static T[] ShuffleArray<T>(T[] inputList)
    {    
        //take any list of GameObjects and return it with Fischer-Yates shuffle
        int i = 0;
        int t = inputList.Length;
        int r = 0;
        T p;
        
        T[] tempList = inputList;
     
        while (i < t)
        {
            r = Random.Range(i,tempList.Length);
            p = tempList[i];
            tempList[i] = tempList[r];
            tempList[r] = p;
            i++;
        }
     
        return tempList;
    }
    
    
}
