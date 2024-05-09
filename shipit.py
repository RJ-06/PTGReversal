import os
import random
import string, re

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
    for x in range(2):
#         amazing.append(random.choice(WORDS))
        pass
    hashy = ""
    for x in range(4):
        hashy += random.choice(string.ascii_letters + string.digits)
    amazing.append(hashy)
    return "_".join(amazing)


for file in bundle:
    output_bundle = []
    n = 0
    with open(file, "r") as f:
        mode = False
        d = f.read()
        names = []
        j = random.randint(1, 10)
        for line in d.splitlines():
            output_bundle.append(line)
            if line.strip().startswith("GameObject:"):
                mode = True
                continue
            if not line.startswith(" "):
                mode = False
            if mode:
                j -= 1
                if j > 0:
                    continue
                else:
                    j = random.randint(1, 10)
                if (match:=re.search(r'component:\s*{\s*fileID:\s*(\d+)\s*}', line)):
                    component = match.group(1)
                    matcher = rf'---.*?&{component}\r?\n(\w+):'
                    if (match:=re.search(matcher, d)):
                        component_type = match.group(1)
                        names.append(component_type)
                if line.strip().startswith("m_Name:") and not line.strip().endswith("HEXAGOn"):
                    new_name = "_".join(names)
                    names = []
                    output_bundle.pop()
                    output_bundle.append(f"  m_Name: {new_name}")
    with open(file, "w") as f:
        f.write("\n".join(output_bundle))
