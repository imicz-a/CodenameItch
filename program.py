
sst = None
with open("text.txt", 'w') as file:
	file.write("test")
with open("text.txt", 'r') as file:
	sst = file.read()
print(sst)