#pragma warning disable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Turret : MonoBehaviour {	
	public Transform target;
	public float rotationSpeed = 5F;
	public float minAngle = -90F;
	public float maxAngle = 90F;
	private Vector3 direction;
	private float targetAngle;
	private float currentAngle;
	private float angle;
	private Quaternion q;
	
	private void Update() {
		MaxyGames.UNode.GraphDebug.Flow(this, -581207326, 30, "exit");
		//Check if target is not null
		if(MaxyGames.UNode.GraphDebug.Value<bool>((target != null), this, -581207326, 6, "condition", false)) {
			MaxyGames.UNode.GraphDebug.Flow(this, -581207326, 6, "onTrue");
			trackPlayer();
			MaxyGames.UNode.GraphDebug.FlowNode(this, -581207326, 129, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, -581207326, 6, true);
		} else {
			MaxyGames.UNode.GraphDebug.FlowNode(this, -581207326, 6, false);
		}
	}
	
	public void trackPlayer() {
		MaxyGames.UNode.GraphDebug.Flow(this, -581207326, 85, "exit");
		MaxyGames.UNode.GraphDebug.Value(direction = MaxyGames.UNode.GraphDebug.Value((MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(target, this, -581207326, 90, "-instance", false).position, this, -581207326, 93, "6b1d5dc4054a812", false) - MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(this.transform, this, -581207326, 92, "-instance", false).position, this, -581207326, 93, "2cb9ae4260e43cb", false)), this, -581207326, 88, "value", false), this, -581207326, 88, "target", true);
		MaxyGames.UNode.GraphDebug.Flow(this, -581207326, 88, "exit");
		MaxyGames.UNode.GraphDebug.Value(direction.z = 0F, this, -581207326, 96, "target", true);
		MaxyGames.UNode.GraphDebug.Flow(this, -581207326, 96, "exit");
		MaxyGames.UNode.GraphDebug.Value(targetAngle = MaxyGames.UNode.GraphDebug.Value((MaxyGames.UNode.GraphDebug.Value(Mathf.Atan2(MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(direction, this, -581207326, 101, "-instance", false).y, this, -581207326, 99, "-0-0", false), MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(direction, this, -581207326, 102, "-instance", false).x, this, -581207326, 99, "-0-1", false)), this, -581207326, 103, "5bf4e7ee627d81ee", false) * MaxyGames.UNode.GraphDebug.Value(Mathf.Rad2Deg, this, -581207326, 103, "28af4bd913fc6038", false)), this, -581207326, 98, "value", false), this, -581207326, 98, "target", true);
		MaxyGames.UNode.GraphDebug.Flow(this, -581207326, 98, "exit");
		MaxyGames.UNode.GraphDebug.Value(currentAngle = MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(MaxyGames.UNode.GraphDebug.Value(this, this, -581207326, 111, "input", false).transform, this, -581207326, 108, "-instance", false).eulerAngles.z, this, -581207326, 106, "value", false), this, -581207326, 106, "target", true);
		MaxyGames.UNode.GraphDebug.Flow(this, -581207326, 106, "exit");
		if(MaxyGames.UNode.GraphDebug.Value((currentAngle > 180F), this, -581207326, 112, "condition", false)) {
			MaxyGames.UNode.GraphDebug.Flow(this, -581207326, 112, "onTrue");
			MaxyGames.UNode.GraphDebug.Value(currentAngle += 360F, this, -581207326, 114, "target", true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, -581207326, 114, true);
			MaxyGames.UNode.GraphDebug.Flow(this, -581207326, 112, "exit");
			MaxyGames.UNode.GraphDebug.Value(targetAngle = MaxyGames.UNode.GraphDebug.Value(Mathf.Clamp(targetAngle, minAngle, maxAngle), this, -581207326, 115, "value", false), this, -581207326, 115, "target", true);
			MaxyGames.UNode.GraphDebug.Flow(this, -581207326, 115, "exit");
			MaxyGames.UNode.GraphDebug.Value(angle = MaxyGames.UNode.GraphDebug.Value(Mathf.MoveTowardsAngle(currentAngle, targetAngle, MaxyGames.UNode.GraphDebug.Value((MaxyGames.UNode.GraphDebug.Value(rotationSpeed, this, -581207326, 122, "270b400a62859af9", false) * MaxyGames.UNode.GraphDebug.Value(Time.deltaTime, this, -581207326, 122, "69f461f73bc00a0b", false)), this, -581207326, 120, "-0-2", false)), this, -581207326, 119, "value", false), this, -581207326, 119, "target", true);
			MaxyGames.UNode.GraphDebug.Flow(this, -581207326, 119, "exit");
			MaxyGames.UNode.GraphDebug.Value(this.transform.eulerAngles = MaxyGames.UNode.GraphDebug.Value(new Vector3(0F, 0F, angle), this, -581207326, 134, "value", false), this, -581207326, 134, "target", true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, -581207326, 134, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, -581207326, 119, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, -581207326, 115, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, -581207326, 112, true);
		} else {
			MaxyGames.UNode.GraphDebug.Flow(this, -581207326, 112, "exit");
			MaxyGames.UNode.GraphDebug.Value(targetAngle = MaxyGames.UNode.GraphDebug.Value(Mathf.Clamp(targetAngle, minAngle, maxAngle), this, -581207326, 115, "value", false), this, -581207326, 115, "target", true);
			MaxyGames.UNode.GraphDebug.Flow(this, -581207326, 115, "exit");
			MaxyGames.UNode.GraphDebug.Value(angle = MaxyGames.UNode.GraphDebug.Value(Mathf.MoveTowardsAngle(currentAngle, targetAngle, MaxyGames.UNode.GraphDebug.Value((MaxyGames.UNode.GraphDebug.Value(rotationSpeed, this, -581207326, 122, "270b400a62859af9", false) * MaxyGames.UNode.GraphDebug.Value(Time.deltaTime, this, -581207326, 122, "69f461f73bc00a0b", false)), this, -581207326, 120, "-0-2", false)), this, -581207326, 119, "value", false), this, -581207326, 119, "target", true);
			MaxyGames.UNode.GraphDebug.Flow(this, -581207326, 119, "exit");
			MaxyGames.UNode.GraphDebug.Value(this.transform.eulerAngles = MaxyGames.UNode.GraphDebug.Value(new Vector3(0F, 0F, angle), this, -581207326, 134, "value", false), this, -581207326, 134, "target", true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, -581207326, 134, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, -581207326, 119, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, -581207326, 115, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, -581207326, 112, false);
		}
		MaxyGames.UNode.GraphDebug.FlowNode(this, -581207326, 106, true);
		MaxyGames.UNode.GraphDebug.FlowNode(this, -581207326, 98, true);
		MaxyGames.UNode.GraphDebug.FlowNode(this, -581207326, 96, true);
		MaxyGames.UNode.GraphDebug.FlowNode(this, -581207326, 88, true);
	}
}

