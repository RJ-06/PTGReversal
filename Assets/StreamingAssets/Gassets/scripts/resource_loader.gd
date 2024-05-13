class_name ResourceLoadManager
extends Node


var load_scene: PackedScene = null
var load_check_timer: Timer
const LOAD_SCENE_PATH := "res://scenes/load.tscn"
var is_loading := false
var load_target: String = "???"
var last_progress: float = 0.0

signal loading_progress(amount: float)
signal loading_failed

# Called when the node enters the scene tree for the first time.
func _ready():
	# prepare the Loading Screen in the background
	ResourceLoader.load_threaded_request(LOAD_SCENE_PATH)
	load_check_timer = Timer.new()
	load_check_timer.wait_time = 0.1
	add_child(load_check_timer)
	load_check_timer.timeout.connect(_validate)
	load_check_timer.start()


func _validate():
	var load_status = ResourceLoader.load_threaded_get_status(LOAD_SCENE_PATH)
	match load_status:
		ResourceLoader.THREAD_LOAD_FAILED:
			assert(false, "the loading screen failed to load?!")
		ResourceLoader.THREAD_LOAD_LOADED:
			print_rich("[color=green]Loading screen ready.[/color]")
			load_check_timer.timeout.disconnect(_validate)
			load_scene = ResourceLoader.load_threaded_get(LOAD_SCENE_PATH) as PackedScene


func switch_with_intermediate(next_scene_path: String):
	if load_scene == null: load_scene = ResourceLoader.load_threaded_get(LOAD_SCENE_PATH) as PackedScene
	get_tree().change_scene_to_packed(load_scene)
	load_target = next_scene_path
	ResourceLoader.load_threaded_request(next_scene_path)
	is_loading = true
	last_progress = -1.0

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if is_loading:
		var progress_out: Array[float] = []
		var load_status := ResourceLoader.load_threaded_get_status(load_target, progress_out)
		if progress_out[0] != last_progress:
			loading_progress.emit(progress_out[0])
		match load_status:
			ResourceLoader.THREAD_LOAD_FAILED:
				print_rich("[color=red]Failed to load " + load_target + "![/color]")
				is_loading = false
				loading_failed.emit()
			ResourceLoader.THREAD_LOAD_LOADED:
				print_rich("[color=green]Loaded " + load_target + "[/color]")
				var scene := ResourceLoader.load_threaded_get(load_target) as PackedScene
				is_loading = false
				get_tree().change_scene_to_packed(scene)
