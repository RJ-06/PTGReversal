extends Node

const GLITCH_MAT = preload("res://shading/glitch_mat.tres")

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	var dbA := AudioServer.get_bus_peak_volume_left_db(AudioServer.get_bus_index("Master"), 0)
	var dbB := AudioServer.get_bus_peak_volume_right_db(AudioServer.get_bus_index("Master"), 0)
	var db := (dbA + dbB) / 2.0
	var min_db = -12.0
	var factor: float = max(abs(-50/(min(0, db) - 1)), 0)
	GLITCH_MAT.set_shader_parameter("offset_amount", factor)
