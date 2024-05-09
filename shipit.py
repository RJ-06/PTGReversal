import yaml
import os
import random
import string

bundle = []
for curdir, dirs, files in os.walk("Assets"):
    for file in files:
        if file.endswith(".unity"):
            bundle.append(os.path.join(curdir, file))


def garbage():
    amazing = []
    WORDS = [
        "skibidi",
        "gyatt",
        "ohio",
        "rizzler",
        "tax",
        "in_the_hole",
        "mid",
        "sus",
        "cap",
        "woke",
        "drip",
        "shin_impact",
        "sussy",
        "SPOILER",
        "WHISK",
        "WATCH",
        "blunderbuss_s",
        "Robux",
        "sinnybun",
        "thug",
        "shaker",
        "simulator"
    ]
    for x in range(5):
        amazing.append(random.choice(WORDS))
    hashy = ""
    for x in range(8):
        hashy += random.choice(string.ascii_letters + string.digits)
    amazing.append(hashy)
    return "_".join(amazing)


for file in bundle:
    output_bundle = []
    with open(file, "r") as f:
        mode = False
        d = f.read()
        for line in d.splitlines():
            output_bundle.append(line)
            if line.strip().startswith("GameObject:"):
                mode = True
                continue
            if not line.startswith(" "):
                mode = False
            if mode and line.strip().startswith("m_Name:") and not line.strip().endswith("HEXAGOn"):
                new_name = garbage()
                output_bundle.pop()
                output_bundle.append(f"  m_Name: {new_name}")
    with open(file, "w") as f:
        f.write("\n".join(output_bundle))
