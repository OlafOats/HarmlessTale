using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] private GameObject _character3D, _characterHD, _character32PPU, _character16PPU, _character8PPU, _characterASCII;
    private List<GameObject> _characters;
    private GameObject _activeCharacter;

    public enum CharacterState
    {
        _3D,
        HD,
        PPU32,
        PPU16,
        PPU8,
        ASCII
    };

    [SerializeField] private CharacterState _currentState;

    private void Awake()
    {
        _characters = new List<GameObject>
        {
            _character3D,
            _characterHD,
            _character32PPU,
            _character16PPU,
            _character8PPU,
            _characterASCII
        };
        _activeCharacter = _characters[(int)_currentState];
        _activeCharacter.SetActive(true);
    }

    public void SwitchState(CharacterState newState)
    {
        SwitchCharacter(newState);
    }
    private void SwitchCharacter(CharacterState newState)
    {
        GameObject previousCharacter = _activeCharacter;       
        previousCharacter.SetActive(false);
        _activeCharacter = _characters[(int)newState];


        if(newState == CharacterState._3D)
            _activeCharacter.transform.position = new Vector3(previousCharacter.transform.position.x, 0, previousCharacter.transform.transform.position.y);

        else if(_currentState == CharacterState._3D)
            _activeCharacter.transform.position = new Vector3(previousCharacter.transform.position.x, previousCharacter.transform.position.z, 0);

        else
            _activeCharacter.transform.position = previousCharacter.transform.position;

        _activeCharacter.SetActive(true);
        _currentState = newState;
    }
}