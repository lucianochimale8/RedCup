using UnityEngine;

public class DropWeaponCommand : ICommand
{
    private PlayerWeaponController weaponController;

    public DropWeaponCommand(PlayerWeaponController controller)
    {
        weaponController = controller;
    }

    public void Execute()
    {
        weaponController.DropWand();
    }
}
