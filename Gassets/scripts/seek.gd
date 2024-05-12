class_name SimpleEnemy
extends CharacterBody2D


@export var move_speed: float
@export var max_speed: float = 100.0
@export var enabled: bool

var target: Node2D


# Called when the node enters the scene tree for the first time.
func _ready():
	target = get_tree().get_nodes_in_group("Player")[0] as Node2D


var queued_vel: Vector2 = Vector2.ZERO
var queue_reset: bool = false


func queue_add_vel(vel: Vector2, clear: bool = false):
	queued_vel += vel
	queue_reset = queue_reset or clear

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _physics_process(delta):
	if enabled:
		var direction := target.global_position - global_position
		
		# how hard are we going in the target direction?
		var effectiveness = velocity.dot(direction.normalized())
		if direction.length() > 1:
			direction = direction.normalized()
		
		if queue_reset:
			velocity = queued_vel
		else:
			velocity += queued_vel
		
		if effectiveness < max_speed:
			velocity += direction * (move_speed * delta)
		
		move_and_slide()
	queued_vel = Vector2.ZERO
	queue_reset = false
