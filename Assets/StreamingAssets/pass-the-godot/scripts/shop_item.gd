class_name ShopItem
extends Resource

@export var name: String
@export_multiline var button_text: String
@export var inital_price: int
@export var price_increase: int
@export var stock: int

var amount_purchased: int: set = _set_amount_purchased

func get_price() -> int:
    return inital_price + amount_purchased * price_increase

func get_button_text() -> String:
    return button_text % get_price()

func _set_amount_purchased(amount: int) -> void:
    print("[Unity Bridge]: %s=%d" % [name, amount])
    amount_purchased = amount