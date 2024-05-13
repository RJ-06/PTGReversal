extends CharacterBody2D

@export var health_container: Container
@export var health_icon_fab: PackedScene
@export var health: int = 2
@export var max_health: int = 2
@export var move_speed: float = 50.0
@export var on_hit_invlun_duration: float = 1.0
@export var on_hit_bounceback_force: float = 500.0
const GRAYSCALE_MAT := preload("res://shading/grayscale_mat.tres") as ShaderMaterial

signal dead

const FLASHES = 5

var invuln := 0.0
var hp_objects: Array[TextureRect] = []

# Called when the node enters the scene tree for the first time.
func _ready():
	for x in max_health:
		var hp := health_icon_fab.instantiate() as TextureRect
		health_container.add_child(hp)
		hp_objects.append(hp)
	health = max_health
	_update_hp()


func _update_hp():
	var i := 0
	for x in hp_objects:
		if (i + 1) <= health:
			x.material = null
		else:
			x.material = GRAYSCALE_MAT
		i += 1
		
	AudioServer.set_bus_effect_enabled(0, 1, health == 1)
	AudioServer.set_bus_volume_db(0, -6 if health == 1 else 0)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float):
	invuln = max(0.0, invuln - delta) as float
	if invuln > 0:
		modulate = Color(Color.WHITE, sin((invuln / on_hit_invlun_duration) * FLASHES * (2 * PI)))
	else:
		modulate = Color.WHITE


func _on_hit(body: Node2D):
	print(body)
	if body is SimpleEnemy:
		print("bouncing!!")
		var vec = body.global_position - global_position
		#var vec := Vector2.UP
		body.queue_add_vel(vec.normalized() * on_hit_bounceback_force, true)
	if invuln > 0:
		return
	health -= 1
	_update_hp()
	invuln = on_hit_invlun_duration
	if health == 0:
		dead.emit()
	
