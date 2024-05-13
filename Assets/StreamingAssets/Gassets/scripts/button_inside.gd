@tool
extends Button
class_name ExpandButton
@onready var inside: Container = $inside

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	custom_minimum_size = inside.size
