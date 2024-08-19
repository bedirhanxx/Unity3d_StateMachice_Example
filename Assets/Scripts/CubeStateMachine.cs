using UnityEngine;

public enum CubeState
{
    Idle,
    Hidden,
    Destroyed,
    Respawned
}

public class CubeStateMachine : MonoBehaviour
{
    public CubeState CurrentState;
    
    private GameObject _cubeInstance;

    private void Start()
    {
        TransitionToState(CubeState.Idle);
    }

    private void Update()
    {
        switch (CurrentState)
        {
            case CubeState.Idle:
                if (Input.GetKeyDown(KeyCode.A))
                {
                    TransitionToState(CubeState.Hidden);
                }
                break;

            case CubeState.Hidden:
                if (Input.GetKeyDown(KeyCode.S))
                {
                    TransitionToState(CubeState.Destroyed);
                }
                break;

            case CubeState.Destroyed:
                if (Input.GetKeyDown(KeyCode.D))
                {
                    TransitionToState(CubeState.Respawned);
                }
                break;

            case CubeState.Respawned:
                TransitionToState(CubeState.Idle);
                break;
        }
    }

    public void TransitionToState(CubeState newState)
    {
        _ExitCurrentState();
        CurrentState = newState;
        _EnterNewState();
    }

    private void _ExitCurrentState()
    {
        // Currently, there are no exit actions needed for the states.
    }

    private void _EnterNewState()
    {
        switch (CurrentState)
        {
            case CubeState.Idle:
                if (_cubeInstance == null)
                {
                    _cubeInstance = ObjectPooler.Instance.GetPooledObject();
                    _cubeInstance.SetActive(true);
                }
                break;

            case CubeState.Hidden:
                _cubeInstance.SetActive(false);
                break;

            case CubeState.Destroyed:
                ObjectPooler.Instance.ReturnToPool(_cubeInstance);
                break;

            case CubeState.Respawned:
                _cubeInstance = ObjectPooler.Instance.GetPooledObject();
                _cubeInstance.SetActive(true);
                break;
        }
    }
}
