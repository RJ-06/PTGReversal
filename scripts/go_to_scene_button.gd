class_name GoToSceneButton
extends Button

@export_file var scene_file: String


func _ready():
	pressed.connect(switch)


func switch():
	GlobalResourceLoader.switch_with_intermediate(scene_file)
