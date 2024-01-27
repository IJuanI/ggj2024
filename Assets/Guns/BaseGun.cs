using UnityEngine;

public abstract class BaseGun<T> : AnyGun where T: IDamageable
{

  protected int capacity, currentAmmo;

  protected float fireRate, damage, reloadTime, activationTime;
  protected float shootStartTime = 0, lastFireTime = 0, reloadStartTime = 0;

  /// <summary>
  /// When shooting is false, the gun is locked and cannot fire.
  /// </summary>
  protected bool shooting = true, multiShoot, isActivating = false;

  /// <summary>
  /// Handles a generic implementation for a Gun.
  /// </summary>
  /// <param name="damage">
  /// The amount of damage the gun does with every hit.
  /// </param>
  /// <param name="fireRate">
  /// The amount of time in seconds between each shot. If the gun is a multi-shot gun, this is the amount of time between each shot.
  /// </param>
  /// <param name="capacity">
  /// The amount of ammo the gun can hold.
  /// </param>
  /// <param name="reloadTime">
  /// The amount of time it takes to reload the gun.
  /// </param>
  /// <param name="activationTime">
  /// The amount of time it takes for the gun to activate.
  /// </param>
  /// <param name="multiShoot">
  /// Whether or not the gun can shoot multiple times in a single activation.
  /// This will keep the gun shooting until the player releases the gun.
  /// </param>
  protected BaseGun(float damage, float fireRate, int capacity, float reloadTime, float activationTime = 0f, bool multiShoot = false) {
    this.damage = damage;
    this.capacity = capacity;
    this.fireRate = fireRate;
    this.activationTime = activationTime;
    this.multiShoot = multiShoot;
    this.reloadTime = reloadTime;

    currentAmmo = capacity;
  }

  public override void Shoot(Vector2 screenPos)
  {
    if (currentAmmo == 0 && !Reload()) return;


    if (Time.time - lastFireTime < fireRate) return;


    if (isGunActive()) {
      DoFire(screenPos);
    }
  }

  public override void CancelShoot() {
    shootStartTime = 0;

    if (currentAmmo == 0) {
      Reload();
    }

    shooting = true;
  }

  public virtual void OnStartReload() { }

  public virtual void OnStartActivate() { }

  public bool Reload() {
    shooting = false;

    if (reloadStartTime == 0) {
      reloadStartTime = Time.time;
      OnStartReload();
    }

    if (Time.time - reloadStartTime < reloadTime) return false;

    currentAmmo = capacity;
    reloadStartTime = 0;
        Debug.Log("paso por acaaaaaaaaaaaaaaaaaaaaaaaaa");
    UIManager.instance.Reload(0, currentAmmo);


    return true;
  }

  private bool isGunActive() {
    if (activationTime == 0) return shooting;

    if (shootStartTime == 0) {
      shootStartTime = Time.time;
      isActivating = true;
      OnStartActivate();
      return false;
    }

    if (Time.time - shootStartTime < activationTime) return false;

    isActivating = false;

    return shooting;
  }

  private void DoFire(Vector2 screenPos) {

    RaycastUtils.RaycastFromCamera(out RaycastHit hit, screenPos);

    if (hit.collider != null)
    {
      T damageable = hit.collider.GetComponent<T>();
      damageable?.TakeDamage(damage);
    }

    currentAmmo--;
    if (!multiShoot) shooting = false;
    lastFireTime = Time.time;
  }

  protected bool GetTarget(Vector2 screenPos, out T target) {
    RaycastUtils.RaycastFromCamera(out RaycastHit hit, screenPos);

    if (hit.collider != null)
    {
      target = hit.collider.GetComponent<T>();
      return target != null;
    }

    target = default;
    return false;
  }
}
