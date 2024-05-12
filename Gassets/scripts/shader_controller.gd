extends Node

const GLITCH_MAT = preload("res://shading/glitch_mat.tres")
@onready var postprocess := %postprocess as CanvasLayer
@onready var postprocess_bounds := %postprocess_bounds as TextureRect

func set_glitches(enable: bool):
	GLITCH_MAT.set_shader_parameter("enabled", enable)
