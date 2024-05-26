using UnityEngine;
using UnityEngine.U2D;

public class QuickCameraSettingsChanger : MonoBehaviour
{
    private PixelPerfectCamera _pixelCamera;
    private Camera _camera;
    const int _correlationX = 80;
    const int _correlationY = 45;
    private void Awake()
    {
        _pixelCamera = GetComponent<PixelPerfectCamera>();
        _camera = GetComponent<Camera>();
    }
    public void SwitchGraphics(PlayerStateManager.CharacterState newState)
    {
        _pixelCamera.enabled = !(newState == PlayerStateManager.CharacterState._3D || newState == PlayerStateManager.CharacterState.HD || newState == PlayerStateManager.CharacterState.ASCII);
        _camera.orthographic = !(newState == PlayerStateManager.CharacterState._3D);
        int ppuValue = newState switch
        {
            PlayerStateManager.CharacterState.PPU32 => 32,
            PlayerStateManager.CharacterState.PPU16 => 16,
            PlayerStateManager.CharacterState.PPU8 => 8,
            _ => _pixelCamera.assetsPPU
        };

        _pixelCamera.assetsPPU = ppuValue;
        _pixelCamera.refResolutionX = ppuValue * _correlationX;
        _pixelCamera.refResolutionY = ppuValue * _correlationY;
    }
}
