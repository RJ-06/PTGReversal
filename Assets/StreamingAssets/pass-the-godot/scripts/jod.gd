extends AnimatedSprite3D

@export var sounds: Array[AudioStream]

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	if Shop.arguments.has("first-time") and Shop.arguments["first-time"] == "True":
		await play_sound(sounds[0])
	else:
		await play_sound(sounds[1])


func play_sound(sound: AudioStream) -> void:
	$AudioStreamPlayer3D.stream = sound
	play("talking")
	$AudioStreamPlayer3D.play()
	await $AudioStreamPlayer3D.finished
	play("default")