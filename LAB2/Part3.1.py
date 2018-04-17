def Write(string, array):
    counter = 0
    for char in string:
        if char == ".":
            returned = counter
        else:
            array.append(char)
        counter = counter + 1
    return returned




firstnum = input("Enter first number: ")
secondnum = input("Enter second number: ")
print("порядок | мантиса")
mantissa1 = []
mantissa2 = []
order1 = Write(firstnum, mantissa1)
order2 = Write(secondnum, mantissa2)
print(order1, "".join(str(i) for i in mantissa1))
print(order2, "".join(str(i) for i in mantissa2))
print("нормуємо порядок")
if order1 > order2:
    dif = order1 - order2
    for i in range(0, dif):
        mantissa2.insert(0,0)
    order2 = order1
elif order2 > order1:
    dif = order2 - order1
    for i in range(0, dif):
        mantissa1.insert(0,0)
    order1 = order2
print(order1, "".join(str(i) for i in mantissa1))
print(order2, "".join(str(i) for i in mantissa2))
print("нормуємо мантису")
if len(mantissa2) > len(mantissa1):
    dif = len(mantissa2) - len(mantissa1)
    for i in range(0,dif):
        mantissa1.append(0)
elif len(mantissa1) > len(mantissa2):
    dif = len(mantissa1) - len(mantissa2)
    for i in range(0,dif):
        mantissa2.append(0)
print(order1, "".join(str(i) for i in mantissa1))
print(order2, "".join(str(i) for i in mantissa2))
print("додаємо")
result = []
k = 0
for i in range(len(mantissa1)):
    if int(mantissa1[i]) + int(mantissa2[i]) + int(k) == 0 :
        result.insert(0,0)
        k = 0
    elif int(mantissa1[i]) + int(mantissa2[i]) + int(k) == 1 :
        result.insert(0,1)
        k = 0
    elif int(mantissa1[i]) + int(mantissa2[i]) + int(k) == 2 :
        result.insert(0,0)
        k = 1
    elif int(mantissa1[i]) + int(mantissa2[i]) + int(k) == 3 :
        result.insert(0,1)
        k = 1
print(order2, "".join(str(i) for i in result))
if k == 1:
    result.insert(0, 1)
    order1 = order1 + 1
result.insert(order1,".")
print("result:","".join(str(i) for i in result))

