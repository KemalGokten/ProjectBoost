using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField]
    private Vector3 _movementVector;

    [SerializeField]
    private float _period = 2f;

    private float _movementFactor;

    private Vector3 _startingPosition;



    // Start is called before the first frame update
    void Start()
    {
        _startingPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(_period <= Mathf.Epsilon)
        {
            return;
        }
        float cycles = Time.time / _period; // continually growing over time

        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        _movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1 so its cleaner

        Vector3 offset = _movementVector * _movementFactor;
        transform.position = _startingPosition + offset;
    }
}
