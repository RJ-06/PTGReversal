class_name Shop
extends Resource

@export var shop_items: Array[ShopItem]
var money: int: set = _set_money
static var arguments: Dictionary 

static func _static_init() -> void:
    for argument in OS.get_cmdline_user_args():
        var key_value := argument.split("=")
        var key := key_value[0]
        var value := key_value[1]
        arguments[key] = value


func _init() -> void:
    for key in arguments:
        var value = arguments[key]

        var shop_item := get_shop_item(key)
        if shop_item != null:
            shop_item.amount_purchased = int(value)
        elif key == "money":
            money = int(value)


func try_purchase(name: String) -> bool:
    var shop_item = get_shop_item(name)
    if money >= shop_item.get_price():
        money -= shop_item.get_price()
        shop_item.amount_purchased += 1
        return true
    else:
        return false


func get_shop_item(name: String) -> ShopItem:
    for shop_item in shop_items:
        if shop_item.name == name:
            return shop_item
    return null


func _set_money(amount: int) -> void:
    print("[Unity Bridge]: money=%d" % amount)
    money = amount