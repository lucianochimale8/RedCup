using UnityEngine;

public class ShootCommand : ICommand
{
    [Header("Weapon")]
    private Wand wand;
    public ShootCommand(Wand wand)
    {
        this.wand = wand;
    }
    public void Execute()
    {
        wand.Shoot();
    }
}
