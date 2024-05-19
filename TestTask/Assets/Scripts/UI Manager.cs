using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public Range EnemySpawnDistance;

    [SerializeField]
    private Transform playerTransform;
    
    [SerializeField, Min(1)]
    public float checkSize;
    [SerializeField]
    private LayerMask groundLayer;
    private Vector3 _checkPos; // gizmos spherre
    
    public void CreateEnemy()
    {
        Vector3 spawnPos;
        Vector2 shiftPos;
        int attempts = 100; // attempts of finding spawn place
        do
        {
            spawnPos = playerTransform.position;
            shiftPos = RandomShift();
            spawnPos += new Vector3(shiftPos.x, 0, shiftPos.y);// shift pos
            attempts--;
        } while (!IsSpaceEmpty(spawnPos, out spawnPos) && attempts > 0);

        if (attempts > 0)
        {
            Instantiate(EnemyPrefab, spawnPos, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Could not find free space to create an enemy");
        }
    }

    private Vector2 RandomShift()
    {
        Vector2 shiftPos = Vector2.zero;
        float distance = 0;

        // generate a new shift position until the condition is done
        while (distance > EnemySpawnDistance.Max || distance < EnemySpawnDistance.Min)
        {
            //generate random value in range (-max;max)
            shiftPos.x = -EnemySpawnDistance.Max + (UnityEngine.Random.value * 2 * EnemySpawnDistance.Max);
            shiftPos.y = -EnemySpawnDistance.Max + (UnityEngine.Random.value * 2 * EnemySpawnDistance.Max);
            distance = shiftPos.magnitude;
        }


        return shiftPos;
    }

    private bool IsSpaceEmpty(Vector3 pos, out Vector3 groundPosition)
    {
        
        Ray ray = new Ray(new Vector3(pos.x, pos.y + 1000, pos.z), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1200, groundLayer)) // check if ground exist
        {
            groundPosition = hit.point; // ground pos

            _checkPos = groundPosition;
            // check if place is empty
            return !Physics.CheckSphere(groundPosition, checkSize, 1 << EnemyPrefab.layer);

        }
        groundPosition = pos;

        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_checkPos, checkSize);
    }

    public void DeselectButton()
    {
        //Deselect a button, so it won't be triggered when Space or Enter pressed
        EventSystem.current.SetSelectedGameObject(null);
    }

    [Serializable]
    public struct Range
    {
        public uint Min;
        public uint Max;
    }
}
