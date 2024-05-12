extends Node
@onready var skip_it := %skip_it as Button
@onready var choreo := %choreo as AnimationPlayer


func _ready():
	skip_it.pressed.connect(do_skip)


func do_skip():
	print("Skipping...")
	choreo.seek(35.2, true)


func _on_pingas_dead():
	GlobalResourceLoader.switch_with_intermediate("res://scenes/level_1_gameover.tscn")
