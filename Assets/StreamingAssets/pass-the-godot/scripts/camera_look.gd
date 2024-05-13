extends Camera3D

## Pixels of mouse movement per radian of rotation
@export var look_speed := 12.0

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	Input.mouse_mode = Input.MOUSE_MODE_CAPTURED


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

func _input(event: InputEvent) -> void:
	if event is InputEventMouseMotion:
		var delta : Vector2 = event.relative / look_speed * -1
		rotation += Vector3(delta.y, delta.x, 0)
		rotation.x = clampf(rotation.x, -PI / 2, PI / 2)
