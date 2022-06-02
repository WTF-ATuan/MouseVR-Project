using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class WallActor : MonoBehaviour
{
    [SerializeField] private List<int> outsideIndex = new List<int>();

    [SerializeField] private List<int> insideIndex = new List<int>();

    [SerializeField] private MeshRenderer insideWallMeshRenderer, outsideWallMeshRenderer;


    [Button]
    public void ChangeInsideOutsideWall()
    {
        Material[] _insidematerials = new Material[insideIndex.Count];
        Material[] _outsidematerials = new Material[outsideIndex.Count];
            
        for (int i = 0 ; i < insideIndex.Count ; i++)
        {
            _insidematerials[i] = outsideWallMeshRenderer.materials[outsideIndex[i]];
            _outsidematerials[i] = insideWallMeshRenderer.materials[insideIndex[i]];


            //var owo = insideWallMeshRenderer.materials[insideIndex[i]];

            //insideWallMeshRenderer.materials[insideIndex[i]] = outsideWallMeshRenderer.materials[outsideIndex[i]];
            //outsideWallMeshRenderer.materials[outsideIndex[i]] = owo;
        }

        insideWallMeshRenderer.materials = _insidematerials;
        outsideWallMeshRenderer.materials = _outsidematerials;
    }
}
