using UnityEngine;
using UnityEngine.U2D;

public class QuickCameraSettingsChanger : MonoBehaviour
{
    private PixelPerfectCamera _pixelCamera;
    const float _correlationX = 40;
    const float _correlationY = 22.5f;
    private void Awake()
    {
        _pixelCamera = GetComponent<PixelPerfectCamera>();
    }
    public void SwitchGraphics(PlayerStateManager.CharacterState newState)
    {
        _pixelCamera.enabled = !(newState == PlayerStateManager.CharacterState._3D || newState == PlayerStateManager.CharacterState.HD || newState == PlayerStateManager.CharacterState.ASCII);

        int ppuValue = newState switch
        {
            PlayerStateManager.CharacterState.PPU32 => 32,
            PlayerStateManager.CharacterState.PPU16 => 16,
            PlayerStateManager.CharacterState.PPU8 => 8,
            _ => _pixelCamera.assetsPPU
        };

        _pixelCamera.assetsPPU = ppuValue;
        _pixelCamera.refResolutionX = (int)(ppuValue * _correlationX);
        _pixelCamera.refResolutionY = (int)(ppuValue * _correlationY);
    }
}
