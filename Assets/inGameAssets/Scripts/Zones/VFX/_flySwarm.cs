#pragma warning disable
using UnityEngine;
using System.Collections.Generic;

public class FlySwarm : MonoBehaviour 
{	
	[Header("Fly Settings")]
	public GameObject flyPrefab;
	public int numberOfFlies = 20;
	public float swarmRadius = 2F;
	[Header("Size Settings")]
	public float baseFlySize = 1F;
	public float minSizeVariation = 0.6F;
	public float maxSizeVariation = 1.4F;
	public bool uniformScale = true;
	public Vector2 nonUniformScaleRange = new Vector2(0.2f, 0.3f);
	[Header("Movement Settings")]
	public float minSpeed = 1F;
	public float maxSpeed = 3F;
	public float rotationSpeed = 2F;
	public float changeDirectionInterval = 1F;
	public float noiseScale = 0.5F;
	[Header("Swarm Behavior")]
	public Transform swarmCenter;
	public float centerAttraction = 0.5F;
	public float randomness = 0.7F;
	[Header("Advanced")]
	public bool sizeAffectsSpeed = true;
	public float sizeSpeedFactor = 0.5F;
	private List<Transform> flies = new List<Transform>();
	private List<Vector2> velocities = new List<Vector2>();
	private List<float> nextDirectionChange = new List<float>();
	private List<float> flySizes = new List<float>();
	
	private void Start() {
		if((swarmCenter == null)) {
			swarmCenter = this.transform;
		}
		SpawnFlies();
	}
	
	private void SpawnFlies() {
		for(int index = 0; index < numberOfFlies; index += 1) {
			//Spawn fly at random position within the swarm radius
			var randomPos = (Random.insideUnitCircle * swarmRadius);
			var fly = Object.Instantiate<UnityEngine.GameObject>(flyPrefab, (((Vector2)swarmCenter.position) + randomPos), Quaternion.identity);
			fly.transform.parent = this.transform;
			//Apply size with variation
			var sizeVariation = Random.Range(minSizeVariation, maxSizeVariation);
			var finalSize = (baseFlySize * sizeVariation);
			flySizes.Add(finalSize);
			if(uniformScale) {
				fly.transform.localScale = new Vector3(finalSize, finalSize, 1F);
			}
			 else {
				//Add slight non-uniform scaling for more variety
				var xVariation = (1F + Random.Range(-nonUniformScaleRange.x, nonUniformScaleRange.x));
				var yVariation = (1F + Random.Range(-nonUniformScaleRange.y, nonUniformScaleRange.y));
				fly.transform.localScale = new Vector3((finalSize * xVariation), (finalSize * yVariation), 1F);
			}
			flies.Add(fly.transform);
			//Adjust speed based on size if enabled
			var sizeSpeedAdjustment = (sizeAffectsSpeed ? Mathf.Lerp(1.3F, 0.7F, (((sizeVariation - minSizeVariation) / (maxSizeVariation - minSizeVariation)) * sizeSpeedFactor)) : 1F);
			//Initialize with random velocity, potentially affected by size
			var flySpeed = (Random.Range(minSpeed, maxSpeed) * sizeSpeedAdjustment);
			velocities.Add((Random.insideUnitCircle.normalized * flySpeed));
			nextDirectionChange.Add(Random.Range(0F, changeDirectionInterval));
		}
	}
	
	private void Update() {
		for(int index1 = 0; index1 < flies.Count; index1 += 1)
		{
			//Update direction change timer
			nextDirectionChange[index1] = (nextDirectionChange[index1] - Time.deltaTime);
			//Calculate movement vector
			var movement = CalculateMovement(index1);
			//Apply movement
			flies[index1].position += (((Vector3)movement) * Time.deltaTime);
			//Rotate sprite to face movement direction
			if((movement != Vector2.zero)) {
				var angle = (Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg);
				flies[index1].rotation = Quaternion.Lerp(flies[index1].rotation, Quaternion.Euler(0, 0, (angle - 90)), (rotationSpeed * Time.deltaTime));
			}
		}
	}
	
