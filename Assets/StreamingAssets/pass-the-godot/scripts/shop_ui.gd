extends Control

@export var shop: Shop
@export var button_scene: PackedScene

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	for shop_item in shop.shop_items:
		var button := button_scene.instantiate() as Button
		button.text = shop_item.get_button_text()
		button.pressed.connect(
				func():
					shop.try_purchase(shop_item.name)
					button.text = shop_item.get_button_text()
		)
		$VBoxContainer.add_child(button)
