public interface IWeapon
{
    public void Shoot();
    public void Reload();
    WeaponInfo weaponInfo { get; }
}

public struct WeaponInfo
{
    public float BaseDamage { get; }
    public float BaseSpeed { get; }
    public float BaseAmmoCount { get; }
    public float BaseAmmoReloadTime { get; }
    public WeaponInfo(float BaseDamage, float BaseSpeed, float BaseAmmoCount, float BaseAmmoReloadTime)
    {
        this.BaseDamage = BaseDamage;
        this.BaseSpeed = BaseSpeed;
        this.BaseAmmoCount = BaseAmmoCount;
        this.BaseAmmoReloadTime = BaseAmmoReloadTime;
    }
}
