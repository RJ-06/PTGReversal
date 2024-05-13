extends Node

@export var max_len_soft: int

const ADJECTIVES: Array[String] = [
	'simulating',
	'preparing',
	'loading',
	'installing',
	'deleting',
	'YOU SHOULD',
	'SPOILER:'
]

const TRASH: Array[String] = [
	'skibidi',
	'gyatt',
	'ohio',
	'rizzler',
	'rizz',
	'tax',
	'sus',
	'cap',
	'mid',
	'in the hole',
	'woke',
	'impact',
	'deez',
	'nuts',
	'Robux',
	'drip',
	
	'sinnybun',
	'Harlem',
	'thug shaker',
	'simulator',
	'3',
	'Dream Minecraft',
	'SPOILER',
	'WHISK',
	'WATCH',
]

func generate_garbage() -> String:
	var names: PackedStringArray = PackedStringArray()
	# pick an adjective
	names.append(ADJECTIVES.pick_random())
	while len(" ".join(names)) < max_len_soft:
		names.append(TRASH.pick_random())
	return " ".join(names)

@onready var brainrot: Label = %brainrot as Label
@onready var progress: Label = %progress as Label

func _ready():
	brainrot.text = generate_garbage()
	GlobalResourceLoader.loading_failed.connect(_failure)
	GlobalResourceLoader.loading_progress.connect(_progress)
	
func _failure():
	progress.text = "loading failed!"
	
func _progress(amount: float):
	progress.text = "Loading %.2f%%" % (amount * 100)


func _on_brainrot_interval_timeout():
	brainrot.text = generate_garbage()
