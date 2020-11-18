using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class NerveMain : MonoBehaviour
{
    public LayerMask iWantDetect;
    public BitVector32 layerBitVector32 = new BitVector32();
    private List<int> activesMask = new List<int>();
    public List<string> activeLayers = new List<string>();
    public List<List<string>> tagsToLayers = new List<List<string>>();
    public List<NerveElement> nerveList = new List<NerveElement>();
    public BoxCollider myTriggerCollider;
    public Collider[] hitColliders;

    private void OnTriggerStay(Collider other)
    {
        var collisionLayerName = LayerMask.LayerToName(other.gameObject.layer);
        var collisionTagName = other.gameObject.tag;
        for (int i = 0; i < nerveList.Count; i++)
        {
            if (collisionLayerName == nerveList[i].layerName)
            {
                nerveList[i].layerActive = true;
            }
            for (int j = 0; j < nerveList[i].tagNames.Count; j++)
            {

                if (collisionTagName == nerveList[i].tagNames[j])
                    nerveList[i].tagActive[j] = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        var collisionLayerName = LayerMask.LayerToName(other.gameObject.layer);
        var collisionTagName = other.gameObject.tag;

        hitColliders = Physics.OverlapBox(myTriggerCollider.transform.position, myTriggerCollider.transform.localScale * 0.5f, myTriggerCollider.transform.rotation, iWantDetect.value);
        List<Collider> tempColliders = new List<Collider>(hitColliders);
        foreach (Collider collider in tempColliders.ToList())
        {
            if (collider.Equals(other))
                tempColliders.Remove(other);
        }
        hitColliders = tempColliders.ToArray();

        var stillIn = false;
        //for (int k = 0; k < hitColliders.Length; k++)
        //{
        //    var collisionLayerName2 = LayerMask.LayerToName(hitColliders[k].gameObject.layer);
        //    var collisionTagName2 = hitColliders[k].gameObject.tag;
        //    for (int i = 0; i < nerveList.Count; i++)
        //    {
        //        if (collisionLayerName2 == nerveList[i].layerName)
        //        {
        //            stillIn = true;
        //        }
        //        for (int j = 0; j < nerveList[i].tagNames.Count; j++)
        //        {

        //            if (collisionTagName2 == nerveList[i].tagNames[j])
        //                stillIn = true;
        //        }
        //    }
        //}
        //Do this just if the my trigger collider is not actually in an collider with the right tag
        if (stillIn == false)
        {
            for (int i = 0; i < nerveList.Count; i++)
            {
                if (collisionLayerName == nerveList[i].layerName)
                {
                    nerveList[i].layerActive = false;
                }
                for (int j = 0; j < nerveList[i].tagNames.Count; j++)
                {
                    if (collisionTagName == nerveList[i].tagNames[j])
                        nerveList[i].tagActive[j] = false;
                }
            }
        }
    }

    #region own methods
    public void RefreshAllLayers()
    {
        layerBitVector32 = new BitVector32(iWantDetect.value);
        BitMaskToList();
    }
    void BitMaskToList()
    {
        activesMask = new List<int>();
        foreach (char layer in layerBitVector32.ToString().Reverse())
        {
            if (layer == '1')
                activesMask.Add(1);
            else if (layer == '0')
                activesMask.Add(0);
            else
                continue;
        }
        for (int i = 0; i < activesMask.ToList().Count; i++)
        {
            var currentLayer = LayerMask.LayerToName(i);
            if (activesMask[i] == 1 && !activeLayers.Contains(currentLayer))
                activeLayers.Add(currentLayer);
            else if(activesMask[i] == 0 && activeLayers.Contains(currentLayer))
            {
                activeLayers.Remove(currentLayer);
            }
        }

        var nerveNames = new List<string>();
        foreach (NerveElement nerveElement in nerveList)
        {
            nerveNames.Add(nerveElement.layerName);
        }
        foreach (string activeLayer in activeLayers)
        {
            if (!nerveNames.Contains(activeLayer))
            {
                var nerveElement = new NerveElement();
                nerveElement.layerName = activeLayer;
                nerveElement.layerActive = false;
                nerveElement.tagNames = new List<string>();
                nerveElement.tagActive = new List<bool>();
                nerveList.Add(nerveElement);
                
            }
        }

        foreach (NerveElement nerveElement in nerveList.ToList())
        {
            if (!activeLayers.Contains(nerveElement.layerName))
                nerveList.Remove(nerveElement);
        }

        //Build a list with all layernames
        var allLayerNames = new List<string>();
        for (int j = 0; j < 32; j++)
        {
            allLayerNames.Add(LayerMask.LayerToName(j));
        }
        //Delete the layers, that dont exists anymore by name
        for (int i = 0; i < nerveList.ToList().Count; i++)
        {
            if (allLayerNames.Contains(nerveList[i].layerName))
            {
                continue;
            }
            else
            {
                nerveList.RemoveAt(i);
            }
        }
    }
    public void AddTag(int i, int j)
    {
        nerveList[i].tagNames.Insert(j, "Default");
        nerveList[i].tagActive.Insert(j, false);
    }
    public void DeleteTag(int i, int j)
    {
        nerveList[i].tagNames.RemoveAt(j);
        nerveList[i].tagActive.RemoveAt(j);
    }
    public bool CheckIfTagExists(string aTag)
    {
        try
        {
            GameObject.FindGameObjectsWithTag(aTag);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public void RefreshProjectLayerChanges()
    {
        nerveList = new List<NerveElement>();
        iWantDetect = 0;
    }
    public static bool IsInLayerMask(int layer, LayerMask layermask)
    {
        return layermask == (layermask | (1 << layer));
    }
    public bool GetLayerActive(string layer)
    {
        var activeStatus = false;
        for (int i = 0; i < nerveList.Count; i++)
        {
            if (nerveList[i].layerName == layer)
                activeStatus = nerveList[i].layerActive;
        }
        return activeStatus;
    }
    public bool GetTagActive(string layer, string tag)
    {
        var activeStatus = false;
        for (int i = 0; i < nerveList.Count; i++)
        {
            if (nerveList[i].layerName == layer)
            {
                for (int j = 0; j < nerveList[i].tagNames.Count; j++)
                {
                    if(nerveList[i].tagNames[j] == tag)
                    {
                        activeStatus = nerveList[i].tagActive[j];
                    }
                }
            }
        }
        return activeStatus;
    }
    public bool GetAnyActives()
    {
        var anyActive = false;
        for (int i = 0; i < nerveList.Count; i++)
        {
            //if layer isn't active, the tags also not active, so return
            if (!nerveList[i].layerActive)
                continue;
            //if the layer is active and no tags are selected, return the value
            else if(nerveList[i].layerActive && nerveList[i].tagNames.Count == 0)
            {
                return anyActive = true;
            }
            //if the layer is active, and the layer have tags, also watch if the tags are active
            else if(nerveList[i].layerActive && nerveList[i].tagNames.Count > 0)
            {
                for (int j = 0; j < nerveList[i].tagActive.Count; j++)
                {
                    if (!nerveList[i].tagActive[j])
                        continue;
                    else
                        return anyActive = true;
                }
            }
        }
        return anyActive;
    }
    #endregion
}

#region own classes
public class ChangeCheck<T>
{
    public T newValue { get; set; }
    public T oldValue { get; set; }

    public ChangeCheck(T value)
    {
        this.oldValue = value;
    }

    public ChangeCheck()
    {
    }

    //Do with an action
    public bool checkChange(T val, Action raise)
    {
        newValue = val;
        bool result = false;
        if (newValue == null)
        {
            newValue = val;
            return result;
        }
        if (oldValue == null)
        { //Initialize the first time
            oldValue = newValue;
            return result;
        }
        if (!val.Equals(oldValue))
        {
            result = true;
            oldValue = newValue;
            raise();
        }
        return result;
    }

    //Do without an action
    public bool checkChange(T val)
    {
        newValue = val;
        bool result = false;
        if (newValue == null)
        {
            newValue = val;
            return result;
        }
        if (oldValue == null)
        { //Initialize the first time
            oldValue = newValue;
            return result;
        }
        if (!val.Equals(oldValue))
        {
            result = true;
            oldValue = newValue;
        }
        return result;
    }
}
[System.Serializable]
public class ListWrapper
{
    public List<string> myList;
}

[System.Serializable]
public class NerveElement
{
    public string layerName;
    public bool layerActive;
    public List<string> tagNames;
    public List<bool> tagActive;
}
#endregion