	private Vector2 CalculateMovement(int index)
	{
		//Get current velocity
		var velocity = velocities[index];
		//If it's time to change direction
		if((nextDirectionChange[index] <= 0)) 
		{
			//Get position relative to swarm center
			var toCenter = (((Vector2)swarmCenter.position) - ((Vector2)flies[index].position));
			//Add center attraction force
			var centerForce = (toCenter.normalized * centerAttraction);
			//Add randomness using Perlin noise for more natural movement
			var noiseX = ((Mathf.PerlinNoise((Time.time * noiseScale), (index * 100F)) * 2F) - 1F);
			var noiseY = ((Mathf.PerlinNoise((index * 100F), (Time.time * noiseScale)) * 2F) - 1F);
			var noiseForce = (new Vector2(noiseX, noiseY) * randomness);
			//Combine forces
			velocity = (centerForce + noiseForce);
			//Apply speed constraints, accounting for fly size if enabled
			var sizeSpeedAdjustment1 = 1F;
			if((sizeAffectsSpeed && (flySizes.Count > index))) 
			{
				var normalizedSize = (((flySizes[index] / baseFlySize) - minSizeVariation) / (maxSizeVariation - minSizeVariation));
				sizeSpeedAdjustment1 = Mathf.Lerp(1.3F, 0.7F, (normalizedSize * sizeSpeedFactor));
			}
			velocity = ((velocity.normalized * Random.Range(minSpeed, maxSpeed)) * sizeSpeedAdjustment1);
			//Store new velocity
			velocities[index] = velocity;
			//Reset timer with some randomness
			nextDirectionChange[index] = Random.Range((changeDirectionInterval * 0.8F), (changeDirectionInterval * 1.2F));
		}
		//Check if fly is too far from center
		var distanceFromCenter = Vector2.Distance(flies[index].position, swarmCenter.position);
		if((distanceFromCenter > swarmRadius)) {
			//Add stronger force back toward center when flies stray too far
			var toCenterStrong = (((Vector2)swarmCenter.position) - ((Vector2)flies[index].position)).normalized;
			velocity = Vector2.Lerp(velocity, (toCenterStrong * maxSpeed), 0.1F);
			velocities[index] = velocity;
		}
		return velocity;
	}
	
	public void UpdateFlySizes(float newBaseSize, float newMinVar, float newMaxVar) {
		baseFlySize = newBaseSize;
		if((newMinVar > 0)) {
			minSizeVariation = newMinVar;
		}
		if((newMaxVar > 0)) {
			maxSizeVariation = newMaxVar;
		}
		//Update all existing flies
		for(int index2 = 0; index2 < flies.Count; index2 += 1) {
			var sizeVariation1 = Random.Range(minSizeVariation, maxSizeVariation);
			var finalSize1 = (baseFlySize * sizeVariation1);
			flySizes[index2] = finalSize1;
			if(uniformScale) {
				flies[index2].localScale = new Vector3(finalSize1, finalSize1, 1F);
			}
			 else {
				//Preserve current non-uniform ratio but update the base size
				var currentScale = flies[index2].localScale;
				var xRatio = (currentScale.x / currentScale.y);
				flies[index2].localScale = new Vector3((finalSize1 * xRatio), finalSize1, 1F);
			}
			//Adjust velocity based on new size if needed
			if(sizeAffectsSpeed) {
				var normalizedSize1 = (((finalSize1 / baseFlySize) - minSizeVariation) / (maxSizeVariation - minSizeVariation));
				var sizeSpeedAdjustment2 = Mathf.Lerp(1.3F, 0.7F, (normalizedSize1 * sizeSpeedFactor));
				velocities[index2] = ((velocities[index2].normalized * velocities[index2].magnitude) * sizeSpeedAdjustment2);
			}
		}
	}
	
	private void OnDrawGizmos() {
		if((swarmCenter != null)) {
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(swarmCenter.position, swarmRadius);
		}
	}
}

