using System;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[OrderBefore(typeof(NetworkTransform))]
[DisallowMultipleComponent]
// ReSharper禁用一次CheckNamespace
public class NetworkCharacterControllerPrototypeCustom : NetworkTransform {
  [Header("角色控制器设置")]
  public float gravity       = -20.0f;//重力
  public float jumpImpulse   = 8.0f;
  public float acceleration  = 10.0f;
  public float braking       = 10.0f;
  public float maxSpeed      = 2.0f;
  public float rotationSpeed = 15.0f;
  private float _targetRotation = 0.0f;
  private float _rotationVelocity;
  [Range(0.0f, 0.3f)]
  public float RotationSmoothTime = 0.12f;

  [Networked]
  [HideInInspector]
  public bool IsGrounded { get; set; }

  [Networked]
  [HideInInspector]
  public Vector3 Velocity { get; set; }

  /// <summary>
  /// 设置默认传送插值速度为CC的当前速度。
  /// 有关如何使用此字段的详细信息，请参见 <see cref="NetworkTransform.TeleportToPosition"/>.
  /// </summary>
  protected override Vector3 DefaultTeleportInterpolationVelocity => Velocity;

  /// <summary>
  /// 将默认传送插值角速度设置为CC在Z轴上的旋转速度。
  /// 有关如何使用此字段的详细信息，请参见 <see cref="NetworkTransform.TeleportToRotation"/>.
  /// </summary>
  protected override Vector3 DefaultTeleportInterpolationAngularVelocity => new Vector3(0f, 0f, rotationSpeed);

  public CharacterController Controller { get; private set; }

  protected override void Awake() {
    base.Awake();
    CacheController();
  }

  public override void Spawned() {
    base.Spawned();
    CacheController();
  }

  private void CacheController() {
    if (Controller == null) {
      Controller = GetComponent<CharacterController>();

      Assert.Check(Controller != null, $"An object with {nameof(NetworkCharacterControllerPrototype)} must also have a {nameof(CharacterController)} component.");
    }
  }

  protected override void CopyFromBufferToEngine() {
    // 技巧:在重置转换状态之前必须禁用CC
    Controller.enabled = false;

    // 从网络数据缓冲区中提取基本(NetworkTransform)状态
    base.CopyFromBufferToEngine();

    // Re-enable CC
    Controller.enabled = true;
  }

  /// <summary>
  /// 跳跃脉冲的基本实现(立即将垂直组件集成到Velocity中)
  /// <param name="ignoreGrounded">跳，即使不是在接地状态。</param>
  /// <param name="overrideImpulse">可选字段覆盖跳跃脉冲。如果为空, <see cref="jumpImpulse"/> is used.</param>
  /// </summary>
  public virtual void Jump(bool ignoreGrounded = false, float? overrideImpulse = null) {
    if (IsGrounded || ignoreGrounded) {
      var newVel = Velocity;
      newVel.y += overrideImpulse ?? jumpImpulse;
      Velocity =  newVel;
    }
  }

  /// <summary>
  /// 基于预定方向的角色控制器的移动功能的基本实现。
  /// <param name="direction">预期的运动方向，受制于运动查询，加速度和最大速度值。</param>
  /// </summary>
  public virtual void Move(Vector3 direction) {
    var deltaTime    = Runner.DeltaTime;
    var previousPos  = transform.position;
    var moveVelocity = Velocity;

    direction = direction.normalized;

    if (IsGrounded && moveVelocity.y < 0) {
      moveVelocity.y = 0f;
    }

    moveVelocity.y += gravity * Runner.DeltaTime;

    var horizontalVel = default(Vector3);
    horizontalVel.x = moveVelocity.x;
    horizontalVel.z = moveVelocity.z;

    if (direction == default) {
      horizontalVel = Vector3.Lerp(horizontalVel, default, braking * deltaTime);
    } else {
      horizontalVel      = Vector3.ClampMagnitude(horizontalVel + direction * acceleration * deltaTime, maxSpeed);
    }

    moveVelocity.x = horizontalVel.x;
    moveVelocity.z = horizontalVel.z;

    Controller.Move(moveVelocity * deltaTime);

    Velocity   = (transform.position - previousPos) * Runner.Simulation.Config.TickRate;
    IsGrounded = Controller.isGrounded;
  }
  
}